using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Main;

namespace Catrobat.IDE.WindowsPhone.Views.Main
{
    public partial class ProjectImportView
    {
        private readonly ProjectImportViewModel _viewModel = 
            ServiceLocator.ViewModelLocator.ProjectImportViewModel;

        

        public ProjectImportView()
        {
            InitializeComponent();
        }
    }
}