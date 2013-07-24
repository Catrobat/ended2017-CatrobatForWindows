using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Catrobat.IDEWindowsPhone.Converters
{
    public class BoolFormulaBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var selectedBackground = Application.Current.Resources["FormulaSelectedBackgrount"] as Brush;
            var notSelectedBackground = Application.Current.Resources["FormulaNotSelectedBackgrount"] as Brush;

            if (value == null || !(value is bool))
                return notSelectedBackground;

            var selected = (bool) value;

            return selected ? selectedBackground : notSelectedBackground;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not Needed.
            return null;
        }
    }
}