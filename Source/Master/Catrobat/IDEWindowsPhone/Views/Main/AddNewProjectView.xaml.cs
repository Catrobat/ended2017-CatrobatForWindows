using System;
using System.Windows;
using System.Windows.Controls;
using Catrobat.IDECommon.Resources.Main;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.ViewModel;
using Catrobat.IDEWindowsPhone.ViewModel.Main;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Practices.ServiceLocation;
using IDEWindowsPhone;
using Catrobat.IDECommon.Resources;
using System.ComponentModel;
using Catrobat.IDECommon.Resources.Editor;
using System.Windows.Navigation;

namespace Catrobat.IDEWindowsPhone.Views.Main
{
    public partial class AddNewProjectView : PhoneApplicationPage
    {
        private readonly AddNewProjectViewModel _viewModel = ServiceLocator.Current.GetInstance<AddNewProjectViewModel>();

        public AddNewProjectView()
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