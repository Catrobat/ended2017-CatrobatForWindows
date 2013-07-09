using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Catrobat.IDEWindowsPhone.Converters
{
    public class BoolVisibilityNegativeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is bool))
                return Visibility.Collapsed;

            var visible = !(bool) value;

            return visible ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not Needed.
            return null;
        }
    }
}