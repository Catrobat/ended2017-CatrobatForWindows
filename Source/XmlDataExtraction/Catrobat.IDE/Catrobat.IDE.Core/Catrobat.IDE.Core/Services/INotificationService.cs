using System;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.Services
{
    public enum MessageboxResult { Ok, Cancel };

    public enum MessageBoxOptions
    {
        Ok, OkCancel
    };

    public enum ToastNotificationTime
    {
        Short, Medeum, Long
    };

    public interface INotificationService
    {
        void ShowToastNotification(string title, string message, ToastNotificationTime timeTillHide, PortableImage image = null);

        void ShowToastNotification(string title, string message, TimeSpan timeTillHide, PortableImage image = null);

        void ShowMessageBox(string title, string message, Action<MessageboxResult> callback, MessageBoxOptions options);
    }
}
