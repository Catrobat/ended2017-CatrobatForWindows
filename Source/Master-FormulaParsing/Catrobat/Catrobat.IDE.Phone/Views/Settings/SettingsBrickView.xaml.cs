using System.ComponentModel;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Settings;
using Microsoft.Phone.Controls;

namespace Catrobat.IDE.Phone.Views.Settings
{
    public partial class SettingsBrickView : PhoneApplicationPage
    {
        private readonly SettingsBrickViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).SettingsBrickViewModel;

        public SettingsBrickView()
        {
            InitializeComponent();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            _viewModel.GoBackCommand.Execute(null);
        }
    }
}