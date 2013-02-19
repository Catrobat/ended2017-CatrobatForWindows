using System;
using System.Windows;
using System.Windows.Controls;
using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Sounds;
using Catrobat.Core.Storage;
using Catrobat.IDECommon.Resources;
using Catrobat.IDECommon.Resources.Editor;
using Catrobat.IDEWindowsPhone.Controls.Buttons;
using Catrobat.IDEWindowsPhone.ViewModel;
using IDEWindowsPhone;
using MetroCatIDE.Views.Editor.Sounds;
using Microsoft.Phone.Controls;
using System.IO;
using System.Threading;
using Microsoft.Phone.Shell;
using System.ComponentModel;


namespace Catrobat.IDEWindowsPhone.Views.Editor.Sounds
{
  public partial class SoundRecorder : PhoneApplicationPage
  {
    private EditorViewModel editorViewModel = (App.Current.Resources["Locator"] as ViewModelLocator).Editor;
    ApplicationBarIconButton btnSave;

    private DateTime _startTime;
    TimeSpan _timeGoneBy;
    Thread _playTime;
    private bool _abort = false;
    private bool _songSelected = false;

    private Recorder _recorder;

    public SoundRecorder()
    {
      InitializeComponent();

      _recorder = new Recorder();

      BuildApplicationBar();
      (App.Current.Resources["LocalizedStrings"] as LocalizedStrings).PropertyChanged += LanguageChanged;

      Dispatcher.BeginInvoke(() =>
      {
        stackPanelChangeName.Visibility = Visibility.Collapsed;
        ApplicationBar.IsVisible = false;
        btnPlay.Visibility = Visibility.Collapsed;
        btnSave.IsEnabled = false;
        btnStop.IsEnabled = false;
      });
    }

    private void BuildApplicationBar()
    {
      ApplicationBar = new ApplicationBar();

      btnSave = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.check.rest.png", UriKind.Relative));
      btnSave.Text = EditorResources.ButtonSave;
      btnSave.Click += btnSave_Click;
      ApplicationBar.Buttons.Add(btnSave);

      ApplicationBarIconButton btnCancel = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.cancel.rest.png", UriKind.Relative));
      btnCancel.Text = EditorResources.ButtonCancel;
      //btnCancel.Click += btnCancel_Click; // TODO Check if this is needed
      ApplicationBar.Buttons.Add(btnCancel);
    }

    private void LanguageChanged(object sender, PropertyChangedEventArgs e)
    {
      BuildApplicationBar();
    }

    protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
    {
      _recorder.StopSound();
      _recorder.StopRecording();

      stackPanelChangeName.Visibility = Visibility.Collapsed;
      ApplicationBar.IsVisible = false;
      btnPlay.Visibility = Visibility.Collapsed;
      btnSave.IsEnabled = false;
      btnStop.IsEnabled = false;
    }

    protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
    {
      if (_songSelected)
      {
        _songSelected = false;

        Dispatcher.BeginInvoke(() =>
        {
          gDisplay.Visibility = Visibility.Visible;
          gButtons.Visibility = Visibility.Visible;
          gRecordButtons.Visibility = Visibility.Visible;
          stackPanelChangeName.Visibility = Visibility.Collapsed;
          ApplicationBar.IsVisible = false;
          Headline.Text = EditorResources.TitleRecorder;
          txtName.Text = "";
        });
        e.Cancel = true;
      }
    }

    private void StartRecording()
    {
      _recorder.StartRecording();

      _startTime = DateTime.UtcNow;
      Thread counter = new System.Threading.Thread(CountTime);
      counter.Start();
    }

    private void CountTime()
    {
      while (!_recorder.StopRequested)
      {
        _timeGoneBy = DateTime.UtcNow - _startTime;
        Dispatcher.BeginInvoke(() =>
          {
            tbTime.Text = _timeGoneBy.Minutes.ToString() + ":" + _timeGoneBy.Seconds.ToString() + ":" +
                          _timeGoneBy.Milliseconds / 100;
          });
        Thread.Sleep(100);
      }
    }

