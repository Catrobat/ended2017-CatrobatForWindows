using Catrobat.Core;
using Catrobat.Core.Misc.ServerCommunication;
using Catrobat.IDECommon.Resources.Main;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using System.Threading;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Catrobat.IDEWindowsPhone.Misc;
using System.Globalization;
using Catrobat.Core.Objects;
using System.Data.Linq;
using System.Windows.Input;
using System;
using System.Windows;
using Microsoft.Phone.Tasks;
using Catrobat.Core.Resources;

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


        #endregion

        #region Properties

        public bool ButtonDownloadIsEnabled
        {
            get
            {
                return _buttonDownloadIsEnabled;
            }
            set
            {
                if (_buttonDownloadIsEnabled != value)
                {
                    _buttonDownloadIsEnabled = value;

                    if (this.PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("ButtonDownloadIsEnabled"));
                        DownloadCommand.RaiseCanExecuteChanged();
                    }
                }
            }
        }

        public string UploadedLabelText
        {
            get
            {
                return _uploadedLabelText;
            }
            set
            {
                if (_uploadedLabelText != value)
                {
                    _uploadedLabelText = value;

                    if (this.PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("UploadedLabelText"));
                    }
                }
            }
        }

        public string VersionLabelText
        {
            get
            {
                return _versionLabelText;
            }
            set
            {
                if (_versionLabelText != value)
                {
                    _versionLabelText = value;

                    if (this.PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("VersionLabelText"));
                    }
                }
            }
        }

        public string ViewsLabelText
        {
            get
            {
                return _viewsLabelText;
            }
            set
            {
                if (_viewsLabelText != value)
                {
                    _viewsLabelText = value;

                    if (this.PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("ViewsLabelText"));
                    }
                }
            }
        }

        public string DownloadsLabelText
        {
            get
            {
                return _downloadsLabelText;
            }
            set
            {
                if (_downloadsLabelText != value)
                {
                    _downloadsLabelText = value;

                    if (this.PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("DownloadsLabelText"));
                    }
                }
            }
        }

        #endregion

        #region Commands

        public ICommand OnLoadCommand
        {
            get;
            private set;
        }

        public RelayCommand<OnlineProjectHeader> DownloadCommand
        {
            get;
            private set;
        }

        public ICommand ReportCommand
        {
            get;
            private set;
        }

        public ICommand LicenseCommand
        {
            get;
            private set;
        }

        public RelayCommand ResetViewModelCommand
        {
            get;
            private set;
        }

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
            UploadedLabelText = String.Format(CultureInfo.InvariantCulture, MainResources.OnlineProjectUploadedBy, dataContext.Uploaded);
            VersionLabelText = String.Format(CultureInfo.InvariantCulture, MainResources.OnlineProjectVersion, dataContext.Version);
            ViewsLabelText = String.Format(CultureInfo.InvariantCulture, MainResources.OnlineProjectViews, dataContext.Views);
            DownloadsLabelText = String.Format(CultureInfo.InvariantCulture, MainResources.OnlineProjectDownloads, dataContext.Downloads);
            ButtonDownloadIsEnabled = true;
        }

        private void DownloadAction(OnlineProjectHeader dataContext)
        {
            ButtonDownloadIsEnabled = false;
            ServerCommunication.DownloadAndSaveProject(dataContext.DownloadUrl, dataContext.ProjectName, DownloadCallback);

            var message = new DialogMessage(MainResources.DownloadQueueMessage, DownloadProjectMessageBoxResult)
            {
                Button = MessageBoxButton.OK,
                Caption = MainResources.MessageBoxInformation
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
            WebBrowserTask browser = new WebBrowserTask();
            browser.Uri = new Uri(ApplicationResources.ProjectLicenseUrl);
            browser.Show();
        }

        private void ResetViewModelAction()
        {
            ResetViewModel();
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
        }

        private void DownloadProjectMessageBoxResult(MessageBoxResult result)
        {
        }

        private void DownloadCallback(string filename)
        {
            CatrobatContext.GetContext().UpdateLocalProjects();
            CatrobatContext.GetContext().SetCurrentProject(filename);

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
                        MessageBox.Show(MainResources.NoDownloadsPending,
                          MainResources.MessageBoxInformation, MessageBoxButton.OK);
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
