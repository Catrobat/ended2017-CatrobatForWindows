using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI;

namespace Catrobat.IDE.WindowsPhone.Controls.SoundControls
{
    public sealed partial class SoundPlayButtonGroup : UserControl
    {
        private readonly List<SoundPlayButton> _buttons;
        object _lockObject = new object();

        private Sound _sound;
        private MediaElement _mediaElement;
        private IPlayPauseButton _activeButton;
        private bool _isLoading;


        #region DependancyProperties

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand),
            typeof(SoundPlayButtonGroup), new PropertyMetadata(null, CommandChanged));

        private static void CommandChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            // Code for dealing with your property changes
        }

        public Program Program
        {
            get
            {
                return (Program)GetValue(ProgramProperty);
            }
            set
            {
                SetValue(ProgramProperty, value);
            }
        }

        public static readonly DependencyProperty ProgramProperty =
            DependencyProperty.Register("Program",
            typeof(Program),
            typeof(SoundPlayButtonGroup),
            new PropertyMetadata(null, ProgramChanged));

        private static void ProgramChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {

        }


        #endregion

        public SoundPlayButtonGroup()
        {
            this.InitializeComponent();
            SetMediaElement(MediaElementSound);
            _buttons = new List<SoundPlayButton>();
        }

        public void Register(SoundPlayButton button)
        {
            if (button != null && !_buttons.Contains(button))
            {
                _buttons.Add(button);
                button.PlayStateChanged += ButtonOnPlayStateChanged;
            }
        }

        public void UnRegister(SoundPlayButton button)
        {
            if (button != null && !_buttons.Contains(button))
            {
                _buttons.Add(button);
                button.PlayStateChanged -= ButtonOnPlayStateChanged;
            }
        }

        private async void ButtonOnPlayStateChanged(
            SoundPlayButton activeButton, SoundPlayState newState)
        {
            lock (_lockObject)
            {
                if (_isLoading) return;
                _isLoading = true;
            }

            if (activeButton == null) return;
            _activeButton = activeButton;

            var notification = new SoundPlayStateChangedNotification
            {
                NewSound = (Sound)activeButton.DataContext,
                NewState = newState
            };

            if (newState == SoundPlayState.Playing)
            {
                foreach (SoundPlayButton button in _buttons)
                {
                    if (button != activeButton &&
                        button.State == SoundPlayState.Playing)
                    {
                        button.State = SoundPlayState.Stopped;
                        MediaElementSound.Stop();
                    }
                }

                await SetSound(activeButton.Sound);
                Play();
            }
            else
            {
                activeButton.State = SoundPlayState.Paused;
                Stop();
            }

            if (Command != null)
                Command.Execute(notification);

            _isLoading = false;
        }

        private void ChangeSoundStateChanged(SoundPlayState newState)
        {
            var oldState = _activeButton.State;
            _activeButton.State = newState;

            var notification = new SoundPlayStateChangedNotification
            {
                OldSound = _sound,
                OldState = oldState,
                NewSound = _sound,
                NewState = newState
            };

            if (Command != null)
                Command.Execute(notification);
        }

        # region MediaElement interaction

        private void MediaElementOnCurrentStateChanged(
            object sender, RoutedEventArgs args)
        {
            var state = _mediaElement.CurrentState;

            switch (state)
            {
                case MediaElementState.Closed:
                    ChangeSoundStateChanged(SoundPlayState.Stopped);
                    break;
                case MediaElementState.Opening:
                    ChangeSoundStateChanged(SoundPlayState.Playing);
                    break;
                case MediaElementState.Buffering:
                    ChangeSoundStateChanged(SoundPlayState.Playing);
                    break;
                case MediaElementState.Playing:
                    ChangeSoundStateChanged(SoundPlayState.Playing);
                    break;
                case MediaElementState.Paused:
                    ChangeSoundStateChanged(SoundPlayState.Paused);
                    break;
                case MediaElementState.Stopped:
                    ChangeSoundStateChanged(SoundPlayState.Stopped);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public async Task SetSound(Sound sound)
        {
            _sound = sound;

            var localFolder = ApplicationData.Current.LocalFolder;
            var projectsFolder = await localFolder.GetFolderAsync(
                StorageConstants.ProgramsPath);
            var projectFolder = await projectsFolder.GetFolderAsync(
                Program.Name);
            var soundsFolder = await projectFolder.GetFolderAsync(
                StorageConstants.ProgramSoundsPath);
            var soundFile = await soundsFolder.GetFileAsync(_sound.FileName);

            //_mediaElement.Volume = 0.5;
            _mediaElement.SetSource(await soundFile.OpenReadAsync(), soundFile.ContentType);
        }

        public void Play()
        {
            _mediaElement.AutoPlay = true;
            _mediaElement.Play();
        }

        public void Pause()
        {
            _mediaElement.AutoPlay = false;
            _mediaElement.Pause();
        }

        public void Stop()
        {
            _mediaElement.AutoPlay = false;
            _mediaElement.Stop();
        }

        public void Clear()
        {
            Stop();
            _mediaElement = null;
            _sound = null;
            _activeButton = null;
        }

        private void SetMediaElement(MediaElement mediaElement)
        {
            if (_mediaElement != null)
                _mediaElement.CurrentStateChanged -= MediaElementOnCurrentStateChanged;

            _mediaElement = mediaElement;
            _mediaElement.CurrentStateChanged += MediaElementOnCurrentStateChanged;
        }

        #endregion
    }
}
