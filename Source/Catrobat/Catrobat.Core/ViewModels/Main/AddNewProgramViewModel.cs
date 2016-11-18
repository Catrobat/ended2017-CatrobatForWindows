using System.Collections.Generic;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Linq;

namespace Catrobat.IDE.Core.ViewModels.Main
{
    public class AddNewProgramViewModel : ViewModelBase
    {
        #region Private Members

        #endregion

        #region Properties


        private Program _currentProgram;
        public Program CurrentProgram
        {
            get { return _currentProgram; }
            set
            {
                _currentProgram = value;

                ServiceLocator.DispatcherService.RunOnMainThread(() => 
                    RaisePropertyChanged(() => CurrentProgram));
            }
        }

        private string _programName;
        public string ProgramName
        {
            get { return _programName; }
            set
            {
                if (_programName != value)
                {
                    _programName = value;

                    RaisePropertyChanged(() => ProgramName);
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


                var programGenerators = ServiceLocator.CreateImplementations<IProgramGenerator>();
                var availableTemplates = programGenerators.Select(programGenerator =>
                    new ProjectTemplateEntry(programGenerator)).ToList();

                availableTemplates.Sort();
                _templateOptions =
                    new ObservableCollection<ProjectTemplateEntry>(availableTemplates);

                if (_templateOptions != null)
                    SelectedTemplateOption = _templateOptions[0];

                return _templateOptions;
            }
        }

        private bool _createEmptyProgram;
        public bool CreateEmptyProgram
        {
            get { return _createEmptyProgram; }

            set
            {
                _createEmptyProgram = value;
                RaisePropertyChanged(() => CreateEmptyProgram);
            }
        }

        private bool _createTemplateProgram;
        public bool CreateTemplateProgram
        {
            get { return _createTemplateProgram; }

            set
            {
                _createTemplateProgram = value;
                RaisePropertyChanged(() => CreateTemplateProgram);
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
            return ProgramName != null && ProgramName.Length >= 2;
        }

        #endregion

        #region Actions

        private async void SaveAction()
        {
            GoBackAction();

            string validName = await ServiceLocator.ContextService.ConvertToValidFileName(ProgramName);
            string uniqueName = await ServiceLocator.ContextService.FindUniqueProgramName(validName);

            if (CreateEmptyProgram)
            {
                CurrentProgram = await ServiceLocator.ContextService.CreateEmptyProgram(uniqueName);
                string backgroundName = "Background";
                var sprite = new Sprite { Name = backgroundName };
                CurrentProgram.Sprites.Add(sprite);
            }
            else if (CreateTemplateProgram)
            {
                CurrentProgram = await SelectedTemplateOption.ProjectGenerator.GenerateProgram(uniqueName, true);
            }

            if (CurrentProgram != null)
            {
                await CurrentProgram.Save();

                //await ServiceLocator.ContextService.
                //    CreateThumbnailsForNewProgram(CurrentProgram.Name);

                var programChangedMessage = new GenericMessage<Program>(CurrentProgram);
                Messenger.Default.Send(programChangedMessage, ViewModelMessagingToken.CurrentProgramChangedListener);
            }

            var localProgramsChangedMessage = new MessageBase();
            Messenger.Default.Send(localProgramsChangedMessage, 
                ViewModelMessagingToken.LocalProgramsChangedListener);
        }

        private void CancelAction()
        {
            GoBackAction();
        }

        #endregion

        #region MessageActions

        private void CurrentProgramChangedAction(GenericMessage<Program> message)
        {
            CurrentProgram = message.Content;
        }

        #endregion

        public AddNewProgramViewModel()
        {
            CreateEmptyProgram = true;

            // Commands
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);

            Messenger.Default.Register<GenericMessage<Program>>(this,
                 ViewModelMessagingToken.CurrentProgramChangedListener, CurrentProgramChangedAction);
        }

        public override void NavigateTo()
        {
            ProgramName = "";
            CreateEmptyProgram = true;
            CreateTemplateProgram = false;
            base.NavigateTo();
        }
    }
}