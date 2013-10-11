using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.IDE.Phone.ViewModel.Service;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDE.Phone.Views.Service
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