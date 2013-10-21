using System;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Store.Services
{
    class NotificationServicePhone : INotifictionService
    {
        public void ShowToastNotification(PortableImage image, string title, string message, ToastNotificationTime timeTillHide)
        {
            throw new NotImplementedException();
        }

        public void ShowMessageBox(string title, string message, Action<MessageboxResult> callback, MessageBoxOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
