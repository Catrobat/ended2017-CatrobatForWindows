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
        private bool _isLocalView;
        private bool _isSelectedVariableInEditMode;

        #endregion

        #region Properties

        public bool IsLocalView
        {
            get { return _isLocalView; }
            set
            {
                _isLocalView = value;
                RaisePropertyChanged(() => IsLocalView);
            }
        }

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
                if (value == _selectedLocalVariable)
                    return;

                var oldSelectedVariable = _selectedLocalVariable;

                _selectedLocalVariable = value;

                var selectedLocalVariable = _selectedLocalVariable;
                ReaddLocalVariable(_selectedLocalVariable);
                _selectedLocalVariable = selectedLocalVariable;

                ReaddLocalVariable(oldSelectedVariable);

                if (_selectedLocalVariable != null)
                {
                    SelectedGlobalVariable = null;

                    if (SelectedVariableContainer != null)
                        SelectedVariableContainer.Variable = _selectedLocalVariable;
                }

                RaisePropertyChanged(() => SelectedLocalVariable);
                RaisePropertyChanged(() => LocalVariables);
            }
        }

        public UserVariable SelectedGlobalVariable
        {
            get { return _selectedGlobalVariable; }
            set
            {
                if (value == _selectedGlobalVariable)
                    return;

                var oldSelectedVariable = _selectedGlobalVariable;

                _selectedGlobalVariable = value;

                var selectedGlobalVariable = _selectedGlobalVariable;
                ReaddGlobalVariable(_selectedGlobalVariable);
                _selectedGlobalVariable = selectedGlobalVariable;

                ReaddGlobalVariable(oldSelectedVariable);

                if (_selectedGlobalVariable != null)
                {
                    SelectedLocalVariable = null;

                    if (SelectedVariableContainer != null)
                        SelectedVariableContainer.Variable = _selectedGlobalVariable;
                }

                RaisePropertyChanged(() => SelectedGlobalVariable);
                RaisePropertyChanged(() => GlobalVariables);
            }
        }

        public VariableConteiner SelectedVariableContainer
        {
            get { return _selectedVariableContainer; }
            set
            {
                _selectedVariableContainer = value;
                RaisePropertyChanged(() => SelectedVariableContainer);

                if (_selectedVariableContainer != null && _selectedVariableContainer.Variable != null)
                {
                    if (VariableHelper.IsVariableLocal(CurrentProject, _selectedVariableContainer.Variable))
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

        public bool IsSelectedVariableInEditMode
        {
            get { return _isSelectedVariableInEditMode; }
            set
            {
                _isSelectedVariableInEditMode = value;
                RaisePropertyChanged(() => IsSelectedVariableInEditMode);
            }
        }

        #endregion

        #region Commands

        public RelayCommand FinishedCommand { get; private set; }

        public RelayCommand AddVariableCommand { get; private set; }

        public RelayCommand DeleteSelectedVariableCommand { get; private set; }

        public RelayCommand StartEditSelectedVariableCommand { get; private set; }

        public RelayCommand EndEditSelectedVariableCommand { get; private set; }

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
            if (IsSelectedVariableInEditMode)
                EndEditSelectedVariableAction();

            if (SelectedVariableContainer == null)
            {
                var selectedVariable = SelectedLocalVariable ?? SelectedGlobalVariable;
                var message = new GenericMessage<UserVariable>(selectedVariable);
                Messenger.Default.Send(message, ViewModelMessagingToken.UserVariableSelectedListener);
            }
            else
            {
                SelectedVariableContainer.Variable = SelectedGlobalVariable ?? SelectedLocalVariable;
            }

            Reset();
            Navigation.NavigateBack();
        }

        private void DeleteSelectedVariableAction()
        {
            if (SelectedGlobalVariable != null)
                VariableHelper.DeleteGlobalVariable(CurrentProject, SelectedGlobalVariable);

            if (SelectedLocalVariable != null)
                VariableHelper.DeleteLocalVariable(CurrentProject, CurrentSprite, SelectedLocalVariable);
        }

        private void AddVariableAction()
        {
            if (IsLocalView)
            {
                var variable = VariableHelper.CreateUniqueLocalVariable(CurrentProject, CurrentSprite);
                VariableHelper.AddLocalVariable(CurrentProject, CurrentSprite, variable);
            }
            else
            {
                var variable = VariableHelper.CreateUniqueGlobalVariable(CurrentProject);
                VariableHelper.AddGlobalVariable(CurrentProject, variable);
            }


            if (GlobalVariables == null)
                GlobalVariables = VariableHelper.GetGlobalVariableList(CurrentProject);

            if (LocalVariables == null)
                LocalVariables = VariableHelper.GetLocalVariableList(CurrentProject, CurrentSprite);
        }

        private void StartEditSelectedVariableAction()
        {
            IsSelectedVariableInEditMode = true;

            var selectedLocalVariable = _selectedLocalVariable;
            ReaddLocalVariable(_selectedLocalVariable);
            _selectedLocalVariable = selectedLocalVariable;

            var selectedGlobalVariable = _selectedGlobalVariable;
            ReaddGlobalVariable(_selectedGlobalVariable);
            _selectedGlobalVariable = selectedGlobalVariable;

            RaisePropertyChanged(() => SelectedLocalVariable);
            RaisePropertyChanged(() => SelectedGlobalVariable);
        }

        private void EndEditSelectedVariableAction()
        {
            IsSelectedVariableInEditMode = false;

            var selectedLocalVariable = _selectedLocalVariable;
            ReaddLocalVariable(_selectedLocalVariable);
            _selectedLocalVariable = selectedLocalVariable;

            var selectedGlobalVariable = _selectedGlobalVariable;
            ReaddGlobalVariable(_selectedGlobalVariable);
            _selectedGlobalVariable = selectedGlobalVariable;

            RaisePropertyChanged(()=> SelectedLocalVariable);
            RaisePropertyChanged(() => SelectedGlobalVariable);
        }

        #endregion

        #region MessageActions

        private void CurrentProjectChangedMessageAction(GenericMessage<Project> message)
        {
            CurrentProject = message.Content;
            GlobalVariables = CurrentProject.VariableList.ProgramVariableList.UserVariables;

            if (CurrentSprite == null) return;

            LocalVariables = VariableHelper.GetLocalVariableList(CurrentProject, CurrentSprite);
        }

        private void CurrentSpriteChangedMesageAction(GenericMessage<Sprite> message)
        {
            CurrentSprite = message.Content;

            if (CurrentProject == null) return;

            LocalVariables = VariableHelper.GetLocalVariableList(CurrentProject, CurrentSprite);
        }

        #endregion

        public VariableSelectionViewModel()
        {
            // Commands
            FinishedCommand = new RelayCommand(FinishedAction, FinishedCommand_CanExecute);
            AddVariableCommand = new RelayCommand(AddVariableAction);
            DeleteSelectedVariableCommand = new RelayCommand(DeleteSelectedVariableAction);
            StartEditSelectedVariableCommand = new RelayCommand(StartEditSelectedVariableAction);
            EndEditSelectedVariableCommand = new RelayCommand(EndEditSelectedVariableAction);

            Messenger.Default.Register<GenericMessage<Project>>(this,
                ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedMessageAction);

            Messenger.Default.Register<GenericMessage<Sprite>>(this,
                ViewModelMessagingToken.CurrentSpriteChangedListener, CurrentSpriteChangedMesageAction);
        }

        public void ReaddLocalVariable(UserVariable variable)
        {
            if (variable != null)
            {
                var index = LocalVariables.IndexOf(variable);

                if (index < 0) return;

                LocalVariables.RemoveAt(index);
                LocalVariables.Insert(index, variable);
            }
        }

        public void ReaddGlobalVariable(UserVariable variable)
        {
            if (variable != null)
            {
                var index = GlobalVariables.IndexOf(variable);

                if (index < 0) return;

                GlobalVariables.RemoveAt(index);
                GlobalVariables.Insert(index, variable);
            }
        }

        public void Reset()
        {
            //SelectedGlobalVariable = null;
            //SelectedLocalVariable = null;
            //SelectedVariableContainer = null;
        }
    }
}