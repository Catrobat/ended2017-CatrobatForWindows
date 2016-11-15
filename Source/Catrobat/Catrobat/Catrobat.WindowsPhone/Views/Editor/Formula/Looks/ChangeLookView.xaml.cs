using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Looks;
using Windows.UI.Xaml.Controls;

namespace Catrobat.IDE.WindowsPhone.Views.Editor.Looks
{
    public partial class ChangeLookView
    {
        private readonly ChangeLookViewModel _viewModel = 
            ServiceLocator.ViewModelLocator.ChangeLookViewModel;
        

        public ChangeLookView()
        {
            InitializeComponent();
        }

        private void TextBoxLookName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.LookName = TextBoxLookName.Text;
        }
    }
}