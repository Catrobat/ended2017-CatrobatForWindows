using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services.Data;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Phone.Views.Main;
using Catrobat.IDE.Phone.Views.Share;
using Coding4Fun.Toolkit.Controls;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;
using Catrobat.IDE.Phone.Views.Service;
using Catrobat.IDE.Phone.Views.Settings;
using Catrobat.IDE.Phone.Views.Editor;

namespace Catrobat.IDE.Phone.ViewModel.Main
{
    public class MainViewModel : ViewModelBase
    {
        #region private Members

        private bool _showDownloadMessage;
        private bool _showUploadMessage;
        private string _filterText = "";
        private string _previousFilterText = "";
        private bool _isLoadingOnlineProjects;
        private bool _isActivatingLocalProject;
        private MessageboxResult _dialogResult;
        private string _deleteProjectName;
        private string _copyProjectName;
        private Project _currentProject;
        private ObservableCollection<ProjectDummyHeader> _localProjects;
        private CatrobatContextBase _context;
        private ObservableCollection<OnlineProjectHeader> _onlineProjects;

        #endregion

        #region Properties

        public bool IsMemoryMonitorEnabled { get { return Context.LocalSettings.IsInDevelopingMode; } }

        public CatrobatContextBase Context
        {
            get { return _context; }
            set
            {
                _context = value;

                if (Context is CatrobatContextDesign)
                {
                    var designContext = (CatrobatContextDesign)_context;
                    LocalProjects = designContext.LocalProjects;
                    OnlineProjects = designContext.OnlineProjects;
                    CurrentProject = designContext.CurrentProject;
                }

                RaisePropertyChanged(() => Context);
            }
        }

        public Project CurrentProject
        {
            get
            {
                return _currentProject;
            }
            set
            {
                if (value == _currentProject) return;

                _currentProject = value;

                RaisePropertyChanged(() => CurrentProject);
                UpdateLocalProjects();
                XmlParserTempProjectHelper.Project = _currentProject;

                var projectChangedMessage = new GenericMessage<Project>(CurrentProject);
                Messenger.Default.Send(projectChangedMessage, ViewModelMessagingToken.CurrentProjectChangedListener);

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        public ProjectDummyHeader PinProjectHeader { get; set; }

        public ObservableCollection<ProjectDummyHeader> LocalProjects
        {
            get { return _localProjects; }
            set
            {
                _localProjects = value;
                RaisePropertyChanged(() => LocalProjects);
            }
        }

        public OnlineProjectHeader SelectedOnlineProject { get; set; }

        public ObservableCollection<OnlineProjectHeader> OnlineProjects
        {
            get { return _onlineProjects; }
            set
            {
                _onlineProjects = value;
                RaisePropertyChanged(() => OnlineProjects);
            }
        }

        public string ApplicationVersionName
        {
            get
            {
                var name = String.Format(AppResources.Main_ApplicationNameAndVersion,
                    ServiceLocator.SystemInformationService.CurrentApplicationVersion);
                return name;
            }
        }

        public String FilterText
        {
            get
            {
                return _filterText;
            }
            set
            {
                if (_filterText != value)
                {
                    _filterText = value;
                    LoadOnlineProjects(false);
                    RaisePropertyChanged(() => FilterText);
                }
            }
        }

        public bool IsLoadingOnlineProjects
        {
            get { return _isLoadingOnlineProjects; }
            set { _isLoadingOnlineProjects = value; RaisePropertyChanged(() => IsLoadingOnlineProjects); }
        }

        public bool IsActivatingLocalProject
        {
            get
            {
                return _isActivatingLocalProject;
            }
            set
            {
                _isActivatingLocalProject = value;
                RaisePropertyChanged(() => IsActivatingLocalProject);
            }
        }

        #endregion

        #region Commands

        public ICommand RenameLocalProjectCommand
        {
            get;
            private set;
        }

        public ICommand DeleteLocalProjectCommand
        {
            get;
            private set;
        }

        public ICommand CopyLocalProjectCommand
        {
            get;
            private set;
        }

        public ICommand PinLocalProjectCommand
        {
            get;
            private set;
        }

        public ICommand ShareLocalProjectCommand
        {
            get;
            private set;
        }

        public ICommand LazyLoadOnlineProjectsCommand
        {
            get;
            private set;
        }

        public ICommand SetCurrentProjectCommand
        {
            get;
            private set;
        }

        public ICommand CreateNewProjectCommand
        {
            get;
            private set;
        }

        public ICommand EditCurrentProjectCommand
        {
            get;
            private set;
        }

        public ICommand SettingsCommand
        {
            get;
            private set;
        }

        public ICommand OnlineProjectTapCommand
        {
            get;
            private set;
        }

        public ICommand PlayCurrentProjectCommand
        {
            get;
            private set;
        }

        public ICommand UploadCurrentProjectCommand
        {
            get;
            private set;
        }

        public RelayCommand ResetViewModelCommand
        {
            get;
            private set;
        }

        public ICommand ShowMessagesCommand
        {
            get;
            private set;
        }

        #endregion

        # region Actions

        private void RenameLocalProjectAction(ProjectDummyHeader project)
        {
            var message = new GenericMessage<ProjectDummyHeader>(project);
            Messenger.Default.Send(message, ViewModelMessagingToken.ChangeLocalProjectListener);

            ServiceLocator.NavigationService.NavigateTo(typeof(ProjectSettingsView));
        }

        private void DeleteLocalProjectAction(string projectName)
        {
            _deleteProjectName = projectName;

            ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_MainDeleteProjectDialogTitle,
                String.Format(AppResources.Main_MainDeleteProjectDialogMessage, projectName), DeleteProjectMessageCallback, MessageBoxOptions.OkCancel);
        }

        private void CopyLocalProjectAction(string projectName)
        {
            _copyProjectName = projectName;

            ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_MainCopyProjectDialogTitle,
                String.Format(AppResources.Main_MainCopyProjectDialogMessage, projectName), CopyProjectMessageCallback, MessageBoxOptions.OkCancel);
        }

