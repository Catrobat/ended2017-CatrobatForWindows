using System;
using System.Globalization;
using System.Windows.Input;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Resources;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.ViewModels.Main;
using Catrobat.IDE.Core.Xml;
using Catrobat.IDE.Core.Xml.VersionConverter;
using Catrobat.IDE.Core.Xml.XmlObjects;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Threading.Tasks;
//using Catrobat.IDE.Core.UI.Converters;

namespace Catrobat.IDE.Core.ViewModels.Service
{
    public class OnlineProjectViewModel : ViewModelBase
    {
        #region private Members

        private bool _buttonDownloadIsEnabled = true;
        private string _uploadedLabelText = "";
        private string _versionLabelText = "";
        private string _viewsLabelText = "";
        private string _downloadsLabelText = "";
        private Project _currentProject;

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

        public RelayCommand<OnlineProjectHeader> DownloadCommand { get; private set; }

        public ICommand ReportCommand { get; private set; }

        public ICommand LicenseCommand { get; private set; }

        #endregion

        #region CommandCanExecute

        private bool DownloadCommand_CanExecute(OnlineProjectHeader dataContext)
        {
            return ButtonDownloadIsEnabled;
        }

        #endregion

        #region Actions

        private void OnLoadAction(OnlineProjectHeader dataContext)
        {
            //var conv = new UnixTimeDateTimeConverter();
            //object output = conv.Convert((object)Convert.ToDouble(dataContext.Uploaded.Split('.')[0]), null, null, null);
            
            UploadedLabelText = String.Format(CultureInfo.InvariantCulture, AppResources.Main_OnlineProjectUploadedBy, ServiceLocator.WebCommunicationService.ConvertUnixTimeStamp(Convert.ToDouble(dataContext.Uploaded.Split('.')[0])));
            VersionLabelText = String.Format(CultureInfo.InvariantCulture, AppResources.Main_OnlineProjectVersion, dataContext.Version);
            ViewsLabelText = String.Format(CultureInfo.InvariantCulture, AppResources.Main_OnlineProjectViews, dataContext.Views);
            DownloadsLabelText = String.Format(CultureInfo.InvariantCulture, AppResources.Main_OnlineProjectDownloads, dataContext.Downloads);
            ButtonDownloadIsEnabled = true;
        }

        private async void DownloadAction(OnlineProjectHeader onlineProjectHeader)
        {
            ButtonDownloadIsEnabled = false;

            ServiceLocator.ProjectImporterService.SetDownloadHeader(onlineProjectHeader);
            ServiceLocator.NavigationService.RemoveBackEntry();
            ServiceLocator.NavigationService.NavigateTo<ProjectImportViewModel>();

            //var projectChangedMessage = new MessageBase();
            //Messenger.Default.Send(projectChangedMessage, 
            //    ViewModelMessagingToken.DownloadProjectStartedListener);

            //CatrobatVersionConverter.VersionConverterError error = await downloadTask;
            //var message = new MessageBase();
            //Messenger.Default.Send(message, ViewModelMessagingToken.LocalProjectsChangedListener);

            //if (error != CatrobatVersionConverter.VersionConverterError.NoError)
            //{
            //    switch (error)
            //    {
            //        case CatrobatVersionConverter.VersionConverterError.VersionTooOld:
            //        case CatrobatVersionConverter.VersionConverterError.VersionTooNew:
            //            ServiceLocator.NotifictionService.ShowToastNotification(null,
            //                AppResources.Main_VersionIsNotSupported, ToastNotificationTime.Medeum);
            //            break;
            //        case CatrobatVersionConverter.VersionConverterError.ProgramDamaged:
            //            ServiceLocator.NotifictionService.ShowToastNotification(null,
            //                AppResources.Main_ProjectNotValid, ToastNotificationTime.Medeum);
            //            break;
            //    }
            //}
            //else
            //{
            //    ServiceLocator.NotifictionService.ShowToastNotification(null,
            //        AppResources.Main_NoDownloadsPending, ToastNotificationTime.Short);
            //}
        }

        private void ReportAction(OnlineProjectHeader onlineProjectHeader)
        {
            ServiceLocator.NavigationService.NavigateTo<OnlineProjectReportViewModel>();
        }

        private void LicenseAction()
        {
            ServiceLocator.NavigationService.NavigateToWebPage(ApplicationResources.PROJECT_LICENSE_URL);
        }

        protected override void GoBackAction()
        {
            base.GoBackAction();
        }

        #endregion

        #region MessageActions

        #endregion

        public OnlineProjectViewModel()
        {
            OnLoadCommand = new RelayCommand<OnlineProjectHeader>(OnLoadAction);
            DownloadCommand = new RelayCommand<OnlineProjectHeader>(DownloadAction, DownloadCommand_CanExecute);
            ReportCommand = new RelayCommand<OnlineProjectHeader>(ReportAction);
            LicenseCommand = new RelayCommand(LicenseAction);
        }


        //private void ResetViewModel()
        //{
        //    ButtonDownloadIsEnabled = true;
        //    UploadedLabelText = "";
        //    VersionLabelText = "";
        //    ViewsLabelText = "";
        //    DownloadsLabelText = "";
        //}
    }
}