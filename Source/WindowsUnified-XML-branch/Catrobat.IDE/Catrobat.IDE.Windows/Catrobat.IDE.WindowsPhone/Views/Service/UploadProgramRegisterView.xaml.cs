using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Service;
using Windows.UI.Xaml.Controls;

namespace Catrobat.IDE.WindowsPhone.Views.Service
{
    public partial class UploadProgramRegisterView : ViewPageBase
    {
        private readonly UploadProgramRegisterViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).UploadProgramRegisterViewModel;

        

        public UploadProgramRegisterView()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            (sender as TextBox).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void PasswordBox_PasswordChanged(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            (sender as PasswordBox).GetBindingExpression(PasswordBox.PasswordProperty).UpdateSource();
        }
    }
}
