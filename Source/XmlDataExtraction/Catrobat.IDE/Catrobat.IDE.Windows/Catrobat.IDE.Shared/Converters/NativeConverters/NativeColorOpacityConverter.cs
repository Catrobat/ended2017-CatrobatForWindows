using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Catrobat.IDE.WindowsShared.Converters.NativeConverters
{
    public class NativeColorOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is SolidColorBrush))
                return null;

            var color = ((SolidColorBrush) value).Color;

            var opacityOverride = (double) double.Parse((string) parameter);
            if (opacityOverride > 1.0)
                opacityOverride = 1.0;
            if (opacityOverride < 0.0)
                opacityOverride = 0.0;

            var newA = (byte) (opacityOverride * 255);

            var newBrush = new SolidColorBrush(new Color {R = color.R, G = color.G, B = color.B, A = newA});
            return newBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // not used
            return null;
        }
    }
}
