using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;
using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Sounds;
using Catrobat.Core.Storage;
using Catrobat.IDEWindowsPhone.Annotations;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.Misc.Sounds;
using Catrobat.IDEWindowsPhone.Views.Editor.Sounds;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using GalaSoft.MvvmLight.Command;
using IDEWindowsPhone;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;
using Catrobat.IDECommon.Resources.Editor;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDEWindowsPhone.ViewModel.Editor.Sounds
{
    public class SoundRecorderViewModel : ViewModelBase
    {
        #region Private Members

        private Sprite _receivedSelectedSprite;
        private Recorder _recorder;
        private Thread _recordTimeUpdateThread;
        private DateTime _recorderStartTime;
        private TimeSpan _recorderTimeGoneBy;

        private Thread _playerTimeUpdateThread;
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
        private string _soundName;

        #endregion

        #region Properties

        public bool IsRecording
        {
            get { return _isRecording; }
            set
            {
                if (value == _isRecording) return;
                _isRecording = value;
                UpdateTextProperties();
                RaisePropertyChanged("IsRecording");
            }
        }

        public bool IsPlaying
        {
            get { return _isPlaying; }
            set
            {
                if (value == _isPlaying) return;
                _isPlaying = value;
                RaisePropertyChanged("IsPlaying");
            }
        }

        public double RecordingTime
        {
            get { return _recordingTime; }
            set
            {
                if (value == _recordingTime) return;
                _recordingTime = value;
                RaisePropertyChanged("RecordingTime");
            }
        }

        public double PlayingTime
        {
            get { return _playingTime; }
            set
            {
                if (value == _playingTime) return;
                _playingTime = value;
                RaisePropertyChanged("PlayingTime");
            }
        }

        public bool RecordingExists
        {
            get { return _recordingExists; }
            set
            {
                if (value ==_recordingExists) return;
                _recordingExists = value;
                RaisePropertyChanged("RecordingExists");
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public string RecordButtonText
        {
            get { return _recordButtonText; }
            set
            {
                if (value == _recordButtonText) return;
                _recordButtonText = value;
                RaisePropertyChanged("RecordButtonText");
            }
        }

        public string RecordButtonHeader
        {
            get { return _recordButtonHeader; }
            set
            {
                if (value == _recordButtonHeader) return;
                _recordButtonHeader = value;
                RaisePropertyChanged("RecordButtonHeader");
            }
        }

        public string ResetButtonHeader
        {
            get { return _resetButtonHeader; }
            set
            {
                if (value == _resetButtonHeader) return;
                _resetButtonHeader = value;
                RaisePropertyChanged("ResetButtonHeader");
            }
        }

        public string ResetButtonText
        {
            get { return _resetButtonText; }
            set
            {
                if (value == _resetButtonText) return;
                _resetButtonText = value;
                RaisePropertyChanged("ResetButtonText");
            }
        }

        public string SoundName
        {
            get { return _soundName; }
            set
            {
                if (value == _soundName) return;
                _soundName = value;
                RaisePropertyChanged("SoundName");
                SaveNameChosenCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Commands

        public RelayCommand StartRecordingCommand
        {
            get;
            private set;
        }

        public RelayCommand ResetCommand
        {
            get;
            private set;
        }

        public RelayCommand PlayPauseCommand
        {
            get;
            private set;
        }

        public RelayCommand SaveCommand
        {
            get;
            private set;
        }

        public RelayCommand CancelCommand
        {
            get;
            private set;
        }

        public RelayCommand SaveNameChosenCommand
        {
            get;
            private set;
        }

        public RelayCommand CancelNameChosenCommand
        {
            get;
            private set;
        }

        public RelayCommand ResetViewModelCommand
        {
            get;
            private set;
        }

        #endregion

        #region CommandCanExecute

        private bool SaveCommand_CanExecute()
        {
            return RecordingExists;
        }

        private bool SaveNameChosenCommand_CanExecute()
        {
            return SoundName != null && SoundName.Length >= 2;
        }

        #endregion

        #region Actions

        private void StartRecordingAction()
        {
            lock (_recorder)
            {
                if (IsRecording)
                {
                    _recorder.StopRecording();
                    _recorder.StopSound();

                    _recordTimeUpdateThread.Abort();

                    RecordingExists = true;
                }
                else
                {
                    IsPlaying = false;
                    RecordingExists = false;
                    _recorder.StartRecording();
                    _recorderStartTime = DateTime.UtcNow;

                    _recordTimeUpdateThread = new Thread(delegate(object o)
                      {
                          try
                          {
                              while (!_recorder.StopRequested)
                              {
                                  _recorderTimeGoneBy = DateTime.UtcNow - _recorderStartTime;
                                  Deployment.Current.Dispatcher.BeginInvoke(() =>
                                    {
                                        if (!_recorder.StopRequested)
                                            RecordingTime = _recorderTimeGoneBy.TotalSeconds;
                                    });

                                  Thread.Sleep(45);
                              }
                          }
                          catch
                          {
                              /* Nothing here */
                          }

                      });

                    _recordTimeUpdateThread.Start();
                }
            }

            IsRecording = !IsRecording;
        }

        private void ResetAction()
        {
            _recorder.StopSound();
            _recorder.StopRecording();
        }

        private void PlayPauseAction()
        {
            lock (_recorder)
            {
                _recorder.InitializeSound();

                if (IsPlaying)
                {

                    IsPlaying = false;
                    _recorder.StopRecording();
                    _recorder.StopSound();
                }
                else
                {
                    _recorder.StopRecording();
                    _playerStartTime = DateTime.UtcNow;
                    IsPlaying = true;
                    _playerTimeUpdateThread = new Thread(delegate(object o)
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
                                          lock (_recorder)
                                          {
                                              PlayingTime = RecordingTime;
                                              IsPlaying = false;
                                              _recorder.StopSound();
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

                    _playerTimeUpdateThread.Start();
                    _recorder.PlaySound();
                }
            }
        }

        private void SaveAction()
        {
            SoundName = EditorResources.Recording;
            Navigation.NavigateTo(typeof(SoundNameChooserView));
        }

        private void CancelAction()
        {
            _recorder.StopSound();
            _recorder.StopRecording();

            ResetViewModel();
            Navigation.NavigateBack();
        }

        private void SaveNameChosenAction()
        {
            var sound = new Sound(SoundName, _receivedSelectedSprite);
            string path = CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.SoundsPath + "/" + sound.FileName;

            using (IStorage storage = StorageSystem.GetStorage())
            {
                using (var stream = storage.OpenFile(path, StorageFileMode.Create, StorageFileAccess.Write))
                {
                    var writer = new BinaryWriter(stream);

                    WaveHeaderWriter.WriteHeader(writer.BaseStream, _recorder.SampleRate);
                    var dataBuffer = _recorder.GetSoundAsStream().GetBuffer();
                    writer.Write(dataBuffer, 0, (int)_recorder.GetSoundAsStream().Length);
                    writer.Flush();
                    writer.Close();
                }
            }

            _receivedSelectedSprite.Sounds.Sounds.Add(sound);

            ResetViewModel();
            Navigation.RemoveBackEntry();
            Navigation.RemoveBackEntry();
            Navigation.NavigateBack();
        }

        private void CancelNameChosenAction()
        {
            SoundName = null;
            UpdateTextProperties();

            Navigation.NavigateBack();
        }

        private void ReceiveSelectedSpriteMessageAction(GenericMessage<Sprite> message)
        {
            _receivedSelectedSprite = message.Content;
        }

        private void ResetViewModelAction()
        {
            ResetViewModel();
        }

        #endregion  

        public SoundRecorderViewModel()
        {
            StartRecordingCommand = new RelayCommand(StartRecordingAction);
            ResetCommand = new RelayCommand(ResetAction);
            PlayPauseCommand = new RelayCommand(PlayPauseAction);
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);
            SaveNameChosenCommand = new RelayCommand(SaveNameChosenAction, SaveNameChosenCommand_CanExecute);
            CancelNameChosenCommand = new RelayCommand(CancelNameChosenAction);
            ResetViewModelCommand = new RelayCommand(ResetViewModelAction);

            Messenger.Default.Register<GenericMessage<Sprite>>(this, ViewModelMessagingToken.SelectedSpriteListener, ReceiveSelectedSpriteMessageAction);

            if (!IsInDesignMode)
            {
                _recorder = new Recorder();
            }

            UpdateTextProperties();

            if (IsInDesignMode)
                CreateDesignData();
        }

        private void UpdateTextProperties()
        {
            //TODO: use Localize strings
            string recordButtonHeaderRecord = EditorResources.RecorderRecord;
            string recordButtonHeaderStop = "stop"; // EditorResources.RecorderStop;
            string recordButtonTextRecord = "start recording"; // EditorResources.RecorderStart;
            string recordButtonTextStop = EditorResources.RecorderStop;

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
            SoundName = null;
            IsPlaying = false;
            IsRecording = false;
            RecordingExists = false;
            RecordingTime = 0;
            PlayingTime = 0;

            _recorder = new Recorder();
            if (_recordTimeUpdateThread != null)
                _recordTimeUpdateThread.Abort();
            _recordTimeUpdateThread = null;
            _recorderStartTime = new DateTime();
            _recorderTimeGoneBy = new TimeSpan();

            if (_playerTimeUpdateThread != null)
                _playerTimeUpdateThread.Abort();
            _playerTimeUpdateThread = null;
            _playerStartTime = new DateTime();
            _playerTimeGoneBy = new TimeSpan();
        }

        public override void Cleanup()
        {
            base.Cleanup();
        }
    }
}