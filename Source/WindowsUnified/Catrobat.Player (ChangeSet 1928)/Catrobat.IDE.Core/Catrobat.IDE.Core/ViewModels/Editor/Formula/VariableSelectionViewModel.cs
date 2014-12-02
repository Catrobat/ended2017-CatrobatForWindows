using System.Collections.ObjectModel;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.Utilities.Helpers;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModels.Editor.Formula
{
    public class VariableSelectionViewModel : ViewModelBase
    {
        #region private Members

        private Program _currentProgram;
        private Sprite _currentSprite;
        private ObservableCollection<LocalVariable> _localVariables;
        private ObservableCollection<GlobalVariable> _globalVariables;
        private LocalVariable _selectedLocalVariable;
        private GlobalVariable _selectedGlobalVariable;
        private VariableConteiner _selectedVariableContainer;
        private bool _isLocalView;

        #endregion

        #region Properties

        public bool IsLocalView
        {
            get { return _isLocalView; }
            set
            {
                _isLocalView = value;
                RaisePropertyChanged(() => IsLocalView);
                EditVariableCommand.RaiseCanExecuteChanged();
                DeleteVariableCommand.RaiseCanExecuteChanged();
            }
        }

        public Program CurrentProgram
        {
            get { return _currentProgram; }
            set
            {
                _currentProgram = value;
                _selectedGlobalVariable = null;
                _selectedLocalVariable = null;
                ServiceLocator.DispatcherService.RunOnMainThread(() =>
                {
                    RaisePropertyChanged(() => CurrentProgram);
                    RaisePropertyChanged(() => SelectedGlobalVariable);
                    RaisePropertyChanged(() => SelectedLocalVariable);
                });
            }
        }

        public Sprite CurrentSprite
        {
            get { return _currentSprite; }
            set
            {
                _currentSprite = value;
                _selectedLocalVariable = null;
                ServiceLocator.DispatcherService.RunOnMainThread(() =>
                {
                    RaisePropertyChanged(() => CurrentSprite);
                    RaisePropertyChanged(() => SelectedLocalVariable);
                });
            }
        }

        public LocalVariable SelectedLocalVariable
        {
            get { return _selectedLocalVariable; }
            set
            {
                if (value == _selectedLocalVariable)
                    return;

                _selectedLocalVariable = value;

                if (_selectedLocalVariable != null)
                {
                    SelectedGlobalVariable = null;

                    if (SelectedVariableContainer != null)
                        SelectedVariableContainer.Variable = _selectedLocalVariable;
                }

                RaisePropertyChanged(() => SelectedLocalVariable);
                EditVariableCommand.RaiseCanExecuteChanged();
                DeleteVariableCommand.RaiseCanExecuteChanged();
            }
        }

        public GlobalVariable SelectedGlobalVariable
        {
            get { return _selectedGlobalVariable; }
            set
            {
                if (value == _selectedGlobalVariable)
                    return;

                _selectedGlobalVariable = value;

                if (_selectedGlobalVariable != null)
                {
                    SelectedLocalVariable = null;

                    if (SelectedVariableContainer != null)
                        SelectedVariableContainer.Variable = _selectedGlobalVariable;
                }

                RaisePropertyChanged(() => SelectedGlobalVariable);
                EditVariableCommand.RaiseCanExecuteChanged();
                DeleteVariableCommand.RaiseCanExecuteChanged();
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
                    if (VariableHelper.IsVariableLocal(CurrentProgram, _selectedVariableContainer.Variable))
                        SelectedLocalVariable = (LocalVariable)_selectedVariableContainer.Variable;
                    else
                        SelectedGlobalVariable = (GlobalVariable)_selectedVariableContainer.Variable;
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

        public RelayCommand AddVariableCommand { get; private set; }

        public RelayCommand DeleteVariableCommand { get; private set; }

        public RelayCommand EditVariableCommand { get; private set; }

        #endregion

        #region CommandCanExecute

        private bool FinishedCommand_CanExecute()
        {
            return true;
        }

        private bool DeleteVariableCommand_CanExecute()
        {
            if (IsLocalView)
                return SelectedLocalVariable != null;
            else
                return SelectedGlobalVariable != null;
        }

        private bool EditVariableCommand_CanExecute()
        {
            if (IsLocalView)
                return SelectedLocalVariable != null;
            else
                return SelectedGlobalVariable != null;
        }

        #endregion

        #region Actions

        private void FinishedAction()
        {
            if (SelectedVariableContainer == null)
            {
                var selectedVariable = (Variable)SelectedLocalVariable ?? SelectedGlobalVariable;
                var message = new GenericMessage<Variable>(selectedVariable);
                Messenger.Default.Send(message, ViewModelMessagingToken.SelectedUserVariableChangedListener);
            }
            else
            {
                SelectedVariableContainer.Variable = (Variable)SelectedGlobalVariable ?? SelectedLocalVariable;
            }

            ResetViewModel();
            base.GoBackAction();
        }

        private void AddVariableAction()
        {
            if (IsLocalView)
                ServiceLocator.NavigationService.NavigateTo<AddNewLocalVariableViewModel>();
            else
                ServiceLocator.NavigationService.NavigateTo<AddNewGlobalVariableViewModel>();
        }

        private void DeleteVariableAction()
        {
            if (SelectedGlobalVariable != null)
                VariableHelper.DeleteGlobalVariable(CurrentProgram, SelectedGlobalVariable);

            if (SelectedLocalVariable != null)
                VariableHelper.DeleteLocalVariable(CurrentProgram, CurrentSprite, SelectedLocalVariable);
        }

        private void EditVariableAction()
        {
            Variable selectedVariable;
            if (IsLocalView)
                selectedVariable = SelectedLocalVariable;
            else
                selectedVariable = SelectedGlobalVariable;

            var message = new GenericMessage<Variable>(selectedVariable);
            Messenger.Default.Send(message, ViewModelMessagingToken.SelectedUserVariableChangedListener);

            ServiceLocator.NavigationService.NavigateTo<ChangeVariableViewModel>();
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
            CurrentSprite = null;
        }

        private void CurrentSpriteChangedMesageAction(GenericMessage<Sprite> message)
        {
            CurrentSprite = message.Content;
        }

        #endregion

        public VariableSelectionViewModel()
        {
            FinishedCommand = new RelayCommand(FinishedAction, FinishedCommand_CanExecute);
            AddVariableCommand = new RelayCommand(AddVariableAction);
            DeleteVariableCommand = new RelayCommand(DeleteVariableAction, DeleteVariableCommand_CanExecute);
            EditVariableCommand = new RelayCommand(EditVariableAction, EditVariableCommand_CanExecute);

            Messenger.Default.Register<GenericMessage<Program>>(this,
                ViewModelMessagingToken.CurrentProgramChangedListener, CurrentProgramChangedMessageAction);

            Messenger.Default.Register<GenericMessage<Sprite>>(this,
                ViewModelMessagingToken.CurrentSpriteChangedListener, CurrentSpriteChangedMesageAction);
        }

        private void ResetViewModel()
        {
        }
    }
}
