using System;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Tests.Services
{
    public class PictureServiceTest : IPictureService
    {
        public enum MethodAction
        {
            Success,
            Cancel,
            Error
        }

        public MethodAction NextMethodAction { get; set; }

        public void ChoosePictureFromLibrary(Action<PortableImage> success, Action cancelled, Action error)
        {
            switch (NextMethodAction)
            {
                    case MethodAction.Success:
                    var picture = new PortableImage();
                    success.Invoke(picture);
                    break;
                    case MethodAction.Cancel:
                    cancelled.Invoke();
                    break;
                    case MethodAction.Error:
                    error.Invoke();
                    break;
            } 
        }

        public void TakePicture(Action<PortableImage> success, Action cancelled, Action error)
        {
            switch (NextMethodAction)
            {
                case MethodAction.Success:
                    var picture = new PortableImage();
                    success.Invoke(picture);
                    break;
                case MethodAction.Cancel:
                    cancelled.Invoke();
                    break;
                case MethodAction.Error:
                    error.Invoke();
                    break;
            } 
        }

        public void DrawPicture(Action<PortableImage> success, Action cancelled, Action error, PortableImage imageToEdit = null)
        {
            switch (NextMethodAction)
            {
                case MethodAction.Success:
                    if(imageToEdit == null)
                        imageToEdit = new PortableImage();
                    success.Invoke(imageToEdit);
                    break;
                case MethodAction.Cancel:
                    cancelled.Invoke();
                    break;
                case MethodAction.Error:
                    error.Invoke();
                    break;
            } 
        }
    }
}
