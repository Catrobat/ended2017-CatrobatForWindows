using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Utilities.Helpers;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModels.Editor.Formula
{
    public class ChangeVariableViewModel : ViewModelBase
    {
        #region Private Members

        private Program _currentProgram;
        private Sprite _selectedSprite;
        private Variable _userVariable;
        private string _userVariableName;

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

        public Sprite SelectedSprite
        {
            get { return _selectedSprite; }
            set
            {
                _selectedSprite = value;
                RaisePropertyChanged(() => SelectedSprite);
            }
        }

        public Variable UserVariable
        {
            get { return _userVariable; }
            set 
            { 
                _userVariable = value; 
                RaisePropertyChanged(() => UserVariable); 
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
                   !VariableHelper.VariableNameExistsCheckSelf(CurrentProgram, SelectedSprite, UserVariable, UserVariableName);
        }

        #endregion

        #region Actions

        private void SaveAction()
        {
            UserVariable.Name = UserVariableName;
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
            SelectedSprite = message.Content;
        }

        private void SelectedUserVariableChangedMessageAction(GenericMessage<Variable> message)
        {
            UserVariable = message.Content;
        }

        #endregion

        public ChangeVariableViewModel()
        {
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);

            Messenger.Default.Register<GenericMessage<Program>>(this,
                 ViewModelMessagingToken.CurrentProgramChangedListener, CurrentProgramChangedMessageAction);
            Messenger.Default.Register<GenericMessage<Sprite>>(this,
                ViewModelMessagingToken.CurrentSpriteChangedListener, SelectedSpriteChangedMessageAction);
            Messenger.Default.Register<GenericMessage<Variable>>(this,
                ViewModelMessagingToken.SelectedUserVariableChangedListener, SelectedUserVariableChangedMessageAction);
        }

        public override void NavigateTo()
        {
            UserVariableName = UserVariable.Name;
            base.NavigateTo();
        }


        private void ResetViewModel()
        {
            UserVariableName = "";
        }
    }
}
