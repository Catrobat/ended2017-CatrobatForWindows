using System;
using System.Globalization;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.Utilities;
using Catrobat.IDE.Core.Utilities.JSON;
using Catrobat.IDE.Core.ViewModels.Main;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;
using Catrobat.IDE.Core.CatrobatObjects;

namespace Catrobat.IDE.Core.ViewModels.Service
{
    public class UploadProgramNewPasswordViewModel : ViewModelBase
    {
        #region private Members

        private CatrobatContextBase _context;
        private MessageboxResult _missingPasswordDataCallbackResult;
        private MessageboxResult _passwordInvalidCallbackResult;
        private MessageboxResult _recoveryHashNotFoundCallbackResult;
        private string _newPassword;
        private string _repeatedPassword;
        private bool _isSending;

        #endregion

        #region Properties

        public CatrobatContextBase Context
        {
            get { return _context; }
            set { _context = value; RaisePropertyChanged(() => Context); }
        }

        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                if (_newPassword != value)
                {
                    _newPassword = value;
                    RaisePropertyChanged(() => NewPassword);
                }
            }
        }

        public string RepeatedPassword
        {
            get { return _repeatedPassword; }
            set
            {
                if (_repeatedPassword != value)
                {
                    _repeatedPassword = value;
                    NewPasswordCommand.RaiseCanExecuteChanged();
                    RaisePropertyChanged(() => RepeatedPassword);
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

        public RelayCommand NewPasswordCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        #endregion

        #region CommandCanExecute

        private bool NewPasswordCommand_CanExecute()
        {
            return IsSending == false;
        }

        #endregion

        #region Actions

        private async void NewPasswordAction()
        {
            IsSending = true;
            if (string.IsNullOrEmpty(_newPassword) || string.IsNullOrEmpty(_repeatedPassword))
            {
                ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_UploadProgramPasswordRecoveryErrorCaption,
                    AppResources.Main_UploadProgramMissingPassword, MissingPasswordDataCallback, MessageBoxOptions.Ok);
            }
            else
            {
                JSONStatusResponse statusResponse = await ServiceLocator.WebCommunicationService.ChangePasswordAsync(_newPassword, _repeatedPassword, ServiceLocator.CultureService.GetCulture().TwoLetterISOLanguageName);

                switch (statusResponse.statusCode)
                {
                    case StatusCodes.ServerResponseOk:
                        this.GoBackAction();
                        ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_UploadProgramNewPassword,
                            AppResources.Main_UploadProgramPasswordChangeSucess, PasswordInvalidCallback, MessageBoxOptions.Ok);
                        break;

                    // because of typing-error in server-message
                    case StatusCodes.ServerResponseRecoveryHashNotFound:
                        ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_UploadProgramPasswordRecoveryErrorCaption,
                            AppResources.Main_UploadProgramRecoveryHashError, RecoveryHashNotFoundCallback, MessageBoxOptions.Ok);
                        break;

                    // may be checked locally
                    case StatusCodes.ServerResponsePasswordMatchFailed:
                        ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_UploadProgramPasswordRecoveryErrorCaption,
                            AppResources.Main_UploadProgramRecoveryPasswordMatchError, PasswordInvalidCallback, MessageBoxOptions.Ok);
                        break;

                    case StatusCodes.HTTPRequestFailed:
                        ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_UploadProgramPasswordRecoveryErrorCaption,
                            AppResources.Main_NoInternetConnection, MissingPasswordDataCallback, MessageBoxOptions.Ok);
                        break;

                    default:
                        string messageString = string.IsNullOrEmpty(statusResponse.answer) ? string.Format(AppResources.Main_UploadProgramUndefinedError, statusResponse.statusCode.ToString()) : statusResponse.answer;
                        ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_UploadProgramPasswordRecoveryErrorCaption,
                            messageString, MissingPasswordDataCallback, MessageBoxOptions.Ok);
                        break;
                }
            }
            IsSending = false;
        }

        private void CancelAction()
        {
            ResetViewModel();
            ServiceLocator.NavigationService.NavigateTo<MainViewModel>();
            ServiceLocator.NavigationService.RemoveBackEntry();
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

        public UploadProgramNewPasswordViewModel()
        {
            NewPasswordCommand = new RelayCommand(NewPasswordAction, NewPasswordCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);
            IsSending = false;

            Messenger.Default.Register<GenericMessage<CatrobatContextBase>>(this,
                 ViewModelMessagingToken.ContextListener, ContextChangedAction);
        }

        #region Callbacks

        private void MissingPasswordDataCallback(MessageboxResult result)
        {
            _missingPasswordDataCallbackResult = result;
        }

        private void PasswordInvalidCallback(MessageboxResult result)
        {
            _passwordInvalidCallbackResult = result;
        }

        private void RecoveryHashNotFoundCallback(MessageboxResult result)
        {
            _recoveryHashNotFoundCallbackResult = result;
            GoBackAction();
        }

        #endregion

        private void ResetViewModel()
        {
            NewPassword = "";
            RepeatedPassword = "";
        }
    }
}