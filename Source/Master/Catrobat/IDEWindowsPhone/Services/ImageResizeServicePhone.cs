using System.Windows.Media.Imaging;
using Catrobat.Core.Services;

namespace Catrobat.IDEWindowsPhone.Services
{
    public class ImageResizeServicePhone : IImageResizeService
    {
        public byte[] ResizeImage(byte[] image, int actualPixelWidth, int actualPixelHeight, int maxWidthHeight)
        {

            var bitmap = new WriteableBitmap(actualPixelWidth, actualPixelHeight);
            bitmap.FromByteArray(image);

            var resizedImage = ResizeImage(bitmap, maxWidthHeight);
            return resizedImage.ToByteArray();
        }

        internal static WriteableBitmap ResizeImage(WriteableBitmap image, int maxWidthHeight)
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
