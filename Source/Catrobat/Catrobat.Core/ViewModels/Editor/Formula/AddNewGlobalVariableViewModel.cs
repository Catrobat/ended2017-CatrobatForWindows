using Catrobat.Core.Resources.Localization;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Utilities.Helpers;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModels.Editor.Formula
{
    public class AddNewGlobalVariableViewModel : ViewModelBase
    {
        #region Private Members

        private Program _currentProgram;
        private Sprite _selectedSprite;
        private string _userVariableName = AppResourcesHelper.Get("Editor_DefaultGlobalVariableName");

        #endregion

        #region Properties

        public Program CurrentProgram
        {
            get { return _currentProgram; }
            private set 
            { 
                _currentProgram = value;
              
                ServiceLocator.DispatcherService.RunOnMainThread(() => 
                    RaisePropertyChanged(() => CurrentProgram)); 
            }
        }

        public Sprite CurrentSprite
        {
            get { return _selectedSprite; }
            set
            {
                _selectedSprite = value;
                RaisePropertyChanged(() => CurrentSprite);
            }
        }

        public string UserVariableName
        {
            get { return _userVariableName; }
            set
            { 
                _userVariableName = value; 
                RaisePropertyChanged(() => UserVariableName); 
                SaveCommand.RaiseCanExecuteChanged(); 
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
            return !string.IsNullOrEmpty(UserVariableName) &&
                   !VariableHelper.VariableNameExists(CurrentProgram, CurrentSprite, UserVariableName);
        }

        #endregion

        #region Actions

        private void SaveAction()
        {
            var newVariable = new GlobalVariable {Name = UserVariableName};
            CurrentProgram.GlobalVariables.Add(newVariable);
            base.GoBackAction();
        }

        private void CancelAction()
        {
            base.GoBackAction();
        }

        protected override void GoBackAction()
        {
            ResetViewModel();
            base.GoBackAction();
        }


        #endregion

        #region MessageActions

        private void CurrentProgramChangedMessageAction(GenericMessage<Program> message)
        {
            CurrentProgram = message.Content;
        }

        private void SelectedSpriteChangedMessageAction(GenericMessage<Sprite> message)
        {
            CurrentSprite = message.Content;
        }

        #endregion

        public AddNewGlobalVariableViewModel()
        {
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);

            Messenger.Default.Register<GenericMessage<Program>>(this,
                 ViewModelMessagingToken.CurrentProgramChangedListener, CurrentProgramChangedMessageAction);
            Messenger.Default.Register<GenericMessage<Sprite>>(this,
                ViewModelMessagingToken.CurrentSpriteChangedListener, SelectedSpriteChangedMessageAction);
        }


        private void ResetViewModel()
        {
            UserVariableName = AppResourcesHelper.Get("Editor_DefaultGlobalVariableName");
        }
    }
}
