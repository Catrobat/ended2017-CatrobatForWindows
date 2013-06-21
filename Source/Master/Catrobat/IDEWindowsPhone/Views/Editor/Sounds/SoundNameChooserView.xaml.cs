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
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Practices.ServiceLocation;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Sounds;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Sounds
{
    public partial class SoundNameChooserView : PhoneApplicationPage
    {
        private readonly SoundRecorderViewModel _soundRecorderViewModel = ServiceLocator.Current.GetInstance<SoundRecorderViewModel>();

        private ApplicationBarIconButton _buttonSave;

        public SoundNameChooserView()
        {
            InitializeComponent();
            BuildApplicationBar();
            ((LocalizedStrings)Application.Current.Resources["LocalizedStrings"]).PropertyChanged += LanguageChanged;
            _soundRecorderViewModel.PropertyChanged += SoundRecorderViewModel_OnPropertyChanged;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //DON'T RESET VIEWMODEL
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                TextBoxSoundName.Focus();
                TextBoxSoundName.SelectAll();
            });

            _buttonSave.IsEnabled = _soundRecorderViewModel.IsSoundNameValid;
            base.OnNavigatedTo(e);
        }

        private void SoundRecorderViewModel_OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "IsSoundNameValid" && _buttonSave != null)
            {
                _buttonSave.IsEnabled = _soundRecorderViewModel.IsSoundNameValid;
            }
        }

        private void TextBoxSoundName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _soundRecorderViewModel.SoundName = TextBoxSoundName.Text;
        }

        #region Appbar

        private void BuildApplicationBar()
        {
            ApplicationBar = new ApplicationBar();

            _buttonSave = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.save.rest.png", UriKind.Relative));
            _buttonSave.Text = EditorResources.ButtonSave;
            _buttonSave.IsEnabled = _soundRecorderViewModel.IsSoundNameValid;
            _buttonSave.Click += buttonSave_Click;
            ApplicationBar.Buttons.Add(_buttonSave);

            var buttonCancel = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.cancel.rest.png", UriKind.Relative));
            buttonCancel.Text = EditorResources.ButtonCancel;
            buttonCancel.Click += buttonCancel_Click;
            ApplicationBar.Buttons.Add(buttonCancel);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            _soundRecorderViewModel.SaveNameChosenCommand.Execute(null);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            _soundRecorderViewModel.CancelNameChosenCommand.Execute(null);
        }

        private void LanguageChanged(object sender, PropertyChangedEventArgs e)
        {
            BuildApplicationBar();
        }

        #endregion
    }
}