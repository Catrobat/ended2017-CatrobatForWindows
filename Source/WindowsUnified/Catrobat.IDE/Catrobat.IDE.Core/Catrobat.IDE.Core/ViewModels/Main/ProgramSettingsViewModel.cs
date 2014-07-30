using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModels.Main
{
    public class ProgramSettingsViewModel : ViewModelBase
    {
        #region Private Members

        private LocalProjectHeader _selectedProjectHeader;
        private string _projectName;
        private string _projectDescription;
        private Program _currentProject;

        #endregion

        #region Properties

        public Program CurrentProject
        {
            get { return _currentProject; }
            private set
            {
                if (value == _currentProject)
                    return;

                _currentProject = value;             
                ServiceLocator.DispatcherService.RunOnMainThread(() => 
                    RaisePropertyChanged(() => CurrentProject));
            }
        }

        public LocalProjectHeader SelectedProjectHeader
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

        public RelayCommand InitializeCommand { get; private set; }

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

        private async void SaveAction()
        {
            if (CurrentProject.LocalProgramHeader == SelectedProjectHeader)
            {
                CurrentProject.LocalProgramHeader.ProjectName = ProjectName;
                await CurrentProject.SetProgramNameAndRenameDirectory(ProjectName);
                CurrentProject.Description = ProjectDescription;
            }
            else
            {
                SelectedProjectHeader.ProjectName = ProjectName;
                await CurrentProject.SetProgramNameAndRenameDirectory(ProjectName);
                CurrentProject.Description = ProjectDescription;
                await CurrentProject.Save();
            }

            base.GoBackAction();

            await App.SaveContext(CurrentProject);
        }

        private void CancelAction()
        {
            GoBackAction();
        }

        protected override void GoBackAction()
        {
            ResetViewModel();
            base.GoBackAction();
        }

        #endregion

        #region Message Actions

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

        private async void CurrentProjectHeaderChangedMessageAction(GenericMessage<LocalProjectHeader> message)
        {
            SelectedProjectHeader = message.Content;

            //SelectedProject = await CatrobatContext.LoadProjectByNameStatic(SelectedProjectHeader.ProjectName);
        }

        #endregion

        public ProgramSettingsViewModel()
        {
            InitializeCommand = new RelayCommand(InitializeAction);
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);

            Messenger.Default.Register<GenericMessage<LocalProjectHeader>>(this, 
                ViewModelMessagingToken.CurrentProjectHeaderChangedListener, CurrentProjectHeaderChangedMessageAction);
            Messenger.Default.Register<GenericMessage<Program>>(this, 
                ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedMessageAction);
        }

        private void ResetViewModel()
        {
            ProjectName = "";
            ProjectDescription = "";
        }
    }
}