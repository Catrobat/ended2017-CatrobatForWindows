using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Looks;
using Catrobat.IDE.Core.ViewModels.Main;

namespace Catrobat.IDE.WindowsPhone.Views.Main
{
    public partial class ProgramExportView
    {
        private readonly ProgramExportViewModel _viewModel =
            ServiceLocator.ViewModelLocator.ProgramExportViewModel;


        public ProgramExportView()
        {
            InitializeComponent();
        }
    }
}