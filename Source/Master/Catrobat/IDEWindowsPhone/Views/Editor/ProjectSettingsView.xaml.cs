using Catrobat.IDECommon.Resources;
using Catrobat.IDECommon.Resources.Editor;
using Catrobat.IDEWindowsPhone.Misc;
using IDEWindowsPhone;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.ComponentModel;
using System;
using Catrobat.IDEWindowsPhone.ViewModel.Editor;
using Microsoft.Practices.ServiceLocation;
using System.Windows.Controls;

namespace Catrobat.IDEWindowsPhone.Views.Editor
{
    public partial class ProjectSettingsView : PhoneApplicationPage
    {
        private readonly ProjectSettingsViewModel _projectSettingsViewModel = ServiceLocator.Current.GetInstance<ProjectSettingsViewModel>();

        private ApplicationBarIconButton _btnSave;

        public ProjectSettingsView()
        {
            InitializeComponent();

            BuildApplicationBar();
            (App.Current.Resources["LocalizedStrings"] as LocalizedStrings).PropertyChanged += LanguageChanged;
            _projectSettingsViewModel.PropertyChanged += ProjectSettingsViewModel_OnPropertyChanged;
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            _projectSettingsViewModel.ResetViewModel();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                TextBoxProjectName.Focus();
                TextBoxProjectName.SelectAll();
            });

            _btnSave.IsEnabled = _projectSettingsViewModel.IsProjectNameValid;
            base.OnNavigatedTo(e);
        }

        private void ProjectSettingsViewModel_OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "IsProjectNameValid" && _btnSave != null)
            {
                _btnSave.IsEnabled = _projectSettingsViewModel.IsProjectNameValid;
            }
        }

        private void TextBoxProjectName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _projectSettingsViewModel.ProjectName = TextBoxProjectName.Text;
        }

        #region Appbar

        private void BuildApplicationBar()
        {
            ApplicationBar = new ApplicationBar();

            _btnSave = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.save.rest.png", UriKind.Relative));
            _btnSave.Text = EditorResources.ButtonSave;
            _btnSave.Click += btnSave_Click;
            ApplicationBar.Buttons.Add(_btnSave);

            ApplicationBarIconButton btnCancel = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.cancel.rest.png", UriKind.Relative));
            btnCancel.Text = EditorResources.ButtonCancel;
            btnCancel.Click += btnCancel_Click;
            ApplicationBar.Buttons.Add(btnCancel);
        }

        private void LanguageChanged(object sender, PropertyChangedEventArgs e)
        {
            BuildApplicationBar();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _projectSettingsViewModel.SaveCommand.Execute(null);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _projectSettingsViewModel.CancelCommand.Execute(null);
        }

        #endregion
    }
}