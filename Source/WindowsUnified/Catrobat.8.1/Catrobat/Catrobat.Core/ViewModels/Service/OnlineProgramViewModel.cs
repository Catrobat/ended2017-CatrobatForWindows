using System;
using System.Globalization;
using System.Windows.Input;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using GalaSoft.MvvmLight.Command;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using Catrobat.Core.Resources;

namespace Catrobat.IDE.Core.ViewModels.Service
{
    public class OnlineProgramViewModel : ViewModelBase
    {
        #region private Members

        private OnlineProgramHeader _selectedOnlineProgram;
        private bool _buttonDownloadIsEnabled = true;
        private string _uploadedLabelText = "";
        private string _versionLabelText = "";
        private string _viewsLabelText = "";
        private string _downloadsLabelText = "";
        private readonly object _importLock = new object();
        private bool _isImporting = false;
        private MessageboxResult _cancelImportCallbackResult;

        #endregion

        #region Properties
        public OnlineProgramHeader SelectedOnlineProgram
        {
            get
            {
                return _selectedOnlineProgram;
            }
            set
            {
                if (ReferenceEquals(_selectedOnlineProgram, value))
                    return;

                _selectedOnlineProgram = value;
                RaisePropertyChanged(() => SelectedOnlineProgram);
            }
        }

        public bool ButtonDownloadIsEnabled
        {
            get { return _buttonDownloadIsEnabled; }
            set
            {
                if (_buttonDownloadIsEnabled != value)
                {
                    _buttonDownloadIsEnabled = value;

                    RaisePropertyChanged(() => ButtonDownloadIsEnabled);
                    DownloadCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string UploadedLabelText
        {
            get { return _uploadedLabelText; }
            set
            {
                if (_uploadedLabelText != value)
                {
                    _uploadedLabelText = value;

                    RaisePropertyChanged(() => UploadedLabelText);
                }
            }
        }

        public string VersionLabelText
        {
            get { return _versionLabelText; }
            set
            {
                if (_versionLabelText != value)
                {
                    _versionLabelText = value;

                    RaisePropertyChanged(() => VersionLabelText);
                }
            }
        }

        public string ViewsLabelText
        {
            get { return _viewsLabelText; }
            set
            {
                if (_viewsLabelText != value)
                {
                    _viewsLabelText = value;
                    RaisePropertyChanged(() => ViewsLabelText);
                }
            }
        }

        public string DownloadsLabelText
        {
            get { return _downloadsLabelText; }
            set
            {
                if (_downloadsLabelText != value)
                {
                    _downloadsLabelText = value;
                    RaisePropertyChanged(() => DownloadsLabelText);
                }
            }
        }

        public bool IsImporting
        {
            get { return _isImporting; }
            set
            {
                if (_isImporting != value)
                {
                    _isImporting = value;
                    CancelDownloadCommand.RaiseCanExecuteChanged();
                    RaisePropertyChanged(() => IsImporting);
                }
            }
        }

        #endregion

        #region Commands
        public RelayCommand<OnlineProgramHeader> DownloadCommand { get; private set; }

        public RelayCommand CancelDownloadCommand { get; private set; }

        public ICommand ReportCommand { get; private set; }

        public ICommand LicenseCommand { get; private set; }

        #endregion

        #region CommandCanExecute

        private bool DownloadCommand_CanExecute(OnlineProgramHeader dataContext)
        {
            return ButtonDownloadIsEnabled;
        }

        private bool CancelDownloadCommand_CanExecute()
        {
            return IsImporting;
        }

        #endregion

        #region Actions
        
        private async void DownloadAction(OnlineProgramHeader programHeader)
        {
            lock (_importLock)
            {
                if (IsImporting)
                {
                    ServiceLocator.NotifictionService.ShowMessageBox(
                        AppResourcesHelper.Get("Main_OnlineProgramLoading"),
                        AppResourcesHelper.Get("Main_OnlineProgramDownloadBusy"),
                        CancelImportCallback,  MessageBoxOptions.Ok);
                    return;
                }

                IsImporting = true;
            }

            var message = new GenericMessage<string>(programHeader.ProjectName);
            Messenger.Default.Send(message, ViewModelMessagingToken.DownloadProgramStartedListener);

            try
            {
                ServiceLocator.DispatcherService.RunOnMainThread(() =>
                    ServiceLocator.NavigationService.NavigateBack<OnlineProgramViewModel>());

                ServiceLocator.ProgramImportService.SetDownloadHeader(programHeader);
                await Task.Run(() => ServiceLocator.ProgramImportService.TryImportWithStatusNotifications()).ConfigureAwait(false);
            }
            finally
            {
                ServiceLocator.DispatcherService.RunOnMainThread(() =>
                {
                    lock (_importLock) { IsImporting = false; }
                });
            }
        }

        private void ReportAction()
        {
            ServiceLocator.NavigationService.NavigateTo<OnlineProgramReportViewModel>();
        }

        private void LicenseAction()
        {
            ServiceLocator.NavigationService.NavigateToWebPage(ApplicationResourcesHelper.Get("PROJECT_LICENSE_URL"));
        }

        private async void CancelDownloadAction()
        {
            await ServiceLocator.ProgramImportService.CancelImport();
        }

        #endregion

        #region MessageActions
        private void SelectedOnlineProgramChangedMessageAction(GenericMessage<OnlineProgramHeader> message)
        {
            SelectedOnlineProgram = message.Content;
        }
        #endregion

        public OnlineProgramViewModel()
        {
            DownloadCommand = new RelayCommand<OnlineProgramHeader>(DownloadAction, DownloadCommand_CanExecute);
            CancelDownloadCommand = new RelayCommand(CancelDownloadAction, CancelDownloadCommand_CanExecute);
            ReportCommand = new RelayCommand(ReportAction);
            LicenseCommand = new RelayCommand(LicenseAction);
            IsImporting = false;

            Messenger.Default.Register<GenericMessage<OnlineProgramHeader>>(this,
               ViewModelMessagingToken.SelectedOnlineProgramChangedListener, SelectedOnlineProgramChangedMessageAction);
        }

        public override void NavigateTo()
        {
            UploadedLabelText = String.Format(CultureInfo.InvariantCulture, AppResourcesHelper.Get("Main_OnlineProgramUploadedBy"), ServiceLocator.WebCommunicationService.ConvertUnixTimeStamp(Convert.ToDouble(_selectedOnlineProgram.Uploaded.Split('.')[0])));
            VersionLabelText = String.Format(CultureInfo.InvariantCulture, AppResourcesHelper.Get("Main_OnlineProgramVersion"), SelectedOnlineProgram.Version);
            ViewsLabelText = String.Format(CultureInfo.InvariantCulture, AppResourcesHelper.Get("Main_OnlineProgramViews"), SelectedOnlineProgram.Views);
            DownloadsLabelText = String.Format(CultureInfo.InvariantCulture, AppResourcesHelper.Get("Main_OnlineProgramDownloads"), SelectedOnlineProgram.Downloads);
            ButtonDownloadIsEnabled = true;

            base.NavigateTo();
        }

        #region Callbacks

        private void CancelImportCallback(MessageboxResult result)
        {
            _cancelImportCallbackResult = result;
            //if (_cancelImportCallbackResult == MessageboxResult.Cancel)
            //{
            //    ServiceLocator.ProgramImportService.CancelImport();
            //}
        }
        #endregion
    }
}