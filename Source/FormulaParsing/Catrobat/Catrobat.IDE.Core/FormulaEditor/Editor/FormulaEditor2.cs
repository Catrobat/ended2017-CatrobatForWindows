using System;
using System.Globalization;
using System.Linq;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using System.Collections.Generic;
using Catrobat.IDE.Core.FormulaEditor;
using Catrobat.IDE.Core.ViewModel;

namespace Catrobat.IDE.Core.FormulaEditor.Editor
{
    [Obsolete("Use FormulaEditor3 instead. ")]
    public class FormulaEditor2 : ViewModelBase
    {
        private class EditorState
        {
            public string FormulaString { get; set; }
            public int CaretIndex { get; set; }
        }

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

        private readonly Stack<EditorState> _undoStack = new Stack<EditorState>();
        private readonly Stack<EditorState> _redoStack = new Stack<EditorState>();

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

        public void ResetViewModel()
        {
            _undoStack.Clear();
            _redoStack.Clear();
        }

        public bool KeyPressed(FormulaEditorKey key)
        {
            switch (key)
            {
                case FormulaEditorKey.Undo:
                    if (_undoStack.Count == 0) return false;
                    PushState(_redoStack);
                    PopState(_undoStack);
                    return true;
                case FormulaEditorKey.Redo:
                    if (_redoStack.Count == 0) return false;
                    PushState(_undoStack);
                    PopState(_redoStack);
                    return true;
                case FormulaEditorKey.Delete:
                    if (CaretIndex == 0) return false;
                    PushState(_redoStack);
                    _redoStack.Clear();
                    FormulaString = FormulaString.Remove(CaretIndex - 1, 1);
                    CaretIndex--;
                    return true;
                default:
                    PushState(_undoStack);
                    _redoStack.Clear();
                    InsertText(GetText(key));
                    return true;
            }
        }

        public bool SensorVariableSelected(SensorVariable variable)
        {
            PushState(_undoStack);
            _redoStack.Clear();
            InsertText(variable.ToString());
            return true;
        }

        public bool ObjectVariableSelected(ObjectVariable variable)
        {
            PushState(_undoStack);
            _redoStack.Clear();
            InsertText(variable.ToString());
            return true;
        }

        public bool GlobalVariableSelected(UserVariable variable)
        {
            PushState(_undoStack);
            _redoStack.Clear();
            InsertText(variable.ToString());
            return true;
        }

        public bool LocalVariableSelected(UserVariable variable)
        {
            PushState(_undoStack);
            _redoStack.Clear();
            InsertText(variable.ToString());
            return true;
        }

        void InsertText(string value)
        {
            FormulaString = FormulaString.Insert(CaretIndex, value);
            CaretIndex += value.Length;
        }

        private void PushState(Stack<EditorState> stack)
        {
            stack.Push(new EditorState
            {
                CaretIndex = CaretIndex,
                FormulaString = FormulaString
            });
        }

        private void PopState(Stack<EditorState> stack)
        {
            var state = stack.Pop();
            CaretIndex = state.CaretIndex;
            FormulaString = state.FormulaString;
        }

        private static string GetText(FormulaEditorKey key)
        {
            switch (key)
            {
                case FormulaEditorKey.Number0: return 0.ToString();
                case FormulaEditorKey.Number1: return 1.ToString();
                case FormulaEditorKey.Number2: return 2.ToString();
                case FormulaEditorKey.Number3: return 3.ToString();
                case FormulaEditorKey.Number4: return 4.ToString();
                case FormulaEditorKey.Number5: return 5.ToString();
                case FormulaEditorKey.Number6: return 6.ToString();
                case FormulaEditorKey.Number7: return 7.ToString();
                case FormulaEditorKey.Number8: return 8.ToString();
                case FormulaEditorKey.Number9: return 9.ToString();
                case FormulaEditorKey.NumberDot: return CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator;
                case FormulaEditorKey.NumberEquals: return "=";
                case FormulaEditorKey.OpenBracket: return "(";
                case FormulaEditorKey.CloseBracket: return ")";
                case FormulaEditorKey.Plus: return "+";
                case FormulaEditorKey.Minus: return "-";
                case FormulaEditorKey.Multiply: return "*";
                case FormulaEditorKey.Divide: return "/";
                case FormulaEditorKey.LogicEqual: return "=";
                case FormulaEditorKey.LogicNotEqual: return "<>";
                case FormulaEditorKey.LogicSmaller: return "<";
                case FormulaEditorKey.LogicSmallerEqual: return "<=";
                case FormulaEditorKey.LogicGreater: return ">";
                case FormulaEditorKey.LogicGreaterEqual: return ">=";
                case FormulaEditorKey.LogicAnd: return " and ";
                case FormulaEditorKey.LogicOr: return " or ";
                case FormulaEditorKey.LogicNot: return "not ";
                case FormulaEditorKey.LogicTrue: return "True";
                case FormulaEditorKey.LogicFalse: return "False";
                case FormulaEditorKey.MathSin: return "sin(";
                case FormulaEditorKey.MathCos: return "cos(";
                case FormulaEditorKey.MathTan: return "tan(";
                case FormulaEditorKey.MathArcSin: return "arcsin(";
                case FormulaEditorKey.MathArcCos: return "arccos(";
                case FormulaEditorKey.MathArcTan: return "arctan(";
                case FormulaEditorKey.MathExp: return "exp(";
                case FormulaEditorKey.MathLn: return "ln(";
                case FormulaEditorKey.MathLog: return "log(";
                case FormulaEditorKey.MathAbs: return "|";
                case FormulaEditorKey.MathRound: return "round(";
                case FormulaEditorKey.MathMod: return "mod(";
                case FormulaEditorKey.MathMin: return "min(";
                case FormulaEditorKey.MathMax: return "max(";
                case FormulaEditorKey.MathSqrt: return "sqrt(";
                case FormulaEditorKey.MathPi: return "pi";
                case FormulaEditorKey.MathRandom: return "rand(";
                default:
                    throw new ArgumentOutOfRangeException("key");
            }
        }

    }
}
