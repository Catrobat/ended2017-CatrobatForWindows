using System;
using System.Globalization;
using System.Windows.Data;

namespace Catrobat.IDEWindowsPhone.Converters
{
    public class BoolPlayButtonStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //var isPlaying = (bool)value;

            //return isPlaying ? PlayButtonState.Play : PlayButtonState.Pause;

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not Needed.
            return null;
        }
    }
}