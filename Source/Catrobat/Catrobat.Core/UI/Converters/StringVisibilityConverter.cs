using System;
using System.Globalization;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.UI.Converters
{
    public class StringVisibilityConverter : IPortableValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null || !(value is string))
                return PortableVisibility.Collapsed;

            var visible = (string) value;

            return visible == "" ? PortableVisibility.Visible : PortableVisibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // Not Needed.
            return null;
        }
    }
}