using System;
using System.Globalization;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.Utilities;
using Catrobat.IDE.Core.Utilities.JSON;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;
using Catrobat.IDE.Core.CatrobatObjects;

namespace Catrobat.IDE.Core.ViewModels.Service
{
    public class OnlineProgramReportViewModel : ViewModelBase
    {
        #region private Members

        private CatrobatContextBase _context;
        private MessageboxResult _missingReportDataCallbackResult;
        private MessageboxResult _reportSuccessfullCallbackResult;
        private string _reason;
        private bool _isSending;
        private OnlineProjectHeader _selectedOnlineProject;

        #endregion

        #region Properties

        //public CatrobatContextBase Context
        //{
        //    get { return _context; }
        //    set { _context = value; RaisePropertyChanged(() => Context); }
        //}

        public OnlineProjectHeader SelectedOnlineProject
        {
            get
            {
                return _selectedOnlineProject;
            }
            set
            {
                if (ReferenceEquals(_selectedOnlineProject, value))
                    return;

                _selectedOnlineProject = value;
                RaisePropertyChanged(() => SelectedOnlineProject);
            }
        }

        public string Reason
        {
            get { return _reason; }
            set
            {
                if (_reason != value)
                {
                    _reason = value;
                    RaisePropertyChanged(() => Reason);
                }
            }
        }

        public bool IsSending
        {
            get { return _isSending; }
            set
            {
                if (_isSending != value)
                {
                    _isSending = value;
                    RaisePropertyChanged(() => IsSending);
                }
            }
        }

        #endregion

        #region Commands

        public RelayCommand ReportCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        #endregion

        #region Actions

        private async void ReportAction()
        {
            IsSending = true;
            if (string.IsNullOrEmpty(_reason))
            {
                ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_ReportErrorCaption,
                    AppResources.Main_ReportMissingData, MissingReportDataCallback, MessageBoxOptions.Ok);
            }
            else
            {
                JSONStatusResponse statusResponse = await ServiceLocator.WebCommunicationService.ReportAsInappropriateAsync(_selectedOnlineProject.ProjectId, _reason, ServiceLocator.CultureService.GetCulture().TwoLetterISOLanguageName);

                switch (statusResponse.statusCode)
                {
                    case StatusCodes.ServerResponseOk:
                        ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_ReportProject,
                            AppResources.Main_ReportContribution, ReportSuccessfullCallback, MessageBoxOptions.Ok);
                        GoBackAction();
                        break;

                    case StatusCodes.HTTPRequestFailed:
                        ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_ReportErrorCaption,
                            AppResources.Main_NoInternetConnection, MissingReportDataCallback, MessageBoxOptions.Ok);
                        break;

                    default:
                        string messageString = string.IsNullOrEmpty(statusResponse.answer) ? string.Format(AppResources.Main_UploadProjectUndefinedError, statusResponse.statusCode.ToString()) :
                                                string.Format(AppResources.Main_ReportError, statusResponse.answer);
                        ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_ReportErrorCaption,
                            messageString, MissingReportDataCallback, MessageBoxOptions.Ok);
                        break;
                }
            }
            IsSending = false;
        }

        private void CancelAction()
        {
            GoBackAction();
        }

        protected override void GoBackAction()
        {
            ResetViewModel();
            base.GoBackAction();
        }

        #endregion

        #region MessageActions
        //private void ContextChangedAction(GenericMessage<CatrobatContextBase> message)
        //{
        //    Context = message.Content;
        //}

        private void SelectedOnlineProjectChangedMessageAction(GenericMessage<OnlineProjectHeader> message)
        {
            ServiceLocator.DispatcherService.RunOnMainThread(() =>
            {
                SelectedOnlineProject = message.Content;
            });
        }
        #endregion


        

        public OnlineProgramReportViewModel()
        {
            ReportCommand = new RelayCommand(ReportAction);
            CancelCommand = new RelayCommand(CancelAction);
            IsSending = false;

            //Messenger.Default.Register<GenericMessage<CatrobatContextBase>>(this,
            //     ViewModelMessagingToken.ContextListener, ContextChangedAction);

            Messenger.Default.Register<GenericMessage<OnlineProjectHeader>>(this,
               ViewModelMessagingToken.SelectedOnlineProjectChangedListener, SelectedOnlineProjectChangedMessageAction);
        }

        #region Callbacks

        private void MissingReportDataCallback(MessageboxResult result)
        {
            _missingReportDataCallbackResult = result;
        }

        private void ReportSuccessfullCallback(MessageboxResult result)
        {
            _reportSuccessfullCallbackResult = result;
        }
        #endregion

        private void ResetViewModel()
        {
            Reason = "";
        }
    }
}