using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.Utilities.JSON;
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;
using Catrobat.IDE.Core.ViewModels.Service;
using Catrobat.IDE.Core.ViewModels.Share;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Threading.Tasks;

namespace Catrobat.IDE.Core.ViewModels.Main
{
    public class ProjectDetailViewModel : ViewModelBase
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

        private Program _currentProject;
        public Program CurrentProject
        {
            get { return _currentProject; }
            set
            {
                _currentProject = value;
                ServiceLocator.DispatcherService.RunOnMainThread(() =>
                    RaisePropertyChanged(() => CurrentProject));
            }
        }

        private LocalProjectHeader _selectedProjectHeader;
        public LocalProjectHeader CurrentProjectHeader
        {
            get
            {
                return _selectedProjectHeader;
            }
            set
            {
                if (value == _selectedProjectHeader) return;

                _selectedProjectHeader = value;
                RaisePropertyChanged(() => CurrentProjectHeader);
            }
        }

        private bool _isActivatingLocalProject;
        public bool IsActivatingLocalProject
        {
            get
            {
                return _isActivatingLocalProject;
            }
            set
            {
                _isActivatingLocalProject = value;

                ServiceLocator.DispatcherService.RunOnMainThread(() =>
                {
                    RaisePropertyChanged(() => IsActivatingLocalProject);
                    EditCurrentProjectCommand.RaiseCanExecuteChanged();
                    UploadCurrentProjectCommand.RaiseCanExecuteChanged();
                    PlayCurrentProjectCommand.RaiseCanExecuteChanged();
                    PinLocalProjectCommand.RaiseCanExecuteChanged();
                    ShareLocalProjectCommand.RaiseCanExecuteChanged();
                    RenameProjectCommand.RaiseCanExecuteChanged();
                });
            }
        }

        #endregion

        #region Commands

        public RelayCommand EditCurrentProjectCommand { get; private set; }

        public RelayCommand UploadCurrentProjectCommand { get; private set; }

        public RelayCommand PlayCurrentProjectCommand { get; private set; }

        public RelayCommand PinLocalProjectCommand { get; private set; }

        public RelayCommand ShareLocalProjectCommand { get; private set; }

        public RelayCommand RenameProjectCommand { get; private set; }

        #endregion

        #region CommandCanExecute

        #endregion

        #region Actions

        private void EditCurrentProjectAction()
        {
            ServiceLocator.NavigationService.NavigateTo<SpritesViewModel>();
        }

        private async void UploadCurrentProjectAction()
        {
            ServiceLocator.NavigationService.NavigateTo<UploadProjectLoadingViewModel>();

            // Determine which page to open
            JSONStatusResponse statusResponse = await ServiceLocator.WebCommunicationService.CheckTokenAsync(Context.CurrentUserName, Context.CurrentToken, ServiceLocator.CultureService.GetCulture().TwoLetterISOLanguageName);

            if (statusResponse.statusCode == StatusCodes.ServerResponseOk)
            {
                ServiceLocator.DispatcherService.RunOnMainThread(() =>
                {
                    ServiceLocator.NavigationService.NavigateTo<UploadProjectViewModel>();
                    ServiceLocator.NavigationService.RemoveBackEntry();
                });
            }
            else
            {
                ServiceLocator.DispatcherService.RunOnMainThread(() =>
                {
                    ServiceLocator.NavigationService.NavigateTo<UploadProjectLoginViewModel>();
                    ServiceLocator.NavigationService.RemoveBackEntry();
                });
            }
        }

        private void PlayCurrentProjectAction()
        {
            ServiceLocator.PlayerLauncherService.LaunchPlayer(CurrentProject);
        }

        private void PinLocalProjectAction()
        {
            var message = new GenericMessage<LocalProjectHeader>(CurrentProject.LocalProgramHeader);
            Messenger.Default.Send(message, ViewModelMessagingToken.PinProjectHeaderListener);

            ServiceLocator.NavigationService.NavigateTo<TileGeneratorViewModel>();
        }

