using System;
using System.Globalization;
using System.Windows.Data;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Phone.ViewModel;
using Catrobat.IDE.Phone.ViewModel.Settings;

namespace Catrobat.IDE.Phone.Converters.NativeConverters
{
    public class ActiveThemeBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var settingsViewModel = ((ViewModelLocator)ServiceLocator.ViewModelLocator).SettingsViewModel;

            var theme = value as Theme;

            if (theme != null && settingsViewModel.ThemeChooser.SelectedTheme == value)
                return Core.Services.ServiceLocator.SystemInformationService.AccentBrush;

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not Needed.
            return null;
        }
    }
}