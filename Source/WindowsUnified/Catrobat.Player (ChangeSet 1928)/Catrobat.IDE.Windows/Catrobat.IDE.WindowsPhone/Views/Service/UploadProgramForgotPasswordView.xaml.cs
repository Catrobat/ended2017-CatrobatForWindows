using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Service;
using Windows.UI.Xaml.Controls;

namespace Catrobat.IDE.WindowsPhone.Views.Service
{
    public partial class UploadProgramForgotPasswordView : ViewPageBase
    {
        private readonly UploadProgramForgotPasswordViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).UploadProgramForgotPasswordViewModel;

        

        public UploadProgramForgotPasswordView()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            (sender as TextBox).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
    }
}
