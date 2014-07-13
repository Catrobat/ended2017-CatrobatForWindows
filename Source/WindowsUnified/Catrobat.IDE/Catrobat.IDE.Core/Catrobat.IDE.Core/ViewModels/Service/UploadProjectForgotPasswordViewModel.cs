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
    public class UploadProjectForgotPasswordViewModel : ViewModelBase
    {
        #region private Members

        private CatrobatContextBase _context;
        private MessageboxResult _missingRecoveryDataCallbackResult;
        private string _passwordRecoveryData;

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

        #endregion

        #region Commands

        public ICommand RecoverCommand { get; private set; }

        #endregion

        #region Actions

        private async void RecoverAction()
        {
            if (string.IsNullOrEmpty(_passwordRecoveryData))
            {
                ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_UploadProjectPasswordRecoveryErrorCaption,
                    AppResources.Main_UploadProjectMissingRecoveryData, MissingRecoveryDataCallback, MessageBoxOptions.Ok);
            }
            else
            {
                ServiceLocator.NavigationService.NavigateTo<UploadProjectLoadingViewModel>();
                JSONStatusResponse status_response = await CatrobatWebCommunicationService.AsyncRecoverPassword(_passwordRecoveryData, ServiceLocator.CultureService.GetCulture().TwoLetterISOLanguageName);

                if (status_response.statusCode == StatusCodes.ServerResponseOk)
                {
                    string recovery_link = status_response.answer;
                    string hash_marker = "?c=";
                    int position = recovery_link.LastIndexOf(hash_marker) + hash_marker.Length;
                    Context.LocalSettings.CurrentUserRecoveryHash = recovery_link.Substring(position, recovery_link.Length - position);
                    ServiceLocator.DispatcherService.RunOnMainThread(() =>
                    {
                        ServiceLocator.NavigationService.NavigateTo<UploadProjectNewPasswordViewModel>();
                        ServiceLocator.NavigationService.RemoveBackEntry();
                        ServiceLocator.NavigationService.RemoveBackEntry();
                    });
                }
                else
                {
                    base.GoBackAction();
                    switch (status_response.statusCode)
                    {
                        case StatusCodes.HTTPRequestFailed:
                            ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_UploadProjectPasswordRecoveryErrorCaption,
                                AppResources.Main_NoInternetConnection, MissingRecoveryDataCallback, MessageBoxOptions.Ok);
                            break;

                        default:
                            string messageString = string.IsNullOrEmpty(status_response.answer) ? string.Format(AppResources.Main_UploadProjectUndefinedError, status_response.statusCode.ToString()) :
                                                    string.Format(AppResources.Main_UploadProjectLoginError, status_response.answer);
                            ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_UploadProjectPasswordRecoveryErrorCaption,
                                messageString, MissingRecoveryDataCallback, MessageBoxOptions.Ok);
                            break;
                    }
                }
            }
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

        public UploadProjectForgotPasswordViewModel()
        {
            RecoverCommand = new RelayCommand(RecoverAction);

            Messenger.Default.Register<GenericMessage<CatrobatContextBase>>(this,
                 ViewModelMessagingToken.ContextListener, ContextChangedAction);
        }

        #region Callbacks

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