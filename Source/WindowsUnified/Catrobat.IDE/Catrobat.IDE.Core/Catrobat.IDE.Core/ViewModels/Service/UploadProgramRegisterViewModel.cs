using System;
using System.Globalization;
using Catrobat.IDE.Core.Utilities;
using Catrobat.IDE.Core.Utilities.JSON;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.Resources.Localization;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModels.Service
{
    public class UploadProgramRegisterViewModel : ViewModelBase
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

        public RelayCommand RegisterCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        #endregion

        #region Actions

        private async void RegisterAction()
        {
            if (string.IsNullOrEmpty(_username) || string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_email))
            {
                ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_UploadProgramLoginErrorCaption,
                    AppResources.Main_UploadProgramMissingLoginData, MissingLoginDataCallback, MessageBoxOptions.Ok);
            }
            else
            {
                JSONStatusResponse statusResponse = await ServiceLocator.WebCommunicationService.LoginOrRegisterAsync(_username, _password, _email,
                                                             ServiceLocator.CultureService.GetCulture().TwoLetterISOLanguageName,
                                                             RegionInfo.CurrentRegion.TwoLetterISORegionName);

                Context.CurrentToken = statusResponse.token;
                Context.CurrentUserName = _username;
                Context.CurrentUserEmail = _email;

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

                    case StatusCodes.ServerResponseRegisterOk:
                        ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_UploadProgramRegistrationSucessful,
                            string.Format(AppResources.Main_UploadProgramWelcome, _username), RegistrationSuccessfulCallback, MessageBoxOptions.Ok);
                        break;

                    case StatusCodes.ServerResponseLoginFailed:
                        ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_UploadProgramLoginErrorCaption,
                            AppResources.Main_UploadProgramRegisterExistingUser, WrongLoginDataCallback, MessageBoxOptions.Ok);
                        break;

                    case StatusCodes.HTTPRequestFailed:
                        ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_UploadProgramLoginErrorCaption,
                            AppResources.Main_NoInternetConnection, WrongLoginDataCallback, MessageBoxOptions.Ok);
                        break;

                    default:
                        string messageString = string.IsNullOrEmpty(statusResponse.answer) ? string.Format(AppResources.Main_UploadProgramUndefinedError, statusResponse.statusCode.ToString()) :
                                                string.Format(AppResources.Main_UploadProgramLoginError, statusResponse.answer);
                        ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_UploadProgramLoginErrorCaption,
                            messageString, WrongLoginDataCallback, MessageBoxOptions.Ok);
                        break;
                }
            }
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
        private void ContextChangedAction(GenericMessage<CatrobatContextBase> message)
        {
            Context = message.Content;
        }
        #endregion

        public UploadProgramRegisterViewModel()
        {
            RegisterCommand = new RelayCommand(RegisterAction);
            CancelCommand = new RelayCommand(CancelAction);

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

        private void RegistrationSuccessfulCallback(MessageboxResult result)
        {
            _registrationSuccessfulCallbackResult = result;

            if (result == MessageboxResult.Ok)
            {
                if (NavigationCallback != null)
                {
                    NavigationCallback();
                }
                else
                {
                    //TODO: Throw error because of navigation callback shouldn't be null
                    throw new Exception("This error shouldn't be thrown. The navigation callback must not be null.");
                }
            }
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