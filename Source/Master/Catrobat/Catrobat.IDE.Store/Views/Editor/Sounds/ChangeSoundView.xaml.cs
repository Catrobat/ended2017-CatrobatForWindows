using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Editor.Costumes;

namespace Catrobat.IDE.Store.Views.Editor.Sounds
{
    public sealed partial class ChangeSoundView : UserControl
    {
        private readonly ChangeCostumeViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).ChangeCostumeViewModel;

        public ChangeSoundView()
        {
            this.InitializeComponent();
        }

        private void TextBoxCostumeName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.CostumeName = TextBoxCostumeName.Text;
        }
    }
}
