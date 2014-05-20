using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Formulas.FormulaToken;
using Catrobat.IDE.Core.Models.Formulas.FormulaTree;
using Catrobat.IDE.Core.ViewModels;

namespace Catrobat.IDE.Core.Formulas.Editor
{
    public class FormulaEditor : ViewModelBase
    {
 
        #region Members

        private IFormulaTree _formula;
        public IFormulaTree Formula
        {
            get { return _formula; }
            set
            {
                _formula = value;
                RaisePropertyChanged(() => Formula);
                Tokens = _formula == null ? 
                    new ObservableCollection<IFormulaToken>() : 
                    new ObservableCollection<IFormulaToken>(FormulaTokenizer.Tokenize(_formula));
            }
        }

        private ObservableCollection<IFormulaToken> _tokens;
        public ObservableCollection<IFormulaToken> Tokens
        {
            get { return _tokens; }
            private set
            {
                if (_tokens == value) return;
                if (_tokens != null) _tokens.CollectionChanged -= Tokens_CollectionChanged;
                _tokens = value;
                if (_tokens != null) _tokens.CollectionChanged += Tokens_CollectionChanged;
                RaisePropertyChanged(() => Tokens);
                RaisePropertyChanged(() => IsTokensEmpty);
                InterpretTokens();
            }
        }
        private void Tokens_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            InterpretTokens();
            RaisePropertyChanged(() => IsTokensEmpty);
        }

        private void InterpretTokens()
        {
            var interpretedFormula = FormulaInterpreter.Interpret(Tokens, out _parsingError);
            if (interpretedFormula != null)
            {
                _formula = interpretedFormula;
                RaisePropertyChanged(() => Formula);
            }
            RaisePropertyChanged(() => ParsingError);
            RaisePropertyChanged(() => HasError);
        }

        private ParsingError _parsingError;
        public ParsingError ParsingError
        {
            get { return _parsingError; }
        }

        private readonly Stack<EditorState> _undoStack = new Stack<EditorState>();
        private readonly Stack<EditorState> _redoStack = new Stack<EditorState>();

        private int _caretIndex;
        public int CaretIndex
        {
            get { return _caretIndex; }
            set
            {
                if (_caretIndex == value) return;
                _caretIndex = value;
                RaisePropertyChanged(() => CaretIndex);
                RaisePropertyChanged(() => CanDelete);
            }
        }

        private int _selectionStart;
        public int SelectionStart
        {
            get { return _selectionStart; }
            set
            {
                if (_selectionStart == value) return;
                _selectionStart = value;
                RaisePropertyChanged(() => SelectionStart);
            }
        }

        private int _selectionLength;
        public int SelectionLength
        {
            get { return _selectionLength; }
            set
            {
                if (_selectionLength == value) return;
                _selectionLength = value;
                RaisePropertyChanged(() => SelectionLength);
                RaisePropertyChanged(() => CanDelete);
            }
        }

        #endregion

        #region Properties

        public bool IsTokensEmpty
        {
            get { return Tokens == null || Tokens.Count == 0;  }
        }

        public bool CanUndo
        {
            get { return _undoStack.Count != 0; }
        }

        public bool CanRedo
        {
            get { return _redoStack.Count != 0; }
        }

        public bool CanDelete
        {
            get { return CaretIndex > 0 || SelectionLength != 0; }
        }

        public bool HasError
        {
            get { return _parsingError != null; }
        }

        #endregion

        #region Key handler

        public bool HandleKey(FormulaEditorKey key, UserVariable variable = null)
        {
            if (key == FormulaEditorKey.Delete)
            {
                PushUndo();
                return Delete();
            }

            PushUndo();
            var token = CreateToken(key, variable);
            return Insert((token is IFormulaFunction)
                ? new[] {token, FormulaTokenFactory.CreateParenthesisToken(true)}
                : new[] {token});
        }

        private bool Insert(IEnumerable<IFormulaToken> tokens)
        {
            if (Tokens == null) Tokens = new ObservableCollection<IFormulaToken>();
            if (SelectionLength > 0)
            {
                if (!(0 <= SelectionStart && SelectionStart + SelectionLength <= Tokens.Count)) return false;
                Tokens.ReplaceRange(SelectionStart, SelectionLength, tokens);
            }
            else
            {
                if (!(0 <= CaretIndex && CaretIndex <= Tokens.Count)) return false;
                Tokens.InsertRange(CaretIndex, tokens);
            }
            RaisePropertyChanged(() => CanDelete);
            return true;
        }

