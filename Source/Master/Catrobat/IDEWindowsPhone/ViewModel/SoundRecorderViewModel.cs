using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;
using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.IDEWindowsPhone.Annotations;
using Catrobat.IDEWindowsPhone.Views.Editor.Sounds;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using GalaSoft.MvvmLight.Command;
using IDEWindowsPhone;
using Microsoft.Phone.Controls;

namespace Catrobat.IDEWindowsPhone.ViewModel
{
  public class SoundRecorderViewModel : ViewModelBase
  {
    private Recorder _recorder;
    private DateTime _recorderStartTime;
    private TimeSpan _recorderTimeGoneBy;

    private DateTime _playerStartTime;
    private TimeSpan _playerTimeGoneBy;

    private bool _isRecording;
    private bool _isPlaying;
    private double _recordingTime;
    private string _recordButtonText;
    private string _resetButtonText;
    private string _recordButtonHeader;
    private string _resetButtonHeader;


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
      if (IsRecording)
      {
        _recorder.StopSound();
      }

      IsRecording = !IsRecording;

      _recorder.StartRecording();

      _recorderStartTime = DateTime.UtcNow;
      var counter = new Thread(delegate(object o)
        {
          while (!_recorder.StopRequested)
          {
            _recorderTimeGoneBy = DateTime.UtcNow - _recorderStartTime;
            Deployment.Current.Dispatcher.BeginInvoke(() =>
              {
                RecordingTime = _recorderTimeGoneBy.TotalSeconds;
              });

            Thread.Sleep(100);
          }
        });

      counter.Start();
    }

    private void ResetAction()
    {
      _recorder.StopSound();
      _recorder.StopRecording();
    }

    private void PlayPauseAction()
    {
      _recorder.InitializeSound();

      if (IsPlaying)
      {
        _recorder.StopSound();
      }
      else
      {
        _recorder.PlaySound();
      }
    }

    private void SaveAction()
    {


    }

    private void CancelAction()
    {
      _recorder.StopSound();
      _recorder.StopRecording();

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

    #endregion

    #region Properties

    public bool IsRecording
    {
      get { return _isRecording; }
      set
      {
        if (value.Equals(_isRecording)) return;
        _isRecording = value;
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


      if (IsInDesignMode)
        CreateDesignData();
      else
        InitializeTranslations();
    }

    private void InitializeTranslations()
    {
      // TODO: replace with localized strings
      RecordingTime = 0;
      RecordButtonHeader = "Rec";
      RecordButtonText = "Start recording";
      ResetButtonHeader = "Reset";
      ResetButtonText = "Delete current recording";
    }

    private void CreateDesignData()
    {
      RecordingTime = 13.42;
      RecordButtonHeader = "Rec";
      RecordButtonText = "Start recording";
      ResetButtonHeader = "Reset";
      ResetButtonText = "Delete current recording";
    }

    public override void Cleanup()
    {
      // Clean up if needed

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
  }
}