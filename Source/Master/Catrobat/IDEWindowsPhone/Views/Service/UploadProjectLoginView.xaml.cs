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
    public partial class UploadProjectLoginView : PhoneApplicationPage
    {
        private readonly UploadProjectLoginViewModel _viewModel = ServiceLocator.Current.GetInstance<UploadProjectLoginViewModel>();

        public UploadProjectLoginView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _viewModel.ResetViewModelCommand.Execute(null);
            base.OnNavigatedFrom(e);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // otherwise bound properties won't get set
            (sender as TextBox).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // otherwise bound properties won't get set
            (sender as PasswordBox).GetBindingExpression(PasswordBox.PasswordProperty).UpdateSource();
        }
    }
}