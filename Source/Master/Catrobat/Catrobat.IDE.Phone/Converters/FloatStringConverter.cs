using System;
using System.Globalization;
using System.Windows.Data;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Phone.Converters
{
    public class FloatStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return StringFormatHelper.ConvertFloat((float) value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return StringFormatHelper.ParseFloat((string) value);
            }
            catch (Exception)
            {
                return parameter;
            }
        }
    }
}