using System.Windows.Media.Imaging;

namespace Catrobat.IDEWindowsPhone.Misc.Images
{
    public class ImageResizer
    {
        public static WriteableBitmap ResizeImage(WriteableBitmap image, int maxWidthHeight)
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
