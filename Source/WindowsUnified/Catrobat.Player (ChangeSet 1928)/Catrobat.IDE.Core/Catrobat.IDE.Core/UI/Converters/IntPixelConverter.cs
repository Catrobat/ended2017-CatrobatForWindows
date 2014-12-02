using System;
using System.Globalization;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.UI.Converters
{
    public class IntPixelConverter : IPortableValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                value = "";

            return value.ToString() + "px";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // Not Needed.
            return null;
        }
    }
}