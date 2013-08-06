using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using Catrobat.Core;
using Catrobat.Core.Misc.ServerCommunication;
using Catrobat.Core.Objects;
using Catrobat.Core.Resources;
using Catrobat.IDEWindowsPhone.Content.Localization;
using Catrobat.IDEWindowsPhone.Misc;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Tasks;

namespace Catrobat.IDEWindowsPhone.ViewModel.Service
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
            set { _currentProject = value; RaisePropertyChanged(() => CurrentProject); }
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
            ServerCommunication.DownloadAndSaveProject(onlineProjectHeader.DownloadUrl, onlineProjectHeader.ProjectName, DownloadCallback);

            var message = new DialogMessage(AppResources.Main_DownloadQueueMessage, DownloadProjectMessageBoxResult)
            {
                Button = MessageBoxButton.OK,
                Caption = AppResources.Main_MessageBoxInformation
            };
            Messenger.Default.Send(message);

            Navigation.NavigateBack();
        }

        private void ReportAction()
        {
            // TODO: Implement.
        }

        private void LicenseAction()
        {
            var browser = new WebBrowserTask {Uri = new Uri(ApplicationResources.ProjectLicenseUrl)};
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

        private void DownloadProjectMessageBoxResult(MessageBoxResult result) {}

        private void DownloadCallback(string filename)
        {
            //TODO: comment in and fix
            //var localProjectsChangedMessage = new MessageBase();
            //Messenger.Default.Send<MessageBase>(localProjectsChangedMessage, ViewModelMessagingToken.LocalProjectsChangedListener);

            //CatrobatContextBase.GetContext().SetCurrentProject(filename);

            Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    if (filename == "")
                    {
                        // TODO: show error message
                    }
                    else
                    {
                        if (ServerCommunication.NoDownloadsPending())
                        {
                            MessageBox.Show(AppResources.Main_NoDownloadsPending,
                                            AppResources.Main_MessageBoxInformation, MessageBoxButton.OK);
                        }
                    }
                });
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