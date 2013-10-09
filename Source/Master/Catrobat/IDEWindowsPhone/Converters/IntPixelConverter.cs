using System;
using System.Globalization;
using System.Windows.Data;

namespace Catrobat.IDEWindowsPhone.Converters
{
    public class IntPixelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                value = "";

            return value.ToString() + "px";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not Needed.
            return null;
        }
    }
}