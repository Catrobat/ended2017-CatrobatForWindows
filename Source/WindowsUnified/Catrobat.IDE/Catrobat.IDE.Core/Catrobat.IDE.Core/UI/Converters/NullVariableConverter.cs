using System;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.UI.Converters
{
    public class NullVariableConverter : IPortableValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
                return value;

            return new GlobalVariable {Name = AppResources.Editor_NoVariableSelected};
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // Not Needed.
            return null;
        }
    }
}
