using System;
using System.IO;
using Windows.UI.Xaml.Media.Imaging;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Store.Services
{
    public class ImageResizeServiceStore : IImageResizeService
    {
        public PortableImage ResizeImage(PortableImage image, int maxWidthHeight)
        {
            if(!(image.ImageSource is WriteableBitmap))
                throw new ArgumentException("image.ImageSource must be of type WriteableBitmap");

            var resizedImage = ResizeImage((WriteableBitmap) image.ImageSource, maxWidthHeight);
            var resizedPortableImage = new PortableImage(resizedImage);

            return resizedPortableImage;
        }

        public PortableImage ResizeImage(PortableImage image, int newWidth, int newHeight)
        {
            if (!(image.ImageSource is WriteableBitmap))
                throw new ArgumentException("image.ImageSource must be of type WriteableBitmap");

            var bitmap = ((WriteableBitmap) image.ImageSource).Clone();

            var resizedImage = bitmap.Resize(newWidth, newHeight, WriteableBitmapExtensions.Interpolation.Bilinear);

            var resizedPortableImage = new PortableImage(resizedImage);

            return resizedPortableImage;
        }

        private static WriteableBitmap ResizeImage(WriteableBitmap image, int maxWidthHeight)
        {
            int maxWidth = maxWidthHeight;
            int maxHeight = maxWidthHeight;
            int width, height;

            if (image.PixelWidth > image.PixelHeight)
            {
                width = maxWidth;
                height = (int)((image.PixelHeight / (double)image.PixelWidth) * maxWidth);
            }
            else
            {
                height = maxHeight;
                width = (int)((image.PixelWidth / (double)image.PixelHeight) * maxHeight);
            }

            return image.Resize(width, height, WriteableBitmapExtensions.Interpolation.Bilinear);
        }
    }
}
