using System.Threading.Tasks;
using System.Windows.Input;
using Catrobat.Core.Models.OnlinePrograms;
using Catrobat.IDE.Core.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Catrobat.Core.Resources.Localization;

namespace Catrobat.IDE.Core.ViewModels.Main.OnlinePrograms
{
  public class DetailedProgramViewModel : ViewModelBase
  {
    #region attributes

    private bool _linkIsVisible;
    private bool _isDownloading;

    private readonly object _importLock = new object();
    private MessageboxResult _cancelImportCallbackResult;

    #endregion

    #region properies

    public ProgramInfo Program { get; private set; }

    public bool LinkIsVisible
    {
      get { return _linkIsVisible; }
      set
      {
        if (_linkIsVisible == value)
        {
          return;
        }

        _linkIsVisible = value;
        RaisePropertyChanged(nameof(LinkIsVisible));
      }
    }

    public bool IsDownloading
    {
      get { return _isDownloading; }
      set
      {
        if (_isDownloading == value)
        {
          return;
        }

        _isDownloading = value;
        RaisePropertyChanged(nameof(IsDownloading));
      }
    }

    #endregion

    #region commands

    public ICommand DownloadCommand => new RelayCommand(Download);

    public ICommand CancelDownloadCommand => new RelayCommand(CancelDownload);

    public ICommand ReportCommand => new RelayCommand(Report);

    public ICommand ShowLinkCommand => new RelayCommand(ShowLink);

    #endregion

    #region construction

    public DetailedProgramViewModel()
    {
      LinkIsVisible = false;

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

    private async void Download()
    {
      // TODO: Taken from previous version, needs to be redone.
      lock (_importLock)
      {
        if (IsDownloading)
        {
          ServiceLocator.NotifictionService.ShowMessageBox(
              AppResourcesHelper.Get("Main_OnlineProgramLoading"),
              AppResourcesHelper.Get("Main_OnlineProgramDownloadBusy"),
              CancelImportCallback, MessageBoxOptions.OkCancel);
          return;
        }

        IsDownloading = true;
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
          lock (_importLock) { IsDownloading = false; }
        });
      }
    }

    private async void CancelDownload()
    {
      // TODO: Taken from previous version, needs to be redone.
      await ServiceLocator.ProgramImportService.CancelImport();
    }

    private void Report()
    {
      // To be implemented.
    }

    private void ShowLink()
    {
      LinkIsVisible = true;
    }

    private void CancelImportCallback(MessageboxResult result)
    {
      // TODO: Taken from previous version, needs to be redone.
      _cancelImportCallbackResult = result;
      if (_cancelImportCallbackResult == MessageboxResult.Cancel)
      {
        ServiceLocator.ProgramImportService.CancelImport();
      }
    }

    #endregion
  }
}
