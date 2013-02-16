using System;
using System.Windows;
using System.Windows.Controls;
using Catrobat.Core;
using Catrobat.Core.Objects.Sounds;
using Catrobat.Core.Storage;
using Catrobat.IDECommon.Resources;
using Catrobat.IDECommon.Resources.Editor;
using Catrobat.IDEWindowsPhone.Controls.Buttons;
using Catrobat.IDEWindowsPhone.ViewModel;
using IDEWindowsPhone;
using Microsoft.Phone.Controls;
using System.Collections.ObjectModel;
using Microsoft.Phone.Shell;
using System.ComponentModel;
using Microsoft.Xna.Framework.Media;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Sounds
{
  public partial class AudioLibrary : PhoneApplicationPage
  {
    private EditorViewModel editorViewModel = (App.Current.Resources["Locator"] as ViewModelLocator).Editor;
    private Song song;
    private ObservableCollection<SoundListItem> songs = new ObservableCollection<SoundListItem>();
    private bool state = false;
    private ApplicationBarIconButton btnSave;

    public AudioLibrary()
    {
      InitializeComponent();
      
      BuildApplicationBar();
      (App.Current.Resources["LocalizedStrings"] as LocalizedStrings).PropertyChanged += LanguageChanged;

      Dispatcher.BeginInvoke(() =>
      {
        txtName.Focus();
        txtName.SelectAll();
        btnSave.IsEnabled = false;

        stackPanelChangeName.Visibility = Visibility.Collapsed;
        ApplicationBar.IsVisible = false;
      });
      
      loadSongs();
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
      btnCancel.Click += btnCancel_Click;
      ApplicationBar.Buttons.Add(btnCancel);
    }

    private void LanguageChanged(object sender, PropertyChangedEventArgs e)
    {
      BuildApplicationBar();
    }

    protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
    {
      state = false;
      MediaPlayer.Pause();
    }

    protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
    {
      if (state)
      {
        state = false;
        Dispatcher.BeginInvoke(() =>
        {
          stackPanelMediaLibrary.Visibility = Visibility.Visible;
          stackPanelChangeName.Visibility = Visibility.Collapsed;
          ApplicationBar.IsVisible = false;
          Headline.Text = EditorResources.TitleChooseSound;
          txtName.Text = "";
        });
        e.Cancel = true;
      }
    }

    private void loadSongs()
    {
      MediaLibrary library = new MediaLibrary();
      foreach (Song song in library.Songs)
        lbxSongs.Items.Add(new SoundListItem { Song = song, State = PlayButtonState.Pause });
    }

    //TODO: Write UI Test
    private void btnSave_Click(object sender, EventArgs e)
    {
      Sound sound = new Sound(txtName.Text, editorViewModel.SelectedSprite);

      string absoluteFileName = CatrobatContext.Instance.CurrentProject.BasePath + "/sounds/" + sound.FileName;

      using (IStorage storage = StorageSystem.GetStorage())
      {
        using (var stream = storage.OpenFile(absoluteFileName, StorageFileMode.Create, StorageFileAccess.Write))
        {
          //TODO: write song to isolated storage
        }
      }

      editorViewModel.SelectedSprite.Sounds.Sounds.Add(sound);
      NavigationService.RemoveBackEntry();
      NavigationService.GoBack();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
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

    private void lbxSongs_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      state = true;

      song = (lbxSongs.SelectedItem as SoundListItem).Song;
      
      Dispatcher.BeginInvoke(() =>
      {
        stackPanelMediaLibrary.Visibility = Visibility.Collapsed;
        stackPanelChangeName.Visibility = Visibility.Visible;
        ApplicationBar.IsVisible = true;

        Headline.Text = EditorResources.TitleChooseSoundName;
        txtName.Text = song.Name;
        txtName.Focus();
        txtName.SelectAll();
      });
    }

    private void btnPlay_Click(object sender, RoutedEventArgs e)
    {
      SoundListItem sound = (sender as PlayButton).DataContext as SoundListItem;

      if (sound.State == PlayButtonState.Play)
      {
        foreach (SoundListItem soundItem in lbxSongs.Items)
          if (soundItem.State == PlayButtonState.Play)
          {
            MediaPlayer.Pause();
            soundItem.State = PlayButtonState.Pause;
          }
        sound.State = PlayButtonState.Play;
        MediaPlayer.Play(sound.Song);
      }
      else
      {
        sound.State = PlayButtonState.Pause;
        MediaPlayer.Pause();
      }
    }
  }
}