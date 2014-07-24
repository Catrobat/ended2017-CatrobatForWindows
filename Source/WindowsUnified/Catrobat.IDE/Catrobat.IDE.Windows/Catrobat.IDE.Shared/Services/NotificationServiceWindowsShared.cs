using System;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.WindowsShared.Services
{
    public enum NotifyType
    {
        StatusMessage,
        ErrorMessage
    };

    public class NotificationServiceWindowsShared : INotificationService
    {
        private Page _rootPage;

        //public NotificationServiceWindowsShared(Page rootPage)
        //{
        //    _rootPage = rootPage;
        //}

        public void ShowToastNotification(string title, string message, ToastNotificationTime timeTillHide, PortableImage image = null)
        {
            // TODO: implement me
            //throw new NotImplementedException();
        }

        public void ShowToastNotification(string title, string message, TimeSpan timeTillHide, PortableImage image = null)
        {
            // TODO: implement me
            //throw new NotImplementedException();
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
