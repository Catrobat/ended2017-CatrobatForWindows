using System;
using System.IO;
using Catrobat.IDE.Core.Services.Data;

namespace Catrobat.IDE.Core.Services
{
    public interface IPictureService
    {
        void ChoosePictureFromLibrary(Action<PortableImage> success, Action cancelled, Action error);

        void TakePicture(Action<PortableImage> success, Action cancelled, Action error);

        void DrawPicture(Action<PortableImage> success, Action cancelled, Action error, PortableImage imageToEdit = null);
    }
}
