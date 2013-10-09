using System;
using System.Globalization;
using System.Windows.Data;
using Catrobat.Core.Utilities.Helpers;
using Catrobat.Core.Services.Common;

namespace Catrobat.IDEWindowsPhone.Converters
{
    public class SecondStringMillisecondConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return StringFormatHelper.ConvertDouble(((int) value)/1000f);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return (int) Math.Round(StringFormatHelper.ParseDouble((string) value)*1000);
            }
            catch (Exception)
            {
                return parameter;
            }
        }
    }
}