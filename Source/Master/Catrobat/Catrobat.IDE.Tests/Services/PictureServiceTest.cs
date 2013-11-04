using System;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Tests.Services
{
    public class PictureServiceTest : IPictureService
    {
        public PictureServiceStatus NextMethodAction { get; set; }

        public void ChoosePictureFromLibrary(Action<PortableImage> success, Action cancelled, Action error)
        {
            switch (NextMethodAction)
            {
                case PictureServiceStatus.Success:
                    var picture = new PortableImage();
                    success.Invoke(picture);
                    break;
                case PictureServiceStatus.Cancelled:
                    cancelled.Invoke();
                    break;
                case PictureServiceStatus.Error:
                    error.Invoke();
                    break;
            } 
        }

        public void TakePicture(Action<PortableImage> success, Action cancelled, Action error)
        {
            switch (NextMethodAction)
            {
                case PictureServiceStatus.Success:
                    var picture = new PortableImage();
                    success.Invoke(picture);
                    break;
                case PictureServiceStatus.Cancelled:
                    cancelled.Invoke();
                    break;
                case PictureServiceStatus.Error:
                    error.Invoke();
                    break;
            }  
        }

        public void DrawPicture(Action<PortableImage> success, Action cancelled, Action error, PortableImage imageToEdit = null)
        {
            switch (NextMethodAction)
            {
                case PictureServiceStatus.Success:
                    var picture = new PortableImage();
                    success.Invoke(picture);
                    break;
                case PictureServiceStatus.Cancelled:
                    cancelled.Invoke();
                    break;
                case PictureServiceStatus.Error:
                    error.Invoke();
                    break;
            } 
        }


        public void ChoosePictureFromLibraryAsync(Func<PortableImage, Task> success, Action cancelled, Action error)
        {
            switch (NextMethodAction)
            {
                case PictureServiceStatus.Success:
                    var picture = new PortableImage();
                    success.Invoke(picture);
                    break;
                case PictureServiceStatus.Cancelled:
                    cancelled.Invoke();
                    break;
                case PictureServiceStatus.Error:
                    error.Invoke();
                    break;
            } 
        }

        public void TakePictureAsync(Func<PortableImage, Task> success, Action cancelled, Action error)
        {
            switch (NextMethodAction)
            {
                case PictureServiceStatus.Success:
                    var picture = new PortableImage();
                    success.Invoke(picture);
                    break;
                case PictureServiceStatus.Cancelled:
                    cancelled.Invoke();
                    break;
                case PictureServiceStatus.Error:
                    error.Invoke();
                    break;
            } 
        }

        public Task<PictureServiceResult> DrawPictureAsync(PortableImage imageToEdit = null)
        {
            var result = new PictureServiceResult
            {
                Status = NextMethodAction
            };

            if (result.Status == PictureServiceStatus.Success)
                result.Image = new PortableImage();

            return Task.Run(() => result);
        }
    }
}
