using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows;
using Catrobat.Core;
using Catrobat.Core.Misc;
using Catrobat.Core.Misc.JSON;
using Catrobat.Core.Misc.ServerCommunication;
using Catrobat.Core.Services;
using Catrobat.IDEWindowsPhone.Content.Localization;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.Views.Service;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDEWindowsPhone.ViewModel.Service
{
    public class UploadProjectLoginViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler PropertyChanged;

        public delegate void NavigationCallbackEvent();

        #region private Members

        private CatrobatContextBase _context;
        private MessageBoxResult _missingLoginDataCallbackResult;
        private MessageBoxResult _wrongLoginDataCallbackResult;
        private MessageBoxResult _registrationSuccessfulCallbackResult;
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

                    if (PropertyChanged != null)
                    {
                        RaisePropertyChanged(() => Username);
                    }
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

                    if (PropertyChanged != null)
                    {
                        RaisePropertyChanged(() => Password);
                    }
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

                    if (PropertyChanged != null)
                    {
                        RaisePropertyChanged(() => Email);
                    }
                }
            }
        }

        #endregion

        #region Commands

        public RelayCommand LoginCommand { get; private set; }

        public RelayCommand ForgottenCommand { get; private set; }

        public RelayCommand ResetViewModelCommand { get; private set; }

        #endregion

        #region Actions

        private void LoginAction()
        {
            ServiceLocator.NavigationService.RemoveBackEntry();

            if (string.IsNullOrEmpty(_username) || string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_email))
            {
                var message = new DialogMessage(AppResources.Main_UploadProjectMissingLoginData, MissingLoginDataCallback)
                {
                    Button = MessageBoxButton.OK,
                    Caption = AppResources.Main_UploadProjectLoginErrorCaption
                };

                Messenger.Default.Send(message);
            }
            else
            {
                ServerCommunication.RegisterOrCheckToken(_username, _password, _email,
                                                         Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName,
                                                         RegionInfo.CurrentRegion.TwoLetterISORegionName,
                                                         Utils.CalculateToken(_username, _password),
                                                         registerOrCheckTokenCallback);
            }
        }

        private void ForgottenAction()
        {
            // TODO: Implement.
        }

        private void ResetViewModelAction()
        {
            ResetViewModel();
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
            // Commands
            LoginCommand = new RelayCommand(LoginAction);
            ForgottenCommand = new RelayCommand(ForgottenAction);
            ResetViewModelCommand = new RelayCommand(ResetViewModelAction);

            Messenger.Default.Register<GenericMessage<CatrobatContextBase>>(this,
                 ViewModelMessagingToken.ContextListener, ContextChangedAction);

            NavigationCallback = navigationCallback;
        }

        #region Callbacks

        private void navigationCallback()
        {
            ServiceLocator.NavigationService.NavigateTo(typeof (UploadProjectView));
        }

        private void MissingLoginDataCallback(MessageBoxResult result)
        {
            _missingLoginDataCallbackResult = result;
        }

        private void WrongLoginDataCallback(MessageBoxResult result)
        {
            _wrongLoginDataCallbackResult = result;
        }

        private void RegistrationSuccessfulCallback(MessageBoxResult result)
        {
            _registrationSuccessfulCallbackResult = result;

            if (result == MessageBoxResult.OK)
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

        private void registerOrCheckTokenCallback(bool registered, string errorCode, string statusMessage)
        {
           Context.CurrentToken = Utils.CalculateToken(_username, _password);

            if (registered)
            {
                var message = new DialogMessage(string.Format(AppResources.Main_UploadProjectWelcome, _username), RegistrationSuccessfulCallback)
                {
                    Button = MessageBoxButton.OK,
                    Caption = AppResources.Main_UploadProjectRegistrationSucessful
                };

                Messenger.Default.Send(message);
            }
            else if (errorCode == StatusCodes.SERVER_RESPONSE_TOKEN_OK.ToString())
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
            else //Unknown error
            {
                var messageString = string.IsNullOrEmpty(statusMessage) ? string.Format(AppResources.Main_UploadProjectUndefinedError, errorCode) :
                                        string.Format(AppResources.Main_UploadProjectLoginError, statusMessage);

                var message = new DialogMessage(messageString, WrongLoginDataCallback)
                {
                    Button = MessageBoxButton.OK,
                    Caption = AppResources.Main_UploadProjectLoginErrorCaption
                };

                Messenger.Default.Send(message);
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