using System;
using System.Globalization;
using System.Windows.Input;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Resources;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using GalaSoft.MvvmLight.Command;


namespace Catrobat.IDE.Core.ViewModels.Service
{
    public class OnlineProgramViewModel : ViewModelBase
    {
        #region private Members

        private bool _buttonDownloadIsEnabled = true;
        private string _uploadedLabelText = "";
        private string _versionLabelText = "";
        private string _viewsLabelText = "";
        private string _downloadsLabelText = "";
        private Program _currentProgram;

        #endregion

        #region Properties

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

        #endregion

        #region Commands

        public ICommand OnLoadCommand { get; private set; }

        public RelayCommand<OnlineProgramHeader> DownloadCommand { get; private set; }

        public ICommand ReportCommand { get; private set; }

        public ICommand LicenseCommand { get; private set; }

        #endregion

        #region CommandCanExecute

        private bool DownloadCommand_CanExecute(OnlineProgramHeader dataContext)
        {
            return ButtonDownloadIsEnabled;
        }

        #endregion

        #region Actions

        private void OnLoadAction(OnlineProgramHeader programHeader)
        {
            //var conv = new UnixTimeDateTimeConverter();
            //object output = conv.Convert((object)Convert.ToDouble(dataContext.Uploaded.Split('.')[0]), null, null, null);

            UploadedLabelText = String.Format(CultureInfo.InvariantCulture, AppResources.Main_OnlineProgramUploadedBy, ServiceLocator.WebCommunicationService.ConvertUnixTimeStamp(Convert.ToDouble(programHeader.Uploaded.Split('.')[0])));
            VersionLabelText = String.Format(CultureInfo.InvariantCulture, AppResources.Main_OnlineProgramVersion, programHeader.Version);
            ViewsLabelText = String.Format(CultureInfo.InvariantCulture, AppResources.Main_OnlineProgramViews, programHeader.Views);
            DownloadsLabelText = String.Format(CultureInfo.InvariantCulture, AppResources.Main_OnlineProgramDownloads, programHeader.Downloads);
            ButtonDownloadIsEnabled = true;
        }

        private readonly object _importLock = new object();
        private bool _isImporting = false;
        private async void DownloadAction(OnlineProgramHeader programHeader)
        {
            lock (_importLock)
            {
                if (_isImporting)
                {
                    ServiceLocator.NotifictionService.ShowMessageBox(
                        "Wait for other download", 
                        "Please wait while the current program has finished downloading",
                        (r)=>{/* no action */}, MessageBoxOptions.Ok); // TODO: localize
                    return;
                }

                _isImporting = true;
            }

            try
            {
                ServiceLocator.DispatcherService.RunOnMainThread(() =>
                    ServiceLocator.NavigationService.NavigateBack<OnlineProgramViewModel>());

                ServiceLocator.ProjectImporterService.SetDownloadHeader(programHeader);
                await ServiceLocator.ProjectImporterService.TryImportWithStatusNotifications();
            }
            finally
            {
                lock (_importLock) { _isImporting = false; }

            }
        }

        private void ReportAction(OnlineProgramHeader onlineProgramHeader)
        {
            ServiceLocator.NavigationService.NavigateTo<OnlineProgramReportViewModel>();
        }

        private void LicenseAction()
        {
            ServiceLocator.NavigationService.NavigateToWebPage(ApplicationResources.PROJECT_LICENSE_URL);
        }

        #endregion

        #region MessageActions

        #endregion

        public OnlineProgramViewModel()
        {
            OnLoadCommand = new RelayCommand<OnlineProgramHeader>(OnLoadAction);
            DownloadCommand = new RelayCommand<OnlineProgramHeader>(DownloadAction, DownloadCommand_CanExecute);
            ReportCommand = new RelayCommand<OnlineProgramHeader>(ReportAction);
            LicenseCommand = new RelayCommand(LicenseAction);
        }
    }
}