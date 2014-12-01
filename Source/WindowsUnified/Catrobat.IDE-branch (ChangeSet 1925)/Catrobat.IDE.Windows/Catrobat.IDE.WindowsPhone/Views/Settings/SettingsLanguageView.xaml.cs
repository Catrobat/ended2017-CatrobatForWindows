using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Settings;

namespace Catrobat.IDE.WindowsPhone.Views.Settings
{
    public partial class SettingsLanguageView
    {
        private readonly SettingsLanguageViewModel _viewModel =
            ServiceLocator.ViewModelLocator.SettingsLanguageViewModel;

        public SettingsLanguageView()
        {
            InitializeComponent();
            PageCacheMode = NavigationCacheMode.Disabled;
        }

        private void Culture_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            _viewModel.SelectCultureCommand.Execute(((FrameworkElement)sender).DataContext);
        }
    }
}