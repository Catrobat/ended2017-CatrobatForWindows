using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Catrobat.Core.CatrobatObjects.Variables;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Formula;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Converters
{
    public class BoolToSelectedPivotItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = (bool) value;
            var invert = false;

            if (parameter is bool)
                invert = (bool) parameter;

            if (invert)
                boolValue = ! boolValue;

            return boolValue ? 0 : 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var intValue = (int)value;
            var invert = false;

            if (parameter is bool)
                invert = (bool)parameter;

            var boolValue = intValue == 0;

            if (invert)
                boolValue = !boolValue;

            return boolValue;
        }
    }
}