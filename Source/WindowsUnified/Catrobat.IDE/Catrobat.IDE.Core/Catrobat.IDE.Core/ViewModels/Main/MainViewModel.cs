using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        private bool _isLoadingOnlinePrograms;
        private MessageboxResult _dialogResult;
        private string _deleteProgramName;
        private string _copyProgramName;
        private Program _currentProgram;
        private ObservableCollection<LocalProjectHeader> _localProjects;
        private CatrobatContextBase _context;
        private OnlineProgramsCollection _onlinePrograms;
        private OnlineProgramHeader _selectedOnlineProgram;

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
                    OnlinePrograms = designContext.OnlineProjects;
                    CurrentProgram = designContext.CurrentProject;
                }

                RaisePropertyChanged(() => Context);
            }
        }

        public Program CurrentProgram
        {
            get
            {
                return _currentProgram;
            }
            set
            {
                if (value == _currentProgram) 
                    return;

                _currentProgram = value;
                ServiceLocator.DispatcherService.RunOnMainThread(() => RaisePropertyChanged(() => CurrentProgram));
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

                var selectedOnlineProgramChangedMessage = new GenericMessage<OnlineProgramHeader>(SelectedOnlineProgram);
                Messenger.Default.Send(selectedOnlineProgramChangedMessage, ViewModelMessagingToken.SelectedOnlineProgramChangedListener);
            }
        }

        public OnlineProgramsCollection OnlinePrograms
        {
            get { return _onlinePrograms; }
            set
            {
                _onlinePrograms = value;
                RaisePropertyChanged(() => OnlinePrograms);
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
        //            //LoadOnlinePrograms(false);
        //            RaisePropertyChanged(() => FilterText);
        //        }
        //    }
        //}

        public bool IsLoadingOnlinePrograms
        {
            get { return _isLoadingOnlinePrograms; }
            set { _isLoadingOnlinePrograms = value; RaisePropertyChanged(() => IsLoadingOnlinePrograms); }
        }

        #endregion

        #region Commands

        public ICommand OpenProgramCommand { get; private set; }

        public ICommand DeleteLocalProgramCommand { get; private set; }

        public ICommand CopyLocalProgramCommand { get; private set; }

        public ICommand CreateNewProgramCommand { get; private set; }

        public ICommand SettingsCommand { get; private set; }

        public ICommand OnlineProgramTapCommand { get; private set; }

        public ICommand ShowMessagesCommand { get; private set; }

        public ICommand LicenseCommand { get; private set; }

        public ICommand AboutCommand { get; private set; }

        public ICommand TouCommand { get; private set; }

        #endregion

        #region Actions

        private void OpenProgramCommandAction(LocalProjectHeader project)
        {
            var message = new GenericMessage<LocalProjectHeader>(project);
            Messenger.Default.Send(message, 
                ViewModelMessagingToken.CurrentProgramHeaderChangedListener);

            ServiceLocator.NavigationService.NavigateTo<ProgramDetailViewModel>();
        }

        private void DeleteLocalProgramAction(string projectName)
        {
            _deleteProgramName = projectName;

            ServiceLocator.NotifictionService.ShowMessageBox(
                AppResources.Main_MainDeleteProgramDialogTitle,
                String.Format(AppResources.Main_MainDeleteProgramDialogMessage, projectName), 
                DeleteProgramMessageCallback, MessageBoxOptions.OkCancel);
        }

        private void CopyLocalProgramAction(string projectName)
        {
            _copyProgramName = projectName;

            ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_MainCopyProgramDialogTitle,
                String.Format(AppResources.Main_MainCopyProgramDialogMessage, projectName), CopyProgramMessageCallback, MessageBoxOptions.OkCancel);
        }

        private void CreateNewProgramAction()
        {
            ServiceLocator.NavigationService.NavigateTo<AddNewProgramViewModel>();
        }

        private void SettingsAction()
        {
            ServiceLocator.NavigationService.NavigateTo<SettingsViewModel>();
        }

        private void OnlineProgramTapAction(OnlineProgramHeader program)
        {
            SelectedOnlineProgram = program;
            ServiceLocator.NavigationService.NavigateTo<OnlineProgramViewModel>();
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
                    AppResources.Main_DownloadQueueMessage, ToastDisplayDuration.Short, portbleImage);

                _showDownloadMessage = false;
            }
            if (_showUploadMessage)
            {
                var portbleImage = new PortableImage();
                await portbleImage.LoadFromResources(ResourceScope.Ide,
                    "Content/Images/ApplicationBar/dark/appbar.upload.rest.png");

                ServiceLocator.NotifictionService.ShowToastNotification(null,
                    AppResources.Main_UploadQueueMessage, ToastDisplayDuration.Short, portbleImage);

                _showUploadMessage = false;
            }
        }

        #endregion

        #region MessageActions

        private async void LocalProgramsChangedMessageAction(MessageBase message)
        {
            await UpdateLocalPrograms();
        }

        private void DownloadProgramStartedMessageAction(MessageBase message)
        {
            _showDownloadMessage = true;
        }

        private void UploadProgramStartedMessageAction(MessageBase message)
        {
            _showUploadMessage = true;
        }

        private void ContextChangedMessageAction(GenericMessage<CatrobatContextBase> message)
        {
            Context = message.Content;
            if (Context is CatrobatContextDesign)
            {
                LocalProjects = (Context as CatrobatContextDesign).LocalProjects;
                OnlinePrograms = (Context as CatrobatContextDesign).OnlineProjects;
            }
        }

        private void CurrentProgramChangedMessageAction(GenericMessage<Program> message)
        {
            ServiceLocator.DispatcherService.RunOnMainThread(() =>
            {
                CurrentProgram = message.Content;
            });
        }

        #endregion

        public MainViewModel()
        {
            OpenProgramCommand = new RelayCommand<LocalProjectHeader>(OpenProgramCommandAction);
            DeleteLocalProgramCommand = new RelayCommand<string>(DeleteLocalProgramAction);
            CopyLocalProgramCommand = new RelayCommand<string>(CopyLocalProgramAction);
            OnlineProgramTapCommand = new RelayCommand<OnlineProgramHeader>(OnlineProgramTapAction);
            SettingsCommand = new RelayCommand(SettingsAction);
            CreateNewProgramCommand = new RelayCommand(CreateNewProgramAction);
            ShowMessagesCommand = new RelayCommand(ShowMessagesAction);
            LicenseCommand = new RelayCommand(LicenseAction);
            AboutCommand = new RelayCommand(AboutAction);
            TouCommand = new RelayCommand(TouAction);


            Messenger.Default.Register<MessageBase>(this,
                ViewModelMessagingToken.LocalProgramsChangedListener, LocalProgramsChangedMessageAction);

            Messenger.Default.Register<MessageBase>(this,
                ViewModelMessagingToken.DownloadProgramStartedListener, DownloadProgramStartedMessageAction);

            Messenger.Default.Register<MessageBase>(this,
               ViewModelMessagingToken.UploadProgramStartedListener, UploadProgramStartedMessageAction);

            Messenger.Default.Register<GenericMessage<CatrobatContextBase>>(this,
               ViewModelMessagingToken.ContextListener, ContextChangedMessageAction);

            Messenger.Default.Register<GenericMessage<Program>>(this,
                 ViewModelMessagingToken.CurrentProgramChangedListener, CurrentProgramChangedMessageAction);
        }

        #region MessageBoxCallback

        private readonly List<string> _programsToDelete = new List<string>();
        private async void DeleteProgramMessageCallback(MessageboxResult result)
        {
            var deleteProgramName = "";

            lock (_programsToDelete)
            {
                 deleteProgramName = _deleteProgramName;

                if (_localProjects.Any(program => 
                    program.ProjectName == deleteProgramName))
                    return;

                if (_programsToDelete.Contains(deleteProgramName))
                    return;

                _programsToDelete.Add(deleteProgramName);
            }
            
            if (deleteProgramName == null)
                return;

            _dialogResult = result;

            if (_dialogResult == MessageboxResult.Ok)
            {
                if (CurrentProgram != null && CurrentProgram.Name == deleteProgramName)
                {
                    var projectChangedMessage = new GenericMessage<Program>(null);
                    Messenger.Default.Send(projectChangedMessage, ViewModelMessagingToken.CurrentProgramChangedListener);
                }

                using (var storage = StorageSystem.GetStorage())
                {
                    await storage.DeleteDirectoryAsync(StorageConstants.ProgramsPath + "/" + _deleteProgramName);
                }

                await UpdateLocalPrograms();

                _deleteProgramName = null;
            }

            lock (_programsToDelete)
            {
                _programsToDelete.Remove(deleteProgramName);
            }

            //await App.SaveContext(CurrentProgram);
        }

        private bool _isCopying = false;
        private readonly object _copyLock = new object();
        private async void CopyProgramMessageCallback(MessageboxResult result)
        {
            lock (_copyLock)
            {
                if (_isCopying)
                    return;

                _isCopying = true;
            }

            _dialogResult = result;

            if (_dialogResult == MessageboxResult.Ok)
            {
                if (_copyProgramName == CurrentProgram.Name)
                    await CurrentProgram.Save();

                await ServiceLocator.ContextService.CopyProgram(_copyProgramName, _copyProgramName);

                await UpdateLocalPrograms();
                _copyProgramName = null;
            }

            lock (_copyLock)
            {
                _isCopying = false;
            }
        }

        #endregion

        #region PropertyChanges

        //public void CurrentProgramPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == PropertyNameHelper.GetPropertyNameFromExpression(() => CurrentProgramScreenshot))
        //        CurrentProgramScreenshot = CurrentProgram.ProjectScreenshot as ImageSource;
        //}

        #endregion

        private void ResetViewModel()
        {
        }

        private async Task UpdateLocalPrograms()
        {
            if (IsInDesignMode) return;

            if (_localProjects == null)
            {
                _localProjects = new ObservableCollection<LocalProjectHeader>();
            }

            //_localProjects.Clear();

            using (var storage = StorageSystem.GetStorage())
            {
                var projectNames = await storage.GetDirectoryNamesAsync(StorageConstants.ProgramsPath);

                //var projects = new List<ProgramDummyHeader>();

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
                            StorageConstants.ProgramsPath, projectName, StorageConstants.ProgramManualScreenshotPath);
                        var automaticProjectScreenshotPath = Path.Combine(
                            StorageConstants.ProgramsPath, projectName, StorageConstants.ProgramAutomaticScreenshotPath);

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
