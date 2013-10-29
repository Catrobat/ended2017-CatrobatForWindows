using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Service;
using Microsoft.Phone.Controls;

namespace Catrobat.IDE.Phone.Views.Service
{
    public partial class UploadProjectLoginView : PhoneApplicationPage
    {
        private readonly UploadProjectLoginViewModel _viewModel = 
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).UploadProjectLoginViewModel;

        public UploadProjectLoginView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _viewModel.GoBackCommand.Execute(null);
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