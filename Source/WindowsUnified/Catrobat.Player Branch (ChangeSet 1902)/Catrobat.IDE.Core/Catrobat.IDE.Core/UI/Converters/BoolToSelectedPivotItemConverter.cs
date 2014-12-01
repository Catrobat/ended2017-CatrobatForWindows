using System;
using System.Globalization;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.UI.Converters
{
    public class BoolToSelectedPivotItemConverter : IPortableValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var boolValue = (bool) value;
            var invert = false;

            if (parameter is bool)
                invert = (bool) parameter;

            if (invert)
                boolValue = ! boolValue;

            return boolValue ? 0 : 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var intValue = (int)value;
            var invert = false;

            if (parameter is bool)
                invert = (bool)parameter;

            var boolValue = intValue == 0;

            if (invert)
                boolValue = !boolValue;

            return boolValue;
        }
    }
}