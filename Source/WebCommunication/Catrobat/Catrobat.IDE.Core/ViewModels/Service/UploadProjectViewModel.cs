using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Catrobat.IDE.Core.Utilities.JSON;
using System.Threading.Tasks;
using Catrobat.IDE.Core.ViewModels.Main;

namespace Catrobat.IDE.Core.ViewModels.Service
{
    public class UploadProjectViewModel : ViewModelBase
    {
        #region private Members

        private string _projectName;
        private string _projectDescription;
        private CatrobatContextBase _context;
        private Project _currentProject;
        private MessageboxResult _uploadErrorCallbackResult;

        #endregion

        #region Properties

        public Project CurrentProject
        {
            get
            {
                return _currentProject;
            }
            private set
            {
                if (value == _currentProject) return;
                _currentProject = value;
                RaisePropertyChanged(() => CurrentProject);
            }
        }


        public CatrobatContextBase Context
        {
            get { return _context; }
            set
            {
                _context = value; 
                RaisePropertyChanged(() => Context);
            }
        }

        public string ProjectName
        {
            get
            {
                return _projectName;
            }
            set
            {
                if (_projectName != value)
                {
                    _projectName = value;
                    RaisePropertyChanged(() => ProjectName);
                    UploadCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string ProjectDescription
        {
            get { return _projectDescription; }
            set
            {
                if (_projectDescription != value)
                {
                    _projectDescription = value;
                    RaisePropertyChanged(() => ProjectDescription);
                }
            }
        }

        #endregion

        #region Commands

        public RelayCommand InitializeCommand { get; private set; }

        public RelayCommand UploadCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        public RelayCommand ChangeUserCommand { get; private set; }

        #endregion

        #region CommandCanExecute

        private bool UploadCommand_CanExecute()
        {
            return ProjectName != null && ProjectName.Length >= 2;
        }

        #endregion

        #region Actions

        private void InitializeAction()
        {
            if (Context != null)
            {
                ProjectName = CurrentProject.ProjectHeader.ProgramName;
                ProjectDescription = CurrentProject.ProjectHeader.Description;
            }
            else
            {
                ProjectName = "";
                ProjectDescription = "";
        }
        }

        private async void UploadAction()
        {
            await CurrentProject.SetProgramNameAndRenameDirectory(ProjectName);
            CurrentProject.ProjectHeader.Description = ProjectDescription;
            await App.SaveContext(CurrentProject);

            Task<JSONStatusResponse> upload_task = CatrobatWebCommunicationService.AsyncUploadProject(ProjectName, Context.CurrentUserName,
                                                          Context.CurrentToken, ServiceLocator.CultureService.GetCulture().TwoLetterISOLanguageName);

            var message = new MessageBase();
            Messenger.Default.Send(message, ViewModelMessagingToken.UploadProjectStartedListener);

            base.GoBackAction();

            JSONStatusResponse status_response = await Task.Run(() => upload_task);

            switch (status_response.statusCode)
            { 
                case StatusCodes.ServerResponseTokenOk:
                    break;

                case StatusCodes.HTTPRequestFailed:
                    ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_UploadProjectErrorCaption,
                            AppResources.Main_NoInternetConnection, UploadErrorCallback, MessageBoxOptions.Ok);                        
                    break;

                default:
                    string messageString = string.IsNullOrEmpty(status_response.answer) ? string.Format(AppResources.Main_UploadProjectUndefinedError, status_response.statusCode.ToString()) :
                                           string.Format(AppResources.Main_UploadProjectError, status_response.answer);

                    ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_UploadProjectErrorCaption,
                                messageString, UploadErrorCallback, MessageBoxOptions.Ok);
                    break;
            }

            if (CatrobatWebCommunicationService.NoUploadsPending())
            {
                ServiceLocator.NotifictionService.ShowToastNotification(null,
                    AppResources.Main_NoUploadsPending, ToastNotificationTime.Short);
            }
        }

        private void CancelAction()
        {
            ResetViewModel();
            ServiceLocator.NavigationService.NavigateTo<MainViewModel>();
        }

        private void ChangeUserAction()
        {
            ResetViewModel();
            Context.CurrentToken = "";
            Context.CurrentUserName = "";
            Context.CurrentUserEmail = "";
            ServiceLocator.NavigationService.NavigateTo<UploadProjectLoginViewModel>();
            ServiceLocator.NavigationService.RemoveBackEntry();
        }

        protected override void GoBackAction()
        {
            ResetViewModel();
            base.GoBackAction();
        }

        #endregion

        #region MessageActions
        private void ContextChangedAction(GenericMessage<CatrobatContextBase> message)
        {
            Context = message.Content;
        }

        private void CurrentProjectChangedChangedAction(GenericMessage<Project> message)
        {
            CurrentProject = message.Content;
        }

        #endregion

        public UploadProjectViewModel()
        {
            InitializeCommand = new RelayCommand(InitializeAction);
            UploadCommand = new RelayCommand(UploadAction, UploadCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);
            ChangeUserCommand = new RelayCommand(ChangeUserAction);

            Messenger.Default.Register<GenericMessage<CatrobatContextBase>>(this,
                 ViewModelMessagingToken.ContextListener, ContextChangedAction);

            Messenger.Default.Register<GenericMessage<Project>>(this,
                ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedChangedAction);
        }

        #region Callbacks
        private void UploadErrorCallback(MessageboxResult result)
            {
            _uploadErrorCallbackResult = result;
        }
        #endregion


        public void ResetViewModel()
        {
            ProjectName = "";
            ProjectDescription = "";
        }
    }
}