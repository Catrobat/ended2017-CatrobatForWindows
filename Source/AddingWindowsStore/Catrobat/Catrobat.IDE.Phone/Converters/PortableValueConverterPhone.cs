using System;
using System.Windows.Data;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Phone.Services;

namespace Catrobat.IDE.Phone.Converters
{
    public class PortableValueConverterPhone<T> : IValueConverter
    {
        private readonly IPortableValueConverter _coreConverter;

        public PortableValueConverterPhone()
        {
            var converter = Activator.CreateInstance<T>();

            if (!(converter is IPortableValueConverter))
                throw new Exception("Converter must be a subtype of IPortableValueConverter");

            _coreConverter = (IPortableValueConverter)converter;
        }

        public object Convert(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            var portableConvertedUIElement = _coreConverter.Convert(value, targetType, parameter, culture);

            var converter = new PortableUIElementsConvertionServicePhone();
            var nativeUIElement = converter.ConvertToNativeUIElement(portableConvertedUIElement);
            return nativeUIElement;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            var converter = new PortableUIElementsConvertionServicePhone();

            var portableValue = converter.ConvertToPortableUIElement(value);

            var portableParameter = converter.ConvertToPortableUIElement(parameter);

            var portableConvertedValue = _coreConverter.ConvertBack(portableValue, targetType, portableParameter, culture);

            return portableConvertedValue;
        }
    }
}
