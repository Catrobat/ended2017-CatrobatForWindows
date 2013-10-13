using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Annotations;

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

    public interface INotifictionService
    {
        void ShowToastNotification(string title, string message, ToastNotificationTime timeTillHide, MessageBoxOptions options);

        void ShowMessageBox(string title, string message, Action<MessageboxResult> callback, MessageBoxOptions options);
    }
}
