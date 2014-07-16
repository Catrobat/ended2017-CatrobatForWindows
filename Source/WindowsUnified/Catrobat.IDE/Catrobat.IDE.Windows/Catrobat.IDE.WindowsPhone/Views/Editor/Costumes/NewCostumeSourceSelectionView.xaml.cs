using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Costumes;

namespace Catrobat.IDE.WindowsPhone.Views.Editor.Costumes
{
    public partial class NewCostumeSourceSelectionView
    {
        private readonly NewCostumeSourceSelectionViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).NewCostumeSourceSelectionViewModel;
        

        public NewCostumeSourceSelectionView()
        {
            InitializeComponent();
        }
    }
}