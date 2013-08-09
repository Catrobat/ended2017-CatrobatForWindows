using System.Collections.ObjectModel;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Variables;
using Catrobat.IDEWindowsPhone.Controls.FormulaControls;
using Catrobat.IDEWindowsPhone.Misc;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDEWindowsPhone.ViewModel.Editor.Formula
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
        private VariableConteiner _selectedVariableContainer;

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
                RaisePropertyChanged(() => CurrentSprite);
            }
        }

        public ObservableCollection<UserVariable> LocalVariables
        {
            get { return _localVariables; }
            set
            {
                _localVariables = value;
                RaisePropertyChanged(() => LocalVariables);
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
                RaisePropertyChanged(() => SelectedLocalVariable);

                if (_selectedLocalVariable != null)
                {
                    SelectedGlobalVariable = null;

                    if (SelectedVariableContainer != null)
                        SelectedVariableContainer.Variable = _selectedLocalVariable;
                }
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
                {
                    SelectedLocalVariable = null;

                    if (SelectedVariableContainer != null)
                        SelectedVariableContainer.Variable = _selectedGlobalVariable;
                }
            }
        }

        public VariableConteiner SelectedVariableContainer
        {
            get { return _selectedVariableContainer; }
            set
            {
                _selectedVariableContainer = value;
                RaisePropertyChanged(() => SelectedVariableContainer);

                if (_selectedVariableContainer!= null && _selectedVariableContainer.Variable != null)
                {
                    if (VariableHelper.IsVariableLogal(CurrentProject, _selectedVariableContainer.Variable))
                        SelectedLocalVariable = _selectedVariableContainer.Variable;
                    else
                        SelectedGlobalVariable = _selectedVariableContainer.Variable;
                }
                else
                {
                    SelectedLocalVariable = null;
                    SelectedGlobalVariable = null;
                }


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
            return true;
        }

        #endregion

        #region Actions

        private void FinishedAction()
        {
            Reset();

            if (SelectedVariableContainer == null)
            {
                var selectedVariable = SelectedLocalVariable ?? SelectedGlobalVariable;
                var message = new GenericMessage<UserVariable>(selectedVariable);
                Messenger.Default.Send(message, ViewModelMessagingToken.UserVariableSelectedListener);
            }

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

        public void Reset()
        {
            SelectedGlobalVariable = null;
            SelectedLocalVariable = null;
            SelectedVariableContainer = null;
        }
    }
}