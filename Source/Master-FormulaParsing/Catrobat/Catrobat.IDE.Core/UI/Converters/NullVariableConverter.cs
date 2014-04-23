using System;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.UI.Converters
{
    public class NullVariableConverter : IPortableValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
                return value;

            return new UserVariable {Name = AppResources.Editor_NoVariableSelected};
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Not Needed.
            return null;
        }
    }
}
