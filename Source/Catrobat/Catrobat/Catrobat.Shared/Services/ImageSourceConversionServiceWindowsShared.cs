using System;
using System.IO;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.WindowsShared.Services
{
    public class ImageSourceConversionServiceWindowsShared : IImageSourceConversionService
    {
        public object ConvertToLocalImageSource(byte[] data, int width, int height)
        {
            if (data == null)
                return null;

            try
            {
                var bitmap = new WriteableBitmap(width, height);
                bitmap.FromByteArray(data);

                return bitmap;
            }
            catch
            {
                return null;
            }
        }

        public void ConvertToBytes(object inputImage, out byte[] outputData, out int outputWidth, out int outputHeight)
        {
            WriteableBitmap writableBitmap = null;

            if (inputImage is BitmapImage)
            {
                var image = (BitmapImage)inputImage;

                writableBitmap = new WriteableBitmap(image.PixelWidth, image.PixelHeight);
                throw new NotImplementedException();
            }
            else if (inputImage is WriteableBitmap)
            {
                writableBitmap = (WriteableBitmap)inputImage;
            }
            else
            {
                throw new ArgumentException("data must be of type WritableBitmap or BitmaoImage");
            }

            outputData = writableBitmap.ToByteArray();
            outputWidth = writableBitmap.PixelWidth;
            outputHeight = writableBitmap.PixelHeight;
        }


        public async Task<object> ConvertFromEncodedStream(Stream encodedStream, int width, int height)
        {
            if (encodedStream == null)
                return null;

            var image = new WriteableBitmap(width, height);
            encodedStream.Seek(0, SeekOrigin.Begin);
            await image.SetSourceAsync(encodedStream.AsRandomAccessStream());

            return image;

            //var writeableImage = new WriteableBitmap(image.PixelWidth, image.PixelHeight);
            //image.SetSource(encodedStream.AsRandomAccessStream());
            //return writeableImage;
        }
    }
}
