using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels.Main;

namespace Catrobat.IDE.WindowsPhone.Views.Main
{
    public partial class ProgramDetailView
    {
        private readonly ProgramSettingsViewModel _viewModel =
            ServiceLocator.ViewModelLocator.ProgramSettingsViewModel;

        public ProgramDetailView()
        {
            InitializeComponent();
        }
    }
}