        private void PinLocalProjectAction(ProjectDummyHeader project)
        {
            PinProjectHeader = project;

            var message = new GenericMessage<ProjectDummyHeader>(PinProjectHeader);
            Messenger.Default.Send(message, ViewModelMessagingToken.PinProjectHeaderListener);

            ServiceLocator.NavigationService.NavigateTo(typeof(TileGeneratorView));
        }

        private void ShareLocalProjectAction(ProjectDummyHeader project)
        {
            if(CurrentProject.ProjectDummyHeader == project)
                CurrentProject.Save();

            PinProjectHeader = project;

            var message = new GenericMessage<ProjectDummyHeader>(PinProjectHeader);
            Messenger.Default.Send(message, ViewModelMessagingToken.ShareProjectHeaderListener);

            ServiceLocator.NavigationService.NavigateTo(typeof(ShareProjectServiceSelectionView));
        }

        private void LazyLoadOnlineProjectsAction()
        {
            LoadOnlineProjects(true);
        }


        private void SetCurrentProjectAction(string projectName)
        {
            lock (CurrentProject)
            {
                if (IsActivatingLocalProject)
                    return;

                IsActivatingLocalProject = true;
            }

            var minLoadingTime = new TimeSpan(0, 0, 0, 0, 500);
            DateTime startTime = DateTime.UtcNow;


            Task.Run(() =>
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    CurrentProject.Save();
                    var newProject = CatrobatContext.LoadNewProjectByNameStatic(projectName);


                    if (newProject != null)
                    {
                        CurrentProject = newProject;

                        var minWaitingTimeRemaining = minLoadingTime.Subtract(DateTime.UtcNow.Subtract(startTime));

                        if (minWaitingTimeRemaining >= new TimeSpan(0))
                            Thread.Sleep(minWaitingTimeRemaining);

                        IsActivatingLocalProject = false;


                    }
                    else
                    {
                        XmlParserTempProjectHelper.Project = CurrentProject;

                        ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_SelectedProjectNotValidMessage, 
                            String.Format(AppResources.Main_SelectedProjectNotValidHeader, projectName), new Action<MessageboxResult>(delegate
                                {
                                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                                    {
                                        IsActivatingLocalProject = false;
                                    });
                                }), MessageBoxOptions.Ok);
                    }
                });
            });
        }

        private void CreateNewProjectAction()
        {
            ServiceLocator.NavigationService.NavigateTo(typeof(AddNewProjectView));
        }

        private void EditCurrentProjectAction()
        {
            ServiceLocator.NavigationService.NavigateTo(typeof(EditorLoadingView));
        }

        private void SettingsAction()
        {
            ServiceLocator.NavigationService.NavigateTo(typeof(SettingsView));
        }

        private void OnlineProjectTapAction(OnlineProjectHeader project)
        {
            SelectedOnlineProject = project;
            ServiceLocator.NavigationService.NavigateTo(typeof(OnlineProjectView));
        }

        private void PlayCurrentProjectAction()
        {
            ServiceLocator.PlayerLauncherService.LaunchPlayer(CurrentProject);
        }

        private void UploadCurrentProjectAction()
        {
            ServiceLocator.NavigationService.NavigateTo(typeof(UploadProjectsLoadingView));

            // Determine which page to open
            Task.Run(() => CatrobatWebCommunicationService.CheckToken(Context.CurrentToken, CheckTokenEvent));
        }

        private void ResetViewModelAction()
        {
            ResetViewModel();
        }

        private void ShowMessagesAction()
        {
            if (_showDownloadMessage)
            {
                var image = new BitmapImage();
                using (var loader = ServiceLocator.ResourceLoaderFactory.CreateResourceLoader())
                {
                    var stream = loader.OpenResourceStream(ResourceScope.IdePhone, "Content/Images/ApplicationBar/dark/appbar.download.rest.png");
                    image.SetSource(stream);
                }

                var toast = new ToastPrompt
                {
                    ImageSource = image,
                    Message = AppResources.Main_DownloadQueueMessage
                };
                toast.Show();

                _showDownloadMessage = false;
            }
            if (_showUploadMessage)
            {
                var image = new BitmapImage();
                using (var loader = ServiceLocator.ResourceLoaderFactory.CreateResourceLoader())
                {
                    var stream = loader.OpenResourceStream(ResourceScope.IdePhone, "Content/Images/ApplicationBar/dark/appbar.upload.rest.png");
                    image.SetSource(stream);
                }

                var toast = new ToastPrompt
                {
                    ImageSource = image,
                    Message = AppResources.Main_UploadQueueMessage
                };
                toast.Show();

                _showUploadMessage = false;
            }
        }

        #endregion

        #region MessageActions

        private void LocalProjectsChangedMessageAction(MessageBase message)
        {
            UpdateLocalProjects();
        }

        private void DownloadProjectStartedMessageAction(MessageBase message)
        {
            _showDownloadMessage = true;
        }

        private void UploadProjectStartedMessageAction(MessageBase message)
        {
            _showUploadMessage = true;
        }

        private void ContextChangedAction(GenericMessage<CatrobatContextBase> message)
        {
            Context = message.Content;
            if (Context is CatrobatContextDesign)
            {
                LocalProjects = (Context as CatrobatContextDesign).LocalProjects;
                OnlineProjects = (Context as CatrobatContextDesign).OnlineProjects;
            }
        }

        private void CurrentProjectChangedMessageAction(GenericMessage<Project> message)
        {
            CurrentProject = message.Content;
        }

        #endregion

        public MainViewModel()
        {
            _onlineProjects = new ObservableCollection<OnlineProjectHeader>();

            RenameLocalProjectCommand = new RelayCommand<ProjectDummyHeader>(RenameLocalProjectAction);
            DeleteLocalProjectCommand = new RelayCommand<string>(DeleteLocalProjectAction);
            CopyLocalProjectCommand = new RelayCommand<string>(CopyLocalProjectAction);
            PinLocalProjectCommand = new RelayCommand<ProjectDummyHeader>(PinLocalProjectAction);
            ShareLocalProjectCommand = new RelayCommand<ProjectDummyHeader>(ShareLocalProjectAction);
            LazyLoadOnlineProjectsCommand = new RelayCommand(LazyLoadOnlineProjectsAction);
            SetCurrentProjectCommand = new RelayCommand<string>(SetCurrentProjectAction);
            OnlineProjectTapCommand = new RelayCommand<OnlineProjectHeader>(OnlineProjectTapAction);
            SettingsCommand = new RelayCommand(SettingsAction);
            CreateNewProjectCommand = new RelayCommand(CreateNewProjectAction);
            EditCurrentProjectCommand = new RelayCommand(EditCurrentProjectAction);
            PlayCurrentProjectCommand = new RelayCommand(PlayCurrentProjectAction);
            UploadCurrentProjectCommand = new RelayCommand(UploadCurrentProjectAction);
            ResetViewModelCommand = new RelayCommand(ResetViewModelAction);
            ShowMessagesCommand = new RelayCommand(ShowMessagesAction);


            Messenger.Default.Register<MessageBase>(this,
                ViewModelMessagingToken.LocalProjectsChangedListener, LocalProjectsChangedMessageAction);

            Messenger.Default.Register<MessageBase>(this,
                ViewModelMessagingToken.DownloadProjectStartedListener, DownloadProjectStartedMessageAction);

            Messenger.Default.Register<MessageBase>(this,
               ViewModelMessagingToken.UploadProjectStartedListener, UploadProjectStartedMessageAction);

            Messenger.Default.Register<GenericMessage<CatrobatContextBase>>(this,
               ViewModelMessagingToken.ContextListener, ContextChangedAction);

            Messenger.Default.Register<GenericMessage<Project>>(this,
                 ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedMessageAction);
        }

        #region MessageBoxCallback

        private void LoadOnlineProjectsCallback(string filterText, List<OnlineProjectHeader> projects, bool append)
        {
            lock (OnlineProjects)
            {
                if (FilterText != filterText && !append)
                    return;

                if (!append)
                {
                    _onlineProjects.Clear();
                }

                IsLoadingOnlineProjects = false;

                foreach (OnlineProjectHeader header in projects)
                {
                    _onlineProjects.Add(header);
                }
            }
        }

        private void DeleteProjectMessageCallback(MessageboxResult result)
        {
            _dialogResult = result;

            if (_dialogResult == MessageboxResult.Ok)
            {
                using (var storage = StorageSystem.GetStorage())
                {
                    storage.DeleteDirectory(CatrobatContextBase.ProjectsPath + "/" + _deleteProjectName);
                }

                if (CurrentProject.ProjectHeader.ProgramName == _deleteProjectName)
                {
                    if (LocalProjects.Count > 0)
                    {
                        var projectName = LocalProjects[0].ProjectName;
                        CurrentProject = CatrobatContext.LoadNewProjectByNameStatic(projectName);
                    }
                    else
                        CurrentProject = CatrobatContext.RestoreDefaultProjectStatic(CatrobatContextBase.DefaultProjectName);
                }
                else
                    UpdateLocalProjects();


                _deleteProjectName = null;
            }
        }

        private void CopyProjectMessageCallback(MessageboxResult result)
        {
            _dialogResult = result;

            if (_dialogResult == MessageboxResult.Ok)
            {
                if (_copyProjectName == CurrentProject.ProjectHeader.ProgramName)
                    CurrentProject.Save();

                CatrobatContext.CopyProject(CurrentProject.ProjectHeader.ProgramName,
                    CurrentProject.ProjectHeader.ProgramName);

                UpdateLocalProjects();
                _copyProjectName = null;
            }
        }

        #endregion


        public void LoadOnlineProjects(bool isAppend, bool isAuto = false)
        {
            if (isAuto && isAppend)
                return;

            IsLoadingOnlineProjects = true;

            if (!isAppend && _previousFilterText != _filterText)
                _onlineProjects.Clear();

            _previousFilterText = _filterText;

            CatrobatWebCommunicationService.LoadOnlineProjects(isAppend, _filterText, _onlineProjects.Count, LoadOnlineProjectsCallback);
        }

        private void CheckTokenEvent(bool registered)
        {
            if (registered)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    ServiceLocator.NavigationService.NavigateTo(typeof(UploadProjectView));
                    ServiceLocator.NavigationService.RemoveBackEntry();
                });
            }
            else
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    ServiceLocator.NavigationService.NavigateTo(typeof(UploadProjectLoginView));
                    ServiceLocator.NavigationService.RemoveBackEntry();
                });
            }
        }

        #region PropertyChanges

        //public void CurrentProjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == PropertyNameHelper.GetPropertyNameFromExpression(() => CurrentProjectScreenshot))
        //        CurrentProjectScreenshot = CurrentProject.ProjectScreenshot as ImageSource;
        //}

        #endregion

        private void ResetViewModel()
        {
        }

        private void UpdateLocalProjects()
        {
            if (IsInDesignMode) return;

            if (CurrentProject == null)
            {
                return;
            }

            if (_localProjects == null)
            {
                _localProjects = new ObservableCollection<ProjectDummyHeader>();
            }

            _localProjects.Clear();

            using (var storage = StorageSystem.GetStorage())
            {
                var projectNames = storage.GetDirectoryNames(CatrobatContextBase.ProjectsPath);

                var projects = new List<ProjectDummyHeader>();

                foreach (string projectName in projectNames)
                {
                    if (projectName != CurrentProject.ProjectHeader.ProgramName)
                    {
                        var screenshotPath = Path.Combine(CatrobatContextBase.ProjectsPath, projectName, Project.ScreenshotPath);
                        var automaticProjectScreenshotPath = Path.Combine(CatrobatContextBase.ProjectsPath, projectName, Project.AutomaticScreenshotPath);
                        PortableImage projectScreenshot = null;

                        if (storage.FileExists(screenshotPath))
                            projectScreenshot = storage.LoadImage(screenshotPath);
                        else if (storage.FileExists(automaticProjectScreenshotPath))
                            projectScreenshot = storage.LoadImage(automaticProjectScreenshotPath);

                        if(projectScreenshot == null)
                            projectScreenshot = new PortableImage();

                        var projectHeader = new ProjectDummyHeader
                        {
                            ProjectName = projectName,
                            Screenshot = projectScreenshot
                        };
                        projects.Add(projectHeader);
                    }
                }
                projects.Sort();
                foreach (ProjectDummyHeader header in projects)
                {
                    _localProjects.Add(header);
                }
            }

            RaisePropertyChanged(() => LocalProjects);
        }
    }
}
