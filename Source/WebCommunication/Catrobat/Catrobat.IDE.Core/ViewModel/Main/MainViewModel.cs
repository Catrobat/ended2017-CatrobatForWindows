using System.IO;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.ViewModel.Editor;
using Catrobat.IDE.Core.ViewModel.Editor.Sprites;
using Catrobat.IDE.Core.ViewModel.Service;
using Catrobat.IDE.Core.ViewModel.Settings;
using Catrobat.IDE.Core.ViewModel.Share;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;


namespace Catrobat.IDE.Core.ViewModel.Main
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
        private ProjectDummyHeader _selectdLocalProject;

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

        public ProjectDummyHeader SelectedLocalProject
        {
            get { return _selectdLocalProject; }
            set
            {
                _selectdLocalProject = value;
                RaisePropertyChanged(() => SelectedLocalProject);
            }
        }


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

        //public ICommand LazyLoadOnlineProjectsCommand
        //{
        //    get;
        //    private set;
        //}

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

        public ICommand ShowMessagesCommand
        {
            get;
            private set;
        }

        #endregion

        # region Actions

        private void RenameLocalProjectAction(ProjectDummyHeader project)
        {
            if (project == null)
                project = SelectedLocalProject;

            var message = new GenericMessage<ProjectDummyHeader>(project);
            Messenger.Default.Send(message, ViewModelMessagingToken.ChangeLocalProjectListener);

            ServiceLocator.NavigationService.NavigateTo<ProjectSettingsViewModel>();
        }

        private void DeleteLocalProjectAction(string projectName)
        {
            if (projectName == null)
                projectName = SelectedLocalProject.ProjectName;

            _deleteProjectName = projectName;

            ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_MainDeleteProjectDialogTitle,
                String.Format(AppResources.Main_MainDeleteProjectDialogMessage, projectName), DeleteProjectMessageCallback, MessageBoxOptions.OkCancel);
        }

        private void CopyLocalProjectAction(string projectName)
        {
            if (projectName == null)
                projectName = SelectedLocalProject.ProjectName;

            _copyProjectName = projectName;

            ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_MainCopyProjectDialogTitle,
                String.Format(AppResources.Main_MainCopyProjectDialogMessage, projectName), CopyProjectMessageCallback, MessageBoxOptions.OkCancel);
        }

        private void PinLocalProjectAction(ProjectDummyHeader project)
        {
            if (project == null)
                project = SelectedLocalProject;

            PinProjectHeader = project;

            var message = new GenericMessage<ProjectDummyHeader>(PinProjectHeader);
            Messenger.Default.Send(message, ViewModelMessagingToken.PinProjectHeaderListener);

            ServiceLocator.NavigationService.NavigateTo<TileGeneratorViewModel>();
        }

        private async void ShareLocalProjectAction(ProjectDummyHeader project)
        {
            if (project == null)
                project = SelectedLocalProject;

            if (CurrentProject.ProjectDummyHeader == project)
                await CurrentProject.Save();

            PinProjectHeader = project;

            var message = new GenericMessage<ProjectDummyHeader>(PinProjectHeader);
            Messenger.Default.Send(message, ViewModelMessagingToken.ShareProjectHeaderListener);

            ServiceLocator.NavigationService.NavigateTo<ShareProjectServiceSelectionViewModel>();
        }

        //private void LazyLoadOnlineProjectsAction()
        //{
        //    LoadOnlineProjects(true);
        //}


        private async void SetCurrentProjectAction(string projectName)
        {
            lock (CurrentProject)
            {
                if (IsActivatingLocalProject)
                    return;

                IsActivatingLocalProject = true;
            }

            var minLoadingTime = new TimeSpan(0, 0, 0, 0, 500);
            DateTime startTime = DateTime.UtcNow;

            await CurrentProject.Save();
            var newProject = await CatrobatContext.LoadNewProjectByNameStatic(projectName);

            if (newProject != null)
            {
                CurrentProject = newProject;

                var minWaitingTimeRemaining = minLoadingTime.Subtract(DateTime.UtcNow.Subtract(startTime));

                if (minWaitingTimeRemaining >= new TimeSpan(0))
                    Task.Delay(minWaitingTimeRemaining).Wait();
                //Thread.Sleep(minWaitingTimeRemaining);

                IsActivatingLocalProject = false;


            }
            else
            {
                XmlParserTempProjectHelper.Project = CurrentProject;

                ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_SelectedProjectNotValidMessage,
                    String.Format(AppResources.Main_SelectedProjectNotValidHeader, projectName), new Action<MessageboxResult>(delegate
                    {
                        ServiceLocator.DispatcherService.RunOnMainThread(() =>
                        {
                            IsActivatingLocalProject = false;
                        });
                    }), MessageBoxOptions.Ok);
            }

            await Core.App.SaveContext(CurrentProject);
        }

        private void CreateNewProjectAction()
        {
            ServiceLocator.NavigationService.NavigateTo<AddNewProjectViewModel>();
        }

        private void EditCurrentProjectAction()
        {
            ServiceLocator.NavigationService.NavigateTo<SpritesViewModel>();
        }

        private void SettingsAction()
        {
            ServiceLocator.NavigationService.NavigateTo<SettingsViewModel>();
        }

        private void OnlineProjectTapAction(OnlineProjectHeader project)
        {
            SelectedOnlineProject = project;
            ServiceLocator.NavigationService.NavigateTo<OnlineProjectViewModel>();
        }

        private void PlayCurrentProjectAction()
        {
            ServiceLocator.PlayerLauncherService.LaunchPlayer(CurrentProject);
        }

        private async void UploadCurrentProjectAction()
        {
            ServiceLocator.NavigationService.NavigateTo<UploadProjectLoadingViewModel>();

            // Determine which page to open
            bool registered = await CatrobatWebCommunicationService.AsyncCheckToken(Context.CurrentUserName, Context.CurrentToken);

            if (registered)
            {
                ServiceLocator.DispatcherService.RunOnMainThread(() =>
                {
                    ServiceLocator.NavigationService.NavigateTo<UploadProjectViewModel>();
                    ServiceLocator.NavigationService.RemoveBackEntry();
                });
            }
            else
            {
                ServiceLocator.DispatcherService.RunOnMainThread(() =>
                {
                    ServiceLocator.NavigationService.NavigateTo<UploadProjectLoginViewModel>();
                    ServiceLocator.NavigationService.RemoveBackEntry();
                });
            }
        }

        protected override void GoBackAction()
        {
            ResetViewModel();
            //base.GoBackAction();
        }

        private async void ShowMessagesAction()
        {
            if (_showDownloadMessage)
            {
                var portbleImage = new PortableImage();
                await portbleImage.LoadFromResources(ResourceScope.IdePhone,
                    "Content/Images/ApplicationBar/dark/appbar.download.rest.png");

                ServiceLocator.NotifictionService.ShowToastNotification(null,
                    AppResources.Main_DownloadQueueMessage, ToastNotificationTime.Short, portbleImage);

                _showDownloadMessage = false;
            }
            if (_showUploadMessage)
            {
                var portbleImage = new PortableImage();
                await portbleImage.LoadFromResources(ResourceScope.IdePhone,
                    "Content/Images/ApplicationBar/dark/appbar.upload.rest.png");

                ServiceLocator.NotifictionService.ShowToastNotification(null,
                    AppResources.Main_UploadQueueMessage, ToastNotificationTime.Short, portbleImage);

                _showUploadMessage = false;
            }
        }

        #endregion

        #region MessageActions

        private async void LocalProjectsChangedMessageAction(MessageBase message)
        {
            await UpdateLocalProjects();
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
            //LazyLoadOnlineProjectsCommand = new RelayCommand(LazyLoadOnlineProjectsAction);
            SetCurrentProjectCommand = new RelayCommand<string>(SetCurrentProjectAction);
            OnlineProjectTapCommand = new RelayCommand<OnlineProjectHeader>(OnlineProjectTapAction);
            SettingsCommand = new RelayCommand(SettingsAction);
            CreateNewProjectCommand = new RelayCommand(CreateNewProjectAction);
            EditCurrentProjectCommand = new RelayCommand(EditCurrentProjectAction);
            PlayCurrentProjectCommand = new RelayCommand(PlayCurrentProjectAction);
            UploadCurrentProjectCommand = new RelayCommand(UploadCurrentProjectAction);
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

        private async void DeleteProjectMessageCallback(MessageboxResult result)
        {
            _dialogResult = result;

            if (_dialogResult == MessageboxResult.Ok)
            {
                using (var storage = StorageSystem.GetStorage())
                {
                    await storage.DeleteDirectoryAsync(CatrobatContextBase.ProjectsPath + "/" + _deleteProjectName);
                }

                if (CurrentProject.ProjectHeader.ProgramName == _deleteProjectName)
                {
                    if (LocalProjects.Count > 0)
                    {
                        var projectName = LocalProjects[0].ProjectName;
                        CurrentProject = await CatrobatContext.LoadNewProjectByNameStatic(projectName);
                    }
                    else
                        CurrentProject = await CatrobatContext.RestoreDefaultProjectStatic(CatrobatContextBase.DefaultProjectName);
                }
                else
                    await UpdateLocalProjects();


                _deleteProjectName = null;
            }

            await Core.App.SaveContext(CurrentProject);
        }

        private async void CopyProjectMessageCallback(MessageboxResult result) // TODO: async, should this be awaitable?
        {
            _dialogResult = result;

            if (_dialogResult == MessageboxResult.Ok)
            {
                if (_copyProjectName == CurrentProject.ProjectHeader.ProgramName)
                    await CurrentProject.Save();

                await CatrobatContext.CopyProject(CurrentProject.ProjectHeader.ProgramName,
                    CurrentProject.ProjectHeader.ProgramName);

                await UpdateLocalProjects();
                _copyProjectName = null;
            }
        }

        #endregion


        public async void LoadOnlineProjects(bool isAppend, bool isAuto = false)
        {
            if (isAuto && isAppend)
                return;

            IsLoadingOnlineProjects = true;

            if (!isAppend && _previousFilterText != _filterText)
                _onlineProjects.Clear();

            _previousFilterText = _filterText;
            List<OnlineProjectHeader> projects = await CatrobatWebCommunicationService.AsyncLoadOnlineProjects(isAppend, _filterText, _onlineProjects.Count);

            lock (OnlineProjects)
            {
                if (FilterText != _filterText && !isAppend)
                    return;

                if (!isAppend)
                    _onlineProjects.Clear();
                
                IsLoadingOnlineProjects = false;

                if (projects != null)
                {
                    foreach (OnlineProjectHeader header in projects)
                    {
                        _onlineProjects.Add(header);
                    }
                }
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

        private async Task UpdateLocalProjects()
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

            //_localProjects.Clear();

            using (var storage = StorageSystem.GetStorage())
            {
                var projectNames = await storage.GetDirectoryNamesAsync(CatrobatContextBase.ProjectsPath);

                //var projects = new List<ProjectDummyHeader>();

                var projectsToRemove = new List<ProjectDummyHeader>();

                foreach (var header in _localProjects)
                {
                    var found = false;
                    foreach (string projectName in projectNames)
                        if (header.ProjectName == projectName)
                            found = true;

                    if (!found)
                        projectsToRemove.Add(header);
                }

                foreach (var header in _localProjects)
                {
                    if (header.ProjectName == CurrentProject.ProjectDummyHeader.ProjectName)
                        projectsToRemove.Add(header);
                }

                foreach (var project in projectsToRemove)
                {
                    _localProjects.Remove(project);
                }


                var projectsToAdd = new List<ProjectDummyHeader>();

                foreach (string projectName in projectNames)
                {
                    var exists = false;

                    foreach (var header in _localProjects)
                    {
                        if (header.ProjectName == projectName)
                            exists = true;
                    }

                    if (!exists && projectName != CurrentProject.ProjectDummyHeader.ProjectName)
                    {
                        var manualScreenshotPath = Path.Combine(
                            CatrobatContextBase.ProjectsPath, projectName, Project.ScreenshotPath);
                        var automaticProjectScreenshotPath = Path.Combine(
                            CatrobatContextBase.ProjectsPath, projectName, Project.AutomaticScreenshotPath);

                        var projectScreenshot = new PortableImage();
                        projectScreenshot.LoadAsync(manualScreenshotPath, automaticProjectScreenshotPath, false);

                        var projectHeader = new ProjectDummyHeader
                        {
                            ProjectName = projectName,
                            Screenshot = projectScreenshot
                        };

                        _localProjects.Insert(0, projectHeader);
                    }
                }

                projectsToAdd.Sort();

                foreach (var project in projectsToAdd)
                {
                    _localProjects.Insert(0, project);
                }

                //foreach (string projectName in projectNames)
                //{
                //    if (projectName != CurrentProject.ProjectHeader.ProgramName)
                //    {
                //        var manualScreenshotPath = Path.Combine(
                //            CatrobatContextBase.ProjectsPath, projectName, Project.ScreenshotPath);
                //        var automaticProjectScreenshotPath = Path.Combine(
                //            CatrobatContextBase.ProjectsPath, projectName, Project.AutomaticScreenshotPath);

                //        var projectScreenshot = new PortableImage();
                //        projectScreenshot.LoadAsync(manualScreenshotPath, automaticProjectScreenshotPath, false);


                //        var projectHeader = new ProjectDummyHeader
                //        {
                //            ProjectName = projectName,
                //            Screenshot = projectScreenshot
                //        };
                //        projects.Add(projectHeader);
                //    }
                //}
                //projects.Sort();
                //foreach (ProjectDummyHeader header in projects)
                //{
                //    _localProjects.Add(header);
                //}
            }

            RaisePropertyChanged(() => LocalProjects);
        }
    }
}
