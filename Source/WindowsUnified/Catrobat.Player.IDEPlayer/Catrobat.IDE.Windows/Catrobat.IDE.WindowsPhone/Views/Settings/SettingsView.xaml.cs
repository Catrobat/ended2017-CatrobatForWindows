using Windows.UI.Xaml.Input;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Settings;

namespace Catrobat.IDE.WindowsPhone.Views.Settings
{
    public partial class SettingsView
    {
        private readonly SettingsViewModel _viewModel = 
            ServiceLocator.ViewModelLocator.SettingsViewModel;

        public SettingsView()
        {
            InitializeComponent();
        }

        private void SettingsLanguage_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            if (_viewModel.ShowLanguageSettingsCommand.CanExecute(null))
            _viewModel.ShowLanguageSettingsCommand.Execute(null);
        }
    }
}