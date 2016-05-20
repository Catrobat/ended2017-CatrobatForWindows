using System;
using System.Globalization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;
namespace Catrobat.IDE.Core.UI.Converters
{
    public class ActiveThemeBrushConverter : IPortableValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var settingsViewModel = ServiceLocator.ViewModelLocator.SettingsViewModel;

            var theme = value as Theme;

            if (theme != null && settingsViewModel.ThemeChooser.SelectedTheme == value)
                return ServiceLocator.SystemInformationService.AccentBrush;

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // Not Needed.
            return null;
        }
    }
}