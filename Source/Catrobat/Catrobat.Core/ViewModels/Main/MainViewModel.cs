using System.Linq;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Models;
using Catrobat.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
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
using System.Threading.Tasks;
using System.Windows.Input;
using Catrobat.Core.Resources;
using Catrobat.Core.ViewModels.Main.OnlinePrograms;

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
        private ObservableCollection<LocalProgramHeader> _localPrograms;
        private CatrobatContextBase _context;
        private OnlineProgramsCollection _onlinePrograms;
        private OnlineProgramHeader _selectedOnlineProgram;
        private string _lastImportedProgram;
        private string _lastUploadedProgram;

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
                    LocalPrograms = designContext.LocalProjects;
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
        public ObservableCollection<LocalProgramHeader> LocalPrograms
        {
            get
            {
                return _localPrograms;
            }
            set
            {
                _localPrograms = value;
                RaisePropertyChanged(() => LocalPrograms);
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
                var name = String.Format(AppResourcesHelper.Get("Main_ApplicationNameAndVersion"),
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

        public ProgramsViewModel OnlineProgramsViewModel { get; set; }

        #endregion

        #region Commands

        public ICommand OpenProgramCommand { get; private set; }

        public ICommand DeleteLocalProgramCommand { get; private set; }

        public ICommand CopyLocalProgramCommand { get; private set; }

        public ICommand CreateNewProgramCommand { get; private set; }

        public ICommand SettingsCommand { get; private set; }

        public ICommand OnlineProgramTapCommand { get; private set; }

        public ICommand ShowMessagesCommand { get; private set; }

        public ICommand AboutCommand { get; private set; }

        public ICommand LicenseCommand { get; private set; }

        #endregion

        #region Actions

        private void OpenProgramCommandAction(LocalProgramHeader program)
        {
            if (program.IsDeleting || program.IsLoading)
                return;

            var message = new GenericMessage<LocalProgramHeader>(program);
            Messenger.Default.Send(message,
                ViewModelMessagingToken.CurrentProgramHeaderChangedListener);

            ServiceLocator.NavigationService.NavigateTo<ProgramDetailViewModel>();
        }

        private void DeleteLocalProgramAction(string programName)
        {
            _deleteProgramName = programName;

            ServiceLocator.NotifictionService.ShowMessageBox(
                AppResourcesHelper.Get("Main_MainDeleteProgramDialogTitle"),
                String.Format(AppResourcesHelper.Get("Main_MainDeleteProgramDialogMessage"), programName),
                DeleteProgramMessageCallback, MessageBoxOptions.OkCancel);
        }

        private void CopyLocalProgramAction(string programName)
        {
            _copyProgramName = programName;

            ServiceLocator.NotifictionService.ShowMessageBox(AppResourcesHelper.Get("Main_MainCopyProgramDialogTitle"),
                String.Format(AppResourcesHelper.Get("Main_MainCopyProgramDialogMessage"), programName),
                CopyProgramMessageCallback, MessageBoxOptions.OkCancel);
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

        private void AboutAction()
        {
            ServiceLocator.NavigationService.NavigateToWebPage(
                ApplicationResourcesHelper.Get("CATROBAT_URL"));
        }

        private void LicenseAction()
        {
            ServiceLocator.NavigationService.NavigateTo<InformationViewModel>();
        }

        protected override void GoBackAction()
        {
            ResetViewModel();
            base.GoBackAction();
        }

        private void ShowMessagesAction()
        {
            string notificationString = "";
            int length = 15;
            if (_showDownloadMessage)
            {
                //var portbleImage = new PortableImage();
                //await portbleImage.LoadFromResources(ResourceScope.Ide,
                //    "Content/Images/ApplicationBar/dark/appbar.download.rest.png");
                if (_lastImportedProgram.Length > length)
                {
                    _lastImportedProgram = _lastImportedProgram.Substring(0, length) + "...";
                }
                notificationString = String.Format(AppResourcesHelper.Get("Main_DownloadQueueMessage"), _lastImportedProgram);

                ServiceLocator.NotifictionService.ShowToastNotification("",
                    notificationString, ToastDisplayDuration.Short, ToastTag.Default);
                _showDownloadMessage = false;
            }
            if (_showUploadMessage)
            {
                //var portbleImage = new PortableImage();
                //await portbleImage.LoadFromResources(ResourceScope.Ide,
                //    "Content/Images/ApplicationBar/dark/appbar.upload.rest.png");
                if (_lastUploadedProgram.Length > length)
                {
                    _lastUploadedProgram = _lastUploadedProgram.Substring(0, length) + "...";
                }
                notificationString = String.Format(AppResourcesHelper.Get("Main_UploadQueueMessage"), _lastUploadedProgram);
                ServiceLocator.NotifictionService.ShowToastNotification("",
                    notificationString, ToastDisplayDuration.Short, ToastTag.Default);
                _showUploadMessage = false;
            }
        }

        #endregion

        #region MessageActions

        private async void LocalProgramsChangedMessageAction(MessageBase message)
        {
            ServiceLocator.DispatcherService.RunOnMainThread(async () =>
            {
                await UpdateLocalPrograms();
            });
        }

        private void DownloadProgramStartedMessageAction(GenericMessage<string> message)
        {
            _showDownloadMessage = true;
            _lastImportedProgram = message.Content;
        }

        private void UploadProgramStartedMessageAction(GenericMessage<string> message)
        {
            _showUploadMessage = true;
            _lastUploadedProgram = message.Content;
        }

        private void ContextChangedMessageAction(GenericMessage<CatrobatContextBase> message)
        {
            Context = message.Content;
            if (Context is CatrobatContextDesign)
            {
                LocalPrograms = (Context as CatrobatContextDesign).LocalProjects;
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

        private void ToastNotificationActivatedMessageAction(GenericMessage<ToastTag> message)
        {
            if (message.Content == ToastTag.ImportFin)
            {
                ServiceLocator.NavigationService.NavigateTo(this.GetType());
            }
        }

        #endregion

        public MainViewModel()
        {
            OpenProgramCommand = new RelayCommand<LocalProgramHeader>(OpenProgramCommandAction);
            DeleteLocalProgramCommand = new RelayCommand<string>(DeleteLocalProgramAction);
            CopyLocalProgramCommand = new RelayCommand<string>(CopyLocalProgramAction);
            OnlineProgramTapCommand = new RelayCommand<OnlineProgramHeader>(OnlineProgramTapAction);
            SettingsCommand = new RelayCommand(SettingsAction);
            CreateNewProgramCommand = new RelayCommand(CreateNewProgramAction);
            ShowMessagesCommand = new RelayCommand(ShowMessagesAction);
            AboutCommand = new RelayCommand(AboutAction);
            LicenseCommand = new RelayCommand(LicenseAction);

            Messenger.Default.Register<MessageBase>(this,
                ViewModelMessagingToken.LocalProgramsChangedListener, LocalProgramsChangedMessageAction);

            Messenger.Default.Register<GenericMessage<string>>(this,
                ViewModelMessagingToken.DownloadProgramStartedListener, DownloadProgramStartedMessageAction);

            Messenger.Default.Register<GenericMessage<string>>(this,
               ViewModelMessagingToken.UploadProgramStartedListener, UploadProgramStartedMessageAction);

            Messenger.Default.Register<GenericMessage<CatrobatContextBase>>(this,
               ViewModelMessagingToken.ContextListener, ContextChangedMessageAction);

            Messenger.Default.Register<GenericMessage<Program>>(this,
                 ViewModelMessagingToken.CurrentProgramChangedListener, CurrentProgramChangedMessageAction);

            Messenger.Default.Register<GenericMessage<ToastTag>>(this,
                ViewModelMessagingToken.ToastNotificationActivated, ToastNotificationActivatedMessageAction);

            OnlineProgramsViewModel = new ProgramsViewModel();
        }

        #region MessageBoxCallback

        private readonly List<string> _programsToDelete = new List<string>();
        private async void DeleteProgramMessageCallback(MessageboxResult result)
        {
            _dialogResult = result;
            if (_dialogResult == MessageboxResult.Ok)
            {
                await DeleteProgram(_deleteProgramName);
            }
        }

        private bool _isDeleting = false;

        private async Task DeleteProgram(string programNameToDelete)
        {
            DateTime deleteStartTime;

            lock (_programsToDelete)
            {
                if (_localPrograms.All(program =>
                    program.ProjectName != programNameToDelete))
                    return;

                var programToDelete = _localPrograms.FirstOrDefault(program =>
                    program.ProjectName == programNameToDelete);

                if (programToDelete == null ||
                    _programsToDelete.Contains(programNameToDelete))
                    return;

                _programsToDelete.Add(programNameToDelete);
                programToDelete.IsDeleting = true;
                deleteStartTime = DateTime.UtcNow;

                _programsToDelete.Add(programNameToDelete);

                if (_isDeleting)
                    return;

                _isDeleting = true;
            }

            while (true)
            {
                List<string> programNames = null;
                lock (_programsToDelete)
                {
                    if (_programsToDelete.Count == 0)
                    {
                        _isDeleting = false;
                        break;
                    }

                    programNames = new List<string>(_programsToDelete);
                    _programsToDelete.Clear();
                }

                foreach (var programName in programNames)
                {
                    if (CurrentProgram != null && CurrentProgram.Name == programName)
                    {
                        var programChangedMessage = new GenericMessage<Program>(null);
                        Messenger.Default.Send(programChangedMessage, 
                            ViewModelMessagingToken.CurrentProgramChangedListener);
                    }

                    using (var storage = StorageSystem.GetStorage())
                    {
                        await storage.DeleteDirectoryAsync(Path.Combine(
                            StorageConstants.ProgramsPath, _deleteProgramName));
                    }
                }

                var minDeleteTime = new TimeSpan(0, 0, 2);
                var remainingDeleteTime = minDeleteTime.Subtract(
                    DateTime.UtcNow.Subtract(deleteStartTime));

                if (remainingDeleteTime > new TimeSpan(0))
                    await Task.Delay(remainingDeleteTime);
                await UpdateLocalPrograms();
            }
        }

        private bool _isCopying = false;
        private readonly object _copyLock = new object();

        private async void CopyProgramMessageCallback(MessageboxResult result)
        {
            DateTime copyStartTime;

            lock (_copyLock)
            {
                if (_isCopying)
                    return;

                _isCopying = true;
                copyStartTime = DateTime.UtcNow;
            }

            _dialogResult = result;

            if (_dialogResult == MessageboxResult.Ok)
            {
                ServiceLocator.TraceService.Add(TraceType.Info, "About to copy local program",
                    "Program name: " + _copyProgramName);

                if (CurrentProgram != null && _copyProgramName == CurrentProgram.Name)
                    await CurrentProgram.Save();

                var sourceProgramName = _copyProgramName;
                var destinationProgramName = await ServiceLocator.ContextService.
                    CopyProgramPart1(_copyProgramName);

                await UpdateLocalPrograms();
                _copyProgramName = null;

                await ServiceLocator.ContextService.CopyProgramPart2(
                    sourceProgramName, destinationProgramName);

                var minDeleteTime = new TimeSpan(0, 0, 2);
                var remainingCopyTime = minDeleteTime.Subtract(
                    DateTime.UtcNow.Subtract(copyStartTime));

                if (remainingCopyTime > new TimeSpan(0))
                    await Task.Delay(remainingCopyTime);

                await UpdateLocalPrograms();

                //var program = await ServiceLocator.ContextService.CopyProgramPart2(
                //    _copyProgramName, updatedProgramName);

                //await UpdateLocalPrograms();

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

            if (_localPrograms == null)
            {
                _localPrograms = new ObservableCollection<LocalProgramHeader>();
            }

            //_localPrograms.Clear();

            using (var storage = StorageSystem.GetStorage())
            {
                var programNames = await storage.GetDirectoryNamesAsync(StorageConstants.ProgramsPath);

                //var programs = new List<ProgramDummyHeader>();

                var programsToRemove = new List<LocalProgramHeader>();

                foreach (var header in _localPrograms)
                {
                    var found = false;
                    foreach (string programName in programNames)
                        if (header.ProjectName == programName)
                            found = true;

                    if (!found)
                        programsToRemove.Add(header);
                }

                //foreach (var header in _localProjects)
                //{
                //    if (header.ProjectName == CurrentProgram.ProjectDummyHeader.ProjectName)
                //        programsToRemove.Add(header);
                //}


                foreach (var program in programsToRemove)
                {
                    _localPrograms.Remove(program);
                }



                //var programsToAdd = new List<LocalProgramHeader>();

                foreach (string programName in programNames)
                {
                    LocalProgramHeader header = null;

                    foreach (var h in _localPrograms)
                    {
                        if (h.ProjectName == programName)
                            header = h;
                    }

                    if (header == null)
                    {
                        var manualScreenshotPath = Path.Combine(
                            StorageConstants.ProgramsPath, programName, StorageConstants.ProgramManualScreenshotPath);
                        var automaticProjectScreenshotPath = Path.Combine(
                            StorageConstants.ProgramsPath, programName, StorageConstants.ProgramAutomaticScreenshotPath);

                        var codePath = Path.Combine(StorageConstants.ProgramsPath,
                            programName, StorageConstants.ProgramCodePath);

                        var programScreenshot = new PortableImage();
                        programScreenshot.LoadAsync(manualScreenshotPath, automaticProjectScreenshotPath, false);

                        var isLoaded = !await storage.FileExistsAsync(codePath);

                        var programHeader = new LocalProgramHeader
                        {
                            ProjectName = programName,
                            Screenshot = programScreenshot,
                            IsLoading = isLoaded
                        };

                        _localPrograms.Insert(0, programHeader);
                    }
                    else if (header.IsLoading)
                    {
                        var codePath = Path.Combine(StorageConstants.ProgramsPath,
                            programName, StorageConstants.ProgramCodePath);
                        var isLoaded = !await storage.FileExistsAsync(codePath);
                        header.IsLoading = isLoaded;
                    }
                }

                //programsToAdd.Sort();


                //foreach (var program in programsToAdd)
                //{
                //    _localPrograms.Insert(0, program);
                //}
            }
        }
    }
}
