using System;
using Windows.UI.Xaml.Data;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.WindowsShared.Services;

namespace Catrobat.IDE.WindowsShared.Converters
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

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var portableConvertedUIElement = _coreConverter.Convert(value, targetType, parameter, language);

            var converter = new PortableUIElementsConvertionServiceStore();
            var nativeUIElement = converter.ConvertToNativeUIElement(portableConvertedUIElement);
            return nativeUIElement;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var converter = new PortableUIElementsConvertionServiceStore();

            var portableValue = converter.ConvertToPortableUIElement(value);

            var portableParameter = converter.ConvertToPortableUIElement(parameter);

            var portableConvertedValue = _coreConverter.ConvertBack(portableValue, targetType, portableParameter, language);

            return portableConvertedValue;
        }
    }
}
