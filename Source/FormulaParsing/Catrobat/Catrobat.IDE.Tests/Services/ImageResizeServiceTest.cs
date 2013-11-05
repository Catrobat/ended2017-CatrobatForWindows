using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Tests.Services
{
    public class ImageResizeServiceTest : IImageResizeService
    {
        public PortableImage ResizeImage(PortableImage image, int maxWidthHeight)
        {
            return new PortableImage(null, maxWidthHeight, maxWidthHeight);
        }

        public PortableImage ResizeImage(PortableImage image, int newWidth, int newHeight)
        {
            return new PortableImage(null, newWidth, newHeight);
        }
    }
}
