using System;
using System.Windows.Data;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.Resources.Localization;

namespace Catrobat.IDE.Phone.Converters
{
    public class NullVariableConverter : IValueConverter
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