        private bool Delete()
        {
            if (Tokens == null) return false;
            if (SelectionLength > 0)
            {
                var index = SelectionStart;
                if (!(0 <= index && index + SelectionLength <= Tokens.Count)) return false;
                Tokens.RemoveRange(index, SelectionLength);
            }
            else
            {
                var index = CaretIndex - 1;
                if (!(0 <= index && index < Tokens.Count)) return false;
                Tokens.RemoveAt(index);
            }
            RaisePropertyChanged(() => CanDelete);
            return true;
        }

        private static IFormulaToken CreateToken(FormulaEditorKey key, UserVariable variable)
        {
            switch (key)
            {
                // Constants
                case FormulaEditorKey.D0: return FormulaTokenFactory.CreateDigitToken(0);
                case FormulaEditorKey.D1: return FormulaTokenFactory.CreateDigitToken(1);
                case FormulaEditorKey.D2: return FormulaTokenFactory.CreateDigitToken(2);
                case FormulaEditorKey.D3: return FormulaTokenFactory.CreateDigitToken(3);
                case FormulaEditorKey.D4: return FormulaTokenFactory.CreateDigitToken(4);
                case FormulaEditorKey.D5: return FormulaTokenFactory.CreateDigitToken(5);
                case FormulaEditorKey.D6: return FormulaTokenFactory.CreateDigitToken(6);
                case FormulaEditorKey.D7: return FormulaTokenFactory.CreateDigitToken(7);
                case FormulaEditorKey.D8: return FormulaTokenFactory.CreateDigitToken(8);
                case FormulaEditorKey.D9: return FormulaTokenFactory.CreateDigitToken(9);
                case FormulaEditorKey.DecimalSeparator: return FormulaTokenFactory.CreateDecimalSeparatorToken();
                case FormulaEditorKey.ParameterSeparator: return FormulaTokenFactory.CreateParameterSeparatorToken();
                case FormulaEditorKey.Pi: return FormulaTokenFactory.CreatePiToken();
                case FormulaEditorKey.True: return FormulaTokenFactory.CreateTrueToken();
                case FormulaEditorKey.False: return FormulaTokenFactory.CreateFalseToken();

                // Operators
                case FormulaEditorKey.Plus: return FormulaTokenFactory.CreatePlusToken();
                case FormulaEditorKey.Minus: return FormulaTokenFactory.CreateMinusToken();
                case FormulaEditorKey.Multiply: return FormulaTokenFactory.CreateMultiplyToken();
                case FormulaEditorKey.Divide: return FormulaTokenFactory.CreateDivideToken();
                case FormulaEditorKey.Caret: return FormulaTokenFactory.CreateCaretToken();
                case FormulaEditorKey.Equals: return FormulaTokenFactory.CreateEqualsToken();
                case FormulaEditorKey.NotEquals: return FormulaTokenFactory.CreateNotEqualsToken();
                case FormulaEditorKey.Greater: return FormulaTokenFactory.CreateGreaterToken();
                case FormulaEditorKey.GreaterEqual: return FormulaTokenFactory.CreateGreaterEqualToken();
                case FormulaEditorKey.Less: return FormulaTokenFactory.CreateLessToken();
                case FormulaEditorKey.LessEqual: return FormulaTokenFactory.CreateLessEqualToken();
                case FormulaEditorKey.And: return FormulaTokenFactory.CreateAndToken();
                case FormulaEditorKey.Or: return FormulaTokenFactory.CreateOrToken();
                case FormulaEditorKey.Not: return FormulaTokenFactory.CreateNotToken();
                case FormulaEditorKey.Mod: return FormulaTokenFactory.CreateModToken();

                // Functions
                case FormulaEditorKey.Exp: return FormulaTokenFactory.CreateExpToken();
                case FormulaEditorKey.Log: return FormulaTokenFactory.CreateLogToken();
                case FormulaEditorKey.Ln: return FormulaTokenFactory.CreateLnToken();
                case FormulaEditorKey.Min: return FormulaTokenFactory.CreateMinToken();
                case FormulaEditorKey.Max: return FormulaTokenFactory.CreateMaxToken();
                case FormulaEditorKey.Sin: return FormulaTokenFactory.CreateSinToken();
                case FormulaEditorKey.Cos: return FormulaTokenFactory.CreateCosToken();
                case FormulaEditorKey.Tan: return FormulaTokenFactory.CreateTanToken();
                case FormulaEditorKey.Arcsin: return FormulaTokenFactory.CreateArcsinToken();
                case FormulaEditorKey.Arccos: return FormulaTokenFactory.CreateArccosToken();
                case FormulaEditorKey.Arctan: return FormulaTokenFactory.CreateArctanToken();
                case FormulaEditorKey.Sqrt: return FormulaTokenFactory.CreateSqrtToken();
                case FormulaEditorKey.Abs: return FormulaTokenFactory.CreateAbsToken();
                case FormulaEditorKey.Round: return FormulaTokenFactory.CreateRoundToken();
                case FormulaEditorKey.Random: return FormulaTokenFactory.CreateRandomToken();

                // Sensors
                case FormulaEditorKey.AccelerationX: return FormulaTokenFactory.CreateAccelerationXToken();
                case FormulaEditorKey.AccelerationY: return FormulaTokenFactory.CreateAccelerationYToken();
                case FormulaEditorKey.AccelerationZ: return FormulaTokenFactory.CreateAccelerationZToken();
                case FormulaEditorKey.Compass: return FormulaTokenFactory.CreateCompassToken();
                case FormulaEditorKey.InclinationX: return FormulaTokenFactory.CreateInclinationXToken();
                case FormulaEditorKey.InclinationY: return FormulaTokenFactory.CreateInclinationYToken();
                case FormulaEditorKey.Loudness: return FormulaTokenFactory.CreateLoudnessToken();

                // Properties
                case FormulaEditorKey.Brightness: return FormulaTokenFactory.CreateBrightnessToken();
                case FormulaEditorKey.Layer: return FormulaTokenFactory.CreateLayerToken();
                case FormulaEditorKey.Transparency: return FormulaTokenFactory.CreateTransparencyToken();
                case FormulaEditorKey.PositionX: return FormulaTokenFactory.CreatePositionXToken();
                case FormulaEditorKey.PositionY: return FormulaTokenFactory.CreatePositionYToken();
                case FormulaEditorKey.Rotation: return FormulaTokenFactory.CreateRotationToken();
                case FormulaEditorKey.Size: return FormulaTokenFactory.CreateSizeToken();

                // Variables
                case FormulaEditorKey.LocalVariable: return FormulaTokenFactory.CreateLocalVariableToken(variable);
                case FormulaEditorKey.GlobalVariable: return FormulaTokenFactory.CreateGlobalVariableToken(variable);

                // brackets
                case FormulaEditorKey.OpeningParenthesis: return FormulaTokenFactory.CreateParenthesisToken(true);
                case FormulaEditorKey.ClosingParenthesis: return FormulaTokenFactory.CreateParenthesisToken(false);

                default: throw new ArgumentOutOfRangeException("key");
            }
        }

