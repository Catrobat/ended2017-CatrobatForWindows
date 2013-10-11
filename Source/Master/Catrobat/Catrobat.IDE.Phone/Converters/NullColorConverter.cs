using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Catrobat.IDE.Phone.Converters
{
    public class NullColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return Application.Current.Resources["PhoneAccentBrush"];
            }
            else
            {
                return Application.Current.Resources["PhoneTextBoxForegroundBrush"];
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not Needed.
            return null;
        }
    }
}