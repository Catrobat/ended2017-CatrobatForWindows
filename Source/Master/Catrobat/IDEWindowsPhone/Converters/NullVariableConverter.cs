using System;
using System.Windows.Data;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDEWindowsPhone.Content.Localization;

namespace Catrobat.IDEWindowsPhone.Converters
{
    public class NullVariableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
                return value;

            return new UserVariable {Name = AppResources.Editor_NoVariableSelected};

            return value == null ? 0 : 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Not Needed.
            return null;
        }
    }
}
