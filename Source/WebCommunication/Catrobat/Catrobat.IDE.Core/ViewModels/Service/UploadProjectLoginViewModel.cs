using System;
using System.Globalization;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.Utilities;
using Catrobat.IDE.Core.Utilities.JSON;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModels.Service
{
    public class UploadProjectLoginViewModel : ViewModelBase
    {
        public delegate void NavigationCallbackEvent();

        #region private Members

        private CatrobatContextBase _context;
        private MessageboxResult _missingLoginDataCallbackResult;
        private MessageboxResult _wrongLoginDataCallbackResult;
        private MessageboxResult _registrationSuccessfulCallbackResult;
        private string _username;
        private string _password;
        private string _email;

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

        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    RaisePropertyChanged(() => Email);
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
                ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_UploadProjectLoginErrorCaption,
                    AppResources.Main_UploadProjectMissingLoginData, MissingLoginDataCallback, MessageBoxOptions.Ok);
            }
            else
            {
                JSONStatusResponse status_response =  await CatrobatWebCommunicationService.AsyncLoginOrRegister(_username, _password, _email,
                                                             ServiceLocator.CultureService.GetCulture().TwoLetterISOLanguageName,
                                                             RegionInfo.CurrentRegion.TwoLetterISORegionName);

                Context.CurrentToken = status_response.token;
                Context.CurrentUserName = _username;

                switch (status_response.statusCode)
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
                        ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_UploadProjectLoginErrorCaption,
                            AppResources.Main_UploadProjectLoginErrorStatic, WrongLoginDataCallback, MessageBoxOptions.Ok);
                        break;

                    case StatusCodes.ServerResponseMissingEmail:
                        ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_UploadProjectLoginErrorCaption,
                            AppResources.Main_UploadProjectLoginNonExistingUser, WrongLoginDataCallback, MessageBoxOptions.Ok);
                        break;

                    case StatusCodes.HTTPRequestFailed:
                        ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_UploadProjectLoginErrorCaption,
                            AppResources.Main_NoInternetConnection, WrongLoginDataCallback, MessageBoxOptions.Ok);                        
                        break;

                    default:
                        string messageString = string.IsNullOrEmpty(status_response.answer) ? string.Format(AppResources.Main_UploadProjectUndefinedError, status_response.statusCode.ToString()) :
                                                string.Format(AppResources.Main_UploadProjectLoginError, status_response.answer);
                        ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_UploadProjectLoginErrorCaption,
                            messageString, WrongLoginDataCallback, MessageBoxOptions.Ok);
                        break;
                }
            }
        }

        private void ForgottenAction()
        {
            // TODO: Implement.
        }

        private void RegisterAction()
        {
            ResetViewModel();
            ServiceLocator.NavigationService.NavigateTo<UploadProjectRegisterViewModel>();
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
        public UploadProjectLoginViewModel()
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
            ServiceLocator.NavigationService.NavigateTo<UploadProjectViewModel>();
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
            Email = "";
        }
    }
}