using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ImageTools;
using ImageTools.Filtering;

namespace Catrobat.IDEWindowsPhone.Misc
{
    public class ImageResizer
    {
        public static ExtendedImage CreateThumbnailImage(ExtendedImage image, int maxWidth, int maxHeight)
        {
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

            var resizedImage = ExtendedImage.Resize(image, width, height, new NearestNeighborResizer());

            return resizedImage;
        }
    }
}
