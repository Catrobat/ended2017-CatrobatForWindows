using Catrobat.Core;
using Catrobat.Core.Misc;
using Catrobat.Core.Misc.ServerCommunication;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using System.Threading;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Catrobat.IDECommon.Resources.Main;
using System.Windows;
using System;

namespace Catrobat.IDEWindowsPhone.ViewModel
{
  public class UploadProjectLoginViewModel : ViewModelBase, INotifyPropertyChanged
  {
    private readonly ICatrobatContext catrobatContext;
    public new event PropertyChangedEventHandler PropertyChanged;
    public delegate void NavigationCallbackEvent();

    private MessageBoxResult _missingLoginDataCallbackResult;
    private MessageBoxResult _wrongLoginDataCallbackResult;
    private MessageBoxResult _registrationSuccessfulCallbackResult;
    
    public NavigationCallbackEvent NavigationCallback { get; set; }

    private string _username;

    public string Username
    {
      get
      {
        return _username;
      }
      set
      {
        if (_username != value)
        {
          _username = value;

          if (this.PropertyChanged != null)
          {
            PropertyChanged(this, new PropertyChangedEventArgs("Username"));
          }
        }
      }
    }

    private string _password;

    public string Password
    {
      get
      {
        return _password;
      }
      set
      {
        if (_password != value)
        {
          _password = value;

          if (this.PropertyChanged != null)
          {
            PropertyChanged(this, new PropertyChangedEventArgs("Password"));
          }
        }
      }
    }

    private string _email;

    public string Email
    {
      get
      {
        return _email;
      }
      set
      {
        if (_email != value)
        {
          _email = value;

          if (this.PropertyChanged != null)
          {
            PropertyChanged(this, new PropertyChangedEventArgs("Email"));
          }
        }
      }
    }

    public RelayCommand LoginCommand
    {
      get;
      private set;
    }

    public RelayCommand ForgottenCommand
    {
      get;
      private set;
    }

    public UploadProjectLoginViewModel()
    {
      // Commands
      LoginCommand = new RelayCommand(Login);
      ForgottenCommand = new RelayCommand(Forgotten);

      if (IsInDesignMode)
      {
        catrobatContext = new CatrobatContextDesign();
      }
      else
      {
        catrobatContext = CatrobatContext.GetContext();
      }
    }

    private void MissingLoginDataCallback(MessageBoxResult result)
    {
      _missingLoginDataCallbackResult = result;
    }

    private void Login()
    {
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
        ServerCommunication.RegisterOrCheckToken(_username,
                                                 _password,
                                                 _email,
                                                 Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName,
                                                 System.Globalization.RegionInfo.CurrentRegion.TwoLetterISORegionName,
                                                 Utils.calculateToken(_username, _password),
                                                 registerOrCheckTokenCallback);
      }
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
      CatrobatContext.GetContext().CurrentToken = Utils.calculateToken(_username, _password);

      if (registered)
      {
        var message = new DialogMessage(string.Format(MainResources.UploadProjectWelcome, _username), RegistrationSuccessfulCallback)
        {
          Button = MessageBoxButton.OK,
          Caption = MainResources.UploadProjectRegistrationSucessful
        };

        Messenger.Default.Send(message);
      }
      else if (errorCode == Catrobat.Core.Misc.JSON.StatusCodes.SERVER_RESPONSE_TOKEN_OK.ToString())
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

    private void Forgotten()
    {
      // TODO: Implement.
    }
  }
}
