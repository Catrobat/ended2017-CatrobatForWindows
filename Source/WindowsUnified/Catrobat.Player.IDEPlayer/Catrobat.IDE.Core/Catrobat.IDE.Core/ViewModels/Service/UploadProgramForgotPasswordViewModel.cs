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
    public class UploadProgramForgotPasswordViewModel : ViewModelBase
    {
        #region private Members

        private CatrobatContextBase _context;
        private MessageboxResult _missingRecoveryDataCallbackResult;
        private MessageboxResult _serverSendedCallbackkResult;
        private string _passwordRecoveryData;
        private bool _isSending;

        #endregion

        #region Properties

        public CatrobatContextBase Context
        {
            get { return _context; }
            set { _context = value; RaisePropertyChanged(() => Context); }
        }

        public string RecoveryData
        {
            get { return _passwordRecoveryData; }
            set
            {
                if (_passwordRecoveryData != value)
                {
                    _passwordRecoveryData = value;
                    RaisePropertyChanged(() => RecoveryData);
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
                    RecoverCommand.RaiseCanExecuteChanged();
                    RaisePropertyChanged(() => IsSending);
                }
            }
        }

        #endregion

        #region Commands

        public RelayCommand RecoverCommand { get; private set; }

        #endregion

        #region CommandCanExecute

        private bool RecoverCommand_CanExecute()
        {
            return IsSending == false;
        }

        #endregion

        #region Actions

        private async void RecoverAction()
        {
            IsSending = true;
            if (string.IsNullOrEmpty(_passwordRecoveryData))
            {
                ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_UploadProgramPasswordRecoveryErrorCaption,
                    AppResources.Main_UploadProgramMissingRecoveryData, MissingRecoveryDataCallback, MessageBoxOptions.Ok);
            }
            else
            {
                JSONStatusResponse statusResponse = await ServiceLocator.WebCommunicationService.RecoverPasswordAsync(_passwordRecoveryData, ServiceLocator.CultureService.GetCulture().TwoLetterISOLanguageName);

                if (statusResponse.statusCode == StatusCodes.ServerResponseOk)
                {
                    // since 08-2014 this only works on the test-server (https://catroid-test.catrob.at/)
                    //string recoveryLink = statusResponse.answer;
                    //string hashMarker = "?c=";
                    //int position = recoveryLink.LastIndexOf(hashMarker) + hashMarker.Length;
                    //string recoveryHash = recoveryLink.Substring(position, recoveryLink.Length - position);
                    //ServiceLocator.WebCommunicationService.SetRecoveryHash(recoveryHash);
                    //ResetViewModel();
                    //ServiceLocator.NavigationService.NavigateTo<UploadProgramNewPasswordViewModel>();
                    //ServiceLocator.NavigationService.RemoveBackEntry();

                    // since 08-2014 pocketcode-server sends an email with the recovery hash in it
                    ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_UploadProgramRecoverPassword,
                                statusResponse.answer, MissingRecoveryDataCallback, MessageBoxOptions.Ok);
                    this.GoBackAction();
                }
                else
                {
                    switch (statusResponse.statusCode)
                    {
                        case StatusCodes.HTTPRequestFailed:
                            ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_UploadProgramPasswordRecoveryErrorCaption,
                                AppResources.Main_NoInternetConnection, MissingRecoveryDataCallback, MessageBoxOptions.Ok);
                            break;

                        default:
                            string messageString = string.IsNullOrEmpty(statusResponse.answer) ? string.Format(AppResources.Main_UploadProgramUndefinedError, statusResponse.statusCode.ToString()) :
                                                    string.Format(AppResources.Main_UploadProgramLoginError, statusResponse.answer);
                            ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_UploadProgramPasswordRecoveryErrorCaption,
                                messageString, MissingRecoveryDataCallback, MessageBoxOptions.Ok);
                            break;
                    }
                }
            }
            IsSending = false;
        }

        protected override void GoBackAction()
        {
            ResetViewModel();
            base.GoBackAction();
        }

        #endregion

        #region MessageActions
        private void ContextChangedAction(GenericMessage<CatrobatContextBase> message)
        {
            Context = message.Content;
        }
        #endregion

        public UploadProgramForgotPasswordViewModel()
        {
            RecoverCommand = new RelayCommand(RecoverAction, RecoverCommand_CanExecute);
            IsSending = false;

            Messenger.Default.Register<GenericMessage<CatrobatContextBase>>(this,
                 ViewModelMessagingToken.ContextListener, ContextChangedAction);
        }

        #region Callbacks

        private void ServerSendedCallback(MessageboxResult result)
        {
            _serverSendedCallbackkResult = result;
        }

        private void MissingRecoveryDataCallback(MessageboxResult result)
        {
            _missingRecoveryDataCallbackResult = result;
        }
        #endregion

        private void ResetViewModel()
        {
            RecoveryData = "";
        }
    }
}