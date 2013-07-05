using System.Windows;
using Catrobat.IDEWindowsPhone.Themes;
using Catrobat.IDEWindowsPhone.ViewModel.Settings;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace Catrobat.IDEWindowsPhone.Views.Settings
{
    public partial class SettingsThemeView : PhoneApplicationPage
    {
        private readonly SettingsViewModel _settingsViewModel = ServiceLocator.Current.GetInstance<SettingsViewModel>();

        public SettingsThemeView()
        {
            InitializeComponent();
        }

        private void Item_Tap(object sender, GestureEventArgs e)
        {
            var frameworkElement = sender as FrameworkElement;
            if (frameworkElement != null)
            {
                var theme = frameworkElement.DataContext as Theme;
                _settingsViewModel.ActiveThemeChangedCommand.Execute(theme);
            }
        }
    }
}