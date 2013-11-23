using System;
using System.Windows;
using System.Windows.Media;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Phone.Utilities;
using Coding4Fun.Toolkit.Controls;

namespace Catrobat.IDE.Phone.Services
{
    class NotificationServicePhone : INotificationService
    {
        public void ShowToastNotification(string title, string message, ToastNotificationTime timeTillHide, PortableImage image)
        {
            TimeSpan timeSpan;

            switch (timeTillHide)
            {
                case ToastNotificationTime.Short:
                    timeSpan = new TimeSpan(0, 0, 0, 1);
                    break;
                case ToastNotificationTime.Medeum:
                    timeSpan = new TimeSpan(0, 0, 0, 2);
                    break;
                case ToastNotificationTime.Long:
                    timeSpan = new TimeSpan(0, 0, 0, 3);
                    break;
                default:
                    timeSpan = new TimeSpan(0, 0, 0, 1);
                    break;
            }

            ShowToastNotification(title, message, timeSpan, image);
        }

        public void ShowToastNotification(string title, string message, TimeSpan timeTillHide, PortableImage image)
        {
            var toast = new ToastPrompt
            {
                Message = message,
                Title = title,
                IsTimerEnabled = true,
                MillisecondsUntilHidden = (int) timeTillHide.TotalMilliseconds
            };

            if (image != null)
                toast.ImageSource = (ImageSource) image.ImageSource;

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
