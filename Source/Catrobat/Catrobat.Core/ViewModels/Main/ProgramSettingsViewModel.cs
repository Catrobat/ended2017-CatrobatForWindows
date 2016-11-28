using System.IO;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Storage;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModels.Main
{
    public class ProgramSettingsViewModel : ViewModelBase
    {
        #region Private Members

        private LocalProgramHeader _currentProgramHeader;
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

        public LocalProgramHeader CurrentProgramHeader
        {
            get { return _currentProgramHeader; }
            set
            {
                if (value == _currentProgramHeader)
                {
                    return;
                }
                _currentProgramHeader = value;
                RaisePropertyChanged(() => CurrentProgramHeader);
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

            string validName = await ServiceLocator.ContextService.ConvertToValidFileName(ProgramName);
            if (CurrentProgram.Name != ProgramName)
            {
                ProgramName = await ServiceLocator.ContextService.FindUniqueProgramName(validName);

                if (CurrentProgram.LocalProgramHeader == CurrentProgramHeader)
                {
                    CurrentProgram.LocalProgramHeader.ProjectName = ProgramName;
                    await CurrentProgram.SetProgramNameAndRenameDirectory(ProgramName);
                    CurrentProgram.Description = ProgramDescription;
                }
                else
                {
                    var oldProgramPath = CurrentProgram.BasePath;
                    CurrentProgramHeader.ProjectName = ProgramName;
                    CurrentProgram.Name = ProgramName;
                    CurrentProgram.Description = ProgramDescription;
                    
                    var programChangedMessage1 = new GenericMessage<Program>(CurrentProgram);
                        Messenger.Default.Send(programChangedMessage1, 
                            ViewModelMessagingToken.CurrentProgramChangedListener);

                    using (var storage = StorageSystem.GetStorage())
                    {
                        await storage.RenameDirectoryAsync(oldProgramPath, ProgramName);
                    }
                    
                    var programChangedMessage2 = new GenericMessage<Program>(CurrentProgram);
                    Messenger.Default.Send(programChangedMessage2,
                        ViewModelMessagingToken.CurrentProgramChangedListener);

                    await App.SaveContext(CurrentProgram);

                    var localProgramsChangedMessage = new MessageBase();
                    Messenger.Default.Send(localProgramsChangedMessage,
                            ViewModelMessagingToken.LocalProgramsChangedListener);
                }
            } 
            else if(CurrentProgram.Description != ProgramDescription)
            {
                CurrentProgram.Description = ProgramDescription;
                await App.SaveContext(CurrentProgram);
            }

            base.GoBackAction();
        }

        private void CancelAction()
        {
            ResetViewModel();
            base.GoBackAction();
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
        }

        private async void CurrentProgramHeaderChangedMessageAction(GenericMessage<LocalProgramHeader> message)
        {
            CurrentProgramHeader = message.Content;

            //SelectedProgram = await CatrobatContext.LoadProgramByNameStatic(SelectedProgramHeader.ProgramName);
        }

        #endregion

        public ProgramSettingsViewModel()
        {
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);

            Messenger.Default.Register<GenericMessage<LocalProgramHeader>>(this,
                ViewModelMessagingToken.CurrentProgramHeaderChangedListener, CurrentProgramHeaderChangedMessageAction);
            Messenger.Default.Register<GenericMessage<Program>>(this,
                ViewModelMessagingToken.CurrentProgramChangedListener, CurrentProgramChangedMessageAction);
        }

        public override void NavigateTo()
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
            base.NavigateTo();
        }

        private void ResetViewModel()
        {
            ProgramName = "";
            ProgramDescription = "";
        }
    }
}