using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Service;
using Windows.UI.Xaml.Controls;

namespace Catrobat.IDE.WindowsPhone.Views.Service
{
    public sealed partial class UploadProgramNewPasswordView : ViewPageBase
    {
        private readonly UploadProgramNewPasswordViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).UploadProgramNewPasswordViewModel;

        

        public UploadProgramNewPasswordView()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            (sender as PasswordBox).GetBindingExpression(PasswordBox.PasswordProperty).UpdateSource();
        }
    }
}
