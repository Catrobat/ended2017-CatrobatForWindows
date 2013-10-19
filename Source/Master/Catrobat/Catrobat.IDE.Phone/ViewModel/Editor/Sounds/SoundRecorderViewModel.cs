using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Resources.Localization;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Catrobat.IDE.Phone.ViewModel.Editor.Sounds
{
    public class SoundRecorderViewModel : ViewModelBase
    {
        #region Private Members

        private Task _recordTimeUpdateTask;
        private DateTime _recorderStartTime;
        private TimeSpan _recorderTimeGoneBy;

        private Task _playerTimeUpdateTask;
        private DateTime _playerStartTime;
        private TimeSpan _playerTimeGoneBy;

        private bool _isRecording;
        private bool _isPlaying;
        private bool _recordingExists;
        private double _recordingTime;
        private double _playingTime;
        private string _recordButtonText;
        private string _resetButtonText;
        private string _recordButtonHeader;
        private string _resetButtonHeader;

        #endregion

        #region Properties

        public bool IsRecording
        {
            get { return _isRecording; }
            set
            {
                if (value == _isRecording)
                {
                    return;
                }
                _isRecording = value;
                UpdateTextProperties();
                RaisePropertyChanged(() => IsRecording);
            }
        }

        public bool IsPlaying
        {
            get { return _isPlaying; }
            set
            {
                if (value == _isPlaying)
                {
                    return;
                }
                _isPlaying = value;
                RaisePropertyChanged(() => IsPlaying);
            }
        }

        public double RecordingTime
        {
            get { return _recordingTime; }
            set
            {
                if (value == _recordingTime)
                {
                    return;
                }
                _recordingTime = value;
                RaisePropertyChanged(() => RecordingTime);
            }
        }

        public double PlayingTime
        {
            get { return _playingTime; }
            set
            {
                if (value == _playingTime)
                {
                    return;
                }
                _playingTime = value;
                RaisePropertyChanged(() => PlayingTime);
            }
        }

        public bool RecordingExists
        {
            get { return _recordingExists; }
            set
            {
                if (value == _recordingExists)
                {
                    return;
                }
                _recordingExists = value;
                RaisePropertyChanged(() => RecordingExists);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public string RecordButtonHeader
        {
            get { return _recordButtonHeader; }
            set
            {
                if (value == _recordButtonHeader)
                {
                    return;
                }
                _recordButtonHeader = value;
                RaisePropertyChanged(() => RecordButtonHeader);
            }
        }

        public string RecordButtonText
        {
            get { return _recordButtonText; }
            set
            {
                if (value == _recordButtonText)
                {
                    return;
                }
                _recordButtonText = value;
                RaisePropertyChanged(() => RecordButtonText);
            }
        }

        public string ResetButtonHeader
        {
            get { return _resetButtonHeader; }
            set
            {
                if (value == _resetButtonHeader)
                {
                    return;
                }
                _resetButtonHeader = value;
                RaisePropertyChanged(() => ResetButtonHeader);
            }
        }

        public string ResetButtonText
        {
            get { return _resetButtonText; }
            set
            {
                if (value == _resetButtonText)
                {
                    return;
                }
                _resetButtonText = value;
                RaisePropertyChanged(() => ResetButtonText);
            }
        }

        #endregion

        #region Commands

        public RelayCommand StartRecordingCommand { get; private set; }

        public RelayCommand ResetCommand { get; private set; }

        public RelayCommand PlayPauseCommand { get; private set; }

        public RelayCommand SaveCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        public RelayCommand ResetViewModelCommand { get; private set; }

        #endregion

        #region CommandCanExecute

        private bool SaveCommand_CanExecute()
        {
            return RecordingExists;
        }

        #endregion

        #region Actions

        private void StartRecordingAction()
        {
            lock (ServiceLocator.SoundRecorderService)
            {
                if (IsRecording)
                {
                    ServiceLocator.SoundRecorderService.StopRecording();
                    ServiceLocator.SoundRecorderService.StopPlayingRecordedSound();

                    if (ServiceLocator.SoundRecorderService.State == SoundRecorderState.Recording)
                        ServiceLocator.SoundRecorderService.StopRecording();

                    //_recordTimeUpdateTaskCancellationToken.Cancel();
                    //_recordTimeUpdateTask.Abort();

                    RecordingExists = true;
                }
                else
                {
                    IsPlaying = false;
                    RecordingExists = false;
                    ServiceLocator.SoundRecorderService.StartRecording();
                    _recorderStartTime = DateTime.UtcNow;

                    //_recordTimeUpdateTaskCancellationToken = new CancellationTokenSource();

                    _recordTimeUpdateTask = new Task(delegate()
                        {
                            try
                            {
                                while (ServiceLocator.SoundRecorderService.State != SoundRecorderState.StoppingRecording &&
                                    ServiceLocator.SoundRecorderService.State != SoundRecorderState.NoAction)
                                {
                                    _recorderTimeGoneBy = DateTime.UtcNow - _recorderStartTime;
                                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                                        {
                                            RecordingTime = _recorderTimeGoneBy.TotalSeconds;
                                        });

                                    Thread.Sleep(45);
                                }
                            }
                            catch
                            {
                                /* Nothing here */
                            }
                        }
                    );

                    _recordTimeUpdateTask.Start();
                }
            }

            IsRecording = !IsRecording;
        }

        private static void ResetAction()
        {
            ServiceLocator.SoundRecorderService.StopPlayingRecordedSound();
            ServiceLocator.SoundRecorderService.StopRecording();
        }

        private void PlayPauseAction()
        {
            lock (ServiceLocator.SoundRecorderService)
            {
                ServiceLocator.SoundRecorderService.InitializeSound();

                if (IsPlaying)
                {
                    IsPlaying = false;
                    ServiceLocator.SoundRecorderService.StopRecording();
                    ServiceLocator.SoundRecorderService.StopPlayingRecordedSound();
                }
                else
                {
                    ServiceLocator.SoundRecorderService.StopRecording();
                    _playerStartTime = DateTime.UtcNow;
                    IsPlaying = true;

                    _playerTimeUpdateTask = new Task(delegate()
                        {
                            try
                            {
                                while (IsPlaying)
                                {
                                    _playerTimeGoneBy = DateTime.UtcNow - _playerStartTime;
                                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                                        {
                                            if (_playerTimeGoneBy.TotalSeconds < RecordingTime)
                                            {
                                                PlayingTime = _playerTimeGoneBy.TotalSeconds;
                                            }
                                            else
                                            {
                                                lock (ServiceLocator.SoundRecorderService)
                                                {
                                                    PlayingTime = RecordingTime;
                                                    IsPlaying = false;
                                                    ServiceLocator.SoundRecorderService.StopPlayingRecordedSound();
                                                }
                                            }
                                        });

                                    Thread.Sleep(45);
                                }
                                IsPlaying = false;
                            }
                            catch
                            {
                                /* Nothing here */
                            }
                        });

                    _playerTimeUpdateTask.Start();
                    ServiceLocator.SoundRecorderService.PlayRecordedSound();
                }
            }
        }

        private static void SaveAction()
        {
            ServiceLocator.NavigationService.NavigateTo(typeof(SoundNameChooserViewModel));
        }

        private void CancelAction()
        {
            ServiceLocator.SoundRecorderService.StopPlayingRecordedSound();
            ServiceLocator.SoundRecorderService.StopRecording();

            ResetViewModel();
            ServiceLocator.NavigationService.NavigateBack();
        }

        private void ResetViewModelAction()
        {
            ResetViewModel();
        }

        #endregion

        #region MessageActions


        #endregion

        public SoundRecorderViewModel()
        {
            StartRecordingCommand = new RelayCommand(StartRecordingAction);
            ResetCommand = new RelayCommand(ResetAction);
            PlayPauseCommand = new RelayCommand(PlayPauseAction);
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);
            ResetViewModelCommand = new RelayCommand(ResetViewModelAction);

            UpdateTextProperties();

            if (IsInDesignMode)
            {
                CreateDesignData();
            }
        }

        private void UpdateTextProperties()
        {
            var recordButtonHeaderRecord = AppResources.Editor_RecorderRecord;
            var recordButtonHeaderStop = AppResources.Editor_RecorderStop;
            var recordButtonTextRecord = AppResources.Editor_RecorderStart;
            var recordButtonTextStop = AppResources.Editor_RecorderStop;

            if (IsRecording)
            {
                RecordButtonHeader = recordButtonHeaderStop;
                RecordButtonText = recordButtonTextStop;
            }
            else
            {
                RecordButtonHeader = recordButtonHeaderRecord;
                RecordButtonText = recordButtonTextRecord;
            }
        }

        private void CreateDesignData()
        {
            RecordingTime = 13.42;
            PlayingTime = 6.23;
        }

        private void ResetViewModel()
        {
            IsPlaying = false;
            IsRecording = false;
            RecordingExists = false;
            RecordingTime = 0;
            PlayingTime = 0;

            if (_recordTimeUpdateTask != null)
            {
                if (ServiceLocator.SoundRecorderService.State == SoundRecorderState.Recording)
                    ServiceLocator.SoundRecorderService.StopRecording();

                if (ServiceLocator.SoundRecorderService.State == SoundRecorderState.Playing)
                    ServiceLocator.SoundRecorderService.StopPlayingRecordedSound();

                //_recordTimeUpdateTaskCancellationToken.Cancel();
                //_recordTimeUpdateTask.Abort();
            }
            _recordTimeUpdateTask = null;
            _recorderStartTime = new DateTime();
            _recorderTimeGoneBy = new TimeSpan();

            //if (_playerTimeUpdateTask != null)
            //{
            //    if (ServiceLocator.SoundRecorderService.State == SoundRecorderState.Recording)
            //    ServiceLocator.SoundRecorderService.StopRecording();

            //    if (ServiceLocator.SoundRecorderService.State == SoundRecorderState.Playing)
            //        ServiceLocator.SoundRecorderService.StopPlayingRecordedSound();

            //    //_playerTimeUpdateTaskCancellationToken.Cancel();
            //    //_playerTimeUpdateTask.Abort();
            //}
            _playerTimeUpdateTask = null;
            _playerStartTime = new DateTime();
            _playerTimeGoneBy = new TimeSpan();
        }
    }
}