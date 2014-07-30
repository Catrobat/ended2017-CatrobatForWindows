using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Main;

namespace Catrobat.IDE.WindowsPhone.Views.Main
{
    public partial class ProgramImportView
    {
        private readonly ProgramImportViewModel _viewModel = 
            ServiceLocator.ViewModelLocator.ProgramImportViewModel;

        public ProgramImportView()
        {
            InitializeComponent();
        }
    }
}