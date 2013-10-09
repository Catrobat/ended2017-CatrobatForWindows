using System;
using System.Globalization;
using System.Windows.Data;
using Catrobat.Core.Utilities.Helpers;
using Catrobat.Core.Services.Common;

namespace Catrobat.IDEWindowsPhone.Converters
{
    public class DoubleStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null)
            {
                return ((double) value).ToString(parameter as String);
            }
            else
            {
                return StringFormatHelper.ConvertDouble((double) value);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return StringFormatHelper.ParseDouble((string) value);
            }
            catch (Exception)
            {
                return parameter;
            }
        }
    }
}