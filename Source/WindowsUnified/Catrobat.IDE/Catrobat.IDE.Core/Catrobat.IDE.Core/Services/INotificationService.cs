using System;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.Services
{
    public enum MessageboxResult { Ok, Cancel };

    public enum MessageBoxOptions
    {
        Ok, OkCancel
    };

    public enum ToastDisplayDuration
    {
        Short, Long
    };

    // Important do not use enum names with more than 10 characters
    public enum ToastTag
    {
        ImportFin,
        Default
    };

    public interface INotificationService
    {
        void ShowToastNotification(string title, string message,
            ToastDisplayDuration timeTillHide, ToastTag tag = ToastTag.Default, PortableImage image = null, bool vibrate = false);

        void ShowMessageBox(string title, string message, 
            Action<MessageboxResult> callback, MessageBoxOptions options);
    }
}
