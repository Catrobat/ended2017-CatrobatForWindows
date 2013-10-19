using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Annotations;
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

    public interface INotifictionService
    {
        void ShowToastNotification(PortableImage image, string title, string message, ToastNotificationTime timeTillHide);

        void ShowMessageBox(string title, string message, Action<MessageboxResult> callback, MessageBoxOptions options);
    }
}
