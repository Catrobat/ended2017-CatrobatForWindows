using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.WindowsShared.Services
{
    public class ImageResizeServiceWindowsShared : IImageResizeService
    {
        public async Task<PortableImage> ResizeImage(PortableImage image, int maxWidthHeight)
        {
            var bitmapImage = (BitmapSource)image.ImageSource;

            int maxWidth = maxWidthHeight;
            int maxHeight = maxWidthHeight;
            int width, height;

            if (bitmapImage.PixelWidth > bitmapImage.PixelHeight)
            {
                width = maxWidth;
                height = (int)((bitmapImage.PixelHeight / (double)bitmapImage.PixelWidth) * maxWidth);
            }
            else
            {
                height = maxHeight;
                width = (int)((bitmapImage.PixelWidth / (double)bitmapImage.PixelHeight) * maxHeight);
            }

            return await ResizeImage(image, width, height);


            //if(!(image.ImageSource is WriteableBitmap))
            //    throw new ArgumentException("image.ImageSource must be of type WriteableBitmap");

            //var resizedImage = ResizeImage((WriteableBitmap) image.ImageSource, maxWidthHeight);
            //var resizedPortableImage = new PortableImage(resizedImage);

            //return resizedPortableImage;
        }

        public async Task<PortableImage> ResizeImage(PortableImage image, int newWidth, int newHeight)
        {
            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(((Stream)image.EncodedData).AsRandomAccessStream());

            var memoryRandomAccessStream = new InMemoryRandomAccessStream();
            BitmapEncoder encoder = await BitmapEncoder.CreateForTranscodingAsync(memoryRandomAccessStream, decoder);

            encoder.BitmapTransform.ScaledHeight = (uint)newHeight;
            encoder.BitmapTransform.ScaledWidth = (uint)newWidth;


            //var bounds = new BitmapBounds();
            //bounds.Height = 50;
            //bounds.Width = 50;
            //bounds.X = 50;
            //bounds.Y = 50;
            //enc.BitmapTransform.Bounds = bounds;

            try
            {
                await encoder.FlushAsync();
            }
            catch (Exception exc)
            {
                var message = "Error on resizing the image: ";

                if (exc.Message != null)
                    message += exc.Message;

                throw new Exception(message);
            }

            var writeableBitmap = new WriteableBitmap(newWidth, newHeight);
            writeableBitmap.SetSourceAsync(memoryRandomAccessStream);
            memoryRandomAccessStream.Seek(0);

            return new PortableImage(writeableBitmap)
            {
                Width = newWidth,
                Height = newHeight,
                EncodedData = memoryRandomAccessStream.AsStream()
            };
        }
    }
}
