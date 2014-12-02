using System;
using System.Globalization;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.UI.Converters
{
    public class UnixTimeDateTimeConverter : IPortableValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);

            if (value == null || !(value is double))
                return origin;

            return origin.AddSeconds((double) value);   
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // Not Needed.
            return null;
        }
    }
}