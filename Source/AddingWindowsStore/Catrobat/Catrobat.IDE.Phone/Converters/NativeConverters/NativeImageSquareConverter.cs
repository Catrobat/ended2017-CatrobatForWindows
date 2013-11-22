using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Catrobat.IDE.Phone.Converters.NativeConverters
{
    public class NativeImageSquareConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is BitmapSource))
                return null;

            try
            {
                var image = (BitmapImage)value;
                var writeableBitmap = new WriteableBitmap(image);

                var croppedScreenshot = (ImageSource)writeableBitmap.Crop(new Rect(
                new Point(0, (writeableBitmap.PixelHeight - writeableBitmap.PixelWidth) / 2.0),
                new Size(writeableBitmap.PixelWidth, writeableBitmap.PixelWidth)));

                var noScreenshotConverter = new NoScreenshotConverterBootstrap();

                croppedScreenshot = (BitmapSource)noScreenshotConverter.Convert(croppedScreenshot, null, null, null);

                return croppedScreenshot;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // not used
            return null;
        }
    }
}
