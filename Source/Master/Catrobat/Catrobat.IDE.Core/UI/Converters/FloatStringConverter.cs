using System;
using System.Globalization;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.UI.Converters
{
    public class FloatStringConverter : IPortableValueConverter
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