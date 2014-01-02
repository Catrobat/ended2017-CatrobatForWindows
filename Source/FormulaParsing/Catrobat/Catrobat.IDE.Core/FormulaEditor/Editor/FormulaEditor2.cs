using System;
using System.Linq;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using System.Collections.Generic;
using Catrobat.IDE.Core.ViewModel;

namespace Catrobat.IDE.Core.FormulaEditor.Editor
{
    public class FormulaEditor2 : ViewModelBase
    {

        #region members

        private readonly FormulaParser _parser = new FormulaParser(Enumerable.Empty<UserVariable>(), null);
        private readonly FormulaSerializer _serializer = new FormulaSerializer();

        private IFormulaTree _formula;
        public IFormulaTree Formula
        {
            get { return _formula; }
            set
            {
                _formula = value;
                RaisePropertyChanged(() => Formula);
                _formulaString = _serializer.Serialize(_formula);
                RaisePropertyChanged(() => FormulaString);
            }
        }

        private string _formulaString = string.Empty;
        public string FormulaString
        {
            get { return _formulaString; }
            set
            {
                if (_formulaString == value) return;
                _formulaString = value;
                RaisePropertyChanged(() => FormulaString);
                IEnumerable<string> parsingErrors;
                _parser.Parse(_formulaString, out _formula, out parsingErrors);
                RaisePropertyChanged(() => Formula);
                _parsingErrors = parsingErrors.ToList();
                RaisePropertyChanged(() => ParsingErrors);
            }
        }

        private List<string> _parsingErrors = new List<string>();
        public List<string> ParsingErrors
        {
            get { return _parsingErrors; }
        }

        private Stack<string> _undoStack = new Stack<string>();

        private Stack<string> _redoStack = new Stack<string>();

        private int _caretIndex;

        public int CaretIndex
        {
            get { return _caretIndex;  }
            set
            {
                if (_caretIndex == value) return;
                _caretIndex = value;
                RaisePropertyChanged(() => CaretIndex);
            }
        }

        #endregion

        #region key handlers

        public bool KeyPressed(FormulaEditorKey key)
        {
            if (key == FormulaEditorKey.Undo) return HandleUndoCommand();
            if (key == FormulaEditorKey.Redo) return HandleRedoCommand();

            _undoStack.Push(FormulaString);
            HandleKey(key);
            _redoStack.Clear();
            return true;
        }

        public bool SensorVariableSelected(SensorVariable variable)
        {
            _undoStack.Push(FormulaString);
            InsertText(variable.ToString());
            _redoStack.Clear();
            return true;
        }

        public bool ObjectVariableSelected(ObjectVariable variable)
        {
            _undoStack.Push(FormulaString);
            InsertText(variable.ToString());
            _redoStack.Clear();
            return true;
        }

        public bool GlobalVariableSelected(UserVariable variable)
        {
            _undoStack.Push(FormulaString);
            InsertText(variable.ToString());
            _redoStack.Clear();
            return true;
        }

        public bool LocalVariableSelected(UserVariable variable)
        {
            _undoStack.Push(FormulaString);
            InsertText(variable.ToString());
            _redoStack.Clear();
            return true;
        }

        #endregion

        void InsertText(string value)
        {
            FormulaString = FormulaString.Insert(CaretIndex, value);
            CaretIndex += value.Length;
        }

        public void ResetViewModel()
        {
            _undoStack.Clear();
            _redoStack.Clear();
        }

        #region specific key handlers

        private void HandleKey(FormulaEditorKey key)
        {
            switch (key)
            {
                case FormulaEditorKey.Number0:
                    InsertText("0");
                    break;
                case FormulaEditorKey.Number1:
                    InsertText("1");
                    break;
                case FormulaEditorKey.Number2:
                    InsertText("2");
                    break;
                case FormulaEditorKey.Number3:
                    InsertText("3");
                    break;
                case FormulaEditorKey.Number4:
                    InsertText("4");
                    break;
                case FormulaEditorKey.Number5:
                    InsertText("5");
                    break;
                case FormulaEditorKey.Number6:
                    InsertText("6");
                    break;
                case FormulaEditorKey.Number7:
                    InsertText("7");
                    break;
                case FormulaEditorKey.Number8:
                    InsertText("8");
                    break;
                case FormulaEditorKey.Number9:
                    InsertText("9");
                    break;
                case FormulaEditorKey.NumberDot:
                    InsertText(".");
                    break;
                case FormulaEditorKey.NumberEquals:
                    InsertText("=");
                    break;
                case FormulaEditorKey.Delete:
                    break;
                case FormulaEditorKey.OpenBracket:
                    InsertText("(");
                    break;
                case FormulaEditorKey.CloseBracket:
                    InsertText(")");
                    break;
                case FormulaEditorKey.Plus:
                    InsertText("+");
                    break;
                case FormulaEditorKey.Minus:
                    InsertText("-");
                    break;
                case FormulaEditorKey.Multiply:
                    InsertText("*");
                    break;
                case FormulaEditorKey.Divide:
                    InsertText("/");
                    break;
                case FormulaEditorKey.LogicEqual:
                    InsertText("=");
                    break;
                case FormulaEditorKey.LogicNotEqual:
                    // TODO
                    break;
                case FormulaEditorKey.LogicSmaller:
                    InsertText("<");
                    break;
                case FormulaEditorKey.LogicSmallerEqual:
                    InsertText("<=");
                    break;
                case FormulaEditorKey.LogicGreater:
                    InsertText(">");
                    break;
                case FormulaEditorKey.LogicGreaterEqual:
                    InsertText(">=");
                    break;
                case FormulaEditorKey.LogicAnd:
                    break;
                case FormulaEditorKey.LogicOr:
                    break;
                case FormulaEditorKey.LogicNot:
                    break;
                case FormulaEditorKey.LogicTrue:
                    break;
                case FormulaEditorKey.LogicFalse:
                    break;
                case FormulaEditorKey.MathSin:
                    break;
                case FormulaEditorKey.MathCos:
                    break;
                case FormulaEditorKey.MathTan:
                    break;
                case FormulaEditorKey.MathArcSin:
                    break;
                case FormulaEditorKey.MathArcCos:
                    break;
                case FormulaEditorKey.MathArcTan:
                    break;
                case FormulaEditorKey.MathExp:
                    break;
                case FormulaEditorKey.MathLn:
                    break;
                case FormulaEditorKey.MathLog:
                    break;
                case FormulaEditorKey.MathAbs:
                    break;
                case FormulaEditorKey.MathRound:
                    break;
                case FormulaEditorKey.MathMod:
                    break;
                case FormulaEditorKey.MathMin:
                    break;
                case FormulaEditorKey.MathMax:
                    break;
                case FormulaEditorKey.MathSqrt:
                    break;
                case FormulaEditorKey.MathPi:
                    break;
                case FormulaEditorKey.MathRandom:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("key");
            }
        }

        private bool HandleUndoCommand()
        {
            if (_undoStack.Count == 0) return false;
            _redoStack.Push(FormulaString);
            FormulaString = _undoStack.Pop();
            CaretIndex = 0;
            return true;
        }

        private bool HandleRedoCommand()
        {
            if (_redoStack.Count == 0) return false;
            _undoStack.Push(FormulaString);
            FormulaString = _redoStack.Pop();
            CaretIndex = 0;
            return true;
        }

        #endregion

    }
}
