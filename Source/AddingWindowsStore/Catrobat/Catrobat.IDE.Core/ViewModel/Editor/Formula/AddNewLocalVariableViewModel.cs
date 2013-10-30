using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Resources.Localization;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModel.Editor.Formula
{
    public class AddNewLocalVariableViewModel : ViewModelBase
    {
        #region Private Members

        private Project _currentProject;
        private Sprite _selectedSprite;
        private string _userVariableName = AppResources.Editor_StandardLocalVariableName;

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
                   !VariableHelper.VariableNameExists(CurrentProject, SelectedSprite, UserVariableName);
        }

        #endregion

        #region Actions

        private void SaveAction()
        {
            VariableHelper.AddLocalVariable(CurrentProject, SelectedSprite, new UserVariable { Name = UserVariableName });
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

        private void CurrentProjectChangedMessageAction(GenericMessage<Project> message)
        {
            CurrentProject = message.Content;
        }

        private void SelectedSpriteChangedMessageAction(GenericMessage<Sprite> message)
        {
            SelectedSprite = message.Content;
        }

        #endregion

        public AddNewLocalVariableViewModel()
        {
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);

            Messenger.Default.Register<GenericMessage<Project>>(this,
                 ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedMessageAction);
            Messenger.Default.Register<GenericMessage<Sprite>>(this,
                ViewModelMessagingToken.CurrentSpriteChangedListener, SelectedSpriteChangedMessageAction);
        }


        private void ResetViewModel()
        {
            UserVariableName = AppResources.Editor_StandardLocalVariableName;
        }
    }
}
