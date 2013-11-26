using System;
using System.Collections;
using Windows.Foundation;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Catrobat.IDE.Store.Converters.NativeConverters
{
    public class NativeScriptSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is IList))
                return null;

            try
            {
                var scripts = (IList)value;

                return Math.Round(scripts.Count / 3.0, MidpointRounding.AwayFromZero);

                //var croppedScreenshot = (ImageSource)writeableBitmap.Crop(new Rect(
                //new Point(0, (writeableBitmap.PixelHeight - writeableBitmap.PixelWidth) / 2.0),
                //new Size(writeableBitmap.PixelWidth, writeableBitmap.PixelWidth)));

                //var noScreenshotConverter = new NoScreenshotConverterBootstrap();

                //croppedScreenshot = (BitmapSource)noScreenshotConverter.Convert(croppedScreenshot, null, null, "");

                //return croppedScreenshot;
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
