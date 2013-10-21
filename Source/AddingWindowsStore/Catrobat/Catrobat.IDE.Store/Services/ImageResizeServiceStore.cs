using System;
using Windows.UI.Xaml.Media.Imaging;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Store.Services
{
    public class ImageResizeServicePhone : IImageResizeService
    {
        public PortableImage ResizeImage(PortableImage image, int maxWidthHeight)
        {
            throw new NotImplementedException();

            //var bitmap = new WriteableBitmap(image.Width, image.Height);
            //bitmap.FromByteArray(image.Data);

            //var resizedImage = ResizeImage(bitmap, maxWidthHeight);
            //var resizedPortableImage = new PortableImage(resizedImage.ToByteArray(), resizedImage.PixelWidth, resizedImage.PixelHeight);

            //return resizedPortableImage;
        }

        public PortableImage ResizeImage(PortableImage image, int newWidth, int newHeight)
        {
            throw new NotImplementedException();

            //var bitmap = new WriteableBitmap(image.Width, image.Height);
            //bitmap.FromByteArray(image.Data);

            //var resizedImage = bitmap.Resize(newWidth, newHeight, WriteableBitmapExtensions.Interpolation.Bilinear);

            //var resizedPortableImage = new PortableImage(resizedImage.ToByteArray(), resizedImage.PixelWidth, resizedImage.PixelHeight);

            //return resizedPortableImage;
        }

        private static WriteableBitmap ResizeImage(WriteableBitmap image, int maxWidthHeight)
        {
            throw new NotImplementedException();

            //int maxWidth = maxWidthHeight;
            //int maxHeight = maxWidthHeight;
            //int width, height;

            //if (image.PixelWidth > image.PixelHeight)
            //{
            //    width = maxWidth;
            //    height = (int)((image.PixelHeight / (double)image.PixelWidth) * maxWidth);
            //}
            //else
            //{
            //    height = maxHeight;
            //    width = (int)((image.PixelWidth / (double)image.PixelHeight) * maxHeight);
            //}

            //return image.Resize(width, height, WriteableBitmapExtensions.Interpolation.Bilinear);
        }
    }
}
