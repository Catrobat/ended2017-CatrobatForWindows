using System;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.Tests.Services
{
    public class NotificationServiceTest : INotificationService
    {
        public int SentToastNotifications { get; set; }
        public int SentMessageBoxes { get; set; }
        public MessageboxResult NextMessageboxResult { get; set; }
        public MessageBoxOptions LastMessageboxOption { get; set; }
        public bool Vibrate { get; set; }

        public string LastNotificationTitle;
        public string LastNotificationMessage;

        public void ShowToastNotification(string title, string message,
            ToastDisplayDuration timeTillHide, ToastTag tag = ToastTag.Default, PortableImage image = null, bool vibrate = false)
        {
            SentToastNotifications++;
            LastNotificationTitle = title;
            LastNotificationMessage = message;
            Vibrate = vibrate;
        }

        public void ShowMessageBox(string title, string message, 
            Action<MessageboxResult> callback, MessageBoxOptions options)
        {
            SentMessageBoxes++;
            LastNotificationTitle = title;
            LastNotificationMessage = message;
            LastMessageboxOption = options;
            callback.Invoke(NextMessageboxResult);
        }
    }
}
