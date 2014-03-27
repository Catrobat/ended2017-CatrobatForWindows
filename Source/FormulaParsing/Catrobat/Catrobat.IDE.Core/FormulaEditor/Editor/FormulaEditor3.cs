using System.Collections.Specialized;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
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

        private readonly ObjectVariableEntry _objectVariable;

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

        private string _parsingError;
        public string ParsingError
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

        #endregion

        public FormulaEditor3(IEnumerable<UserVariable> userVariables, ObjectVariableEntry objectVariable)
        {
            _objectVariable = objectVariable;
            _tokenizer = new FormulaTokenizer(userVariables, objectVariable);
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

        public bool HandleKey(FormulaEditorKey key, ObjectVariableEntry objectVariable, UserVariable userVariable)
        {
            switch (key)
            {
                case FormulaEditorKey.Left:
                    if (CaretIndex < 1) return false;
                    CaretIndex--;
                    return true;
                case FormulaEditorKey.Right:
                    if (CaretIndex >= Tokens.Count) return false;
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
                    var token = CreateToken(key, objectVariable, userVariable);
                    InsertToken(token);
                    if (token is FormulaNodeUnaryFunction || token is FormulaNodeBinaryFunction)
                    {
                        InsertToken(FormulaTokenFactory.CreateParenthesisToken(true));
                    }
                    return true;
            }
        }

        private static IFormulaToken CreateToken(FormulaEditorKey key, ObjectVariableEntry objectVariable, UserVariable userVariable)
        {
            switch (key)
            {
                // numbers
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
                case FormulaEditorKey.ArgumentSeparator: return FormulaTokenFactory.CreateArgumentSeparatorToken();
                case FormulaEditorKey.Pi: return FormulaTokenFactory.CreatePiToken();

                // arithemtic
                case FormulaEditorKey.Plus: return FormulaTokenFactory.CreatePlusToken();
                case FormulaEditorKey.Minus: return FormulaTokenFactory.CreateMinusToken();
                case FormulaEditorKey.Multiply: return FormulaTokenFactory.CreateMultiplyToken();
                case FormulaEditorKey.Divide: return FormulaTokenFactory.CreateDivideToken();
                case FormulaEditorKey.Caret: return FormulaTokenFactory.CreateCaretToken();

                // relational operators
                case FormulaEditorKey.Equals: return FormulaTokenFactory.CreateEqualsToken();
                case FormulaEditorKey.NotEquals: return FormulaTokenFactory.CreateNotEqualsToken();
                case FormulaEditorKey.Greater: return FormulaTokenFactory.CreateGreaterToken();
                case FormulaEditorKey.GreaterEqual: return FormulaTokenFactory.CreateGreaterEqualToken();
                case FormulaEditorKey.Less: return FormulaTokenFactory.CreateLessToken();
                case FormulaEditorKey.LessEqual: return FormulaTokenFactory.CreateLessEqualToken();

                // logic
                case FormulaEditorKey.True: return FormulaTokenFactory.CreateTrueToken();
                case FormulaEditorKey.False: return FormulaTokenFactory.CreateFalseToken();
                case FormulaEditorKey.And: return FormulaTokenFactory.CreateAndToken();
                case FormulaEditorKey.Or: return FormulaTokenFactory.CreateOrToken();
                case FormulaEditorKey.Not: return FormulaTokenFactory.CreateNotToken();

                // min/max
                case FormulaEditorKey.Min: return FormulaTokenFactory.CreateMinToken();
                case FormulaEditorKey.Max: return FormulaTokenFactory.CreateMaxToken();

                // exponential function and logarithms
                case FormulaEditorKey.Exp: return FormulaTokenFactory.CreateExpToken();
                case FormulaEditorKey.Log: return FormulaTokenFactory.CreateLogToken();
                case FormulaEditorKey.Ln: return FormulaTokenFactory.CreateLnToken();

                // trigonometric functions
                case FormulaEditorKey.Sin: return FormulaTokenFactory.CreateSinToken();
                case FormulaEditorKey.Cos: return FormulaTokenFactory.CreateCosToken();
                case FormulaEditorKey.Tan: return FormulaTokenFactory.CreateTanToken();
                case FormulaEditorKey.Arcsin: return FormulaTokenFactory.CreateArcsinToken();
                case FormulaEditorKey.Arccos: return FormulaTokenFactory.CreateArccosToken();
                case FormulaEditorKey.Arctan: return FormulaTokenFactory.CreateArctanToken();

                // miscellaneous functions
                case FormulaEditorKey.Sqrt: return FormulaTokenFactory.CreateSqrtToken();
                case FormulaEditorKey.Abs: return FormulaTokenFactory.CreateAbsToken();
                case FormulaEditorKey.Mod: return FormulaTokenFactory.CreateModToken();
                case FormulaEditorKey.Round: return FormulaTokenFactory.CreateRoundToken();
                case FormulaEditorKey.Random: return FormulaTokenFactory.CreateRandomToken();

                // sensors
                case FormulaEditorKey.AccelerationX: return FormulaTokenFactory.CreateAccelerationXToken();
                case FormulaEditorKey.AccelerationY: return FormulaTokenFactory.CreateAccelerationYToken();
                case FormulaEditorKey.AccelerationZ: return FormulaTokenFactory.CreateAccelerationZToken();
                case FormulaEditorKey.Compass: return FormulaTokenFactory.CreateCompassToken();
                case FormulaEditorKey.InclinationX: return FormulaTokenFactory.CreateInclinationXToken();
                case FormulaEditorKey.InclinationY: return FormulaTokenFactory.CreateInclinationYToken();
                case FormulaEditorKey.Loudness: return FormulaTokenFactory.CreateLoudnessToken();

                // object variables
                case FormulaEditorKey.Brightness: return FormulaTokenFactory.CreateBrightnessToken();
                case FormulaEditorKey.Layer: return FormulaTokenFactory.CreateLayerToken();
                case FormulaEditorKey.Opacity: return FormulaTokenFactory.CreateOpacityToken();
                case FormulaEditorKey.PositionX: return FormulaTokenFactory.CreatePositionXToken();
                case FormulaEditorKey.PositionY: return FormulaTokenFactory.CreatePositionYToken();
                case FormulaEditorKey.Rotation: return FormulaTokenFactory.CreateRotationToken();
                case FormulaEditorKey.Size: return FormulaTokenFactory.CreateSizeToken();

                // user variables
                case FormulaEditorKey.UserVariable: return FormulaTokenFactory.CreateUserVariableToken(userVariable);

                // brackets
                case FormulaEditorKey.OpeningParenthesis: return FormulaTokenFactory.CreateParenthesisToken(true);
                case FormulaEditorKey.ClosingParenthesis: return FormulaTokenFactory.CreateParenthesisToken(false);

                default: throw new ArgumentOutOfRangeException("key");
            }
        }

        #endregion

        #region Undo/redo

        private class EditorState
        {
            public List<IFormulaToken> Tokens { get; set; }
            public int CaretIndex { get; set; }
        }

        private bool Undo()
        {
            if (_undoStack.Count == 0) return false;
            PushState(_redoStack);
            PopState(_undoStack);
            RaisePropertyChanged(() => CanDelete);
            RaisePropertyChanged(() => CanUndo);
            RaisePropertyChanged(() => CanRedo);
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
            stack.Push(new EditorState
            {
                CaretIndex = CaretIndex,
                Tokens = Tokens == null ? null : Tokens.ToList()
            });
        }

        private void PopState(Stack<EditorState> stack)
        {
            var state = stack.Pop();
            CaretIndex = state.CaretIndex;
            Tokens = state.Tokens == null ? null : new ObservableCollection<IFormulaToken>(state.Tokens);
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
        }

        private void InsertToken(IFormulaToken token)
        {
            if (Tokens == null) Tokens = new ObservableCollection<IFormulaToken>();
            Tokens.Insert(CaretIndex, token);
            InterpretTokens();
            RaisePropertyChanged(() => CanDelete);
        }

        private bool RemoveToken()
        {
            if (Tokens == null) return false;
            var index = CaretIndex - 1;
            if (!(0 <= index && index < Tokens.Count)) return false;
            Tokens.RemoveAt(index);
            InterpretTokens();
            RaisePropertyChanged(() => CanDelete);
            return true;
        }
    }
}
