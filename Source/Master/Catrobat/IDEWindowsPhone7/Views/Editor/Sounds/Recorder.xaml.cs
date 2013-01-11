using System;
using System.Windows;
using System.Windows.Controls;
using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Sounds;
using Catrobat.Core.Storage;
using Catrobat.IDECommon.Resources;
using Catrobat.IDECommon.Resources.Editor;
using Catrobat.IDEWindowsPhone7.Controls.Buttons;
using Catrobat.IDEWindowsPhone7.ViewModel;
using MetroCatIDE.Views.Editor.Sounds;
using Microsoft.Phone.Controls;
using System.IO;
using System.Threading;
using Microsoft.Phone.Shell;
using System.ComponentModel;
using MetroCatIDE.ViewModel;
using Microsoft.Xna.Framework.Audio;

namespace Catrobat.IDEWindowsPhone7.Views.Editor.Sounds
{
  public partial class Recorder : PhoneApplicationPage
  {
    private EditorViewModel editorViewModel = (App.Current.Resources["Locator"] as ViewModelLocator).Editor;
    ApplicationBarIconButton btnSave;

    private Microphone currentMicrophone;
    private byte[] audioBuffer;
    private int sampleRate;
    private bool stopRequested = false;
    private MemoryStream currentRecordingStream;
    private SoundEffectInstance sound;
    private DateTime startTime;
    TimeSpan timeGoneBy;
    Thread playTime;
    bool abort = false;
    bool state = false;

    public Recorder()
    {
      InitializeComponent();

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
      if(sound != null)
        sound.Stop();

      if (currentMicrophone != null)
      {
        currentMicrophone.Stop();
        currentMicrophone.BufferReady -= currentMicrophone_BufferReady;
        currentMicrophone = null;
      }
      stopRequested = false;
      currentRecordingStream = null;
      sound = null;
      abort = false;

      stackPanelChangeName.Visibility = Visibility.Collapsed;
      ApplicationBar.IsVisible = false;
      btnPlay.Visibility = Visibility.Collapsed;
      btnSave.IsEnabled = false;
      btnStop.IsEnabled = false;
    }

    protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
    {
      if (state)
      {
        state = false;
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
      currentRecordingStream = new MemoryStream(1048576);

      if (currentMicrophone == null)
      {
        currentMicrophone = Microphone.Default;
        currentMicrophone.BufferDuration = TimeSpan.FromMilliseconds(300);
        currentMicrophone.BufferReady += currentMicrophone_BufferReady;
        audioBuffer = new byte[currentMicrophone.GetSampleSizeInBytes(
                            currentMicrophone.BufferDuration)];
        sampleRate = currentMicrophone.SampleRate;
      }

      stopRequested = false;
      currentMicrophone.Start();

      startTime = DateTime.UtcNow;
      Thread counter = new System.Threading.Thread(CountTime);
      counter.Start();
    }

    private void CountTime()
    {  
      while (!stopRequested)
      {
        timeGoneBy = DateTime.UtcNow - startTime;
        Dispatcher.BeginInvoke(() =>
        {
          tbTime.Text = timeGoneBy.Minutes.ToString() + ":" + timeGoneBy.Seconds.ToString() + ":" + timeGoneBy.Milliseconds/100;
        });
        Thread.Sleep(100);
      }
    }

    private void RequestStopRecording()
    {
      stopRequested = true;
    }

    private void currentMicrophone_BufferReady(object sender, EventArgs e)
    {
      currentMicrophone.GetData(audioBuffer);
      currentRecordingStream.Write(audioBuffer, 0, audioBuffer.Length);
      if (!stopRequested)
        return;
      currentMicrophone.Stop();
    }

    private void btnRecord_Click(object sender, RoutedEventArgs e)
    {
      if (sound != null)
      {
        sound.Stop();
        sound.Dispose();
        sound = null;
      }

      Dispatcher.BeginInvoke(() =>
      {
        btnUse.IsEnabled = false;
        btnStop.IsEnabled = true;
        btnRecord.IsEnabled = false;
        btnPlay.State = PlayButtonState.Pause;
        btnPlay.Visibility = Visibility.Collapsed;
      });

      StartRecording();
    }

    private void btnStop_Click(object sender, RoutedEventArgs e)
    {
      RequestStopRecording();

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
      if(sound == null)
        sound = new SoundEffect(currentRecordingStream.ToArray(), currentMicrophone.SampleRate, AudioChannels.Mono).CreateInstance();

      if (btnPlay.State == PlayButtonState.Play)//this is actually Pause but because the playButton Template already set the new state by itself it is Play
      {
        abort = false;
        tbTime.Text = "0:0:0";
        startTime = DateTime.UtcNow;
        playTime = new System.Threading.Thread(ShowPlayTime);
        playTime.Start();

        sound.Play();
      }
      else
      {
        abort = true;
        sound.Stop();
      }
    }

    private void ShowPlayTime()
    {
      TimeSpan playedTime = DateTime.UtcNow - startTime;
      while (timeGoneBy > playedTime && !abort)
      {
        Dispatcher.BeginInvoke(() =>
        {
          playedTime = DateTime.UtcNow - startTime;
          tbTime.Text = playedTime.Minutes.ToString() + ":" + playedTime.Seconds.ToString() + ":" + playedTime.Milliseconds / 100;
        });
        Thread.Sleep(100);
      }
      Dispatcher.BeginInvoke(() =>
      {
        tbTime.Text = timeGoneBy.Minutes.ToString() + ":" + timeGoneBy.Seconds.ToString() + ":" + timeGoneBy.Milliseconds / 100;
        btnPlay.State = PlayButtonState.Pause;
      });
    }

    private void btnUse_Click(object sender, RoutedEventArgs e)
    {
      state = true;

      if (sound != null)
        sound.Stop();

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
      currentRecordingStream = null;
      if (sound != null)
        sound.Stop();

      NavigationService.RemoveBackEntry();
      NavigationService.GoBack();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      Sound sound = new Sound(txtName.Text, editorViewModel.SelectedSprite);
      string path = CatrobatContext.Instance.CurrentProject.BasePath + "/" + Project.SoundsPath + "/" + sound.FileName;


      using (IStorage storage = StorageSystem.GetStorage())
      {
        using(var stream = storage.OpenFile(path, StorageFileMode.Create, StorageFileAccess.Write))
        {
          BinaryWriter writer = new BinaryWriter(stream);

          WaveHeaderWriter.WriteHeader(writer.BaseStream, sampleRate);
          var dataBuffer = currentRecordingStream.GetBuffer();
          writer.Write(dataBuffer, 0, (int)currentRecordingStream.Length);
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