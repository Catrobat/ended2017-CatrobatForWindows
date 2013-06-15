using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.IDECommon.Resources;
using Catrobat.IDECommon.Resources.Editor;
using Catrobat.IDEWindowsPhone.Controls.Buttons;
using IDEWindowsPhone;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Practices.ServiceLocation;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Sounds;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Sounds
{
    public partial class SoundRecorderView : PhoneApplicationPage
    {
        private readonly SoundRecorderViewModel _soundRecorderViewModel = ServiceLocator.Current.GetInstance<SoundRecorderViewModel>();
        private ApplicationBarIconButton _buttonSave;

        public SoundRecorderView()
        {
            InitializeComponent();
            BuildApplicationBar();
            ((LocalizedStrings)Application.Current.Resources["LocalizedStrings"]).PropertyChanged += LanguageChanged;
            _soundRecorderViewModel.PropertyChanged += SoundRecorderViewModel_OnPropertyChanged;
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            _soundRecorderViewModel.ResetViewModel();
        }

        private void SoundRecorderViewModel_OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "IsRecording")
            {
                if (_soundRecorderViewModel.IsRecording)
                    RecordingAnimation.Begin();
                else
                    RecordingAnimation.Stop();
            }

            if (propertyChangedEventArgs.PropertyName == "IsPlaying")
            {
                PlayButton.State = _soundRecorderViewModel.IsPlaying ? PlayButtonState.Play : PlayButtonState.Pause;
            }

            if (propertyChangedEventArgs.PropertyName == "RecordingExists" && _buttonSave != null)
            {
                _buttonSave.IsEnabled = _soundRecorderViewModel.RecordingExists;
            }
        }

        private void PlayButton_OnClick(object sender, RoutedEventArgs e)
        {
            _soundRecorderViewModel.PlayPauseCommand.Execute(null);
        }

        #region Appbar

        private void BuildApplicationBar()
        {
            ApplicationBar = new ApplicationBar();

            _buttonSave = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.save.rest.png",
                                                   UriKind.Relative));
            _buttonSave.IsEnabled = _soundRecorderViewModel.RecordingExists;
            _buttonSave.Text = EditorResources.ButtonSave;
            _buttonSave.Click += buttonSave_Click;
            ApplicationBar.Buttons.Add(_buttonSave);

            var buttonCancel = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.cancel.rest.png", UriKind.Relative));
            buttonCancel.Text = EditorResources.ButtonCancel;
            buttonCancel.Click += buttonCancel_Click;
            ApplicationBar.Buttons.Add(buttonCancel);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            _soundRecorderViewModel.SaveCommand.Execute(null);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            _soundRecorderViewModel.CancelCommand.Execute(null);
        }

        private void LanguageChanged(object sender, PropertyChangedEventArgs e)
        {
            BuildApplicationBar();
        }

        #endregion
    }
}