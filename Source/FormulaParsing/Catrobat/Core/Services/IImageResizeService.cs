using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.Core.Services.Data;

namespace Catrobat.Core.Services
{
    public interface IImageResizeService
    {
        PortableImage ResizeImage(PortableImage image, int maxWidthHeight);

        PortableImage ResizeImage(PortableImage image, int newWidth, int newHeight);
    }
}
