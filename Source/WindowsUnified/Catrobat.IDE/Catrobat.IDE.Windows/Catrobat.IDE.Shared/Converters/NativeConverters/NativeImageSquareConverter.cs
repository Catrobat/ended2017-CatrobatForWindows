using System;
using Windows.Foundation;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Catrobat.IDE.WindowsShared.Converters.NativeConverters
{
    public class NativeImageSquareConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is WriteableBitmap))
                return null;

            try
            {
                var writeableBitmap = (WriteableBitmap)value;

                var croppedScreenshot = (ImageSource)writeableBitmap.Crop(new Rect(
                new Point(0, (writeableBitmap.PixelHeight - writeableBitmap.PixelWidth) / 2.0),
                new Size(writeableBitmap.PixelWidth, writeableBitmap.PixelWidth)));

                var noScreenshotConverter = new NoScreenshotConverterBootstrap();

                croppedScreenshot = (BitmapSource)noScreenshotConverter.Convert(croppedScreenshot, null, null, "");

                return croppedScreenshot;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // not used
            return null;
        }
    }
}
