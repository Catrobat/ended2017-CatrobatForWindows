using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Threading.Tasks;
using Catrobat.Core.Resources.Localization;

namespace Catrobat.IDE.Core.ViewModels.Service
{
    public class UploadProgramViewModel : ViewModelBase
    {
        #region private Members

        private string _programName;
        private string _programDescription;
        private CatrobatContextBase _context;
        private Program _currentProgram;
        private MessageboxResult _cancelExportCallbackResult;
        private bool _isSending;
        private readonly object _exportLock = new object();

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

        public bool IsSending
        {
            get { return _isSending; }
            set
            {
                if (_isSending != value)
                {
                    _isSending = value;
                    CancelUploadCommand.RaiseCanExecuteChanged();
                    RaisePropertyChanged(() => IsSending);
                }
            }
        }

        #endregion

        #region Commands

        public RelayCommand UploadCommand { get; private set; }

        public RelayCommand CancelUploadCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        public RelayCommand ChangeUserCommand { get; private set; }

        #endregion

        #region CommandCanExecute

        private bool UploadCommand_CanExecute()
        {
            return ProgramName != null && ProgramName.Length >= 2;
        }

        private bool CancelUploadCommand_CanExecute()
        {
            return IsSending;
        }

        #endregion

        #region Actions

        private async void UploadAction()
        {
            lock (_exportLock)
            {
                if (IsSending)
                {
                    ServiceLocator.NotifictionService.ShowMessageBox(
                        AppResources.Main_Sending,
                        AppResources.Export_Busy,
                        CancelExportCallback, MessageBoxOptions.Ok);
                    return;
                }
                IsSending = true;
            }

            try
            {
                //await CurrentProgram.SetProgramNameAndRenameDirectory(ProgramName);
                //CurrentProgram.Description = ProgramDescription;
                //await App.SaveContext(CurrentProgram);

                var message = new GenericMessage<string>(ProgramName);
                Messenger.Default.Send(message, ViewModelMessagingToken.UploadProgramStartedListener);

                Task uploadTask = ServiceLocator.ProgramExportService.ExportToPocketCodeOrgWithNotifications(
                    ProgramName, Context.CurrentUserName, Context.CurrentToken);

                this.GoBackAction();
                await Task.Run(() => uploadTask);
            }
            finally
            {
                ServiceLocator.DispatcherService.RunOnMainThread(() =>
                {
                    lock (_exportLock) { IsSending = false; }
                });
            }
        }

        private async void CancelUploadAction()
        {
            await ServiceLocator.ProgramExportService.CancelExport();
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
            ResetViewModel();
            base.GoBackAction();
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
        }

        #endregion

        public UploadProgramViewModel()
        {
            UploadCommand = new RelayCommand(UploadAction, UploadCommand_CanExecute);
            CancelUploadCommand = new RelayCommand(CancelUploadAction, CancelUploadCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);
            ChangeUserCommand = new RelayCommand(ChangeUserAction);
            IsSending = false;

            Messenger.Default.Register<GenericMessage<CatrobatContextBase>>(this,
                 ViewModelMessagingToken.ContextListener, ContextChangedMessageAction);

            Messenger.Default.Register<GenericMessage<Program>>(this,
                ViewModelMessagingToken.CurrentProgramChangedListener, 
                CurrentProgramChangedMessageAction);
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

        #region Callbacks
        private void CancelExportCallback(MessageboxResult result)
        {
            _cancelExportCallbackResult = result;
        }
        #endregion


        public void ResetViewModel()
        {
            ProgramName = "";
            ProgramDescription = "";
        }
    }
}