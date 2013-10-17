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
            var portableConvertedUIElement = _coreConverter.Convert(value, targetType, parameter, culture);

            var nativeUIElement = ServiceLocator.PortableUIElementConversionService.
                ConvertToNativeUIElement(portableConvertedUIElement);

            return nativeUIElement;
        }

        public object ConvertBack(object value, System.Type targetType, 
            object parameter, System.Globalization.CultureInfo culture)
        {
            var portableValue = ServiceLocator.PortableUIElementConversionService.
                ConvertToPortableUIElement(value);

            var portableParameter = ServiceLocator.PortableUIElementConversionService.
                ConvertToPortableUIElement(parameter);

            var portableConvertedValue = _coreConverter.ConvertBack(portableValue, targetType, portableParameter, culture);

            return portableConvertedValue;
        }
    }
}
