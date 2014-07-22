using System;

namespace Catrobat.IDE.Core.UI.PortableUI
{
    public interface IPortableValueConverter
    {
        object Convert(object value, Type targetType, object parameter, string language);

        object ConvertBack(object value, Type targetType, object parameter, string language);
    }
}
