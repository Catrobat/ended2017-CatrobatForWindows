using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.CatrobatObjects;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Catrobat.IDE.Core.Utilities.JSON;
using System.Threading.Tasks;
using Catrobat.IDE.Core.ViewModels.Main;

namespace Catrobat.IDE.Core.ViewModels.Service
{
    public class UploadProgramViewModel : ViewModelBase
    {
        #region private Members

        private string _programName;
        private string _programDescription;
        private CatrobatContextBase _context;
        private Program _currentProgram;
        private MessageboxResult _uploadErrorCallbackResult;

        #endregion

        #region Properties

        public Program CurrentProgram
        {
            get
            {
                return _currentProgram;
            }
            private set
            {
                if (value == _currentProgram) 
                    return;

                _currentProgram = value;
                ServiceLocator.DispatcherService.RunOnMainThread(() => RaisePropertyChanged(() => CurrentProgram));
            }
        }

        public CatrobatContextBase Context
        {
            get { return _context; }
            set
            {
                _context = value;
                RaisePropertyChanged(() => Context);
            }
        }

        public string ProgramName
        {
            get
            {
                return _programName;
            }
            set
            {
                if (_programName != value)
                {
                    _programName = value;
                    RaisePropertyChanged(() => ProgramName);
                    UploadCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string ProgramDescription
        {
            get { return _programDescription; }
            set
            {
                if (_programDescription != value)
                {
                    _programDescription = value;
                    RaisePropertyChanged(() => ProgramDescription);
                }
            }
        }

        #endregion

        #region Commands

        public RelayCommand InitializeCommand { get; private set; }

        public RelayCommand UploadCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        public RelayCommand ChangeUserCommand { get; private set; }

        #endregion

        #region CommandCanExecute

        private bool UploadCommand_CanExecute()
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

        private async void UploadAction()
        {
            await CurrentProgram.SetProgramNameAndRenameDirectory(ProgramName);
            CurrentProgram.Description = ProgramDescription;
            await App.SaveContext(CurrentProgram);

            Task<JSONStatusResponse> upload_task = ServiceLocator.WebCommunicationService.UploadProjectAsync(ProgramName, Context.CurrentUserName,
                                                          Context.CurrentToken, ServiceLocator.CultureService.GetCulture().TwoLetterISOLanguageName);

            var message = new MessageBase();
            Messenger.Default.Send(message, ViewModelMessagingToken.UploadProgramStartedListener);

            GoBackAction();

            JSONStatusResponse statusResponse = await Task.Run(() => upload_task);

            switch (statusResponse.statusCode)
            {
                case StatusCodes.ServerResponseOk:
                    break;

                case StatusCodes.HTTPRequestFailed:
                    ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_UploadProgramErrorCaption,
                            AppResources.Main_NoInternetConnection, UploadErrorCallback, MessageBoxOptions.Ok);
                    break;

                default:
                    string messageString = string.IsNullOrEmpty(statusResponse.answer) ? string.Format(AppResources.Main_UploadProgramUndefinedError, statusResponse.statusCode.ToString()) :
                                           string.Format(AppResources.Main_UploadProgramError, statusResponse.answer);
                    ServiceLocator.NotifictionService.ShowMessageBox(AppResources.Main_UploadProgramErrorCaption,
                                messageString, UploadErrorCallback, MessageBoxOptions.Ok);
                    break;
            }

            if (ServiceLocator.WebCommunicationService.NoUploadsPending())
            {
                ServiceLocator.NotifictionService.ShowToastNotification(null,
                    AppResources.Main_NoUploadsPending, ToastDisplayDuration.Short);
            }
        }

        private void ChangeUserAction()
        {
            ResetViewModel();
            Context.CurrentToken = "";
            Context.CurrentUserName = "";
            Context.CurrentUserEmail = "";
            ServiceLocator.NavigationService.NavigateTo<UploadProgramLoginViewModel>();
            ServiceLocator.NavigationService.RemoveBackEntry();
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
        private void ContextChangedMessageAction(GenericMessage<CatrobatContextBase> message)
        {
            Context = message.Content;
        }

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

        #endregion

        public UploadProgramViewModel()
        {
            InitializeCommand = new RelayCommand(InitializeAction);
            UploadCommand = new RelayCommand(UploadAction, UploadCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);
            ChangeUserCommand = new RelayCommand(ChangeUserAction);

            Messenger.Default.Register<GenericMessage<CatrobatContextBase>>(this,
                 ViewModelMessagingToken.ContextListener, ContextChangedMessageAction);

            Messenger.Default.Register<GenericMessage<Program>>(this,
                ViewModelMessagingToken.CurrentProgramChangedListener, CurrentProgramChangedMessageAction);
        }

        #region Callbacks
        private void UploadErrorCallback(MessageboxResult result)
        {
            _uploadErrorCallbackResult = result;
        }
        #endregion


        public void ResetViewModel()
        {
            ProgramName = "";
            ProgramDescription = "";
        }
    }
}