using System.Collections.Specialized;
using System.Security.Principal;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Catrobat.IDE.Core.FormulaEditor.Editor
{
    public class FormulaEditor3 : ViewModelBase
    {
 
        #region Members

        private readonly FormulaTokenizer _tokenizer;
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
                    new ObservableCollection<IFormulaToken>(_tokenizer.Tokenize(_formula));
                CaretIndex = Tokens.Count;
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
                InterpretTokens();
            }
        }
        private void Tokens_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            InterpretTokens();
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
                RaisePropertyChanged(() => CanLeft);
                RaisePropertyChanged(() => CanRight);
                RaisePropertyChanged(() => CanDelete);
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
            }
        }

        #endregion

        #region Properties

        public bool CanUndo
        {
            get { return _undoStack.Count != 0; }
        }

        public bool CanRedo
        {
            get { return _redoStack.Count != 0; }
        }

        public bool CanLeft
        {
            get { return CaretIndex > 0; }
        }

        public bool CanRight
        {
            get { return CaretIndex < Tokens.Count; }
        }

        public bool CanDelete
        {
            get { return CaretIndex > 0; }
        }

        public bool HasError
        {
            get { return _parsingError != null; }
        }

        #endregion

        public FormulaEditor3()
        {
            _tokenizer = new FormulaTokenizer();
        }

        public void ResetViewModel()
        {
            _undoStack.Clear();
            RaisePropertyChanged(() => CanUndo);
            _redoStack.Clear();
            RaisePropertyChanged(() => CanRedo);
            Formula = null;
            RaisePropertyChanged(() => CanDelete);
        }

        #region Key handler

        public bool HandleKey(FormulaEditorKey key, UserVariable variable = null)
        {
            switch (key)
            {
                case FormulaEditorKey.Left:
                    if (CaretIndex < 1) return false;
                    CaretIndex--;
                    return true;
                case FormulaEditorKey.Right:
                    if (!(Tokens != null && CaretIndex < Tokens.Count)) return false;
                    CaretIndex++;
                    return true;
                case FormulaEditorKey.Delete:
                    if (CaretIndex == 0) return false;
                    PushUndo();
                    return RemoveToken();
                case FormulaEditorKey.Undo:
                    return Undo();
                case FormulaEditorKey.Redo:
                    return Redo();
                default:
                    PushUndo();
                    var token = CreateToken(key, variable);
                    return InsertToken(token) && 
                        (!(token is FormulaNodeUnaryFunction || token is FormulaNodeBinaryFunction) || 
                            InsertToken(FormulaTokenFactory.CreateParenthesisToken(true)));
            }
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
        }

        private bool Undo()
        {
            if (_undoStack.Count == 0) return false;
            PushState(_redoStack);
            PopState(_undoStack);
            RaisePropertyChanged(() => CanDelete);
            RaisePropertyChanged(() => CanUndo);
            RaisePropertyChanged(() => CanRedo);
            RaisePropertyChanged(() => CanLeft);
            RaisePropertyChanged(() => CanRight);
            return true;
        }

        private bool Redo()
        {
            if (_redoStack.Count == 0) return false;
            PushState(_undoStack);
            PopState(_redoStack);
            RaisePropertyChanged(() => CanDelete);
            RaisePropertyChanged(() => CanUndo);
            RaisePropertyChanged(() => CanRedo);
            RaisePropertyChanged(() => CanLeft);
            RaisePropertyChanged(() => CanRight);
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
            SelectionLength = state.SelectionLength;
        }

        #endregion

        private readonly FormulaInterpreter _interpreter = new FormulaInterpreter();
        private void InterpretTokens()
        {
            var interpretedFormula = _interpreter.Interpret(Tokens, out _parsingError);
            if (interpretedFormula != null)
            {
                _formula = interpretedFormula;
                RaisePropertyChanged(() => Formula);
            }
            RaisePropertyChanged(() => ParsingError);
            RaisePropertyChanged(() => HasError);
        }

        private bool InsertToken(IFormulaToken token)
        {
            if (Tokens == null) Tokens = new ObservableCollection<IFormulaToken>();
            var index = CaretIndex;
            if (!(0 <= index && index <= Tokens.Count)) return false;
            if (0 <= index && index < Tokens.Count)
            {
                Tokens.ReplaceRange(index, SelectionLength, token);
                SelectionLength = 0;
            }
            else
            {
                Tokens.Insert(index, token);
            }
            CaretIndex = index + 1;
            InterpretTokens();
            RaisePropertyChanged(() => CanLeft);
            RaisePropertyChanged(() => CanDelete);
            return true;
        }

        private bool RemoveToken()
        {
            if (Tokens == null) return false;
            var index = CaretIndex - 1;
            if (!(0 <= index && index < Tokens.Count)) return false;
            Tokens.RemoveAt(index);
            CaretIndex = index;
            InterpretTokens();
            RaisePropertyChanged(() => CanLeft);
            RaisePropertyChanged(() => CanDelete);
            return true;
        }
    }
}
