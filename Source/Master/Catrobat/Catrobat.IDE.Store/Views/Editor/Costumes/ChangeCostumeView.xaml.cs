using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Editor.Costumes;

namespace Catrobat.IDE.Store.Views.Editor.Costumes
{
    public sealed partial class ChangeCostumeView : UserControl
    {
        private readonly ChangeCostumeViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).ChangeCostumeViewModel;

        public ChangeCostumeView()
        {
            this.InitializeComponent();
        }

        private void TextBoxCostumeName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.CostumeName = TextBoxCostumeName.Text;
        }
    }
}
