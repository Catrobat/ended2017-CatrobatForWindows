using System.ComponentModel;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Settings;
using Microsoft.Phone.Controls;

namespace Catrobat.IDE.Phone.Views.Settings
{
    public partial class SettingsLanguageView : PhoneApplicationPage
    {
        private readonly SettingsLanguageViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).SettingsLanguageViewModel;

        public SettingsLanguageView()
        {
            InitializeComponent();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            _viewModel.GoBackCommand.Execute(null);
        }
    }
}