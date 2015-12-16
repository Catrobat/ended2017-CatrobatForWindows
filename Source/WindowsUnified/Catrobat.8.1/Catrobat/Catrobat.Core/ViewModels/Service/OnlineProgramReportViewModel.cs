using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Utilities.JSON;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.Core.Resources.Localization;

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
        private OnlineProgramHeader _selectedOnlineProgram;

        #endregion

        #region Properties

        //public CatrobatContextBase Context
        //{
        //    get { return _context; }
        //    set { _context = value; RaisePropertyChanged(() => Context); }
        //}

        public OnlineProgramHeader SelectedOnlineProgram
        {
            get
            {
                return _selectedOnlineProgram;
            }
            set
            {
                if (ReferenceEquals(_selectedOnlineProgram, value))
                    return;

                _selectedOnlineProgram = value;
                RaisePropertyChanged(() => SelectedOnlineProgram);
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
                    ReportCommand.RaiseCanExecuteChanged();
                    RaisePropertyChanged(() => IsSending);
                }
            }
        }

        #endregion

        #region Commands

        public RelayCommand ReportCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        #endregion

        #region CommandCanExecute

        private bool ReportCommand_CanExecute()
        {
            return IsSending == false;
        }

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
                JSONStatusResponse statusResponse = await ServiceLocator.WebCommunicationService.ReportAsInappropriateAsync(_selectedOnlineProgram.ProjectId, _reason, ServiceLocator.CultureService.GetCulture().TwoLetterISOLanguageName);

                switch (statusResponse.statusCode)
                {
                    case StatusCodes.ServerResponseOk:
                        ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_ReportProgram,
                            AppResources.Main_ReportContribution, ReportSuccessfullCallback, MessageBoxOptions.Ok);
                        GoBackAction();
                        break;

                    case StatusCodes.HTTPRequestFailed:
                        ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_ReportErrorCaption,
                            AppResources.Main_NoInternetConnection, MissingReportDataCallback, MessageBoxOptions.Ok);
                        break;

                    default:
                        string messageString = string.IsNullOrEmpty(statusResponse.answer) ? string.Format(AppResources.Main_UploadProgramUndefinedError, statusResponse.statusCode.ToString()) :
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

        private void SelectedOnlineProgramChangedMessageAction(GenericMessage<OnlineProgramHeader> message)
        {
            ServiceLocator.DispatcherService.RunOnMainThread(() =>
            {
                SelectedOnlineProgram = message.Content;
            });
        }
        #endregion



        public OnlineProgramReportViewModel()
        {
            ReportCommand = new RelayCommand(ReportAction, ReportCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);
            IsSending = false;

            //Messenger.Default.Register<GenericMessage<CatrobatContextBase>>(this,
            //     ViewModelMessagingToken.ContextListener, ContextChangedAction);

            Messenger.Default.Register<GenericMessage<OnlineProgramHeader>>(this,
               ViewModelMessagingToken.SelectedOnlineProgramChangedListener, SelectedOnlineProgramChangedMessageAction);
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