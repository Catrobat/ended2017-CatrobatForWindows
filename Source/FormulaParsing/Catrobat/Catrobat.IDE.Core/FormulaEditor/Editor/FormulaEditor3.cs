using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaNodes;
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
        private class EditorState
        {
            public List<IFormulaToken> Tokens { get; set; }
            public int CaretIndex { get; set; }
        }

        #region members

        private readonly ObjectVariableEntry _objectVariable;
        private readonly FormulaTokenizer _tokenizer;
        private readonly FormulaInterpreter _interpreter = new FormulaInterpreter();

        private IFormulaTree _formula;
        public IFormulaTree Formula
        {
            get { return _formula; }
            set
            {
                _formula = value;
                RaisePropertyChanged(() => Formula);
                if (_formula == null)
                {
                    _tokens.Clear();
                }
                else
                {
                    _tokens = new ObservableCollection<IFormulaToken>(_tokenizer.Tokenize(_formula));
                    RaisePropertyChanged(() => Tokens);
                }
            }
        }

        private ObservableCollection<IFormulaToken> _tokens = new ObservableCollection<IFormulaToken>();
        public ObservableCollection<IFormulaToken> Tokens
        {
            get { return _tokens; }
            private set
            {
                if (_tokens == value) return;
                _tokens = value;
                RaisePropertyChanged(() => Tokens);
                InterpretTokens();
            }
        }

        private string _parsingError = null;
        public string ParsingError
        {
            get { return _parsingError; }
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

        private void InterpretTokens()
        {
            var interpretedFormula = _interpreter.Interpret(_tokens, out _parsingError);
            if (interpretedFormula != null)
            {
                _formula = interpretedFormula;
                RaisePropertyChanged(() => Formula);
            }
            RaisePropertyChanged(() => ParsingError);

        }

        public FormulaEditor3(IEnumerable<UserVariable> userVariables, ObjectVariableEntry objectVariable)
        {
            _objectVariable = objectVariable;
            _tokenizer = new FormulaTokenizer(userVariables, objectVariable);
        }

        #region key handler

        public bool HandleKey(FormulaEditorKey key)
        {
            switch (key)
            {
                case FormulaEditorKey.Undo: return Undo();
                case FormulaEditorKey.Redo: return Redo();
                case FormulaEditorKey.Delete:
                    if (CaretIndex == 0) return false;
                    PushUndo();
                    RemoveToken();
                    return true;
                default:
                    PushUndo();
                    var token = CreateToken(key);
                    InsertToken(token);
                    if (token is FormulaNodeUnaryFunction || token is FormulaNodeBinaryFunction)
                    {
                        InsertToken(FormulaTokenFactory.CreateParenthesisToken(true));
                    }
                    return true;
            }
        }

        public bool HandleKey(SensorVariable variable)
        {
            PushUndo();
            InsertToken(CreateToken(variable));
            return true;
        }

        public bool HandleKey(UserVariable variable)
        {
            PushUndo();
            InsertToken(CreateToken(variable));
            return true;
        }

        public bool HandleKey(ObjectVariable variable)
        {
            PushUndo();
            InsertToken(CreateToken(variable));
            return true;
        }

        #endregion

        #region undo/redo

        private bool Undo()
        {
            if (_redoStack.Count == 0) return false;
            PushState(_undoStack);
            PopState(_redoStack);
            return true;
        }

        private bool Redo()
        {
            if (_undoStack.Count == 0) return false;
            PushState(_redoStack);
            PopState(_undoStack);
            return true;
        }

        private void PushUndo()
        {
            PushState(_undoStack);
            _redoStack.Clear();
        }

        private void PushState(Stack<EditorState> stack)
        {
            stack.Push(new EditorState
            {
                CaretIndex = CaretIndex,
                Tokens = Tokens.ToList()
            });
        }

        private void PopState(Stack<EditorState> stack)
        {
            var state = stack.Pop();
            CaretIndex = state.CaretIndex;
            Tokens = new ObservableCollection<IFormulaToken>(state.Tokens);
        }

        #endregion

        public void ResetViewModel()
        {
            _undoStack.Clear();
            _redoStack.Clear();
            Tokens.Clear();
        }

        private void InsertToken(IFormulaToken token)
        {
            Tokens.Insert(CaretIndex, token);
            InterpretTokens();
            CaretIndex++;
        }

        private bool RemoveToken()
        {
            var index = CaretIndex - 1;
            if (!(0 <= index && index < Tokens.Count)) return false;
            Tokens.RemoveAt(CaretIndex - 1);
            InterpretTokens();
            CaretIndex--;
            return true;
        }

        private static IFormulaToken CreateToken(FormulaEditorKey key)
        {
            // TODO: merge digits

            switch (key)
            {
                case FormulaEditorKey.Number0: return FormulaTokenFactory.CreateDigitToken(0);
                case FormulaEditorKey.Number1: return FormulaTokenFactory.CreateDigitToken(1);
                case FormulaEditorKey.Number2: return FormulaTokenFactory.CreateDigitToken(2);
                case FormulaEditorKey.Number3: return FormulaTokenFactory.CreateDigitToken(3);
                case FormulaEditorKey.Number4: return FormulaTokenFactory.CreateDigitToken(4);
                case FormulaEditorKey.Number5: return FormulaTokenFactory.CreateDigitToken(5);
                case FormulaEditorKey.Number6: return FormulaTokenFactory.CreateDigitToken(6);
                case FormulaEditorKey.Number7: return FormulaTokenFactory.CreateDigitToken(7);
                case FormulaEditorKey.Number8: return FormulaTokenFactory.CreateDigitToken(8);
                case FormulaEditorKey.Number9: return FormulaTokenFactory.CreateDigitToken(9);
                case FormulaEditorKey.NumberDot: return FormulaTokenFactory.CreateDecimalSeparatorToken();
                case FormulaEditorKey.NumberEquals: return FormulaTokenFactory.CreateEqualsToken();
                case FormulaEditorKey.OpenBracket: return FormulaTokenFactory.CreateParenthesisToken(true);
                case FormulaEditorKey.CloseBracket: return FormulaTokenFactory.CreateParenthesisToken(false);
                case FormulaEditorKey.Plus: return FormulaTokenFactory.CreatePlusToken();
                case FormulaEditorKey.Minus: return FormulaTokenFactory.CreateMinusToken();
                case FormulaEditorKey.Multiply: return FormulaTokenFactory.CreateMultiplyToken();
                case FormulaEditorKey.Divide: return FormulaTokenFactory.CreateDivideToken();
                case FormulaEditorKey.LogicEqual: return FormulaTokenFactory.CreateEqualsToken();
                case FormulaEditorKey.LogicNotEqual: return FormulaTokenFactory.CreateNotEqualsToken();
                case FormulaEditorKey.LogicSmaller: return FormulaTokenFactory.CreateLessToken();
                case FormulaEditorKey.LogicSmallerEqual: return FormulaTokenFactory.CreateLessEqualToken();
                case FormulaEditorKey.LogicGreater: return FormulaTokenFactory.CreateGreaterToken();
                case FormulaEditorKey.LogicGreaterEqual: return FormulaTokenFactory.CreateGreaterEqualToken();
                case FormulaEditorKey.LogicAnd: return FormulaTokenFactory.CreateAndToken();
                case FormulaEditorKey.LogicOr: return FormulaTokenFactory.CreateOrToken();
                case FormulaEditorKey.LogicNot: return FormulaTokenFactory.CreateNotToken();
                case FormulaEditorKey.LogicTrue: return FormulaTokenFactory.CreateTrueToken();
                case FormulaEditorKey.LogicFalse: return FormulaTokenFactory.CreateFalseToken();
                case FormulaEditorKey.MathSin: return FormulaTokenFactory.CreateSinToken();
                case FormulaEditorKey.MathCos: return FormulaTokenFactory.CreateCosToken();
                case FormulaEditorKey.MathTan: return FormulaTokenFactory.CreateTanToken();
                case FormulaEditorKey.MathArcSin: return FormulaTokenFactory.CreateArcsinToken();
                case FormulaEditorKey.MathArcCos: return FormulaTokenFactory.CreateArccosToken();
                case FormulaEditorKey.MathArcTan: return FormulaTokenFactory.CreateArctanToken();
                case FormulaEditorKey.MathExp: return FormulaTokenFactory.CreateExpToken();
                case FormulaEditorKey.MathLn: return FormulaTokenFactory.CreateLnToken();
                case FormulaEditorKey.MathLog: return FormulaTokenFactory.CreateLogToken();
                case FormulaEditorKey.MathAbs: return FormulaTokenFactory.CreateAbsToken();
                case FormulaEditorKey.MathRound: return FormulaTokenFactory.CreateRoundToken();
                case FormulaEditorKey.MathMod: return FormulaTokenFactory.CreateModToken();
                case FormulaEditorKey.MathMin: return FormulaTokenFactory.CreateMinToken();
                case FormulaEditorKey.MathMax: return FormulaTokenFactory.CreateMaxToken();
                case FormulaEditorKey.MathSqrt: return FormulaTokenFactory.CreateSqrtToken();
                case FormulaEditorKey.MathPi: return FormulaTokenFactory.CreatePiToken();
                case FormulaEditorKey.MathRandom: return FormulaTokenFactory.CreateRandomToken();
                default: throw new ArgumentOutOfRangeException("key");
            }
        }

        private static IFormulaToken CreateToken(SensorVariable variable)
        {
            switch (variable)
            {
                case SensorVariable.AccelerationX: return FormulaTokenFactory.CreateAccelerationXToken();
                case SensorVariable.AccelerationY: return FormulaTokenFactory.CreateAccelerationYToken();
                case SensorVariable.AccelerationZ: return FormulaTokenFactory.CreateAccelerationZToken();
                case SensorVariable.CompassDirection: return FormulaTokenFactory.CreateCompassToken();
                case SensorVariable.InclinationX: return FormulaTokenFactory.CreateInclinationXToken();
                case SensorVariable.InclinationY: return FormulaTokenFactory.CreateInclinationYToken();
                default: throw new ArgumentOutOfRangeException("variable");
            }
        }

        private static IFormulaToken CreateToken(UserVariable variable)
        {
            return FormulaTokenFactory.CreateUserVariableToken();
        }

        private IFormulaToken CreateToken(ObjectVariable variable)
        {
            switch (variable)
            {
                case ObjectVariable.Brightness: return FormulaTokenFactory.CreateBrightnessToken();
                case ObjectVariable.Direction: return FormulaTokenFactory.CreateDirectionToken();
                case ObjectVariable.GhostEffect: return FormulaTokenFactory.CreateGhostEffectToken();
                case ObjectVariable.Layer: return FormulaTokenFactory.CreateLayerToken();
                case ObjectVariable.PositionX: return FormulaTokenFactory.CreatePositionXToken();
                case ObjectVariable.PositionY: return FormulaTokenFactory.CreatePositionYToken();
                case ObjectVariable.Transparency: return FormulaTokenFactory.CreateOpacityToken();
                case ObjectVariable.Size: return FormulaTokenFactory.CreateSizeToken();
                case ObjectVariable.Rotation: return FormulaTokenFactory.CreateRotationToken();
                default: throw new ArgumentOutOfRangeException("variable");
            }
        }
    }
}
