using System;
using System.Globalization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Utilities.JSON;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Catrobat.Core.Resources.Localization;

namespace Catrobat.IDE.Core.ViewModels.Service
{
    public class UploadProgramLoginViewModel : ViewModelBase
    {
        public delegate void NavigationCallbackEvent();

        #region private Members

        private CatrobatContextBase _context;
        private MessageboxResult _missingLoginDataCallbackResult;
        private MessageboxResult _wrongLoginDataCallbackResult;
        private string _username;
        private string _password;

        #endregion

        #region Properties

        public CatrobatContextBase Context
        {
            get { return _context; }
            set { _context = value; RaisePropertyChanged(() => Context); }
        }
        public NavigationCallbackEvent NavigationCallback { get; set; }

        public string Username
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;

                    RaisePropertyChanged(() => Username);
                }
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;

                    RaisePropertyChanged(() => Password);
                }
            }
        }

        #endregion

        #region Commands

        public RelayCommand LoginCommand { get; private set; }

        public RelayCommand ForgottenCommand { get; private set; }

        public RelayCommand RegisterCommand { get; private set; }

        #endregion

        #region Actions

        private async void LoginAction()
        {
            if (string.IsNullOrEmpty(_username) || string.IsNullOrEmpty(_password))
            {
                ServiceLocator.NotifictionService.ShowMessageBox(AppResourcesHelper.Get("Main_UploadProgramLoginErrorCaption"),
                    AppResourcesHelper.Get("Main_UploadProgramMissingLoginData"), MissingLoginDataCallback, MessageBoxOptions.Ok);
            }
            else
            {
                JSONStatusResponse statusResponse = await ServiceLocator.WebCommunicationService.LoginOrRegisterAsync(_username, _password, null,
                                                             ServiceLocator.CultureService.GetCulture().TwoLetterISOLanguageName,
                                                             RegionInfo.CurrentRegion.TwoLetterISORegionName);
                Context.CurrentToken = statusResponse.token;
                Context.CurrentUserName = _username;

                switch (statusResponse.statusCode)
                {
                    case StatusCodes.ServerResponseOk:
                        if (NavigationCallback != null)
                        {
                            NavigationCallback();
                        }
                        else
                        {
                            //TODO: Throw error because of navigation callback shouldn't be null
                            throw new Exception("This error shouldn't be thrown. The navigation callback must not be null.");
                        }
                        break;

                    case StatusCodes.ServerResponseLoginFailed:
                    case StatusCodes.ServerResponseRegistrationFailed:
                        ServiceLocator.NotifictionService.ShowMessageBox(AppResourcesHelper.Get("Main_UploadProgramLoginErrorCaption"),
                            AppResourcesHelper.Get("Main_UploadProgramLoginErrorStatic"), WrongLoginDataCallback, MessageBoxOptions.Ok);
                        break;

                    case StatusCodes.ServerResponseMissingEmail:
                        ServiceLocator.NotifictionService.ShowMessageBox(AppResourcesHelper.Get("Main_UploadProgramLoginErrorCaption"),
                            AppResourcesHelper.Get("Main_UploadProgramLoginNonExistingUser"), WrongLoginDataCallback, MessageBoxOptions.Ok);
                        break;

                    case StatusCodes.HTTPRequestFailed:
                        ServiceLocator.NotifictionService.ShowMessageBox(AppResourcesHelper.Get("Main_UploadProgramLoginErrorCaption"),
                            AppResourcesHelper.Get("Main_NoInternetConnection"), WrongLoginDataCallback, MessageBoxOptions.Ok);
                        break;

                    default:
                        string messageString = string.IsNullOrEmpty(statusResponse.answer) ? string.Format(AppResourcesHelper.Get("Main_UploadProgramUndefinedError"), statusResponse.statusCode.ToString()) :
                                                string.Format(AppResourcesHelper.Get("Main_UploadProgramLoginError"), statusResponse.answer);
                        ServiceLocator.NotifictionService.ShowMessageBox(AppResourcesHelper.Get("Main_UploadProgramLoginErrorCaption"),
                            messageString, WrongLoginDataCallback, MessageBoxOptions.Ok);
                        break;
                }
            }
        }

        private void ForgottenAction()
        {
            ResetViewModel();
            ServiceLocator.NavigationService.NavigateTo<UploadProgramForgotPasswordViewModel>();
        }

        private void RegisterAction()
        {
            ResetViewModel();
            ServiceLocator.NavigationService.NavigateTo<UploadProgramRegisterViewModel>();
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
        public UploadProgramLoginViewModel()
        {
            LoginCommand = new RelayCommand(LoginAction);
            ForgottenCommand = new RelayCommand(ForgottenAction);
            RegisterCommand = new RelayCommand(RegisterAction);

            Messenger.Default.Register<GenericMessage<CatrobatContextBase>>(this,
                 ViewModelMessagingToken.ContextListener, ContextChangedAction);

            NavigationCallback = navigationCallback;
        }

        #region Callbacks

        private void navigationCallback()
        {
            ResetViewModel();
            ServiceLocator.NavigationService.NavigateTo<UploadProgramViewModel>();
            ServiceLocator.NavigationService.RemoveBackEntry();
        }

        private void MissingLoginDataCallback(MessageboxResult result)
        {
            _missingLoginDataCallbackResult = result;
        }

        private void WrongLoginDataCallback(MessageboxResult result)
        {
            _wrongLoginDataCallbackResult = result;
        }
        #endregion

        private void ResetViewModel()
        {
            Username = "";
            Password = "";
        }
    }
}
