using System;
using System.Globalization;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.UI.Converters
{
    public class BoolVisibilityConverter : IPortableValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is bool))
                return PortableVisibility.Collapsed;

            var visible = (bool) value;

            if (parameter != null && (bool) parameter)
            {
                visible = !(bool) value;
            }

            return visible ? PortableVisibility.Visible : PortableVisibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not Needed.
            return null;
        }
    }
}