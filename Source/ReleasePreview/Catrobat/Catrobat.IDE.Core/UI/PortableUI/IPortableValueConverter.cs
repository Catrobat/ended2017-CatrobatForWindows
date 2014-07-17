using System;
using System.Globalization;

namespace Catrobat.IDE.Core.UI.PortableUI
{
    public interface IPortableValueConverter
    {
        object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
    }
}
