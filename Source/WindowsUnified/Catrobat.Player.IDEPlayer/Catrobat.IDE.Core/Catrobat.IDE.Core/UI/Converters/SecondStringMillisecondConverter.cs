using System;
using System.Globalization;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.UI.Converters
{
    public class SecondStringMillisecondConverter : IPortableValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return StringFormatHelper.ConvertDouble(((int) value)/1000f);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
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