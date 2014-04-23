using System;
using System.Globalization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.ViewModel;

namespace Catrobat.IDE.Core.UI.Converters
{
    public class ActiveThemeBrushConverter : IPortableValueConverter
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