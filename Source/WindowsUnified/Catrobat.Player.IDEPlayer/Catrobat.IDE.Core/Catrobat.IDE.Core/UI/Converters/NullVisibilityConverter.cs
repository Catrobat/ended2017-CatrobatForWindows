using System;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.UI.Converters
{
    public class NullVisibilityConverter : IPortableValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value == null ? PortableVisibility.Collapsed : PortableVisibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // Not Needed.
            return null;
        }
    }
}