        private async void ShareLocalProjectAction()
        {
            await CurrentProject.Save();

            var message = new GenericMessage<LocalProjectHeader>(CurrentProject.LocalProgramHeader);
            Messenger.Default.Send(message, ViewModelMessagingToken.ShareProjectHeaderListener);

            ServiceLocator.ShareService.ShateProject(CurrentProject.Name);
            //ServiceLocator.NavigationService.NavigateTo<ShareProjectServiceSelectionViewModel>();
        }

        private void RenameProjectAction()
        {
            ServiceLocator.NavigationService.NavigateTo<ProjectSettingsViewModel>();
        }

        #endregion

        #region Message Actions


        private async void CurrentProjectHeaderChangedMessageAction(GenericMessage<LocalProjectHeader> message)
        {
            CurrentProjectHeader = message.Content;
            //CurrentProject = await CatrobatContext.LoadProjectByNameStatic(CurrentProjectHeader.ProjectName);
        }

        private void ContextChangedMessageAction(GenericMessage<CatrobatContextBase> message)
        {
            Context = message.Content;
        }

        #endregion

        public ProjectDetailViewModel()
        {
            EditCurrentProjectCommand = new RelayCommand(EditCurrentProjectAction, () => !IsActivatingLocalProject);
            UploadCurrentProjectCommand = new RelayCommand(UploadCurrentProjectAction, () => !IsActivatingLocalProject);
            PlayCurrentProjectCommand = new RelayCommand(PlayCurrentProjectAction, () => !IsActivatingLocalProject);
            RenameProjectCommand = new RelayCommand(RenameProjectAction, () => !IsActivatingLocalProject);
            PinLocalProjectCommand = new RelayCommand(PinLocalProjectAction, () => !IsActivatingLocalProject);
            ShareLocalProjectCommand = new RelayCommand(ShareLocalProjectAction, () => !IsActivatingLocalProject);

            Messenger.Default.Register<GenericMessage<CatrobatContextBase>>(this,
                ViewModelMessagingToken.ContextListener, ContextChangedMessageAction);

            Messenger.Default.Register<GenericMessage<LocalProjectHeader>>(this,
                ViewModelMessagingToken.CurrentProjectHeaderChangedListener, CurrentProjectHeaderChangedMessageAction);
        }

        public override void NavigateTo()
        {
            Task.Run(async () =>
            {
                if (CurrentProject == null || 
                    CurrentProject.Name != CurrentProjectHeader.ProjectName)
                {
                    lock (_loadingLock)
                    {
                        if (IsActivatingLocalProject)
                            return;

                        IsActivatingLocalProject = true;
                    }

                    if (CurrentProject != null)
                        await CurrentProject.Save();

                    var newProject = await ServiceLocator.ContextService.
                        LoadProgramByName(CurrentProjectHeader.ProjectName);
                    
                    if (newProject != null)
                    {
                        ServiceLocator.DispatcherService.RunOnMainThread(() => {
                            CurrentProjectHeader.ValidityState = LocalProjectState.Valid;
                        });

                        CurrentProject = newProject;
                        IsActivatingLocalProject = false;

                        var projectChangedMessage = new GenericMessage<Program>(newProject);
                        Messenger.Default.Send(projectChangedMessage, 
                            ViewModelMessagingToken.CurrentProjectChangedListener);

                        IsActivatingLocalProject = false;
                    }
                    else
                    {
                        IsActivatingLocalProject = false;

                        ServiceLocator.DispatcherService.RunOnMainThread(() => 
                        {
                            CurrentProjectHeader.ValidityState = LocalProjectState.Damaged;
                            // TODO: get real ValidityState from "LoadProjectByNameStatic"
                         
                            ServiceLocator.NavigationService.NavigateBack(this.GetType());

                            CurrentProject = null;
                            CurrentProjectHeader = null;
                            var message = new GenericMessage<LocalProjectHeader>(null);
                            Messenger.Default.Send(message, 
                                ViewModelMessagingToken.CurrentProjectHeaderChangedListener);
                        });
                    }
                }
            });

            base.NavigateTo();
        }

    }
}