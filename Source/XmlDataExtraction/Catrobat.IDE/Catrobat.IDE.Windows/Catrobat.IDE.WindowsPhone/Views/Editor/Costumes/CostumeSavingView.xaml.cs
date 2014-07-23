using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Costumes;

namespace Catrobat.IDE.WindowsPhone.Views.Editor.Costumes
{
    public partial class CostumeSavingView
    {
        private readonly CostumeSavingViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).CostumeSavingViewModel;
        

        public CostumeSavingView()
        {
            InitializeComponent();
        }
    }
}