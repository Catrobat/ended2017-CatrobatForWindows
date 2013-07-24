using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows;
using Catrobat.Core;
using Catrobat.Core.Misc;
using Catrobat.Core.Misc.JSON;
using Catrobat.Core.Misc.ServerCommunication;
using Catrobat.IDECommon.Resources.IDE.Main;
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

        private readonly ICatrobatContext catrobatContext;
        private MessageBoxResult _missingLoginDataCallbackResult;
        private MessageBoxResult _wrongLoginDataCallbackResult;
        private MessageBoxResult _registrationSuccessfulCallbackResult;
        private string _username;
        private string _password;
        private string _email;

        #endregion

        #region Properties

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
                        PropertyChanged(this, new PropertyChangedEventArgs("Username"));
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
                        PropertyChanged(this, new PropertyChangedEventArgs("Password"));
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
                        PropertyChanged(this, new PropertyChangedEventArgs("Email"));
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
            Navigation.RemoveBackEntry();

            if (string.IsNullOrEmpty(_username) || string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_email))
            {
                var message = new DialogMessage(MainResources.UploadProjectMissingLoginData, MissingLoginDataCallback)
                {
                    Button = MessageBoxButton.OK,
                    Caption = MainResources.UploadProjectLoginErrorCaption
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

        public UploadProjectLoginViewModel()
        {
            // Commands
            LoginCommand = new RelayCommand(LoginAction);
            ForgottenCommand = new RelayCommand(ForgottenAction);
            ResetViewModelCommand = new RelayCommand(ResetViewModelAction);

            if (IsInDesignMode)
            {
                catrobatContext = new CatrobatContextDesign();
            }
            else
            {
                catrobatContext = CatrobatContext.GetContext();
            }

            NavigationCallback = navigationCallback;
        }

        #region Callbacks

        private void navigationCallback()
        {
            Navigation.NavigateTo(typeof (UploadProjectView));
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
            CatrobatContext.GetContext().CurrentToken = Utils.CalculateToken(_username, _password);

            if (registered)
            {
                var message = new DialogMessage(string.Format(MainResources.UploadProjectWelcome, _username), RegistrationSuccessfulCallback)
                {
                    Button = MessageBoxButton.OK,
                    Caption = MainResources.UploadProjectRegistrationSucessful
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
                var messageString = string.IsNullOrEmpty(statusMessage) ? string.Format(MainResources.UploadProjectUndefinedError, errorCode) :
                                        string.Format(MainResources.UploadProjectLoginError, statusMessage);

                var message = new DialogMessage(messageString, WrongLoginDataCallback)
                {
                    Button = MessageBoxButton.OK,
                    Caption = MainResources.UploadProjectLoginErrorCaption
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