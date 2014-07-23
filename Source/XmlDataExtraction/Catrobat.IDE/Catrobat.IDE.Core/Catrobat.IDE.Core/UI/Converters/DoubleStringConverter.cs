using System;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.UI.Converters
{
    public class DoubleStringConverter : IPortableValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
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

        public object ConvertBack(object value, Type targetType, object parameter, string language)
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