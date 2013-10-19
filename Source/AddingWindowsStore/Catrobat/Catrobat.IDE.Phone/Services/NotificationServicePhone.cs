using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;
using Coding4Fun.Toolkit.Controls;

namespace Catrobat.IDE.Phone.Services
{
    class NotificationServicePhone : INotifictionService
    {
        public void ShowToastNotification(PortableImage image, string title, string message, ToastNotificationTime timeTillHide)
        {
            var toast = new ToastPrompt
            {
                ImageSource = (ImageSource)image.ImageSource,
                Message = AppResources.Main_DownloadQueueMessage,
                Title = title,
            };
            toast.Show();
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
