using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.Utilities.JSON;
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;
using Catrobat.IDE.Core.ViewModels.Service;
using Catrobat.IDE.Core.ViewModels.Share;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;

namespace Catrobat.IDE.Core.ViewModels.Main
{
    public class ProjectDetailViewModel : ViewModelBase
    {
        #region Private Members


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

        private Project _currentProject;
        public Project CurrentProject
        {
            get { return _currentProject; }
            set
            {
                _currentProject = value;
                RaisePropertyChanged(() => CurrentProject);
            }
        }

        private ProjectDummyHeader _selectedProjectHeader;
        public ProjectDummyHeader CurrentProjectHeader
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

        #endregion

        #region Commands

        public ICommand EditCurrentProjectCommand { get; private set; }

        public ICommand UploadCurrentProjectCommand { get; private set; }

        public ICommand PlayCurrentProjectCommand { get; private set; }

        public ICommand PinLocalProjectCommand { get; private set; }

        public ICommand ShareLocalProjectCommand { get; private set; }

        public ICommand RenameProjectCommand { get; private set; }

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
            JSONStatusResponse status_response = await CatrobatWebCommunicationService.AsyncCheckToken(Context.CurrentUserName, Context.CurrentToken, ServiceLocator.CultureService.GetCulture().TwoLetterISOLanguageName);

            if (status_response.statusCode == StatusCodes.ServerResponseOk)
            {
                ServiceLocator.DispatcherService.RunOnMainThread(() =>
                {
                    ServiceLocator.NavigationService.NavigateTo<UploadProjectViewModel>();
                    //ServiceLocator.NavigationService.RemoveBackEntry();
                });
            }
            else
            {
                ServiceLocator.DispatcherService.RunOnMainThread(() =>
                {
                    ServiceLocator.NavigationService.NavigateTo<UploadProjectLoginViewModel>();
                    //ServiceLocator.NavigationService.RemoveBackEntry();
                });
            }
        }

        private void PlayCurrentProjectAction()
        {
            ServiceLocator.PlayerLauncherService.LaunchPlayer(CurrentProject);
        }

        private void PinLocalProjectAction()
        {
            var message = new GenericMessage<ProjectDummyHeader>(CurrentProject.ProjectDummyHeader);
            Messenger.Default.Send(message, ViewModelMessagingToken.PinProjectHeaderListener);

            ServiceLocator.NavigationService.NavigateTo<TileGeneratorViewModel>();
        }

        private async void ShareLocalProjectAction()
        {
            await CurrentProject.Save();

            var message = new GenericMessage<ProjectDummyHeader>(CurrentProject.ProjectDummyHeader);
            Messenger.Default.Send(message, ViewModelMessagingToken.ShareProjectHeaderListener);

            ServiceLocator.NavigationService.NavigateTo<ShareProjectServiceSelectionViewModel>();
        }

        private void RenameProjectAction()
        {
            ServiceLocator.NavigationService.NavigateTo<ProjectSettingsViewModel>();
        }

        #endregion

        #region Message Actions


        private async void CurrentProjectHeaderCahngedMessageAction(GenericMessage<ProjectDummyHeader> message)
        {
            CurrentProjectHeader = message.Content;
            CurrentProject = await CatrobatContext.LoadNewProjectByNameStatic(CurrentProjectHeader.ProjectName);
        }

        private void ContextChangedMessageAction(GenericMessage<CatrobatContextBase> message)
        {
            Context = message.Content;
        }

        #endregion

        public ProjectDetailViewModel()
        {

            EditCurrentProjectCommand = new RelayCommand(EditCurrentProjectAction);
            UploadCurrentProjectCommand = new RelayCommand(UploadCurrentProjectAction);
            PlayCurrentProjectCommand = new RelayCommand(PlayCurrentProjectAction);
            RenameProjectCommand = new RelayCommand(RenameProjectAction);
            PinLocalProjectCommand = new RelayCommand(PinLocalProjectAction);
            ShareLocalProjectCommand = new RelayCommand(ShareLocalProjectAction);

            Messenger.Default.Register<GenericMessage<CatrobatContextBase>>(this,
                ViewModelMessagingToken.ContextListener, ContextChangedMessageAction);

            Messenger.Default.Register<GenericMessage<ProjectDummyHeader>>(this,
                ViewModelMessagingToken.CurrentProjectHeaderChangedListener, CurrentProjectHeaderCahngedMessageAction);
        }
    }
}