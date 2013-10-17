using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Phone.ViewModel.Settings;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDE.Phone.Converters.NativeConverters
{
    public class ActiveThemeBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var settingsViewModel = ServiceLocator.Current.GetInstance<SettingsViewModel>();
            var theme = value as Theme;

            if (theme != null && settingsViewModel.ThemeChooser.SelectedTheme == value)
            {
                return Application.Current.Resources["PhoneAccentBrush"];
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not Needed.
            return null;
        }
    }
}