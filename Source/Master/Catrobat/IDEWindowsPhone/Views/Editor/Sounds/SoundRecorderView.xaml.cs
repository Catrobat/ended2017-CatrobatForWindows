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
        private readonly SoundRecorderViewModel _viewModel = ServiceLocator.Current.GetInstance<SoundRecorderViewModel>();
        private ApplicationBarIconButton _buttonSave;

        public SoundRecorderView()
        {
            InitializeComponent();
            BuildApplicationBar();
            ((LocalizedStrings)Application.Current.Resources["LocalizedStrings"]).PropertyChanged += LanguageChanged;
            _viewModel.PropertyChanged += SoundRecorderViewModel_OnPropertyChanged;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _viewModel.ResetViewModel();
            base.OnNavigatedFrom(e);
        }

        private void SoundRecorderViewModel_OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "IsRecording")
            {
                if (_viewModel.IsRecording)
                    RecordingAnimation.Begin();
                else
                    RecordingAnimation.Stop();
            }

            if (propertyChangedEventArgs.PropertyName == "IsPlaying")
            {
                PlayButton.State = _viewModel.IsPlaying ? PlayButtonState.Play : PlayButtonState.Pause;
            }

            if (propertyChangedEventArgs.PropertyName == "RecordingExists" && _buttonSave != null)
            {
                _buttonSave.IsEnabled = _viewModel.RecordingExists;
            }
        }

        private void PlayButton_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.PlayPauseCommand.Execute(null);
        }

        #region Appbar

        private void BuildApplicationBar()
        {
            ApplicationBar = new ApplicationBar();

            _buttonSave = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.save.rest.png",
                                                   UriKind.Relative));
            _buttonSave.IsEnabled = _viewModel.RecordingExists;
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
            _viewModel.SaveCommand.Execute(null);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            _viewModel.CancelCommand.Execute(null);
        }

        private void LanguageChanged(object sender, PropertyChangedEventArgs e)
        {
            BuildApplicationBar();
        }

        #endregion
    }
}