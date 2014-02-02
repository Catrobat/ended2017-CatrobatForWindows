using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaNodes;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Catrobat.IDE.Core.FormulaEditor.Editor
{
    class FormulaTokenizer
    {

        private readonly IDictionary<string, UserVariable> _userVariables;
        private readonly ObjectVariableEntry _objectVariable;

        public FormulaTokenizer(IEnumerable<UserVariable> userVariables, ObjectVariableEntry objectVariable)
        {
            _userVariables = userVariables.ToDictionary(variable => variable.Name);
            _objectVariable = objectVariable;
        }

        #region tokenize string

        public bool Tokenize(string input, out IEnumerable<IFormulaToken> tokens, out IEnumerable<string> parsingErrors)
        {
            var tokens2 = new List<IFormulaToken>();
            tokens = tokens2;
            var parsingErrors2 = new List<string>();
            parsingErrors = parsingErrors2;
            return Tokenize(input, ref tokens2, ref parsingErrors2);
        }

        private bool Tokenize(string input, ref List<IFormulaToken> tokens, ref List<string> parsingErrors)
        {
            var index = 0;
            while (index < input.Length)
            {
                // ignore whitespace
                if (char.IsWhiteSpace(input[index])) index++;

                // brackets
                else if (Tokenize(input, ref index, "(", () => FormulaTokenFactory.CreateParenthesisToken(true), ref tokens)) { }
                else if (Tokenize(input, ref index, ")", () => FormulaTokenFactory.CreateParenthesisToken(false), ref tokens)) { }

                // numbers
                else if (Tokenize(input, ref index, "pi", FormulaTokenFactory.CreatePiToken, ref tokens)) { }

                // arithmetic
                else if (Tokenize(input, ref index, "+", FormulaTokenFactory.CreatePlusToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "-", FormulaTokenFactory.CreateMinusToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "*", FormulaTokenFactory.CreateMultiplyToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "/", FormulaTokenFactory.CreateDivideToken, ref tokens)) { }
                else if (Tokenize(input, ref index, ":", FormulaTokenFactory.CreateDivideToken, ref tokens)) { }

                // relational operators
                else if (Tokenize(input, ref index, "==", FormulaTokenFactory.CreateEqualsToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "=", FormulaTokenFactory.CreateEqualsToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "<>", FormulaTokenFactory.CreateNotEqualsToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "!=", FormulaTokenFactory.CreateNotEqualsToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "<=", FormulaTokenFactory.CreateLessEqualToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "<", FormulaTokenFactory.CreateLessToken, ref tokens)) { }
                else if (Tokenize(input, ref index, ">=", FormulaTokenFactory.CreateGreaterEqualToken, ref tokens)) { }
                else if (Tokenize(input, ref index, ">", FormulaTokenFactory.CreateGreaterToken, ref tokens)) { }
                
                // logic
                // TODO

                // min/max
                else if (Tokenize(input, ref index, "min", FormulaTokenFactory.CreateMinToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "max", FormulaTokenFactory.CreateMaxToken, ref tokens)) { }

                // exponential function and logarithms
                else if (Tokenize(input, ref index, "exp", FormulaTokenFactory.CreateExpToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "e^", FormulaTokenFactory.CreateExpToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "log", FormulaTokenFactory.CreateLogToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "ln", FormulaTokenFactory.CreateLnToken, ref tokens)) { }

                // trigonometric functions
                else if (Tokenize(input, ref index, "sin", FormulaTokenFactory.CreateSinToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "cos", FormulaTokenFactory.CreateCosToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "tan", FormulaTokenFactory.CreateTanToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "arcsin", FormulaTokenFactory.CreateArcsinToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "arccos", FormulaTokenFactory.CreateArccosToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "arctan", FormulaTokenFactory.CreateArctanToken, ref tokens)) { }

                // miscellaneous functions
                else if (Tokenize(input, ref index, "sqrt", FormulaTokenFactory.CreateSqrtToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "abs", FormulaTokenFactory.CreateAbsToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "mod", FormulaTokenFactory.CreateModToken, ref tokens)) { }

                // sensors
                // TODO

                // object variables
                // TODO

                // user variables

                // numbers
                else if (TokenizeNumber(input, ref index, ref tokens)) { }
                else
                {
                    // TODO: add parsing error like "Unknown token at ..."
                    parsingErrors.Add("An error occured. ");
                    return false;
                }
            }
            return true;
        }

        private static bool Tokenize(string input, ref int startIndex, string tokenValue, Func<IFormulaToken> constructor, ref List<IFormulaToken> tokens)
        {
            if (!input.StartsWith(tokenValue, startIndex, StringComparison.CurrentCultureIgnoreCase)) return false;
            tokens.Add(constructor());
            startIndex += tokenValue.Length;
            return true;
        }

        [Obsolete("Use FormulaToken.CreateDigitToken instead as used in Tokenize(FormulaNodeNumber). ")]
        private static bool TokenizeNumber(string input, ref int startIndex, ref List<IFormulaToken> tokens)
        {
            int length;
            double value = 0;
            for (length = 1; startIndex + length <= input.Length; length++)
            {
                double parsedValue = 0;
                if (input[startIndex + length - 1] != '+' &&
                    input[startIndex + length - 1] != '-' &&
                    double.TryParse(
                        s: input.Substring(startIndex, length),
                        style: NumberStyles.Number,
                        provider: CultureInfo.CurrentCulture,
                        result: out parsedValue))
                {
                    value = parsedValue;
                } else
                {
                    length--;
                    break;
                }
            }
            if (length == 0) return false;
            tokens.Add(FormulaTreeFactory.CreateNumberNode(value));
            startIndex += length;
            return true;
        }

        #endregion

        #region tokenize formula

        public IEnumerable<IFormulaToken> Tokenize(IFormulaTree formula)
        {
            if (formula == null) return null;
            var type = formula.GetType();

            if (type == typeof(FormulaNodeNumber)) return Tokenize((FormulaNodeNumber)formula);
            if (type == typeof(FormulaNodePi)) return Tokenize((FormulaNodePi)formula, FormulaTokenFactory.CreatePiToken);

                // arithmetic
            if (type == typeof(FormulaNodeAdd)) return Tokenize((FormulaNodeAdd)formula, FormulaTokenFactory.CreatePlusToken);
            if (type == typeof(FormulaNodeSubtract)) return Tokenize((FormulaNodeSubtract)formula, FormulaTokenFactory.CreateMinusToken);
            if (type == typeof(FormulaNodeMultiply)) return Tokenize((FormulaNodeMultiply)formula, FormulaTokenFactory.CreateMultiplyToken);
            if (type == typeof(FormulaNodeDivide)) return Tokenize((FormulaNodeDivide)formula, FormulaTokenFactory.CreateDivideToken);

                // relational operators
            if (type == typeof(FormulaNodeEquals)) return Tokenize((FormulaNodeEquals)formula, FormulaTokenFactory.CreateEqualsToken);
            if (type == typeof(FormulaNodeNotEquals)) return Tokenize((FormulaNodeNotEquals)formula, FormulaTokenFactory.CreateNotEqualsToken);
            if (type == typeof(FormulaNodeLess)) return Tokenize((FormulaNodeLess)formula, FormulaTokenFactory.CreateLessToken);
            if (type == typeof(FormulaNodeLessEqual)) return Tokenize((FormulaNodeLessEqual)formula, FormulaTokenFactory.CreateLessEqualToken);
            if (type == typeof(FormulaNodeGreater)) return Tokenize((FormulaNodeGreater)formula, FormulaTokenFactory.CreateGreaterToken);
            if (type == typeof(FormulaNodeGreaterEqual)) return Tokenize((FormulaNodeGreaterEqual)formula, FormulaTokenFactory.CreateGreaterEqualToken);

                // logic
            if (type == typeof(FormulaNodeTrue)) return Tokenize((FormulaNodeTrue)formula, FormulaTokenFactory.CreateTrueToken);
            if (type == typeof(FormulaNodeFalse)) return Tokenize((FormulaNodeFalse)formula, FormulaTokenFactory.CreateFalseToken);
            if (type == typeof(FormulaNodeAnd)) return Tokenize((FormulaNodeAnd)formula, FormulaTokenFactory.CreateAndToken);
            if (type == typeof(FormulaNodeOr)) return Tokenize((FormulaNodeOr)formula, FormulaTokenFactory.CreateOrToken);
            if (type == typeof(FormulaNodeNot)) return Tokenize((FormulaNodeNot)formula, FormulaTokenFactory.CreateNotToken);

                // min/max
            if (type == typeof(FormulaNodeMin)) return Tokenize((FormulaNodeMin)formula, FormulaTokenFactory.CreateMinToken);
            if (type == typeof(FormulaNodeMax)) return Tokenize((FormulaNodeMax)formula, FormulaTokenFactory.CreateMaxToken);

                // exponential functions and logarithms
            if (type == typeof(FormulaNodeExp)) return Tokenize((FormulaNodeExp)formula, FormulaTokenFactory.CreateExpToken);
            if (type == typeof(FormulaNodeLog)) return Tokenize((FormulaNodeLog)formula, FormulaTokenFactory.CreateLogToken);
            if (type == typeof(FormulaNodeLn)) return Tokenize((FormulaNodeLn)formula, FormulaTokenFactory.CreateLnToken);

                // trigonometric functions
            if (type == typeof(FormulaNodeSin)) return Tokenize((FormulaNodeSin)formula, FormulaTokenFactory.CreateSinToken);
            if (type == typeof(FormulaNodeCos)) return Tokenize((FormulaNodeCos)formula, FormulaTokenFactory.CreateCosToken);
            if (type == typeof(FormulaNodeTan)) return Tokenize((FormulaNodeTan)formula, FormulaTokenFactory.CreateTanToken);
            if (type == typeof(FormulaNodeArcsin)) return Tokenize((FormulaNodeArcsin)formula, FormulaTokenFactory.CreateArcsinToken);
            if (type == typeof(FormulaNodeArccos)) return Tokenize((FormulaNodeArccos)formula, FormulaTokenFactory.CreateArccosToken);
            if (type == typeof(FormulaNodeArctan)) return Tokenize((FormulaNodeArctan)formula, FormulaTokenFactory.CreateArctanToken);

                // miscellaneous functions
            if (type == typeof(FormulaNodeSqrt)) return Tokenize((FormulaNodeSqrt)formula, FormulaTokenFactory.CreateSqrtToken);
            if (type == typeof(FormulaNodeAbs)) return Tokenize((FormulaNodeAbs)formula, FormulaTokenFactory.CreateAbsToken);
            if (type == typeof(FormulaNodeMod)) return Tokenize((FormulaNodeMod)formula, FormulaTokenFactory.CreateModToken);

            // sensors
            if (type == typeof(FormulaNodeAccelerationX)) return Tokenize((FormulaNodeAccelerationX)formula, FormulaTokenFactory.CreateAccelerationXToken);
            if (type == typeof(FormulaNodeAccelerationY)) return Tokenize((FormulaNodeAccelerationY)formula, FormulaTokenFactory.CreateAccelerationYToken);
            if (type == typeof(FormulaNodeAccelerationZ)) return Tokenize((FormulaNodeAccelerationZ)formula, FormulaTokenFactory.CreateAccelerationZToken);
            if (type == typeof(FormulaNodeCompass)) return Tokenize((FormulaNodeCompass)formula, FormulaTokenFactory.CreateCompassToken);
            if (type == typeof(FormulaNodeInclinationX)) return Tokenize((FormulaNodeInclinationX)formula, FormulaTokenFactory.CreateInclinationXToken);
            if (type == typeof(FormulaNodeInclinationY)) return Tokenize((FormulaNodeInclinationY)formula, FormulaTokenFactory.CreateInclinationYToken);

            // object variables
            if (type == typeof(FormulaNodeBrightness)) return Tokenize((FormulaNodeBrightness)formula, FormulaTokenFactory.CreateBrightnessToken);
            if (type == typeof(FormulaNodeDirection)) return Tokenize((FormulaNodeDirection)formula, FormulaTokenFactory.CreateDirectionToken);
            if (type == typeof(FormulaNodeGhostEffect)) return Tokenize((FormulaNodeGhostEffect)formula, FormulaTokenFactory.CreateGhostEffectToken);
            if (type == typeof(FormulaNodeLayer)) return Tokenize((FormulaNodeLayer)formula, FormulaTokenFactory.CreateLayerToken);
            if (type == typeof(FormulaNodePositionX)) return Tokenize((FormulaNodePositionX)formula, FormulaTokenFactory.CreatePositionXToken);
            if (type == typeof(FormulaNodePositionY)) return Tokenize((FormulaNodePositionY)formula, FormulaTokenFactory.CreatePositionYToken);
            if (type == typeof(FormulaNodeRotation)) return Tokenize((FormulaNodeRotation)formula, FormulaTokenFactory.CreateRotationToken);
            if (type == typeof(FormulaNodeSize)) return Tokenize((FormulaNodeSize)formula, FormulaTokenFactory.CreateSizeToken);
            if (type == typeof(FormulaNodeOpacity)) return Tokenize((FormulaNodeOpacity)formula, FormulaTokenFactory.CreateOpacityToken);

            // user variables
            if (type == typeof(FormulaNodeUserVariable)) return Tokenize((FormulaNodeUserVariable)formula, FormulaTokenFactory.CreateUserVariableToken);

            // brackets
            if (type == typeof(FormulaNodeParentheses)) return Tokenize((FormulaNodeParentheses)formula);

            throw new NotImplementedException();
        }

        private static IEnumerable<IFormulaToken> Tokenize(FormulaNodeNumber formula)
        {
            // initialize list of possible tokens
            var tokenMappings = new List<KeyValuePair<string, Func<IFormulaToken>>>
            {
                new KeyValuePair<string, Func<IFormulaToken>>(
                    key: CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator,
                    value: FormulaTokenFactory.CreateDecimalSeparatorToken),
                new KeyValuePair<string, Func<IFormulaToken>>(
                    key: CultureInfo.CurrentCulture.NumberFormat.NegativeSign,
                    value: FormulaTokenFactory.CreateMinusToken)
            };
            for (var i = 0; i <= 9; i++)
            {
                var digit = i;
                tokenMappings.Add(new KeyValuePair<string, Func<IFormulaToken>>(
                    key: i.ToString(), 
                    value: () => FormulaTokenFactory.CreateDigitToken(digit)));
            }

            // split off tokens from the string representation
            var tokens = new List<IFormulaToken>();
            var value = formula.Value.ToString();
            while (value.Length != 0)
            {
                var mapping = tokenMappings.First(kvp => value.StartsWith(kvp.Key));
                tokens.Add(mapping.Value.Invoke());
                value = value.Remove(0, mapping.Key.Length);
            }
            return tokens;
        }

        private static IEnumerable<ConstantFormulaTree> Tokenize(ConstantFormulaTree formula, Func<ConstantFormulaTree> tokenCreator)
        {
            return Enumerable.Repeat(tokenCreator.Invoke(), 1);
        }

        private IEnumerable<IFormulaToken> Tokenize(FormulaNodeInfixOperator formula, Func<FormulaNodeInfixOperator> tokenCreator)
        {
            // TODO: clone node
            return Tokenize(formula.LeftChild)
                .Concat(Enumerable.Repeat(tokenCreator.Invoke(), 1))
                .Concat(Tokenize(formula.RightChild));
        }

        private IEnumerable<IFormulaToken> Tokenize(FormulaNodeUnaryFunction formula, Func<FormulaNodeUnaryFunction> tokenCreator)
        {
            // TODO: clone node
            return Enumerable.Repeat<IFormulaToken>(tokenCreator.Invoke(), 1)
                .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateParenthesisToken(true), 1))
                .Concat(Tokenize(formula.Child))
                .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateParenthesisToken(false), 1));
        }

        private IEnumerable<IFormulaToken> Tokenize(FormulaNodeBinaryFunction formula, Func<FormulaNodeBinaryFunction> tokenCreator)
        {
            // TODO: clone node
            return Enumerable.Repeat<IFormulaToken>(tokenCreator.Invoke(), 1)
                .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateParenthesisToken(true), 1))
                .Concat(Tokenize(formula.FirstChild))
                .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateArgumentSeparatorToken(), 1))
                .Concat(Tokenize(formula.SecondChild))
                .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateParenthesisToken(false), 1));
        }

        private IEnumerable<IFormulaToken> Tokenize(FormulaNodeParentheses formula)
        {
            // TODO: clone node
            return Enumerable.Repeat<IFormulaToken>(FormulaTokenFactory.CreateParenthesisToken(true), 1)
                .Concat(Tokenize(formula.Child))
                .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateParenthesisToken(false), 1));
        }

        private IEnumerable<IFormulaToken> Tokenize(UnaryFormulaTree formula, Func<UnaryFormulaTree> tokenCreator)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<IFormulaToken> Tokenize(FormulaNodeSensor formula, Func<FormulaNodeSensor> tokenCreator)
        {
            return Enumerable.Repeat(tokenCreator.Invoke(), 1);
        }

        private IEnumerable<IFormulaToken> Tokenize(FormulaNodeUserVariable formula, Func<FormulaNodeUserVariable> tokenCreator)
        {
            return Enumerable.Repeat(tokenCreator.Invoke(), 1);
        }

        private IEnumerable<IFormulaToken> Tokenize(FormulaNodeObjectVariable formula, Func<FormulaNodeObjectVariable> tokenCreator)
        {
            return Enumerable.Repeat(tokenCreator.Invoke(), 1);
        }


        #endregion
    }
}
