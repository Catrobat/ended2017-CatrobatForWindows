using System;
using System.Globalization;
using System.Windows.Data;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Phone.Converters
{
    public class IntStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return StringFormatHelper.ConvertInt((int) value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return StringFormatHelper.ParseInt((string) value);
            }
            catch (Exception)
            {
                return parameter;
            }
        }
    }
}