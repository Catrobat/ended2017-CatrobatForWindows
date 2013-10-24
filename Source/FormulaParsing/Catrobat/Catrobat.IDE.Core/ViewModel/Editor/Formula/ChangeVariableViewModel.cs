using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModel.Editor.Formula
{
    public class ChangeVariableViewModel : ViewModelBase
    {
        #region Private Members

        private Project _currentProject;
        private Sprite _selectedSprite;
        private UserVariable _userVariable;
        private string _userVariableName;

        #endregion

        #region Properties

        public Project CurrentProject
        {
            get { return _currentProject; }
            private set { _currentProject = value; RaisePropertyChanged(() => CurrentProject); }
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

        public UserVariable UserVariable
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

        public RelayCommand InitializeCommand { get; private set; }

        public RelayCommand SaveCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        public RelayCommand ResetViewModelCommand { get; private set; }

        #endregion

        #region CommandCanExecute

        private bool SaveCommand_CanExecute()
        {
            return !string.IsNullOrEmpty(UserVariableName) &&
                   !VariableHelper.VariableNameExistsCheckSelf(CurrentProject, SelectedSprite, UserVariable, UserVariableName);
        }

        #endregion

        #region Actions

        private void InitializeAction()
        {
            UserVariableName = UserVariable.Name;
        }

        private void SaveAction()
        {
            UserVariable.Name = UserVariableName;
            ServiceLocator.NavigationService.NavigateBack();
        }

        private void CancelAction()
        {
            ServiceLocator.NavigationService.NavigateBack();
        }

        private void ResetViewModelAction()
        {
            ResetViewModel();
        }


        #endregion

        #region MessageActions

        private void CurrentProjectChangedMessageAction(GenericMessage<Project> message)
        {
            CurrentProject = message.Content;
        }

        private void SelectedSpriteChangedMessageAction(GenericMessage<Sprite> message)
        {
            SelectedSprite = message.Content;
        }

        private void SelectedUserVariableChangedMessageAction(GenericMessage<UserVariable> message)
        {
            UserVariable = message.Content;
        }

        #endregion

        public ChangeVariableViewModel()
        {
            InitializeCommand = new RelayCommand(InitializeAction);
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);
            ResetViewModelCommand = new RelayCommand(ResetViewModelAction);

            Messenger.Default.Register<GenericMessage<Project>>(this,
                 ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedMessageAction);
            Messenger.Default.Register<GenericMessage<Sprite>>(this,
                ViewModelMessagingToken.CurrentSpriteChangedListener, SelectedSpriteChangedMessageAction);
            Messenger.Default.Register<GenericMessage<UserVariable>>(this,
                ViewModelMessagingToken.SelectedUserVariableChangedListener, SelectedUserVariableChangedMessageAction);
        }


        private void ResetViewModel()
        {
            
        }
    }
}
