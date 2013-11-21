using System.Threading.Tasks;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.Services
{
    public interface IImageResizeService
    {
        Task<PortableImage> ResizeImage(PortableImage image, int maxWidthHeight);

        Task<PortableImage> ResizeImage(PortableImage image, int newWidth, int newHeight);
    }
}
