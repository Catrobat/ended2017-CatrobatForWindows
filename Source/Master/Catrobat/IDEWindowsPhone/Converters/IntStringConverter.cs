using System;
using System.Globalization;
using System.Windows.Data;
using Catrobat.Core.Utilities.Helpers;
using Catrobat.Core.Services.Common;

namespace Catrobat.IDEWindowsPhone.Converters
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