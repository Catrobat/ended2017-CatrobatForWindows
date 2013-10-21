using System;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Store.Services
{
    public class PictureServicePhone : IPictureService
    {
        private Action<PortableImage> _successCallback;
        private Action _errorCallback;
        private Action _cancelleCallback;

        public void ChoosePictureFromLibrary(Action<PortableImage> success, Action cancelled, Action error)
        {
            _successCallback = success;
            _cancelleCallback = cancelled;
            _errorCallback = error;

            throw new NotImplementedException();
        }

        public void TakePicture(Action<PortableImage> success, Action cancelled, Action error)
        {
            _successCallback = success;
            _cancelleCallback = cancelled;
            _errorCallback = error;

            throw new NotImplementedException();
        }

        public void DrawPicture(Action<PortableImage> success, Action cancelled, Action error, PortableImage imageToEdit = null)
        {
            _successCallback = success;
            _cancelleCallback = cancelled;
            _errorCallback = error;

            throw new NotImplementedException();
        }
    }
}
