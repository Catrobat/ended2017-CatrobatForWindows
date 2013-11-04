using System;
using System.Threading.Tasks;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.Services
{
    public enum PictureServiceStatus
    {
        Success,
        Cancelled,
        Error
    }

    public class PictureServiceResult
    {
        public PictureServiceStatus Status { get; set; }
        public PortableImage Image { get; set; }

    }

    public interface IPictureService
    {
        void ChoosePictureFromLibrary(Action<PortableImage> success, Action cancelled, Action error);

        void TakePicture(Action<PortableImage> success, Action cancelled, Action error);

        void DrawPicture(Action<PortableImage> success, Action cancelled, Action error, PortableImage imageToEdit = null);


        void ChoosePictureFromLibraryAsync(Func<PortableImage, Task> success, Action cancelled, Action error);

        void TakePictureAsync(Func<PortableImage, Task> success, Action cancelled, Action error);

        //Task DrawPictureAsync(Func<PortableImage, Task> success, Action cancelled, Action error, PortableImage imageToEdit = null);
        Task<PictureServiceResult> DrawPictureAsync(PortableImage imageToEdit = null);

    }
}
