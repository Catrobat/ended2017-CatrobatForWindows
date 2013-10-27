using System;
using Windows.UI.Xaml.Media.Imaging;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.Store.Services
{
    public class ImageSourceConversionServiceStore : IImageSourceConversionService
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
                var image = (BitmapImage) inputImage;
                
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
            outputHeight = writableBitmap.PixelWidth;
        }


        public object ConvertFromEncodedStreeam(System.IO.MemoryStream encodedStream)
        {
            throw new NotImplementedException();
        }
    }
}
