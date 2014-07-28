using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.CatrobatObjects;
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
        private Program _currentProject;
        private MessageboxResult _uploadErrorCallbackResult;

        #endregion

        #region Properties

        public Program CurrentProject
        {
            get
            {
                return _currentProject;
            }
            private set
            {
                if (value == _currentProject) 
                    return;

                _currentProject = value;
                ServiceLocator.DispatcherService.RunOnMainThread(() => RaisePropertyChanged(() => CurrentProject));
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
            if (CurrentProject != null)
            {
                ProjectName = CurrentProject.Name;
                ProjectDescription = CurrentProject.Description;
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
            CurrentProject.Description = ProjectDescription;
            await App.SaveContext(CurrentProject);

            Task<JSONStatusResponse> upload_task = ServiceLocator.WebCommunicationService.UploadProjectAsync(ProjectName, Context.CurrentUserName,
                                                          Context.CurrentToken, ServiceLocator.CultureService.GetCulture().TwoLetterISOLanguageName);

            var message = new MessageBase();
            Messenger.Default.Send(message, ViewModelMessagingToken.UploadProjectStartedListener);

            GoBackAction();

            JSONStatusResponse statusResponse = await Task.Run(() => upload_task);

            switch (statusResponse.statusCode)
            {
                case StatusCodes.ServerResponseOk:
                    break;

                case StatusCodes.HTTPRequestFailed:
                    ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_UploadProjectErrorCaption,
                            AppResources.Main_NoInternetConnection, UploadErrorCallback, MessageBoxOptions.Ok);
                    break;

                default:
                    string messageString = string.IsNullOrEmpty(statusResponse.answer) ? string.Format(AppResources.Main_UploadProjectUndefinedError, statusResponse.statusCode.ToString()) :
                                           string.Format(AppResources.Main_UploadProjectError, statusResponse.answer);
                    ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_UploadProjectErrorCaption,
                                messageString, UploadErrorCallback, MessageBoxOptions.Ok);
                    break;
            }

            if (ServiceLocator.WebCommunicationService.NoUploadsPending())
            {
                ServiceLocator.NotifictionService.ShowToastNotification(null,
                    AppResources.Main_NoUploadsPending, ToastDisplayDuration.Short);
            }
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

        private void CancelAction()
        {
            ResetViewModel();
            ServiceLocator.NavigationService.NavigateTo<ProjectDetailViewModel>();
            ServiceLocator.NavigationService.RemoveBackEntry();
        } 

        protected override void GoBackAction()
        {
            ResetViewModel();
            base.GoBackAction();
        }

        #endregion

        #region MessageActions
        private void ContextChangedMessageAction(GenericMessage<CatrobatContextBase> message)
        {
            Context = message.Content;
        }

        private void CurrentProjectChangedMessageAction(GenericMessage<Program> message)
        {
            CurrentProject = message.Content;
            if (CurrentProject != null)
            {
                ProjectName = CurrentProject.Name;
                ProjectDescription = CurrentProject.Description;
            }
            else
            {
                ProjectName = "";
                ProjectDescription = "";
            }
        }

        #endregion

        public UploadProjectViewModel()
        {
            InitializeCommand = new RelayCommand(InitializeAction);
            UploadCommand = new RelayCommand(UploadAction, UploadCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);
            ChangeUserCommand = new RelayCommand(ChangeUserAction);

            Messenger.Default.Register<GenericMessage<CatrobatContextBase>>(this,
                 ViewModelMessagingToken.ContextListener, ContextChangedMessageAction);

            Messenger.Default.Register<GenericMessage<Program>>(this,
                ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedMessageAction);
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