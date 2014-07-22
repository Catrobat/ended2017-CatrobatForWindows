using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Linq;

namespace Catrobat.IDE.Core.ViewModels.Main
{
    public class AddNewProjectViewModel : ViewModelBase
    {
        #region Private Members

        private Project _currentProject;
        private string _projectName;
        private bool _copyCurrentProjectAsTemplate;

        #endregion

        #region Properties

        public Project CurrentProject
        {
            get { return _currentProject; }
            set
            {
                _currentProject = value;

                ServiceLocator.DispatcherService.RunOnMainThread(() => 
                    RaisePropertyChanged(() => CurrentProject));
            }
        }

        //public string TextCopyCurrentProjectAsTemplate
        //{
        //    get { return String.Format(AppResources.Main_CreateProjectBasedOnCurrentProject, CurrentProject.Name); }
        //}

        public string ProjectName
        {
            get { return _projectName; }
            set
            {
                if (_projectName != value)
                {
                    _projectName = value;

                    RaisePropertyChanged(() => ProjectName);
                    SaveCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private ProjectTemplateEntry _selectedTemplateOption;
        public ProjectTemplateEntry SelectedTemplateOption
        {
            get { return _selectedTemplateOption; }
            set
            {
                _selectedTemplateOption = value;
                RaisePropertyChanged(() => SelectedTemplateOption);
            }
        }

        private ObservableCollection<ProjectTemplateEntry> _templateOptions;
        public ObservableCollection<ProjectTemplateEntry> TemplateOptions
        {
            get
            {
                if (_templateOptions != null) return _templateOptions;


                var projectGenerators = ServiceLocator.CreateImplementations<IProjectGenerator>();
                var availableTemplates = projectGenerators.Select(projectGenerator =>
                    new ProjectTemplateEntry(projectGenerator)).ToList();

                availableTemplates.Sort();
                _templateOptions =
                    new ObservableCollection<ProjectTemplateEntry>(availableTemplates);

                if (_templateOptions != null)
                    SelectedTemplateOption = _templateOptions[0];

                return _templateOptions;
            }
        }

        private bool _createEmptyProject;
        public bool CreateEmptyProject
        {
            get { return _createEmptyProject; }

            set
            {
                _createEmptyProject = value;
                RaisePropertyChanged(() => CreateEmptyProject);
            }
        }

        private bool _createCopyOfCurrentProject;
        public bool CreateCopyOfCurrentProject
        {
            get { return _createCopyOfCurrentProject; }

            set
            {
                _createCopyOfCurrentProject = value;
                RaisePropertyChanged(() => CreateCopyOfCurrentProject);
            }
        }

        private bool _createTemplateProject;
        public bool CreateTemplateProject
        {
            get { return _createTemplateProject; }

            set
            {
                _createTemplateProject = value;
                RaisePropertyChanged(() => CreateTemplateProject);
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
            if (CurrentProject != null)
                await CurrentProject.Save();

            if (CreateEmptyProject)
            {
                CurrentProject = await CatrobatContext.CreateEmptyProject(_projectName);
            }
            else if (CreateCopyOfCurrentProject)
            {
                CurrentProject = await CatrobatContext.CopyProject(CurrentProject.Name, _projectName);
            }
            else if (CreateTemplateProject)
            {
                CurrentProject = await SelectedTemplateOption.ProjectGenerator.GenerateProject(ProjectName, true);
            }

            if (CurrentProject != null)
            {
                await CurrentProject.Save();

                var projectChangedMessage = new GenericMessage<Project>(CurrentProject);
                Messenger.Default.Send(projectChangedMessage, ViewModelMessagingToken.CurrentProjectChangedListener);
            }

            var localProjectsChangedMessage = new MessageBase();
            Messenger.Default.Send(localProjectsChangedMessage, ViewModelMessagingToken.LocalProjectsChangedListener);

            GoBackAction();
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

        #region MessageActions

        private void CurrentProjectChangedAction(GenericMessage<Project> message)
        {
            CurrentProject = message.Content;
        }

        #endregion

        public AddNewProjectViewModel()
        {
            CreateEmptyProject = true;

            // Commands
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);

            Messenger.Default.Register<GenericMessage<Project>>(this,
                 ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedAction);
        }

        public void ResetViewModel()
        {
            ProjectName = "";
            CreateEmptyProject = true;
            CreateCopyOfCurrentProject = false;
            CreateTemplateProject = false;
        }
    }
}