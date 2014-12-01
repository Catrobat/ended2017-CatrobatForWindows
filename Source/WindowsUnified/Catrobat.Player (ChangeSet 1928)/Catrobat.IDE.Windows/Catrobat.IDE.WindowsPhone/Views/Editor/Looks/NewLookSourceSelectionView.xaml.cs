using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Looks;

namespace Catrobat.IDE.WindowsPhone.Views.Editor.Looks
{
    public partial class NewLookSourceSelectionView
    {
        private readonly NewLookSourceSelectionViewModel _viewModel =
            ServiceLocator.ViewModelLocator.NewLookSourceSelectionViewModel;
        

        public NewLookSourceSelectionView()
        {
            InitializeComponent();
        }
    }
}