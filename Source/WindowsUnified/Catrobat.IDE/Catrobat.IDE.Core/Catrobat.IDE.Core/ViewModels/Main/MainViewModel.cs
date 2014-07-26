using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Resources;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.ViewModels.Service;
using Catrobat.IDE.Core.ViewModels.Settings;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Catrobat.IDE.Core.ViewModels.Main
{
    public class MainViewModel : ViewModelBase
    {
        #region private Members

        private bool _showDownloadMessage;
        private bool _showUploadMessage;
        //private string _filterText = "";
        //private string _previousFilterText = "";
        private bool _isLoadingOnlineProjects;
        private MessageboxResult _dialogResult;
        private string _deleteProjectName;
        private string _copyProjectName;
        private Project _currentProject;
        private ObservableCollection<LocalProjectHeader> _localProjects;
        private CatrobatContextBase _context;
        private OnlineProgramsCollection _onlineProjects;
        //private CancellationTokenSource _taskCancellation;

        #endregion

        #region Properties

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
                if (value == _currentProject) 
                    return;

                _currentProject = value;
                ServiceLocator.DispatcherService.RunOnMainThread(() => RaisePropertyChanged(() => CurrentProject));
            }
        }


        private bool first = true;
        public ObservableCollection<LocalProjectHeader> LocalProjects
        {
            get
            {
                    return _localProjects;
            }
            set
            {
                _localProjects = value;
                RaisePropertyChanged(() => LocalProjects);
            }
        }

        public OnlineProjectHeader SelectedOnlineProject { get; set; }

        public OnlineProgramsCollection OnlineProjects
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

        //public String FilterText
        //{
        //    get
        //    {
        //        return _filterText;
        //    }
        //    set
        //    {
        //        if (_filterText != value)
        //        {
        //            _filterText = value;
        //            //LoadOnlineProjects(false);
        //            RaisePropertyChanged(() => FilterText);
        //        }
        //    }
        //}

        public bool IsLoadingOnlineProjects
        {
            get { return _isLoadingOnlineProjects; }
            set { _isLoadingOnlineProjects = value; RaisePropertyChanged(() => IsLoadingOnlineProjects); }
        }

        #endregion

        #region Commands

        public ICommand OpenProjectCommand { get; private set; }

        public ICommand DeleteLocalProjectCommand { get; private set; }

        public ICommand CopyLocalProjectCommand { get; private set; }

        public ICommand CreateNewProjectCommand { get; private set; }

        public ICommand SettingsCommand { get; private set; }

        public ICommand OnlineProjectTapCommand { get; private set; }

        public ICommand ShowMessagesCommand { get; private set; }

        public ICommand LicenseCommand { get; private set; }

        public ICommand AboutCommand { get; private set; }

        public ICommand TouCommand { get; private set; }

        #endregion

        #region Actions

        private void OpenProjectCommandAction(LocalProjectHeader project)
        {
            var message = new GenericMessage<LocalProjectHeader>(project);
            Messenger.Default.Send(message, 
                ViewModelMessagingToken.CurrentProjectHeaderChangedListener);

            ServiceLocator.NavigationService.NavigateTo<ProjectDetailViewModel>();
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

        private void CreateNewProjectAction()
        {
            ServiceLocator.NavigationService.NavigateTo<AddNewProjectViewModel>();
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

        private void LicenseAction()
        {
            ServiceLocator.NavigationService.NavigateToWebPage(ApplicationResources.CATROBAT_LICENSES_URL);
        }

        private void AboutAction()
        {
            ServiceLocator.NavigationService.NavigateToWebPage(ApplicationResources.CATROBAT_URL);
        }

        private void TouAction()
        {
            ServiceLocator.NavigationService.NavigateToWebPage(ApplicationResources.CATROBAT_TOU_URL);
        }

        protected override void GoBackAction()
        {
            ResetViewModel();
            base.GoBackAction();
        }

        private async void ShowMessagesAction()
        {
            if (_showDownloadMessage)
            {
                var portbleImage = new PortableImage();
                await portbleImage.LoadFromResources(ResourceScope.Ide,
                    "Content/Images/ApplicationBar/dark/appbar.download.rest.png");

                ServiceLocator.NotifictionService.ShowToastNotification(null,
                    AppResources.Main_DownloadQueueMessage, ToastNotificationTime.Short, portbleImage);

                _showDownloadMessage = false;
            }
            if (_showUploadMessage)
            {
                var portbleImage = new PortableImage();
                await portbleImage.LoadFromResources(ResourceScope.Ide,
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

        private void ContextChangedMessageAction(GenericMessage<CatrobatContextBase> message)
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
            ServiceLocator.DispatcherService.RunOnMainThread(() =>
            {
                CurrentProject = message.Content;
            });
        }

        #endregion

        public MainViewModel()
        {
            OpenProjectCommand = new RelayCommand<LocalProjectHeader>(OpenProjectCommandAction);
            DeleteLocalProjectCommand = new RelayCommand<string>(DeleteLocalProjectAction);
            CopyLocalProjectCommand = new RelayCommand<string>(CopyLocalProjectAction);
            OnlineProjectTapCommand = new RelayCommand<OnlineProjectHeader>(OnlineProjectTapAction);
            SettingsCommand = new RelayCommand(SettingsAction);
            CreateNewProjectCommand = new RelayCommand(CreateNewProjectAction);
            ShowMessagesCommand = new RelayCommand(ShowMessagesAction);
            LicenseCommand = new RelayCommand(LicenseAction);
            AboutCommand = new RelayCommand(AboutAction);
            TouCommand = new RelayCommand(TouAction);


            Messenger.Default.Register<MessageBase>(this,
                ViewModelMessagingToken.LocalProjectsChangedListener, LocalProjectsChangedMessageAction);

            Messenger.Default.Register<MessageBase>(this,
                ViewModelMessagingToken.DownloadProjectStartedListener, DownloadProjectStartedMessageAction);

            Messenger.Default.Register<MessageBase>(this,
               ViewModelMessagingToken.UploadProjectStartedListener, UploadProjectStartedMessageAction);

            Messenger.Default.Register<GenericMessage<CatrobatContextBase>>(this,
               ViewModelMessagingToken.ContextListener, ContextChangedMessageAction);

            Messenger.Default.Register<GenericMessage<Project>>(this,
                 ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedMessageAction);

            //_taskCancellation = new CancellationTokenSource();
        }

        #region MessageBoxCallback

        private async void DeleteProjectMessageCallback(MessageboxResult result)
        {
            _dialogResult = result;

            if (_dialogResult == MessageboxResult.Ok)
            {
                if (CurrentProject != null && CurrentProject.ProjectDummyHeader.ProjectName == _deleteProjectName)
                {
                    var projectChangedMessage = new GenericMessage<Project>(null);
                    Messenger.Default.Send(projectChangedMessage, ViewModelMessagingToken.CurrentProjectChangedListener);
                }

                using (var storage = StorageSystem.GetStorage())
                {
                    await storage.DeleteDirectoryAsync(StorageConstants.ProjectsPath + "/" + _deleteProjectName);
                }

                await UpdateLocalProjects();

                _deleteProjectName = null;
            }

            await App.SaveContext(CurrentProject);
        }

        private async void CopyProjectMessageCallback(MessageboxResult result) // TODO: async, should this be awaitable?
        {
            _dialogResult = result;

            if (_dialogResult == MessageboxResult.Ok)
            {
                if (_copyProjectName == CurrentProject.Name)
                    await CurrentProject.Save();

                await ServiceLocator.ContextService.CopyProject(_copyProjectName, _copyProjectName);

                await UpdateLocalProjects();
                _copyProjectName = null;
            }
        }

        #endregion

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

            if (_localProjects == null)
            {
                _localProjects = new ObservableCollection<LocalProjectHeader>();
            }

            //_localProjects.Clear();

            using (var storage = StorageSystem.GetStorage())
            {
                var projectNames = await storage.GetDirectoryNamesAsync(StorageConstants.ProjectsPath);

                //var projects = new List<ProjectDummyHeader>();

                var projectsToRemove = new List<LocalProjectHeader>();

                foreach (var header in _localProjects)
                {
                    var found = false;
                    foreach (string projectName in projectNames)
                        if (header.ProjectName == projectName)
                            found = true;

                    if (!found)
                        projectsToRemove.Add(header);
                }

                //foreach (var header in _localProjects)
                //{
                //    if (header.ProjectName == CurrentProject.ProjectDummyHeader.ProjectName)
                //        projectsToRemove.Add(header);
                //}

                foreach (var project in projectsToRemove)
                {
                    _localProjects.Remove(project);
                }


                var projectsToAdd = new List<LocalProjectHeader>();

                foreach (string projectName in projectNames)
                {
                    var exists = false;

                    foreach (var header in _localProjects)
                    {
                        if (header.ProjectName == projectName)
                            exists = true;
                    }

                    if (!exists)
                    {
                        var manualScreenshotPath = Path.Combine(
                            StorageConstants.ProjectsPath, projectName, Project.ScreenshotPath);
                        var automaticProjectScreenshotPath = Path.Combine(
                            StorageConstants.ProjectsPath, projectName, Project.AutomaticScreenshotPath);

                        var projectScreenshot = new PortableImage();
                        projectScreenshot.LoadAsync(manualScreenshotPath, automaticProjectScreenshotPath, false);

                        var projectHeader = new LocalProjectHeader
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
            }
        }
    }
}
