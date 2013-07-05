using System;
using System.Globalization;
using System.Windows.Data;
using Catrobat.IDEWindowsPhone.Themes;
using Catrobat.IDEWindowsPhone.ViewModel.Settings;
using IDEWindowsPhone;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Converters
{
    public class ActiveThemeBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var settingsViewModel = ServiceLocator.Current.GetInstance<SettingsViewModel>();
            var theme = value as Theme;

            if (theme != null && settingsViewModel.ActiveTheme == value)
            {
                return App.Current.Resources["PhoneAccentBrush"];
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