using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Utilities.JSON;
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;
using Catrobat.IDE.Core.ViewModels.Service;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

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

        private LocalProjectHeader _selectedProjectHeader;
        public LocalProjectHeader CurrentProgramHeader
        {
            get
            {
                return _selectedProjectHeader;
            }
            set
            {
                if (value == _selectedProjectHeader) return;

                _selectedProjectHeader = value;
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

                ServiceLocator.DispatcherService.RunOnMainThread(() =>
                {
                    RaisePropertyChanged(() => IsActivatingLocalProgram);
                    EditCurrentProgramCommand.RaiseCanExecuteChanged();
                    UploadCurrentProgramCommand.RaiseCanExecuteChanged();
                    PlayCurrentProgramCommand.RaiseCanExecuteChanged();
                    PinLocalProgramCommand.RaiseCanExecuteChanged();
                    ShareLocalProgramCommand.RaiseCanExecuteChanged();
                    RenameProgramCommand.RaiseCanExecuteChanged();
                });
            }
        }

        #endregion

        #region Commands

        public RelayCommand EditCurrentProgramCommand { get; private set; }

        public RelayCommand UploadCurrentProgramCommand { get; private set; }

        public RelayCommand PlayCurrentProgramCommand { get; private set; }

        public RelayCommand PinLocalProgramCommand { get; private set; }

        public RelayCommand ShareLocalProgramCommand { get; private set; }

        public RelayCommand RenameProgramCommand { get; private set; }

        #endregion

        #region CommandCanExecute

        #endregion

        #region Actions

        private void EditCurrentProgramAction()
        {
            ServiceLocator.NavigationService.NavigateTo<SpritesViewModel>();
        }

        private async void UploadCurrentProgramAction()
        {
            ServiceLocator.NavigationService.NavigateTo<UploadProgramLoadingViewModel>();

            // Determine which page to open
            JSONStatusResponse statusResponse = await ServiceLocator.WebCommunicationService.CheckTokenAsync(Context.CurrentUserName, Context.CurrentToken, ServiceLocator.CultureService.GetCulture().TwoLetterISOLanguageName);

            if (statusResponse.statusCode == StatusCodes.ServerResponseOk)
            {
                ServiceLocator.DispatcherService.RunOnMainThread(() =>
                {
                    ServiceLocator.NavigationService.NavigateTo<UploadProgramViewModel>();
                    ServiceLocator.NavigationService.RemoveBackEntry();
                });
            }
            else
            {
                ServiceLocator.DispatcherService.RunOnMainThread(() =>
                {
                    ServiceLocator.NavigationService.NavigateTo<UploadProgramLoginViewModel>();
                    ServiceLocator.NavigationService.RemoveBackEntry();
                });
            }
        }

        private void PlayCurrentProgramAction()
        {
            ServiceLocator.PlayerLauncherService.LaunchPlayer(CurrentProgram);
        }

        private void PinLocalProgramAction()
        {
            var message = new GenericMessage<LocalProjectHeader>(CurrentProgram.LocalProgramHeader);
            Messenger.Default.Send(message, ViewModelMessagingToken.PinProgramHeaderListener);

            ServiceLocator.NavigationService.NavigateTo<TileGeneratorViewModel>();
        }

        private async void ShareLocalProgramAction()
        {
            await CurrentProgram.Save();

            var message = new GenericMessage<LocalProjectHeader>(CurrentProgram.LocalProgramHeader);
            Messenger.Default.Send(message, ViewModelMessagingToken.ShareProgramHeaderListener);

            ServiceLocator.ShareService.ShateProject(CurrentProgram.Name);
        }

        private void RenameProgramAction()
        {
            ServiceLocator.NavigationService.NavigateTo<ProgramSettingsViewModel>();
        }

        #endregion

        #region Message Actions

        private async void CurrentProgramHeaderChangedMessageAction(GenericMessage<LocalProjectHeader> message)
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
            EditCurrentProgramCommand = new RelayCommand(EditCurrentProgramAction, () => !IsActivatingLocalProgram);
            UploadCurrentProgramCommand = new RelayCommand(UploadCurrentProgramAction, () => !IsActivatingLocalProgram);
            PlayCurrentProgramCommand = new RelayCommand(PlayCurrentProgramAction, () => !IsActivatingLocalProgram);
            RenameProgramCommand = new RelayCommand(RenameProgramAction, () => !IsActivatingLocalProgram);
            PinLocalProgramCommand = new RelayCommand(PinLocalProgramAction, () => !IsActivatingLocalProgram);
            ShareLocalProgramCommand = new RelayCommand(ShareLocalProgramAction, () => !IsActivatingLocalProgram);

            Messenger.Default.Register<GenericMessage<CatrobatContextBase>>(this,
                ViewModelMessagingToken.ContextListener, ContextChangedMessageAction);

            Messenger.Default.Register<GenericMessage<LocalProjectHeader>>(this,
                ViewModelMessagingToken.CurrentProgramHeaderChangedListener, CurrentProgramHeaderChangedMessageAction);
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
                    }

                    if (CurrentProgram != null)
                        await CurrentProgram.Save();

                    var newProgram = await ServiceLocator.ContextService.
                        LoadProgramByName(CurrentProgramHeader.ProjectName);

                    if (newProgram != null)
                    {
                        ServiceLocator.DispatcherService.RunOnMainThread(() =>
                        {
                            CurrentProgramHeader.ValidityState = LocalProjectState.Valid;
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
                            CurrentProgramHeader.ValidityState = LocalProjectState.Damaged;
                            // TODO: get real ValidityState from "LoadProjectByNameStatic"

                            ServiceLocator.NavigationService.NavigateBack(this.GetType());

                            CurrentProgram = null;
                            CurrentProgramHeader = null;
                            var message = new GenericMessage<LocalProjectHeader>(null);
                            Messenger.Default.Send(message,
                                ViewModelMessagingToken.CurrentProgramHeaderChangedListener);
                        });

                        IsActivatingLocalProgram = false;
                    }
                }

            base.NavigateTo();
        }
    }
}