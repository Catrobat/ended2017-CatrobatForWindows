using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Service;
using Windows.UI.Xaml.Controls;

namespace Catrobat.IDE.WindowsPhone.Views.Service
{
    public sealed partial class UploadProjectNewPasswordView : ViewPageBase
    {
        private readonly UploadProjectNewPasswordViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).UploadProjectNewPasswordViewModel;

        protected override ViewModelBase GetViewModel() { return _viewModel; }

        public UploadProjectNewPasswordView()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            (sender as PasswordBox).GetBindingExpression(PasswordBox.PasswordProperty).UpdateSource();
        }
    }
}
