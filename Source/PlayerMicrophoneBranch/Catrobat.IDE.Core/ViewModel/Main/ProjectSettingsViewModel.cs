using Catrobat.IDE.Core.CatrobatObjects;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModel.Main
{
    public class ProjectSettingsViewModel : ViewModelBase
    {
        #region Private Members

        private Project _selectedProject;
        private ProjectDummyHeader _selectedProjectHeader;
        private string _projectName;
        private string _projectDescription;
        private Project _currentProject;

        #endregion

        #region Properties

        public Project CurrentProject
        {
            get { return _currentProject; }
            set
            {
                _currentProject = value;
                RaisePropertyChanged(() => CurrentProject);
            }
        }

        public Project SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                if (value == _selectedProject)
                {
                    return;
                }
                _selectedProject = value;
                RaisePropertyChanged(() => SelectedProject);
            }
        }

        public ProjectDummyHeader SelectedProjectHeader
        {
            get { return _selectedProjectHeader; }
            set
            {
                if (value == _selectedProjectHeader)
                {
                    return;
                }
                _selectedProjectHeader = value;
                RaisePropertyChanged(() => SelectedProjectHeader);
            }
        }

        public string ProjectName
        {
            get { return _projectName; }
            set
            {
                if (value == _projectName) return;
                _projectName = value;
                RaisePropertyChanged(() => ProjectName);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public string ProjectDescription
        {
            get { return _projectDescription; }
            set
            {
                if (value == _projectDescription) return;
                _projectDescription = value;
                RaisePropertyChanged(() => ProjectDescription);
            }
        }

        #endregion

        #region Commands

        public RelayCommand SaveCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        #endregion

        #region CommandCanExecute

        private bool SaveCommand_CanExecute()
        {
            return ProjectName != null && ProjectName.Length >= 2;
        }

        #endregion

        #region Actions

        private async void SaveAction()
        {
            if (CurrentProject.ProjectDummyHeader == SelectedProjectHeader)
            {
                CurrentProject.ProjectDummyHeader.ProjectName = ProjectName;
                await CurrentProject.SetProgramNameAndRenameDirectory(ProjectName);
                CurrentProject.ProjectHeader.Description = ProjectDescription;
            }
            else
            {
                SelectedProjectHeader.ProjectName = ProjectName;
                await SelectedProject.SetProgramNameAndRenameDirectory(ProjectName);
                SelectedProject.ProjectHeader.Description = ProjectDescription;
                await SelectedProject.Save();
            }

            base.GoBackAction();

            await App.SaveContext(CurrentProject);
        }

        private void CancelAction()
        {
            base.GoBackAction();
        }

        protected override void GoBackAction()
        {
            ResetViewModel();
            base.GoBackAction();
        }

        #endregion

        #region Message Actions

        private void CurrentProjectChangedMessageAction(GenericMessage<Project> message)
        {
            CurrentProject = message.Content;
        }

        private async void ChangeProjectNameMessageAction(GenericMessage<ProjectDummyHeader> message)
        {
            SelectedProjectHeader = message.Content;

            SelectedProject = await CatrobatContext.LoadNewProjectByNameStatic(SelectedProjectHeader.ProjectName);
            ProjectName = SelectedProject.ProjectHeader.ProgramName;
            ProjectDescription = SelectedProject.ProjectHeader.Description;
        }

        #endregion

        public ProjectSettingsViewModel()
        {
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);

            Messenger.Default.Register<GenericMessage<ProjectDummyHeader>>(this, 
                ViewModelMessagingToken.ChangeLocalProjectListener, ChangeProjectNameMessageAction);
            Messenger.Default.Register<GenericMessage<Project>>(this, 
                ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedMessageAction);
        }

        private void ResetViewModel()
        {
            ProjectName = "";
            ProjectDescription = "";
        }
    }
}