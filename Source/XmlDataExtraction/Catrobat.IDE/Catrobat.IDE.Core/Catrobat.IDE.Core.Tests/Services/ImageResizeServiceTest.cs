using System.Threading.Tasks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.Tests.Services
{
    public class ImageResizeServiceTest : IImageResizeService
    {
        public Task<PortableImage> ResizeImage(PortableImage image, int maxWidthHeight)
        {
            return Task.Run(() => new PortableImage(maxWidthHeight, maxWidthHeight));
        }

        public Task<PortableImage> ResizeImage(PortableImage image, int newWidth, int newHeight)
        {
            return Task.Run(()=> new PortableImage(newWidth, newHeight));
        }
    }
}
