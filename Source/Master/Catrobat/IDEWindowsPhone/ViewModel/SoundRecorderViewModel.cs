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
using Catrobat.IDEWindowsPhone.Views.Editor.Sounds;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using GalaSoft.MvvmLight.Command;
using IDEWindowsPhone;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;
using Catrobat.IDECommon.Resources.Editor;

namespace Catrobat.IDEWindowsPhone.ViewModel
{
  public class SoundRecorderViewModel : ViewModelBase
  {
    private readonly EditorViewModel _editorViewModel = ServiceLocator.Current.GetInstance<EditorViewModel>();

    #region Private Members

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
      NavigateTo("/Views/Editor/Sounds/SoundNameChooserView.xaml");
    }

    private void CancelAction()
    {
      _recorder.StopSound();
      _recorder.StopRecording();

      NavigateBack();
    }

    private void SaveNameChosenAction()
    {
      Sound sound = new Sound(SoundName, _editorViewModel.SelectedSprite);
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

      _editorViewModel.SelectedSprite.Sounds.Sounds.Add(sound);
      Cleanup();

      RemoveNavigationBackEntry();
      RemoveNavigationBackEntry();
      NavigateBack();
    }

    private void CancelNameChosenAction()
    {
      SoundName = null;
      UpdateTextProperties();
      NavigateBack();
    }

    #endregion

    #region Events

    public void PlayPauseEvent()
    {
      PlayPauseAction();
    }

    public void SaveEvent()
    {
      SaveAction();
    }

    public void CancelEvent()
    {
      CancelAction();
    }

    public void SaveNameChosenEvent()
    {
      SaveNameChosenAction();
    }

    public void CancelNameChosenEvent()
    {
      CancelNameChosenAction();
    }

    #endregion

    #region Properties

    public bool IsRecording
    {
      get { return _isRecording; }
      set
      {
        if (value.Equals(_isRecording)) return;
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
        if (value.Equals(_isPlaying)) return;
        _isPlaying = value;
        RaisePropertyChanged("IsPlaying");
      }
    }

    public double RecordingTime
    {
      get { return _recordingTime; }
      set
      {
        if (value.Equals(_recordingTime)) return;
        _recordingTime = value;
        RaisePropertyChanged("RecordingTime");
      }
    }

    public double PlayingTime
    {
      get { return _playingTime; }
      set
      {
        if (value.Equals(_playingTime)) return;
        _playingTime = value;
        RaisePropertyChanged("PlayingTime");
      }
    }

    public bool RecordingExists
    {
      get { return _recordingExists; }
      set
      {
        if (value.Equals(_recordingExists)) return;
        _recordingExists = value;
        RaisePropertyChanged("RecordingExists");
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
        bool isSoundNameValidBefore = IsSoundNameValid;
        if (value == _soundName) return;
        _soundName = value;
        RaisePropertyChanged("SoundName");
        if (isSoundNameValidBefore != IsSoundNameValid)
          RaisePropertyChanged("IsSoundNameValid");
      }
    }

    public bool IsSoundNameValid
    {
      get
      {
        return SoundName != null && SoundName.Length >= 2;
      }
    }


    #endregion

    public SoundRecorderViewModel()
    {
      PlayPauseCommand = new RelayCommand(PlayPauseAction);
      StartRecordingCommand = new RelayCommand(StartRecordingAction);
      ResetCommand = new RelayCommand(ResetAction);

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
      string defaultSoundName = EditorResources.Recording;

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


      if (SoundName == null)
        SoundName = defaultSoundName;
    }

    private void CreateDesignData()
    {
      RecordingTime = 13.42;
      PlayingTime = 6.23;
    }

    public override void Cleanup()
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

      base.Cleanup();
    }

    private void NavigateTo(string path)
    {
      ((PhoneApplicationFrame)Application.Current.RootVisual).Navigate(new Uri(path, UriKind.Relative));
    }

    private void NavigateBack()
    {
      ((PhoneApplicationFrame)Application.Current.RootVisual).GoBack();
    }

    private void RemoveNavigationBackEntry()
    {
      ((PhoneApplicationFrame)Application.Current.RootVisual).RemoveBackEntry();
    }
  }
}