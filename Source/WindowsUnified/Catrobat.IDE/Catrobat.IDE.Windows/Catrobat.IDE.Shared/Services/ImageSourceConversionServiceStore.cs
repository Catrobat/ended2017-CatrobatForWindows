using System;
using System.IO;
using Windows.UI.Xaml.Media.Imaging;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.WindowsShared.Services
{
    public class ImageSourceConversionServiceStore : IImageSourceConversionService
    {
        public object ConvertToLocalImageSource(byte[] data, int width, int height)
        {
            throw new NotImplementedException();
            //if (data == null)
            //    return null;

            //try
            //{
            //    var bitmap = new WriteableBitmap(width, height);
            //    bitmap.FromByteArray(data);

            //    return bitmap;
            //}
            //catch
            //{
            //    return null;
            //}
        }

        public void ConvertToBytes(object inputImage, out byte[] outputData, out int outputWidth, out int outputHeight)
        {
            throw new NotImplementedException();
            //WriteableBitmap writableBitmap = null;

            //if (inputImage is BitmapImage)
            //{
            //    var image = (BitmapImage)inputImage;

            //    writableBitmap = new WriteableBitmap(image.PixelWidth, image.PixelHeight);
            //    throw new NotImplementedException();
            //}
            //else if (inputImage is WriteableBitmap)
            //{
            //    writableBitmap = (WriteableBitmap)inputImage;
            //}
            //else
            //{
            //    throw new ArgumentException("data must be of type WritableBitmap or BitmaoImage");
            //}

            //outputData = writableBitmap.ToByteArray();
            //outputWidth = writableBitmap.PixelWidth;
            //outputHeight = writableBitmap.PixelHeight;
        }


        public object ConvertFromEncodedStream(System.IO.MemoryStream encodedStream)
        {
            if (encodedStream == null)
                return null;

            var image = new BitmapImage();
            image.SetSource(encodedStream.AsRandomAccessStream());
            image.CreateOptions = BitmapCreateOptions.None;

            var writeableImage = new WriteableBitmap(image.PixelWidth, image.PixelHeight);
            image.SetSource(encodedStream.AsRandomAccessStream());
            return writeableImage;
        }
    }
}
