using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels.Service;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Catrobat.IDE.WindowsPhone.Views.Service
{
    public partial class UploadProgramView : ViewPageBase
    {
        private readonly UploadProgramViewModel _viewModel =
            ServiceLocator.ViewModelLocator.UploadProgramViewModel;

        public UploadProgramView()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            (sender as TextBox).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
    }
}
