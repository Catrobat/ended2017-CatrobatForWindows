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
    public class ImageResizeServiceStore : IImageResizeService
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
            return image; // TODO: fix resize code and reove this


            //// hard coded image location
            //string filePath = "C:\\Users\\Public\\Pictures\\Sample Pictures\\fantasy-dragons-wallpaper.jpg";

            //StorageFile file = await StorageFile.GetFileFromPathAsync(filePath);
            //if (file == null)
            //    return;

            //// create a stream from the file and decode the image
            //var fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
            //BitmapDecoder decoder = await BitmapDecoder.CreateAsync(fileStream);

            if(image.EncodedData == null)
                throw new ArgumentException("Property 'EncodedData' of parameter 'image' must not be null!");

            var decoder = await BitmapDecoder.CreateAsync(image.EncodedData.AsRandomAccessStream());


            // create a new stream and encoder for the new image
            var ras = new InMemoryRandomAccessStream();
            BitmapEncoder enc = await BitmapEncoder.CreateForTranscodingAsync(ras, decoder);

            // convert the entire bitmap to a 100px by 100px bitmap
            enc.BitmapTransform.ScaledHeight = (uint) newWidth;
            enc.BitmapTransform.ScaledWidth = (uint) newHeight;


            //var bounds = new BitmapBounds
            //{
            //    Height = 50, Width = 50, X = 50, Y = 50
            //};

            //enc.BitmapTransform.Bounds = bounds;

            // write out to the stream
            try
            {
                await enc.FlushAsync();
            }
            catch (Exception ex)
            {
                string s = ex.ToString();
            }

            // render the stream to the screen
            //var bitmap = new BitmapImage();
            //bitmap.SetSource(ras);

            var memoryStream = new MemoryStream();
            ras.AsStream().CopyTo(memoryStream);


            return new PortableImage()
            {
                EncodedData = memoryStream,
                //Width = bitmap.PixelWidth,
                //Height = bitmap.PixelHeight
            };



            //if (!(image.ImageSource is WriteableBitmap))
            //    throw new ArgumentException("image.ImageSource must be of type WriteableBitmap");

            //var bitmap = ((WriteableBitmap) image.ImageSource).Clone();
            //var resizedImage = bitmap.Resize(newWidth, newHeight, WriteableBitmapExtensions.Interpolation.Bilinear);

            //var resizedPortableImage = new PortableImage(resizedImage);

            //return resizedPortableImage;
        }

        //private async Task<PortableImage> ResizeImage(PortableImage image, int maxWidthHeight)
        //{

        //    //return image.Resize(width, height, WriteableBitmapExtensions.Interpolation.Bilinear);
        //}
    }
}
