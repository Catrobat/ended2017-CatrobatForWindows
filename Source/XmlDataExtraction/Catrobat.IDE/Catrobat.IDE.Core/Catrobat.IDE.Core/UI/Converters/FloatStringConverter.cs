using System;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.UI.Converters
{
    public class FloatStringConverter : IPortableValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return StringFormatHelper.ConvertFloat((float) value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
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