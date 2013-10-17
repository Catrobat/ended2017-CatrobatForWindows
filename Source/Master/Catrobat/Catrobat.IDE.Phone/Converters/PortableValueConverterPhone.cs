using System;
using System.Reflection;
using System.Windows.Data;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Phone.Converters
{
    public class PortableValueConverterPhone<T> : IValueConverter
    {
        private readonly Core.UI.PortableUI.IPortableValueConverter _coreConverter;

        public PortableValueConverterPhone()
        {
            var converter = (T) Activator.CreateInstance<T>();

            if (!(converter is IPortableValueConverter))
                throw new Exception("Converter must be a subtype of IPortableValueConverter");

            _coreConverter = (IPortableValueConverter)converter;
        }

        public object Convert(object value, System.Type targetType, 
            object parameter, System.Globalization.CultureInfo culture)
        {
            var portableUIElement = _coreConverter.Convert(value, targetType, parameter, culture);

            return ServiceLocator.PortableUIElementConversionService.
                ConvertToNativeUIElement(portableUIElement);
        }

        public object ConvertBack(object value, System.Type targetType, 
            object parameter, System.Globalization.CultureInfo culture)
        {
            return _coreConverter.ConvertBack(value, targetType, parameter, culture);
        }
    }
}
