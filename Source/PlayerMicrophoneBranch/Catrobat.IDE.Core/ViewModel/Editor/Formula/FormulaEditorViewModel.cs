﻿using Catrobat.IDE.Core.FormulaEditor.Editor;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.Formula;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;

namespace Catrobat.IDE.Core.ViewModel.Editor.Formula
{
    public delegate void FormulaChanged(SelectedFormulaInformation formulaInformation);

    public delegate void ErrorOccurred();

    public class FormulaEditorViewModel : ViewModelBase
    {
        #region Events

        public FormulaChanged FormulaChanged;

        public ErrorOccurred ErrorOccurred;

        private void RaiseFormulaChanged(SelectedFormulaInformation formulaInformation)
        {
            if (FormulaChanged != null)
                FormulaChanged.Invoke(formulaInformation);
        }

        private void RaiseKeyError()
        {
            if (ErrorOccurred != null)
                ErrorOccurred.Invoke();
        }

        #endregion

        #region Private Members

        private Core.CatrobatObjects.Formulas.Formula _formula;
        private Sprite _selectedSprite;
        private IPortableFormulaButton _formulaButton;
        private Project _currentProject;
        private SelectedFormulaInformation _selectedFormulaInformation;
        private readonly Stack<FormulaTree> _undoStack = new Stack<FormulaTree>();
        private readonly Stack<FormulaTree> _redoStack = new Stack<FormulaTree>(); 

        #endregion

        #region Properties

        public Project CurrentProject
        {
            get { return _currentProject; }
            private set { _currentProject = value; RaisePropertyChanged(() => CurrentProject); }
        }

        public IPortableFormulaButton FormulaButton
        {
            get { return _formulaButton; }
            set
            {
                _formulaButton = value;
                RaisePropertyChanged(() => FormulaButton);
            }
        }

        public SelectedFormulaInformation SelectedFormulaInformation
        {
            get { return _selectedFormulaInformation; }
            set
            {
                _selectedFormulaInformation = value;
                RaisePropertyChanged(() => SelectedFormulaInformation);
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

        public Core.CatrobatObjects.Formulas.Formula Formula
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

        public RelayCommand<IPortableUIFormula> FormulaPartSelectedComand { get; private set; }

        public RelayCommand FormulaChangedCommand { get; private set; }

        public RelayCommand<SensorVariable> SensorVariableSelectedCommand { get; private set; }

        public RelayCommand<ObjectVariable> ObjectVariableSelectedCommand { get; private set; }

        public RelayCommand<FormulaEditorKey> KeyPressedCommand { get; private set; }

        #endregion

        #region Actions

        private static void FormulaPartSelectedAction(IPortableUIFormula formula)
        {
            bool wasSelected = formula.IsSelected;

            formula.ClearAllSelection();

            if (formula.IsEditEnabled)
                formula.IsSelected = !wasSelected;
        }

        private void FormulaChangedAction()
        {
            if (FormulaButton != null)
                FormulaButton.FormulaChanged();
        }

        private void KeyPressedCommandAction(FormulaEditorKey key)
        {
            var formulaEditor = new FormulaEditor.Editor.FormulaEditor
            {
                SelectedFormula = SelectedFormulaInformation,
                UndoStack = _undoStack,
                RedoStack = _redoStack
            };
            if (!formulaEditor.KeyPressed(key))
                RaiseKeyError();

            FormulaButton.FormulaChanged();
            RaiseFormulaChanged(SelectedFormulaInformation);
        }

        private void ObjectVariableSelectedAction(ObjectVariable variable)
        {
            var formulaEditor = new FormulaEditor.Editor.FormulaEditor
            {
                SelectedFormula = SelectedFormulaInformation,
                UndoStack = _undoStack,
                RedoStack = _redoStack
            };
            if (!formulaEditor.ObjectVariableSelected(variable))
                RaiseKeyError();

            FormulaButton.FormulaChanged();
            RaiseFormulaChanged(SelectedFormulaInformation);
        }

        private void SensorVariableSelectedAction(SensorVariable variable)
        {
            var formulaEditor = new FormulaEditor.Editor.FormulaEditor
            {
                SelectedFormula = SelectedFormulaInformation,
                UndoStack = _undoStack,
                RedoStack = _redoStack
            };
            if (!formulaEditor.SensorVariableSelected(variable))
                RaiseKeyError();

            FormulaButton.FormulaChanged();
            RaiseFormulaChanged(SelectedFormulaInformation);
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

        private void SelectedUserVariableChangedMessageAction(GenericMessage<UserVariable> message)
        {
            var formulaEditor = new FormulaEditor.Editor.FormulaEditor { SelectedFormula = SelectedFormulaInformation };
            var variable = message.Content;

            if (VariableHelper.IsVariableLocal(CurrentProject, variable))
            {
                if (!formulaEditor.LocalVariableSelected(variable))
                    RaiseKeyError();
            }
            else
            {
                if (!formulaEditor.GlobalVariableSelected(variable))
                    RaiseKeyError();
            }

            RaiseFormulaChanged(SelectedFormulaInformation);
        }

        #endregion

        public FormulaEditorViewModel()
        {
            FormulaPartSelectedComand = new RelayCommand<IPortableUIFormula>(FormulaPartSelectedAction);
            FormulaChangedCommand = new RelayCommand(FormulaChangedAction);

            SensorVariableSelectedCommand = new RelayCommand<SensorVariable>(SensorVariableSelectedAction);
            ObjectVariableSelectedCommand = new RelayCommand<ObjectVariable>(ObjectVariableSelectedAction);
            KeyPressedCommand = new RelayCommand<FormulaEditorKey>(KeyPressedCommandAction);


            Messenger.Default.Register<GenericMessage<Sprite>>(this,
                ViewModelMessagingToken.CurrentSpriteChangedListener, SelectedSpriteChangedMessageAction);

            Messenger.Default.Register<GenericMessage<Project>>(this,
                 ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedMessageAction);


            Messenger.Default.Register<GenericMessage<UserVariable>>(this,
                 ViewModelMessagingToken.SelectedUserVariableChangedListener, SelectedUserVariableChangedMessageAction); 
        }


        private void ResetViewModel() { }

        public override void Cleanup()
        {
            base.Cleanup();
            _undoStack.Clear();
            _redoStack.Clear();
        }
    }
}