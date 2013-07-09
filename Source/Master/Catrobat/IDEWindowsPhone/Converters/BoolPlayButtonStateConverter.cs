using System;
using System.Globalization;
using System.Windows.Data;
using Catrobat.IDEWindowsPhone.Controls.Buttons;

namespace Catrobat.IDEWindowsPhone.Converters
{
    public class BoolPlayButtonStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isPlaying = (bool)value;

            return isPlaying ? PlayPauseButtonState.Play : PlayPauseButtonState.Pause;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var playState = (PlayPauseButtonState)value;

            return playState == PlayPauseButtonState.Play;
        }
    }
}