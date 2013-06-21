using System;
using System.Windows;
using System.Windows.Controls;
using Catrobat.IDECommon.Resources.Main;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.ViewModel;
using Catrobat.IDEWindowsPhone.ViewModel.Service;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Practices.ServiceLocation;
using System.Windows.Navigation;

namespace Catrobat.IDEWindowsPhone.Views.Service
{
    public partial class UploadProjectView : PhoneApplicationPage
    {
        private readonly UploadProjectViewModel _viewModel = ServiceLocator.Current.GetInstance<UploadProjectViewModel>();
        private ApplicationBarIconButton _uploadButton;

        public UploadProjectView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _viewModel.ResetViewModel();
            base.OnNavigatedFrom(e);
        }

        private void LocalizeApplicationBar()
        {
            _uploadButton = (ApplicationBarIconButton)ApplicationBar.Buttons[0];
            _uploadButton.Text = MainResources.ButtonUpload;
            ((ApplicationBarIconButton)ApplicationBar.Buttons[1]).Text = MainResources.ButtonCancel;
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            LocalizeApplicationBar();
        }

        private void Upload_Click(object sender, EventArgs e)
        {
            _viewModel.UploadCommand.Execute(sender);
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Navigation.NavigateBack();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Cannot be implemented using MVVM - the Appbar is not XAML compatible.
            _uploadButton.IsEnabled = (sender as TextBox).Text != "";

            // Hack for Windows Phone 7
            (sender as TextBox).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
    }
}