        #endregion

        #region Undo/redo

        protected class EditorState
        {
            public EditorState(IEnumerable<IFormulaToken> tokens, int caretIndex, int selectionLength)
            {
                Tokens = tokens == null ? null : tokens.ToList();
                CaretIndex = caretIndex;
                SelectionLength = selectionLength;
            }

            public List<IFormulaToken> Tokens { get; private set; }
            public int CaretIndex { get; private set; }
            public int SelectionLength { get; private set; }
            public int SelectionStart { get { return CaretIndex - SelectionLength; } }
        }

        public bool Undo()
        {
            if (_undoStack.Count == 0) return false;
            PushState(_redoStack);
            PopState(_undoStack);
            RaisePropertyChanged(() => CanUndo);
            RaisePropertyChanged(() => CanRedo);
            return true;
        }

        public bool Redo()
        {
            if (_redoStack.Count == 0) return false;
            PushState(_undoStack);
            PopState(_redoStack);
            RaisePropertyChanged(() => CanUndo);
            RaisePropertyChanged(() => CanRedo);
            return true;
        }

        private void PushUndo()
        {
            PushState(_undoStack);
            _redoStack.Clear();
            RaisePropertyChanged(() => CanUndo);
            RaisePropertyChanged(() => CanRedo);
        }

        private void PushState(Stack<EditorState> stack)
        {
            stack.Push(new EditorState(Tokens, CaretIndex, SelectionLength));
        }

        private void PopState(Stack<EditorState> stack)
        {
            var state = stack.Pop();
            Tokens = state.Tokens == null ? null : new ObservableCollection<IFormulaToken>(state.Tokens);
            CaretIndex = state.CaretIndex;
            SelectionStart = state.SelectionStart;
            SelectionLength = state.SelectionLength;
        }

        #endregion

        public void ResetViewModel()
        {
            _undoStack.Clear();
            RaisePropertyChanged(() => CanUndo);
            _redoStack.Clear();
            RaisePropertyChanged(() => CanRedo);
            Formula = null;
            RaisePropertyChanged(() => CanDelete);
            CaretIndex = 0;
            SelectionStart = 0;
            SelectionLength = 0;
        }
    }
}
