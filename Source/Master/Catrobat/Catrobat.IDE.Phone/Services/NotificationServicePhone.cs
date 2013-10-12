using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.Phone.Services
{
    class NotificationServicePhone : INotifictionService
    {
        public void ShowToastNotification(string title, string message, ToastNotificationTime timeTillHide, MessageBoxOptions options)
        {
            // TODO: show tile notification
            throw new NotImplementedException();
        }

        public void ShowMessageBox(string title, string message, Action<MessageboxResult> callback, MessageBoxOptions options)
        {
            var messageboxButton = MessageBoxButton.OKCancel;

            switch (options)
            {
                case MessageBoxOptions.Ok:
                    messageboxButton = MessageBoxButton.OK;
                    break;
                case MessageBoxOptions.OkCancel:
                    messageboxButton = MessageBoxButton.OKCancel;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("options");
            }

            MessageBoxResult result = MessageBox.Show(message, title, messageboxButton);

            var messageBoxResult = MessageboxResult.Ok;

            switch (result)
            {
                case MessageBoxResult.None:
                    messageBoxResult = MessageboxResult.Ok;
                    break;
                case MessageBoxResult.OK:
                    messageBoxResult = MessageboxResult.Ok;
                    break;
                case MessageBoxResult.Cancel:
                    messageBoxResult = MessageboxResult.Cancel;
                    break;
                case MessageBoxResult.Yes:
                    messageBoxResult = MessageboxResult.Ok;
                    break;
                case MessageBoxResult.No:
                    messageBoxResult = MessageboxResult.Cancel;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            callback.Invoke(messageBoxResult);
        }
    }
}
