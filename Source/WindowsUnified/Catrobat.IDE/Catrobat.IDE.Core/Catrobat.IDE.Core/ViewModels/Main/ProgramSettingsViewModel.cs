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

        private LocalProjectHeader _selectedProgramHeader;
        private string _programName;
        private string _programDescription;
        private Program _currentProgram;

        #endregion

        #region Properties

        public Program CurrentProgram
        {
            get { return _currentProgram; }
            private set
            {
                if (value == _currentProgram)
                    return;

                _currentProgram = value;             
                ServiceLocator.DispatcherService.RunOnMainThread(() => 
                    RaisePropertyChanged(() => CurrentProgram));
            }
        }

        public LocalProjectHeader SelectedProgramHeader
        {
            get { return _selectedProgramHeader; }
            set
            {
                if (value == _selectedProgramHeader)
                {
                    return;
                }
                _selectedProgramHeader = value;
                RaisePropertyChanged(() => SelectedProgramHeader);
            }
        }

        public string ProgramName
        {
            get { return _programName; }
            set
            {
                if (value == _programName) return;
                _programName = value;
                RaisePropertyChanged(() => ProgramName);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public string ProgramDescription
        {
            get { return _programDescription; }
            set
            {
                if (value == _programDescription) return;
                _programDescription = value;
                RaisePropertyChanged(() => ProgramDescription);
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
            return ProgramName != null && ProgramName.Length >= 2;
        }

        #endregion

        #region Actions

        private void InitializeAction()
        {
            if (CurrentProgram != null)
            {
                ProgramName = CurrentProgram.Name;
                ProgramDescription = CurrentProgram.Description;
            }
            else
            {
                ProgramName = "";
                ProgramDescription = "";
            }
        }

        private async void SaveAction()
        {
            if (CurrentProgram.LocalProgramHeader == SelectedProgramHeader)
            {
                CurrentProgram.LocalProgramHeader.ProjectName = ProgramName;
                await CurrentProgram.SetProgramNameAndRenameDirectory(ProgramName);
                CurrentProgram.Description = ProgramDescription;
            }
            else
            {
                SelectedProgramHeader.ProjectName = ProgramName;
                await CurrentProgram.SetProgramNameAndRenameDirectory(ProgramName);
                CurrentProgram.Description = ProgramDescription;
                await CurrentProgram.Save();
            }

            base.GoBackAction();

            await App.SaveContext(CurrentProgram);
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

        private void CurrentProgramChangedMessageAction(GenericMessage<Program> message)
        {
            CurrentProgram = message.Content;
            //if (CurrentProgram != null)
            //{
            //    ProgramName = CurrentProgram.Name;
            //    ProgramDescription = CurrentProgram.Description;  
            //}
            //else
            //{
            //    ProgramName = "";
            //    ProgramDescription = ""; 
            //}
        }

        private async void CurrentProgramHeaderChangedMessageAction(GenericMessage<LocalProjectHeader> message)
        {
            SelectedProgramHeader = message.Content;

            //SelectedProgram = await CatrobatContext.LoadProgramByNameStatic(SelectedProgramHeader.ProgramName);
        }

        #endregion

        public ProgramSettingsViewModel()
        {
            InitializeCommand = new RelayCommand(InitializeAction);
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);

            Messenger.Default.Register<GenericMessage<LocalProjectHeader>>(this, 
                ViewModelMessagingToken.CurrentProgramHeaderChangedListener, CurrentProgramHeaderChangedMessageAction);
            Messenger.Default.Register<GenericMessage<Program>>(this, 
                ViewModelMessagingToken.CurrentProgramChangedListener, CurrentProgramChangedMessageAction);
        }

        private void ResetViewModel()
        {
            ProgramName = "";
            ProgramDescription = "";
        }
    }
}