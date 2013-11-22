using System.ComponentModel;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Settings;
using Microsoft.Phone.Controls;

namespace Catrobat.IDE.Phone.Views.Settings
{
    public partial class SettingsView : PhoneApplicationPage
    {
        private readonly SettingsViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).SettingsViewModel;

        public SettingsView()
        {
            InitializeComponent();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            _viewModel.GoBackCommand.Execute(null);
        }
    }
}