using System;
using System.Collections;
using System.Linq;
using System.Windows.Data;
using System.Windows;
using Catrobat.Core;
using Catrobat.Core.CatrobatObjects;
using System.Collections.Generic;

namespace Catrobat.IDEWindowsPhone.Converters
{
    public class NullIntCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value == null ? 0 : 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Not Needed.
            return null;
        }
    }
}
