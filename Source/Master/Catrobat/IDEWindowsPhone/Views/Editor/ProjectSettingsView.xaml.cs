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
using System.Windows.Navigation;

namespace Catrobat.IDEWindowsPhone.Views.Editor
{
    public partial class ProjectSettingsView : PhoneApplicationPage
    {
        private readonly ProjectSettingsViewModel _viewModel = ServiceLocator.Current.GetInstance<ProjectSettingsViewModel>();

        public ProjectSettingsView()
        {
            InitializeComponent();

            Dispatcher.BeginInvoke(() =>
            {
                TextBoxProjectName.Focus();
                TextBoxProjectName.SelectAll();
            });
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _viewModel.ResetViewModelCommand.Execute(null);
            base.OnNavigatedFrom(e);
        }

        private void TextBoxProjectName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.ProjectName = TextBoxProjectName.Text;
        }
    }
}