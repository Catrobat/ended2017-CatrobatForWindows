using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Devices.SmartCards;
using Catrobat.Core.Models.OnlinePrograms;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Catrobat.Core.Resources.Localization;

namespace Catrobat.IDE.Core.ViewModels.Main.OnlinePrograms
{
  public class DetailedProgramViewModel : ViewModelBase
  {
    #region attributes

    private bool linkIsVisible_;
    private bool isImporting_;
    private bool buttonDownloadEnabled_;
    private readonly object importLock_ = new object();
    private MessageboxResult _cancelImportCallbackResult;

    #endregion

    #region properies

    public ProgramInfo Program { get; private set; }

    public bool LinkIsVisible
    {
      get { return linkIsVisible_; }
      set
      {
        if (linkIsVisible_ == value)
        {
          return;
        }

        linkIsVisible_ = value;
        RaisePropertyChanged(nameof(LinkIsVisible));
      }
    }

    public bool ButtonDownloadIsEnabled
    {
      get { return buttonDownloadEnabled_; }
      set
      {
        if (buttonDownloadEnabled_ == value)
        {
          return;
        }

        buttonDownloadEnabled_ = value;
        RaisePropertyChanged(nameof(ButtonDownloadIsEnabled));
        DownloadRelayCommand.RaiseCanExecuteChanged();
        
      }
    }

    public bool IsImporting
    {
      get { return isImporting_; }
      set
      {
        if (isImporting_ == value)
        {
          return;
        }

        isImporting_ = value;
        RaisePropertyChanged(nameof(IsImporting));
        CancelDownloadRelayCommand.RaiseCanExecuteChanged();
      }
    }

    #endregion

    #region commands

    public RelayCommand CancelDownloadRelayCommand { get; }

    public RelayCommand DownloadRelayCommand { get; }

    public ICommand DownloadCommand => DownloadRelayCommand;

    public ICommand ReportCommand => new RelayCommand(Report);

    public ICommand ShowLinkCommand => new RelayCommand(ShowLink);

    #endregion

    #region command can execute
    private bool DownloadCommand_CanExecute()
    {
      return ButtonDownloadIsEnabled;
    }

    private bool CancelDownloadCommand_CanExecute()
    {
      return IsImporting;
    }
    #endregion

    #region construction

    public DetailedProgramViewModel()
    {
      DownloadRelayCommand = new RelayCommand(Download, DownloadCommand_CanExecute);
      CancelDownloadRelayCommand = new RelayCommand(CancelDownload, CancelDownloadCommand_CanExecute);
      
      LinkIsVisible = false;
      buttonDownloadEnabled_ = true;

      Messenger.Default.Register<GenericMessage<ProgramInfo>>(this,
               ViewModelMessagingToken.ShowDetailedOnlineProgram, ShowDetailedOnlineProgramMessageAction);
    }

    #endregion

    #region helper

    private void ShowDetailedOnlineProgramMessageAction(GenericMessage<ProgramInfo> message)
    {
      LinkIsVisible = false;

      Program = message.Content;
    }

    private void Report()
    {
      
    }

    private void ShowLink()
    {
      LinkIsVisible = true;
    }

    private async void Download()
    {
      lock (importLock_)
      {
        if (IsImporting)
        {
          ServiceLocator.NotifictionService.ShowMessageBox(
              AppResourcesHelper.Get("Main_OnlineProgramLoading"),
              AppResourcesHelper.Get("Main_OnlineProgramDownloadBusy"),
              CancelImportCallback, MessageBoxOptions.Ok);
          return;
        }

        IsImporting = true;
      }

      var message = new GenericMessage<string>(Program.Name);
      Messenger.Default.Send(message, ViewModelMessagingToken.DownloadProgramStartedListener);

      try
      {
        ServiceLocator.DispatcherService.RunOnMainThread(() =>
            ServiceLocator.NavigationService.NavigateBack<ProgramsViewModel>());

        ServiceLocator.ProgramImportService.SetDownloadHeader(Program);
        await Task.Run(() => ServiceLocator.ProgramImportService.TryImportWithStatusNotifications()).ConfigureAwait(false);
      }
      finally
      {
        ServiceLocator.DispatcherService.RunOnMainThread(() =>
        {
          lock (importLock_) { IsImporting = false; }
        });
      }
    }

    private async void CancelDownload()
    {
      await ServiceLocator.ProgramImportService.CancelImport();
    }

    private void CancelImportCallback(MessageboxResult result)
    {
      _cancelImportCallbackResult = result;
      if (_cancelImportCallbackResult == MessageboxResult.Cancel)
      {
        ServiceLocator.ProgramImportService.CancelImport();
      }
    }
    #endregion
  }
}
