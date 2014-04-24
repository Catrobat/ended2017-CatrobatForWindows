using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Costumes;

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
