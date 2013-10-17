using System;
using System.Reflection;
using System.Windows.Data;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Phone.Converters
{
    public class NativeConverterPhone<T> : IValueConverter
    {
        private readonly Core.UI.Converters.IValueConverter _coreConverter;

        public NativeConverterPhone()
        {
            var coreConverters = ReflectionHelper.GetInstances<T>();

            if (coreConverters.Count < 1)
                throw new Exception("No instances of where found");

            if(coreConverters.Count > 1)
                throw new Exception("More than one instances where found");

            _coreConverter = (Core.UI.Converters.IValueConverter) coreConverters[0];
        }


        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return _coreConverter.Convert(value, targetType, parameter, culture);
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return _coreConverter.ConvertBack(value, targetType, parameter, culture);
        }
    }
}
