using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Resources;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.VersionConverter;
using Catrobat.IDE.Phone.Content.Localization;
using Coding4Fun.Toolkit.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Tasks;

namespace Catrobat.IDE.Phone.ViewModel.Service
{
    public class OnlineProjectViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler PropertyChanged;

        #region private Members

        private bool _buttonDownloadIsEnabled = true;
        private string _uploadedLabelText = "";
        private string _versionLabelText = "";
        private string _viewsLabelText = "";
        private string _downloadsLabelText = "";
        private Project _currentProject;

        #endregion

        #region Properties

        public Project CurrentProject
        {
            get { return _currentProject; }
            private set { _currentProject = value; RaisePropertyChanged(() => CurrentProject); }
        }

        public bool ButtonDownloadIsEnabled
        {
            get { return _buttonDownloadIsEnabled; }
            set
            {
                if (_buttonDownloadIsEnabled != value)
                {
                    _buttonDownloadIsEnabled = value;

                    if (PropertyChanged != null)
                    {
                        RaisePropertyChanged(() => ButtonDownloadIsEnabled);
                        DownloadCommand.RaiseCanExecuteChanged();
                    }
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

                    if (PropertyChanged != null)
                    {
                        RaisePropertyChanged(() => UploadedLabelText);
                    }
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

                    if (PropertyChanged != null)
                    {
                        RaisePropertyChanged(() => VersionLabelText);
                    }
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

                    if (PropertyChanged != null)
                    {
                        RaisePropertyChanged(() => ViewsLabelText);
                    }
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

                    if (PropertyChanged != null)
                    {
                        RaisePropertyChanged(() => DownloadsLabelText);
                    }
                }
            }
        }

        #endregion

        #region Commands

        public ICommand OnLoadCommand { get; private set; }

        public RelayCommand<OnlineProjectHeader> DownloadCommand { get; private set; }

        public ICommand ReportCommand { get; private set; }

        public ICommand LicenseCommand { get; private set; }

        public RelayCommand ResetViewModelCommand { get; private set; }

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
            UploadedLabelText = String.Format(CultureInfo.InvariantCulture, AppResources.Main_OnlineProjectUploadedBy, dataContext.Uploaded);
            VersionLabelText = String.Format(CultureInfo.InvariantCulture, AppResources.Main_OnlineProjectVersion, dataContext.Version);
            ViewsLabelText = String.Format(CultureInfo.InvariantCulture, AppResources.Main_OnlineProjectViews, dataContext.Views);
            DownloadsLabelText = String.Format(CultureInfo.InvariantCulture, AppResources.Main_OnlineProjectDownloads, dataContext.Downloads);
            ButtonDownloadIsEnabled = true;
        }

        private void DownloadAction(OnlineProjectHeader onlineProjectHeader)
        {
            ButtonDownloadIsEnabled = false;
            CatrobatWebCommunicationService.DownloadAndSaveProject(onlineProjectHeader.DownloadUrl, onlineProjectHeader.ProjectName, DownloadCallback);

            var projectChangedMessage = new MessageBase();
            Messenger.Default.Send(projectChangedMessage, ViewModelMessagingToken.DownloadProjectStartedListener);

            ServiceLocator.NavigationService.NavigateBack();
        }

        private void ReportAction()
        {
            // TODO: Implement.
        }

        private void LicenseAction()
        {
            var browser = new WebBrowserTask { Uri = new Uri(ApplicationResources.ProjectLicenseUrl) };
            browser.Show();
        }

        private void ResetViewModelAction()
        {
            ResetViewModel();
        }

        #endregion

        #region MessageActions
        private void CurrentProjectChangedAction(GenericMessage<Project> message)
        {
            CurrentProject = message.Content;
        }
        #endregion

        public OnlineProjectViewModel()
        {
            // Commands
            OnLoadCommand = new RelayCommand<OnlineProjectHeader>(OnLoadAction);
            DownloadCommand = new RelayCommand<OnlineProjectHeader>(DownloadAction, DownloadCommand_CanExecute);
            ReportCommand = new RelayCommand(ReportAction);
            LicenseCommand = new RelayCommand(LicenseAction);
            ResetViewModelCommand = new RelayCommand(ResetViewModelAction);

            Messenger.Default.Register<GenericMessage<Project>>(this,
                 ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedAction);
        }


        private void DownloadCallback(string filename, CatrobatVersionConverter.VersionConverterError error)
        {
            var message = new MessageBase();
            Messenger.Default.Send(message, ViewModelMessagingToken.LocalProjectsChangedListener);

            if (error != CatrobatVersionConverter.VersionConverterError.NoError)
            {
                switch (error)
                {
                    case CatrobatVersionConverter.VersionConverterError.VersionNotSupported:
                        var toast = new ToastPrompt { Message = AppResources.Main_VersionIsNotSupported };
                        toast.Show();
                        break;
                    case CatrobatVersionConverter.VersionConverterError.ProjectCodeNotValid:
                        toast = new ToastPrompt { Message = AppResources.Main_ProjectNotValid };
                        toast.Show();
                        break;
                }
            }
            else
            {
                var toast = new ToastPrompt { Message = AppResources.Main_NoDownloadsPending };
                toast.Show();
            }
        }


        private void ResetViewModel()
        {
            ButtonDownloadIsEnabled = true;
            UploadedLabelText = "";
            VersionLabelText = "";
            ViewsLabelText = "";
            DownloadsLabelText = "";
        }
    }
}