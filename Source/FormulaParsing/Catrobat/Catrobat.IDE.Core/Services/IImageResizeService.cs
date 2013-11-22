using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.Services
{
    public interface IImageResizeService
    {
        PortableImage ResizeImage(PortableImage image, int maxWidthHeight);

        PortableImage ResizeImage(PortableImage image, int newWidth, int newHeight);
    }
}
