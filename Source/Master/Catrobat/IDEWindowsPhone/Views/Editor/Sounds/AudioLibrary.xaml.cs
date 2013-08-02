using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.Core;
using Catrobat.Core.Objects.Sounds;
using Catrobat.Core.Storage;
using Catrobat.IDEWindowsPhone.Content.Localization;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.Misc.Sounds;
using Catrobat.IDEWindowsPhone.ViewModel.Editor;
using IDEWindowsPhone;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Sounds
{
    public partial class AudioLibrary : PhoneApplicationPage
    {
        private readonly EditorViewModel _editorViewModel = ServiceLocator.Current.GetInstance<EditorViewModel>();
        private Song _selectedSong;
        private ObservableCollection<SoundListItem> _songs = new ObservableCollection<SoundListItem>();
        private bool _songSelected = false;
        private ApplicationBarIconButton _btnSave;

        public AudioLibrary()
        {
            InitializeComponent();

            BuildApplicationBar();
            ((LocalizedStrings)Application.Current.Resources["LocalizedStrings"]).PropertyChanged += LanguageChanged;

            Dispatcher.BeginInvoke(() =>
                {
                    TxtName.Focus();
                    TxtName.SelectAll();
                    _btnSave.IsEnabled = false;

                    StackPanelChangeName.Visibility = Visibility.Collapsed;
                    ApplicationBar.IsVisible = false;
                });

            loadSongs();
        }

        private void BuildApplicationBar()
        {
            ApplicationBar = new ApplicationBar();

            _btnSave = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.check.rest.png", UriKind.Relative));
            _btnSave.Text = AppResources.Editor_ButtonSave;
            _btnSave.Click += btnSave_Click;
            ApplicationBar.Buttons.Add(_btnSave);

            var btnCancel = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.cancel.rest.png", UriKind.Relative));
            btnCancel.Text = AppResources.Editor_ButtonCancel;
            btnCancel.Click += btnCancel_Click;
            ApplicationBar.Buttons.Add(btnCancel);
        }

        private void LanguageChanged(object sender, PropertyChangedEventArgs e)
        {
            BuildApplicationBar();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _songSelected = false;
            MediaPlayer.Pause();
            FrameworkDispatcher.Update();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (_songSelected)
            {
                _songSelected = false;
                Dispatcher.BeginInvoke(() =>
                    {
                        StackPanelMediaLibrary.Visibility = Visibility.Visible;
                        StackPanelChangeName.Visibility = Visibility.Collapsed;
                        ApplicationBar.IsVisible = false;
                        Headline.Text = AppResources.Editor_TitleChooseSound;
                        TxtName.Text = "";
                    });
                e.Cancel = true;
            }
        }

        private void loadSongs()
        {
            var library = new MediaLibrary();
            foreach (Song song in library.Songs)
            {
                LbxSongs.Items.Add(new SoundListItem {Song = song});
            }
        }

        //TODO: Write UI Test
        private void btnSave_Click(object sender, EventArgs e)
        {
            var sound = new Sound(TxtName.Text);

            var absoluteFileName = Path.Combine(CatrobatContext.GetContext().CurrentProject.BasePath, "sounds", sound.FileName);

            using (var storage = StorageSystem.GetStorage())
            {
                using (var stream = storage.OpenFile(absoluteFileName, StorageFileMode.Create, StorageFileAccess.Write))
                {
                    //TODO: write song to isolated storage
                }
            }

            _editorViewModel.SelectedSprite.Sounds.Sounds.Add(sound);
            Navigation.RemoveBackEntry();
            Navigation.NavigateBack();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Navigation.RemoveBackEntry();
            Navigation.NavigateBack();
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtName.Text))
            {
                _btnSave.IsEnabled = true;
            }
            else
            {
                _btnSave.IsEnabled = false;
            }
        }

        private void lbxSongs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _songSelected = true;

            MediaPlayer.Pause();
            FrameworkDispatcher.Update();

            var soundListItem = LbxSongs.SelectedItem as SoundListItem;
            if (soundListItem != null)
            {
                _selectedSong = soundListItem.Song;

                Dispatcher.BeginInvoke(() =>
                    {
                        StackPanelMediaLibrary.Visibility = Visibility.Collapsed;
                        StackPanelChangeName.Visibility = Visibility.Visible;
                        ApplicationBar.IsVisible = true;

                        Headline.Text = AppResources.Editor_TitleChooseSoundName;
                        TxtName.Text = _selectedSong.Name;
                        TxtName.Focus();
                        TxtName.SelectAll();
                    });
            }
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            //SoundListItem sound = (sender as PlayButton).DataContext as SoundListItem;

            //if (sound.State == PlayButtonState.Play)
            //{
            //  foreach (SoundListItem soundItem in lbxSongs.Items)
            //    if (soundItem.State == PlayButtonState.Play)
            //    {
            //      MediaPlayer.Pause();
            //      soundItem.State = PlayButtonState.Pause;
            //      FrameworkDispatcher.Update();
            //    }
            //  sound.State = PlayButtonState.Play;
            //  MediaPlayer.Play(sound.Song);
            //  FrameworkDispatcher.Update();
            //}
            //else
            //{
            //  sound.State = PlayButtonState.Pause;
            //  MediaPlayer.Pause();
            //  FrameworkDispatcher.Update();
            //}
        }
    }
}