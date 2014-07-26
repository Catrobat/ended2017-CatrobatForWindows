using System;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.Tests.Services.Storage
{
    public class NotificationServiceTest : INotificationService
    {
        public int SentToastNotifications { get; set; }
        public int SentMessageBoxes { get; set; }
        public MessageboxResult NextMessageboxResult { get; set; }
        public MessageBoxOptions LastMessageboxOption { get; set; }

        public void ShowToastNotification(string title, string message, 
            ToastDisplayDuration timeTillHide, PortableImage image = null)
        {
            SentToastNotifications++;
        }

        public void ShowToastNotification(string title, string message, 
            TimeSpan timeTillHide, PortableImage image = null)
        {
            SentToastNotifications++;
        }

        public void ShowMessageBox(string title, string message, 
            Action<MessageboxResult> callback, MessageBoxOptions options)
        {
            SentMessageBoxes++;
            LastMessageboxOption = options;
            callback.Invoke(NextMessageboxResult);
        }


    }
}
