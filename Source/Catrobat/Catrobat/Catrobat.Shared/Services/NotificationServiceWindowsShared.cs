using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using NotificationsExtensions.ToastContent;
using Windows.Phone.Devices.Notification;

namespace Catrobat.IDE.WindowsShared.Services
{
    public enum NotifyType
    {
        StatusMessage,
        ErrorMessage
    };

    public class NotificationServiceWindowsShared : INotificationService
    {
        public void ShowToastNotification(string title, string message,
            ToastDisplayDuration displayDuration, ToastTag tag = ToastTag.Default, PortableImage image = null, bool vibrate = false)
        {
            var duration = ToastDuration.Short;

            switch (displayDuration)
            {
                case ToastDisplayDuration.Short:
                    duration = ToastDuration.Short;
                    break;
                case ToastDisplayDuration.Long:
                    duration = ToastDuration.Long;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("timeTillHide");
            }

            IToastText02 templateContent = ToastContentFactory.CreateToastText02();
            //templateContent.TextHeading.Text = title;
            templateContent.TextBodyWrap.Text = message;
            templateContent.Duration = duration;

            templateContent.Audio.Content = ToastAudioContent.Silent;


            var toast = templateContent.CreateNotification();
            toast.Tag = tag.ToString();
            toast.Activated += ToastOnActivated;
            toast.Dismissed += ToastOnDismissed;
            toast.Failed += ToastOnFailed;

            if (vibrate)
            {
                VibrationDevice.GetDefault().Vibrate(TimeSpan.FromSeconds(0.5));
            }

            ServiceLocator.DispatcherService.RunOnMainThread(() =>
                ToastNotificationManager.CreateToastNotifier().Show(toast));
        }

        private void ToastOnActivated(ToastNotification toastNotification, object args)
        {
            var tagString = toastNotification.Tag;
            var tagEnum = (ToastTag)Enum.Parse(typeof(ToastTag), tagString);
            ToastNotificationManager.History.Remove(tagString);

            if (tagEnum != ToastTag.Default)
            {

                var toastActivatedMessage = new GenericMessage<ToastTag>(tagEnum);
                Messenger.Default.Send(toastActivatedMessage, ViewModelMessagingToken.ToastNotificationActivated);
            }
        }

        private void ToastOnFailed(ToastNotification toastNotification, ToastFailedEventArgs args)
        {
            var tagString = toastNotification.Tag;
            ToastNotificationManager.History.Remove(tagString);
        }

        private void ToastOnDismissed(ToastNotification toastNotification, ToastDismissedEventArgs args)
        {
            var tagString = toastNotification.Tag;
            ToastNotificationManager.History.Remove(tagString);
        }

        //void DisplayTextToastWithStringManipulation(ToastTemplateType templateType)
        //{
        //    string toastXmlString = String.Empty;
        //    if (templateType == ToastTemplateType.ToastText01)
        //    {
        //        toastXmlString = "<toast>"
        //                       + "<visual version='1'>"
        //                       + "<binding template='ToastText01'>"
        //                       + "<text id='1'>Body text that wraps over three lines</text>"
        //                       + "</binding>"
        //                       + "</visual>"
        //                       + "</toast>";
        //    }
        //    else if (templateType == ToastTemplateType.ToastText02)
        //    {
        //        toastXmlString = "<toast>"
        //                       + "<visual version='1'>"
        //                       + "<binding template='ToastText02'>"
        //                       + "<text id='1'>Heading text</text>"
        //                       + "<text id='2'>Body text that wraps over two lines</text>"
        //                       + "</binding>"
        //                       + "</visual>"
        //                       + "</toast>";
        //    }
        //    else if (templateType == ToastTemplateType.ToastText03)
        //    {
        //        toastXmlString = "<toast>"
        //                       + "<visual version='1'>"
        //                       + "<binding template='ToastText03'>"
        //                       + "<text id='1'>Heading text that is very long and wraps over two lines</text>"
        //                       + "<text id='2'>Body text</text>"
        //                       + "</binding>"
        //                       + "</visual>"
        //                       + "</toast>";
        //    }
        //    else if (templateType == ToastTemplateType.ToastText04)
        //    {
        //        toastXmlString = "<toast>"
        //                       + "<visual version='1'>"
        //                       + "<binding template='ToastText04'>"
        //                       + "<text id='1'>Heading text</text>"
        //                       + "<text id='2'>First body text</text>"
        //                       + "<text id='3'>Second body text</text>"
        //                       + "</binding>"
        //                       + "</visual>"
        //                       + "</toast>";
        //    }

        //    var toastDOM = new Windows.Data.Xml.Dom.XmlDocument();
        //    toastDOM.LoadXml(toastXmlString);

        //    NotifyUser(toastDOM.GetXml(), NotifyType.StatusMessage);

        //    // Create a toast, then create a ToastNotifier object to show
        //    // the toast
        //    var toast = new ToastNotification(toastDOM);

        //    // If you have other applications in your package, you can specify the AppId of
        //    // the app to create a ToastNotifier for that application
        //    ToastNotificationManager.CreateToastNotifier().Show(toast);
        //}

        //public void NotifyUser(string strMessage, NotifyType type)
        //{
        //    switch (type)
        //    {
        //        case NotifyType.StatusMessage:
        //            StatusBlock.Style = Windows.UI.Xaml.Resources["StatusStyle"] as Style;
        //            break;
        //        case NotifyType.ErrorMessage:
        //            StatusBlock.Style = Windows.UI.Xaml.Resources["ErrorStyle"] as Style;
        //            break;
        //    }
        //    StatusBlock.Text = strMessage;
        //}




        private Action<MessageboxResult> _messageBoxCallback;
        public async void ShowMessageBox(string title, string message, Action<MessageboxResult> callback, MessageBoxOptions options)
        {
            _messageBoxCallback = callback;
            var messageDialog = new MessageDialog(message, title);

            switch (options)
            {
                case MessageBoxOptions.OkCancel:
                    messageDialog.Commands.Add(new UICommand(
                        "OK", CommandInvokedHandler, MessageboxResult.Ok));
                    messageDialog.Commands.Add(new UICommand(
                        "Cancel", CommandInvokedHandler, MessageboxResult.Cancel));

                    messageDialog.DefaultCommandIndex = 0;
                    messageDialog.CancelCommandIndex = 1;
                    break;
                case MessageBoxOptions.Ok:
                    messageDialog.Commands.Add(new UICommand(
                        "OK", CommandInvokedHandler, MessageboxResult.Ok));

                    messageDialog.DefaultCommandIndex = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("options");
            }

            ServiceLocator.DispatcherService.RunOnMainThread(() =>
            {
                messageDialog.ShowAsync();
            });
        }

        private void CommandInvokedHandler(IUICommand command)
        {
            var result = MessageboxResult.Cancel;

            if ((MessageboxResult)command.Id == MessageboxResult.Ok)
                result = MessageboxResult.Ok;
            else if ((MessageboxResult)command.Id == MessageboxResult.Cancel)
                result = MessageboxResult.Cancel;

            if (_messageBoxCallback != null)
                _messageBoxCallback(result);
        }
    }
}
