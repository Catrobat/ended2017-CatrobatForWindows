using System.Collections.ObjectModel;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Variables;
using Catrobat.IDEWindowsPhone.Misc;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDEWindowsPhone.ViewModel.Editor.Variables
{
    public class VariableSelectionViewModel : ViewModelBase
    {
        #region private Members

        private Project _currentProject;
        private Sprite _currentSprite;
        private ObservableCollection<UserVariable> _localVariables;
        private ObservableCollection<UserVariable> _globalVariables;
        private UserVariable _selectedLocalVariable;
        private UserVariable _selectedGlobalVariable;

        #endregion

        #region Properties

        public Project CurrentProject
        {
            get { return _currentProject; }
            set { _currentProject = value; RaisePropertyChanged(() => CurrentProject); }
        }

        public Sprite CurrentSprite
        {
            get { return _currentSprite; }
            set
            {
                _currentSprite = value; 
                RaisePropertyChanged(()=> CurrentSprite);
            }
        }

        public ObservableCollection<UserVariable> LocalVariables
        {
            get { return _localVariables; }
            set
            {
                _localVariables = value; 
                RaisePropertyChanged(()=> LocalVariables);
            }
        }

        public ObservableCollection<UserVariable> GlobalVariables
        {
            get { return _globalVariables; }
            set
            {
                _globalVariables = value;
                RaisePropertyChanged(() => GlobalVariables);
            }
        }

        public UserVariable SelectedLocalVariable
        {
            get { return _selectedLocalVariable; }
            set
            {
                _selectedLocalVariable = value; 
                RaisePropertyChanged(()=> SelectedLocalVariable);

                if (_selectedLocalVariable != null)
                    SelectedGlobalVariable = null;
            }
        }

        public UserVariable SelectedGlobalVariable
        {
            get { return _selectedGlobalVariable; }
            set
            {
                _selectedGlobalVariable = value;
                RaisePropertyChanged(() => SelectedGlobalVariable);

                if (_selectedGlobalVariable != null)
                    SelectedLocalVariable = null;
            }
        }

        #endregion

        #region Commands

        public RelayCommand FinishedCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        #endregion

        #region CommandCanExecute

        private bool FinishedCommand_CanExecute()
        {
            return false;
        }

        #endregion

        #region Actions

        private void FinishedAction()
        {
            // TODO: send message

            Navigation.NavigateBack();
        }


        private void CancelAction()
        {
            Navigation.NavigateBack();
        }

        #endregion

        #region MessageActions

        private void CurrentProjectChangedMessageAction(GenericMessage<Project> message)
        {
            CurrentProject = message.Content;
            GlobalVariables = CurrentProject.VariableList.ProgramVariableList.UserVariables;

            if (CurrentSprite == null) return;

            LocalVariables = VariableHelper.GetAndCreateLocalVariableList(CurrentProject, CurrentSprite);
        }

        private void CurrentSpriteChangedMesageAction(GenericMessage<Sprite> message)
        {
            CurrentSprite = message.Content;

            if (CurrentProject == null) return;

            LocalVariables = VariableHelper.GetAndCreateLocalVariableList(CurrentProject, CurrentSprite);
        }

        #endregion

        public VariableSelectionViewModel()
        {
            // Commands
            FinishedCommand = new RelayCommand(FinishedAction, FinishedCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);

            Messenger.Default.Register<GenericMessage<Project>>(this,
                ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedMessageAction);

            Messenger.Default.Register<GenericMessage<Sprite>>(this,
                ViewModelMessagingToken.CurrentSpriteChangedListener, CurrentSpriteChangedMesageAction);
        }

        public override void Cleanup()
        {
            CurrentProject = null;
            base.Cleanup();
        }
    }
}