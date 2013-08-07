using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Catrobat.Core;
using Catrobat.Core.Misc;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Misc.ServerCommunication;
using Catrobat.Core.Objects;
using Catrobat.Core.Resources;
using Catrobat.Core.Storage;
using Catrobat.IDEWindowsPhone.Content.Localization;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.Themes;
using Catrobat.IDEWindowsPhone.Views.Main;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Media;
using System.Windows.Input;
using Catrobat.IDEWindowsPhone.Views.Service;
using Catrobat.IDEWindowsPhone.Views.Settings;
using Catrobat.IDEWindowsPhone.Views.Editor;

namespace Catrobat.IDEWindowsPhone.ViewModel.Main
{
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {
        #region private Members

        private string _filterText = "";
        private bool _isLoadingOnlineProjects;
        private MessageBoxResult _dialogResult;
        private string _deleteProjectName;
        private string _copyProjectName;
        private Project _currentProject;
        private ImageSource _currentProjectScreenshot;
        private ObservableCollection<ProjectDummyHeader> _localProjects;
        private CatrobatContextBase _context;
        private ObservableCollection<OnlineProjectHeader> _onlineProjects;

        #endregion

        #region Properties

        public CatrobatContextBase Context
        {
            get { return _context; }
            set { _context = value; RaisePropertyChanged(() => Context); }
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
                ProjectHolder.Project = _currentProject;

                var projectChangedMessage = new GenericMessage<Project>(CurrentProject);
                Messenger.Default.Send<GenericMessage<Project>>(projectChangedMessage,
                    ViewModelMessagingToken.CurrentProjectChangedListener);

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        public ProjectDummyHeader PinProjectHeader { get; set; }

        public ImageSource CurrentProjectScreenshot
        {
            get
            {
                return _currentProjectScreenshot;
            }

            set
            {
                if (_currentProjectScreenshot == value) return;

                _currentProjectScreenshot = value;
                RaisePropertyChanged(() => CurrentProjectScreenshot);
            }
        }

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
                    PlatformInformationHelper.CurrentApplicationVersion);
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

        public bool IsActiveatingLocalProject
        {
            get
            {
                return _isLoadingOnlineProjects;
            }
            set
            {
                _isLoadingOnlineProjects = value;
                RaisePropertyChanged(() => IsActiveatingLocalProject);
            }
        }

        #endregion

        #region Commands

        public ICommand RenameCurrentProjectCommand
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

        #endregion

        # region Actions

        private void RenameCurrentProjectAction()
        {
            var message = new GenericMessage<Project>(CurrentProject);
            Messenger.Default.Send<GenericMessage<Project>>(message, ViewModelMessagingToken.ProjectNameListener);

            Navigation.NavigateTo(typeof(ProjectSettingsView));
        }

        private void DeleteLocalProjectAction(string projectName)
        {
            _deleteProjectName = projectName;

            var message = new DialogMessage(String.Format(AppResources.Main_MainDeleteProjectDialogMessage, projectName), DeleteProjectMessageCallback)
            {
                Button = MessageBoxButton.OKCancel,
                Caption = AppResources.Main_MainDeleteProjectDialogTitle
            };

            Messenger.Default.Send(message);
        }

        private void CopyLocalProjectAction(string projectName)
        {
            _copyProjectName = projectName;

            var message = new DialogMessage(String.Format(AppResources.Main_MainCopyProjectDialogMessage, projectName), CopyProjectMessageCallback)
            {
                Button = MessageBoxButton.OKCancel,
                Caption = AppResources.Main_MainCopyProjectDialogTitle
            };

            Messenger.Default.Send(message);
        }

        private void PinLocalProjectAction(ProjectDummyHeader project)
        {
            PinProjectHeader = project;
            Navigation.NavigateTo(typeof(TileGeneratorView));
        }

        private void LazyLoadOnlineProjectsAction()
        {
            LoadOnlineProjects(true);
        }


        private void SetCurrentProjectAction(string projectName)
        {
            lock (CurrentProject)
            {
                if (IsActiveatingLocalProject)
                    return;

                IsActiveatingLocalProject = true;
            }

            var minLoadingTime = new TimeSpan(0, 0, 0, 0, 500);
            DateTime startTime = DateTime.UtcNow;


            var setActiveTask = Task.Run(() =>
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    try
                    {
                        CurrentProject = CatrobatContext.CreateNewProjectByNameStatic(projectName);
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                    var minWaitindTimeRemaining = minLoadingTime.Subtract(DateTime.UtcNow.Subtract(startTime));

                    if (minWaitindTimeRemaining >= new TimeSpan(0))
                        Thread.Sleep(minWaitindTimeRemaining);

                    IsActiveatingLocalProject = false;
                });

            });
        }

        private void CreateNewProjectAction()
        {
            Navigation.NavigateTo(typeof(AddNewProjectView));
        }

        private void EditCurrentProjectAction()
        {
            Navigation.NavigateTo(typeof(EditorLoadingView));
        }

        private void SettingsAction()
        {
            Navigation.NavigateTo(typeof(SettingsView));
        }

        private void OnlineProjectTapAction(OnlineProjectHeader project)
        {
            SelectedOnlineProject = project;
            Navigation.NavigateTo(typeof(OnlineProjectView));
        }

        private void PlayCurrentProjectAction()
        {
            PlayerLauncher.LaunchPlayer(CurrentProject.ProjectHeader.ProgramName);
        }

        private void UploadCurrentProjectAction()
        {
            Navigation.NavigateTo(typeof(UploadProjectsLoadingView));

            // Determine which page to open
            Task.Run(() => ServerCommunication.CheckToken(Context.CurrentToken, CheckTokenEvent));
        }

        private void ResetViewModelAction()
        {
            ResetViewModel();
        }

        #endregion

        #region MessageActions

        private void LocalProjectsChangedMessageAction(MessageBase message)
        {
            UpdateLocalProjects();
        }

        private void ContextChangedAction(GenericMessage<CatrobatContextBase> message)
        {
            Context = message.Content;
            if (Context is CatrobatContextDesign)
            {
                LocalProjects = (Context as CatrobatContextDesign).LocalProjects;
                OnlineProjects = (Context as CatrobatContextDesign).OnlineProjects;
            }
            else
            {
                //Context.CurrentProject.PropertyChanged += CurrentProjectPropertyChanged;
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


            RenameCurrentProjectCommand = new RelayCommand(RenameCurrentProjectAction);
            DeleteLocalProjectCommand = new RelayCommand<string>(DeleteLocalProjectAction);
            CopyLocalProjectCommand = new RelayCommand<string>(CopyLocalProjectAction);
            PinLocalProjectCommand = new RelayCommand<ProjectDummyHeader>(PinLocalProjectAction);
            LazyLoadOnlineProjectsCommand = new RelayCommand(LazyLoadOnlineProjectsAction);
            SetCurrentProjectCommand = new RelayCommand<string>(SetCurrentProjectAction);
            OnlineProjectTapCommand = new RelayCommand<OnlineProjectHeader>(OnlineProjectTapAction);
            SettingsCommand = new RelayCommand(SettingsAction);
            CreateNewProjectCommand = new RelayCommand(CreateNewProjectAction);
            EditCurrentProjectCommand = new RelayCommand(EditCurrentProjectAction);
            PlayCurrentProjectCommand = new RelayCommand(PlayCurrentProjectAction);
            UploadCurrentProjectCommand = new RelayCommand(UploadCurrentProjectAction);
            ResetViewModelCommand = new RelayCommand(ResetViewModelAction);


            Messenger.Default.Register<MessageBase>(this,
                ViewModelMessagingToken.LocalProjectsChangedListener, LocalProjectsChangedMessageAction);
            Messenger.Default.Register<GenericMessage<CatrobatContextBase>>(this,
                             ViewModelMessagingToken.ContextListener, ContextChangedAction);
            Messenger.Default.Register<GenericMessage<Project>>(this,
                 ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedMessageAction);
        }

        #region MessageBoxCallback

        private void LoadOnlineProjectsCallback(List<OnlineProjectHeader> projects, bool append)
        {
            lock (OnlineProjects)
            {
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

        private void DeleteProjectMessageCallback(MessageBoxResult result)
        {
            _dialogResult = result;

            if (_dialogResult == MessageBoxResult.OK)
            {
                using (var storage = StorageSystem.GetStorage())
                {
                    storage.DeleteDirectory(CatrobatContextBase.ProjectsPath + "/" + _deleteProjectName);
                }

                if (CurrentProject.ProjectHeader.ProgramName == _deleteProjectName)
                {
                    CurrentProject = CatrobatContext.RestoreDefaultProjectStatic(CatrobatContextBase.DefaultProjectName);
                }
                else
                    UpdateLocalProjects();


                _deleteProjectName = null;
            }
        }

        private void CopyProjectMessageCallback(MessageBoxResult result)
        {
            _dialogResult = result;

            if (_dialogResult == MessageBoxResult.OK)
            {
                using (var storage = StorageSystem.GetStorage())
                {
                    var sourcePath = Path.Combine(CatrobatContextBase.ProjectsPath, _copyProjectName);
                    var newProjectName = _copyProjectName;
                    var destinationPath = Path.Combine(CatrobatContextBase.ProjectsPath, newProjectName);

                    var counter = 1;
                    while (storage.DirectoryExists(destinationPath))
                    {
                        newProjectName = _copyProjectName + counter;
                        destinationPath = Path.Combine(CatrobatContextBase.ProjectsPath, newProjectName);
                        counter++;
                    }

                    storage.CopyDirectory(sourcePath, destinationPath);

                    var tempXmlPath = Path.Combine(destinationPath, Project.ProjectCodePath);
                    var xml = storage.ReadTextFile(tempXmlPath);
                    var newProject = new Project(xml);
                    newProject.SetProgramName(newProjectName);
                    newProject.Save();
                }

                UpdateLocalProjects();

                _copyProjectName = null;
            }
        }

        #endregion


        public void LoadOnlineProjects(bool append)
        {
            IsLoadingOnlineProjects = true;

            if(!append)
                _onlineProjects.Clear();

            ServerCommunication.LoadOnlineProjects(append, _filterText, _onlineProjects.Count, LoadOnlineProjectsCallback);
        }

        private void CheckTokenEvent(bool registered)
        {
            if (registered)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    Navigation.NavigateTo(typeof(UploadProjectView));
                    Navigation.RemoveBackEntry();
                });
            }
            else
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    Navigation.NavigateTo(typeof(UploadProjectLoginView));
                    Navigation.RemoveBackEntry();
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
                        var projectScreenshot = storage.LoadImage(screenshotPath);
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
        }
    }
}
