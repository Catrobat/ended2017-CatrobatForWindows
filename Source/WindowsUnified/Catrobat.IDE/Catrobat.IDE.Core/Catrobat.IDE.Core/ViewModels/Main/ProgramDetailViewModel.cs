using System.IO;
using System.Linq;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Utilities.JSON;
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;
using Catrobat.IDE.Core.ViewModels.Service;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Diagnostics;

namespace Catrobat.IDE.Core.ViewModels.Main
{
    public class ProgramDetailViewModel : ViewModelBase
    {
        #region Private Members

        private object _loadingLock = new object();

        #endregion

        #region Properties

        private CatrobatContextBase _context;
        public CatrobatContextBase Context
        {
            get { return _context; }
            set
            {
                _context = value;
                RaisePropertyChanged(() => Context);
            }
        }

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

        private LocalProgramHeader _currentProgramHeader;
        public LocalProgramHeader CurrentProgramHeader
        {
            get
            {
                return _currentProgramHeader;
            }
            set
            {
                if (value == _currentProgramHeader) return;

                _currentProgramHeader = value;
                RaisePropertyChanged(() => CurrentProgramHeader);
            }
        }

        private bool _isActivatingLocalProgram;
        public bool IsActivatingLocalProgram
        {
            get
            {
                return _isActivatingLocalProgram;
            }
            set
            {
                _isActivatingLocalProgram = value;
            }
        }

        public ProgramState CurrentProgramState
        {
            get
            {
                if (CurrentProgramHeader == null) return ProgramState.Unknown;

                return CurrentProgramHeader.ValidityState;
            }
        }

        public bool IsValid
        {
            get
            {
                if (CurrentProgramHeader == null) return false;
                return !IsActivatingLocalProgram && CurrentProgramHeader.IsValid;
            }
        }

        public int NumberOfSprites
        {
            get
            {
                if (_currentProgram == null)
                    return 0;

                return _currentProgram.Sprites.Count;
            }
        }

        public int NumberOfActions
        {
            get
            {
                return _currentProgram == null ? 0 :
                    _currentProgram.Sprites.Sum(sprite => sprite.ActionsCount);
            }
        }

        public int NumberOfLooks
        {
            get
            {
                return _currentProgram == null ? 0 :
                    _currentProgram.Sprites.Sum(sprite => sprite.Looks.Count);
            }
        }

        public int NumberOfSounds
        {
            get
            {
                return _currentProgram == null ? 0 :
                    _currentProgram.Sprites.Sum(sprite => sprite.Sounds.Count);
            }
        }

        #endregion

        #region Commands

        public RelayCommand EditCurrentProgramCommand { get; private set; }

        public RelayCommand PlayCurrentProgramCommand { get; private set; }

        public RelayCommand PinLocalProgramCommand { get; private set; }

        public RelayCommand ShareLocalProgramCommand { get; private set; }

        public RelayCommand RenameProgramCommand { get; private set; }

        #endregion

        #region CommandCanExecute
        private bool CommandsCanExecute()
        {
            return IsValid;
        }

        #endregion

        #region Actions

        private void EditCurrentProgramAction()
        {
            ServiceLocator.NavigationService.NavigateTo<SpritesViewModel>();
        }

        private void PlayCurrentProgramAction()
        {
            ServiceLocator.PlayerLauncherService.LaunchPlayer(CurrentProgram);
        }

        private async void ShareLocalProgramAction()
        {
            await CurrentProgram.Save();

            //var message = new GenericMessage<LocalProgramHeader>(CurrentProgram.LocalProgramHeader);
            //Messenger.Default.Send(message, ViewModelMessagingToken.ShareProgramHeaderListener);

            ServiceLocator.NavigationService.NavigateTo<ProgramExportViewModel>();
        }

        private void RenameProgramAction()
        {
            ServiceLocator.NavigationService.NavigateTo<ProgramSettingsViewModel>();
        }
        #endregion

        #region Message Actions

        private async void CurrentProgramHeaderChangedMessageAction(GenericMessage<LocalProgramHeader> message)
        {
            CurrentProgramHeader = message.Content;
            //CurrentProgram = await CatrobatContext.LoadProjectByNameStatic(CurrentProgramHeader.ProjectName);
        }

        private void ContextChangedMessageAction(GenericMessage<CatrobatContextBase> message)
        {
            Context = message.Content;
        }

        #endregion

        public ProgramDetailViewModel()
        {
            EditCurrentProgramCommand = new RelayCommand(EditCurrentProgramAction, CommandsCanExecute);
            PlayCurrentProgramCommand = new RelayCommand(PlayCurrentProgramAction, CommandsCanExecute);
            RenameProgramCommand = new RelayCommand(RenameProgramAction, CommandsCanExecute);
            ShareLocalProgramCommand = new RelayCommand(ShareLocalProgramAction, CommandsCanExecute);

            Messenger.Default.Register<GenericMessage<CatrobatContextBase>>(this,
                ViewModelMessagingToken.ContextListener, ContextChangedMessageAction);

            Messenger.Default.Register<GenericMessage<LocalProgramHeader>>(this,
                ViewModelMessagingToken.CurrentProgramHeaderChangedListener, CurrentProgramHeaderChangedMessageAction);
        }

        private void RaisePropertiesChanges()
        {
            RaisePropertyChanged(() => CurrentProgramState);
            RaisePropertyChanged(() => IsValid);
            RaisePropertyChanged(() => NumberOfSprites);
            RaisePropertyChanged(() => NumberOfActions);
            RaisePropertyChanged(() => NumberOfLooks);
            RaisePropertyChanged(() => NumberOfSounds);
            RaisePropertyChanged(() => IsActivatingLocalProgram);
            EditCurrentProgramCommand.RaiseCanExecuteChanged();
            PlayCurrentProgramCommand.RaiseCanExecuteChanged();
            ShareLocalProgramCommand.RaiseCanExecuteChanged();
            RenameProgramCommand.RaiseCanExecuteChanged();
        }

        public async override void NavigateTo()
        {
            if (CurrentProgram == null ||
                CurrentProgram.Name != CurrentProgramHeader.ProjectName)
            {
                lock (_loadingLock)
                {
                    if (IsActivatingLocalProgram)
                        return;

                    IsActivatingLocalProgram = true;
                    CurrentProgramHeader.ValidityState = ProgramState.Unknown;
                    RaisePropertiesChanges();
                }

                if (CurrentProgram != null)
                    await CurrentProgram.Save();

                var result = await ServiceLocator.ProgramValidationService.CheckProgram(
                    Path.Combine(StorageConstants.ProgramsPath, CurrentProgramHeader.ProjectName));

                var newProgram = result.Program;

                CurrentProgramHeader.ValidityState = result.State;


                IsActivatingLocalProgram = false;

                if (newProgram != null)
                {
                    ServiceLocator.DispatcherService.RunOnMainThread(() =>
                    {
                        CurrentProgramHeader.ValidityState = ProgramState.Valid;
                    });

                    CurrentProgram = newProgram;

                    var projectChangedMessage = new GenericMessage<Program>(newProgram);
                    Messenger.Default.Send(projectChangedMessage,
                        ViewModelMessagingToken.CurrentProgramChangedListener);

                    IsActivatingLocalProgram = false;
                }
                else
                {
                    ServiceLocator.DispatcherService.RunOnMainThread(() =>
                    {
                        CurrentProgram = null;
                    });
                }
            }
            IsActivatingLocalProgram = false;
            RaisePropertiesChanges();          

            base.NavigateTo();
        }
    }
}