    private void btnRecord_Click(object sender, RoutedEventArgs e)
    {
      _recorder.StopSound();
      StartRecording();

      Dispatcher.BeginInvoke(() =>
        {
          btnUse.IsEnabled = false;
          btnStop.IsEnabled = true;
          btnRecord.IsEnabled = false;
          btnPlay.State = PlayButtonState.Pause;
          btnPlay.Visibility = Visibility.Collapsed;
        });
    }

    private void btnStop_Click(object sender, RoutedEventArgs e)
    {
      _recorder.StopRecording();

      Dispatcher.BeginInvoke(() =>
        {
          btnUse.IsEnabled = true;
          btnPlay.Visibility = Visibility.Visible;
          btnStop.IsEnabled = false;
          btnRecord.IsEnabled = true;
        });
    }

    private void btnPlay_Click(object sender, RoutedEventArgs e)
    {
      _recorder.InitializeSound();

      if (btnPlay.State == PlayButtonState.Play)//Pause
      {
        _abort = false;
        tbTime.Text = "0:0:0";
        _startTime = DateTime.UtcNow;
        _playTime = new System.Threading.Thread(ShowPlayTime);
        _playTime.Start();

        _recorder.PlaySound();
      }
      else
      {
        _abort = true;
        _recorder.StopSound();
      }
    }

    private void ShowPlayTime()
    {
      TimeSpan playedTime = DateTime.UtcNow - _startTime;
      while (_timeGoneBy > playedTime && !_abort)
      {
        Dispatcher.BeginInvoke(() =>
        {
          playedTime = DateTime.UtcNow - _startTime;
          tbTime.Text = playedTime.Minutes.ToString() + ":" + playedTime.Seconds.ToString() + ":" + playedTime.Milliseconds / 100;
        });
        Thread.Sleep(100);
      }
      Dispatcher.BeginInvoke(() =>
      {
        tbTime.Text = _timeGoneBy.Minutes.ToString() + ":" + _timeGoneBy.Seconds.ToString() + ":" + _timeGoneBy.Milliseconds / 100;
        btnPlay.State = PlayButtonState.Pause;
      });
    }

    private void btnUse_Click(object sender, RoutedEventArgs e)
    {
      _songSelected = true;
      _recorder.StopSound();

      Dispatcher.BeginInvoke(() =>
      {
        gDisplay.Visibility = Visibility.Collapsed;
        gRecordButtons.Visibility = Visibility.Collapsed;
        gButtons.Visibility = Visibility.Collapsed;
        stackPanelChangeName.Visibility = Visibility.Visible;
        ApplicationBar.IsVisible = true;

        Headline.Text = EditorResources.TitleChooseSoundName;
        txtName.Text = EditorResources.Recording;
        txtName.Focus();
        txtName.SelectAll();
      });
    }

    private void btnCancel_Click(object sender, RoutedEventArgs e)
    {
      _recorder.StopSound();
      _recorder.StopRecording();

      NavigationService.RemoveBackEntry();
      NavigationService.GoBack();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      Sound sound = new Sound(txtName.Text, editorViewModel.SelectedSprite);
      string path = CatrobatContext.Instance.CurrentProject.BasePath + "/" + Project.SoundsPath + "/" + sound.FileName;


      using (IStorage storage = StorageSystem.GetStorage())
      {
        using (var stream = storage.OpenFile(path, StorageFileMode.Create, StorageFileAccess.Write))
        {
          BinaryWriter writer = new BinaryWriter(stream);

          WaveHeaderWriter.WriteHeader(writer.BaseStream, _recorder.SampleRate);
          var dataBuffer = _recorder.GetSoundAsStream().GetBuffer();
          writer.Write(dataBuffer, 0, (int)_recorder.GetSoundAsStream().Length);
          writer.Flush();
          writer.Close();
        }
      }

      editorViewModel.SelectedSprite.Sounds.Sounds.Add(sound);
      NavigationService.RemoveBackEntry();
      NavigationService.GoBack();
    }

    private void txtName_TextChanged(object sender, TextChangedEventArgs e)
    {
      if (txtName.Text != "")
        btnSave.IsEnabled = true;
      else
        btnSave.IsEnabled = false;
    }
  }
}