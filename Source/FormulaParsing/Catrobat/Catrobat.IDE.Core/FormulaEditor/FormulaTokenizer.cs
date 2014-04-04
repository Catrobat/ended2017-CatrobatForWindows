using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.Core.FormulaEditor
{
    class FormulaTokenizer
    {
        public static IEnumerable<IFormulaToken> EmptyChild = Enumerable.Empty<IFormulaToken>();

        #region Tokenize string

        private readonly IDictionary<string, UserVariable> _userVariables = null;
        private readonly IEnumerable<UserVariable> _objectVariable = null;

        public FormulaTokenizer(IEnumerable<UserVariable> userVariables, IEnumerable<UserVariable> objectVariable)
        {
            _userVariables = userVariables.ToDictionary(variable => variable.Name);
            _objectVariable = objectVariable;
        }

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
                        provider: ServiceLocator.CulureService.GetCulture(),
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

        #region Tokenize formula

        public FormulaTokenizer()
        {
        }

        public IEnumerable<IFormulaToken> Tokenize(IFormulaTree formula)
        {
            if (formula == null) return null;
#if DEBUG
            return formula.Tokenize();
#else
            try
            {
                return formula.Tokenize();
            }
            catch (Exception)
            {
                return null;
            }
#endif
        }

        #endregion
    }

}
