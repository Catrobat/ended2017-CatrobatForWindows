using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Editor.Costumes;

namespace Catrobat.IDE.Store.Views.Editor.Sounds
{
    public sealed partial class NewSoundSourceSelectionView : UserControl
    {
        private readonly NewCostumeSourceSelectionViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).NewCostumeSourceSelectionViewModel;

        public NewSoundSourceSelectionView()
        {
            this.InitializeComponent();
        }
    }
}
