using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Editor.Sprites;

namespace Catrobat.IDE.Store.Views.Editor.Costumes
{
    public sealed partial class NewCostumeSourceSelectionView : UserControl
    {
        private readonly AddNewSpriteViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).AddNewSpriteViewModel;

        public NewCostumeSourceSelectionView()
        {
            this.InitializeComponent();
        }
    }
}
