using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Settings;

namespace Catrobat.IDE.WindowsPhone.Views.Settings
{
    public partial class SettingsView
    {
        private readonly SettingsViewModel _viewModel = 
            ServiceLocator.ViewModelLocator.SettingsViewModel;

        protected override ViewModelBase GetViewModel() { return _viewModel; }

        public SettingsView()
        {
            InitializeComponent();
        }
    }
}