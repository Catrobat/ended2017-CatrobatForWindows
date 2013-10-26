using System;
using System.IO;
using System.Windows.Media.Imaging;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.Phone.Services
{
    public class ImageSourceConversionServicePhone : IImageSourceConversionService
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

        public void ConvertToBytes(object inputData, out byte[] outputData, out int outputWidth, out int outputHeight)
        {
            WriteableBitmap writableBitmap = null;

            if (inputData is BitmapImage)
            {
                writableBitmap = new WriteableBitmap((BitmapImage)inputData);
            }
            else if (inputData is WriteableBitmap)
            {
                writableBitmap = (WriteableBitmap)inputData;
            }
            else
            {
                throw new ArgumentException("data must be of type WritableBitmap or BitmaoImage");
            }

            outputData = writableBitmap.ToByteArray();
            outputWidth = writableBitmap.PixelWidth;
            outputHeight = writableBitmap.PixelWidth;
        }

        public object ConvertFromEncodedStreeam(MemoryStream encodedStream)
        {
            var image = new BitmapImage {CreateOptions = BitmapCreateOptions.None};

            if(encodedStream != null)
                image.SetSource(encodedStream);

            return image;
        }
    }
}
