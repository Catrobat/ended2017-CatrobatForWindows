using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.Resources.Localization;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModel.Service
{
    public class UploadProjectViewModel : ViewModelBase
    {
        #region private Members

        private string _projectName;
        private string _projectDescription;
        private CatrobatContextBase _context;
        private Project _currentProject;

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
                ProjectName = CurrentProject.ProjectHeader.ProgramName;
            else
                ProjectName = "";
        }

        private void UploadAction()
        {
            CurrentProject.ProjectHeader.ProgramName = ProjectName;

            CatrobatWebCommunicationService.UploadProject(_projectName, _projectDescription,
                                              Context.CurrentUserEmail,
                                              ServiceLocator.CulureService.GetCulture().TwoLetterISOLanguageName,
                                              Context.CurrentToken, UploadCallback);

            var message = new MessageBase();
            Messenger.Default.Send(message, ViewModelMessagingToken.UploadProjectStartedListener);

            ServiceLocator.NavigationService.RemoveBackEntry();
            ServiceLocator.NavigationService.NavigateBack();
        }

        private void CancelAction()
        {
            ServiceLocator.NavigationService.RemoveBackEntry();
            ServiceLocator.NavigationService.NavigateBack();
        }

        protected override void GoBackAction()
        {
            ResetViewModel();
            ServiceLocator.NavigationService.NavigateBack();
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

            Messenger.Default.Register<GenericMessage<CatrobatContextBase>>(this,
                 ViewModelMessagingToken.ContextListener, ContextChangedAction);

            Messenger.Default.Register<GenericMessage<Project>>(this,
                ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedChangedAction);
        }


        private void UploadCallback(bool successful)
        {
            if (CatrobatWebCommunicationService.NoUploadsPending())
            {
                ServiceLocator.NotifictionService.ShowToastNotification(null, null,
                    AppResources.Main_NoUploadsPending, ToastNotificationTime.Short);
            }
        }

        public void ResetViewModel()
        {
            ProjectName = "";
            ProjectDescription = "";
        }
    }
}