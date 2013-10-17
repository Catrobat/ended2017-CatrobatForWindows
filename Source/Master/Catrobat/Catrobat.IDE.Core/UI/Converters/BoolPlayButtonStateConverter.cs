using System;
using System.Globalization;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.UI.Converters
{
    public class BoolPlayButtonStateConverter : IPortableValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                return PlayPauseButtonState.Pause;

            var isPlaying = (bool)value;

            return isPlaying ? PlayPauseButtonState.Play : PlayPauseButtonState.Pause;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is PlayPauseButtonState))
                return false;

            var playState = (PlayPauseButtonState)value;

            return playState == PlayPauseButtonState.Play;
        }
    }
}