using System.Collections.ObjectModel;
using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Variables;
using Catrobat.IDECommon.Formula.Editor;
using Catrobat.IDEWindowsPhone.Controls.FormulaControls;
using Catrobat.IDEWindowsPhone.Controls.FormulaControls.Formulas;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.Views.Editor.Sounds;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDEWindowsPhone.ViewModel.Editor.Formula
{
    public delegate void FormulaChangedCallback(Core.Objects.Formulas.Formula formula);


    public class FormulaEditorViewModel : ViewModelBase
    {
        #region Events

        public FormulaChangedCallback FormulaChangedCallback;

        #endregion

        #region Private Members

        private Core.Objects.Formulas.Formula _formula;
        private Sprite _selectedSprite;
        private Project _selectedProject;
        private FormulaButton _formulaButton;
        private Project _currentProject;

        #endregion

        #region Properties

        public Project CurrentProject
        {
            get { return _currentProject; }
            private set { _currentProject = value; RaisePropertyChanged(() => CurrentProject);}
        }

        public FormulaButton FormulaButton
        {
            get { return _formulaButton; }
            set
            {
                _formulaButton = value;
                RaisePropertyChanged(() => FormulaButton);
            }
        }

        public Project SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                _selectedProject = value;
                RaisePropertyChanged(() => SelectedProject);

                if (_selectedProject == null) return;
            }
        }

        public Sprite SelectedSprite
        {
            get { return _selectedSprite; }
            set
            {
                _selectedSprite = value;
                RaisePropertyChanged(() => SelectedSprite);

                if (_selectedSprite == null) return;
            }
        }

        public Core.Objects.Formulas.Formula Formula
        {
            get { return _formula; }
            set
            {
                _formula = value;
                RaisePropertyChanged(() => Formula);
            }
        }

        #endregion

        #region Commands

        public RelayCommand<UiFormula> FormulaPartSelectedComand { get; private set; }

        public RelayCommand FormulaChangedCommand { get; private set; }

        #endregion

        #region Actions

        private void FormulaPartSelectedAction(UiFormula formula)
        {
            bool wasSelected = formula.IsSelected;

            formula.ClearAllSelection();

            if(formula.IsEditEnabled)
                formula.IsSelected = !wasSelected;
        }

        private void FormulaChangedAction()
        {
            if(FormulaButton != null)
                FormulaButton.FormulaChanged();
        }

        #endregion

        #region MessageActions

        private void CurrentProjectChangedMessageAction(GenericMessage<Project> message)
        {
            CurrentProject = message.Content;
        }

        private void SelectedSpriteChangesMessageAction(GenericMessage<Sprite> message)
        {
            SelectedSprite = message.Content;
        }

        private void UserVariableSelectedMessageAction(GenericMessage<UserVariable> message)
        {
            //TODO: add SelectedFormulaInformation, pass all events from Keyboard to viewer through the ViewModel (with commands)

            //var formulaEditor = new FormulaEditor { SelectedFormula = FormulaViewer.GetSelectedFormula() };
            //if (!formulaEditor.ObjectVariableSelected(variable))
            //    ShowKeyErrorAnimation();
            //FormulaViewer.FormulaChanged();
            //_viewModel.FormulaChangedCommand.Execute(null);
        }

        #endregion

        public FormulaEditorViewModel()
        {
            Messenger.Default.Register<GenericMessage<Sprite>>(this, 
                ViewModelMessagingToken.CurrentSpriteChangedListener, SelectedSpriteChangesMessageAction);

            Messenger.Default.Register<GenericMessage<Project>>(this,
                 ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedMessageAction);


            Messenger.Default.Register<GenericMessage<UserVariable>>(this,
                 ViewModelMessagingToken.UserVariableSelectedListener, UserVariableSelectedMessageAction);

            FormulaPartSelectedComand = new RelayCommand<UiFormula>(FormulaPartSelectedAction);
            FormulaChangedCommand = new RelayCommand(FormulaChangedAction);
        }

        private void ResetViewModel() { }
    }
}