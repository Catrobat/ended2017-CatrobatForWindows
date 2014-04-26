using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Formulas.FormulaToken;
using Catrobat.IDE.Core.Models.Formulas.FormulaTree;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.Core.Formulas
{
    class FormulaTokenizer
    {
        public static IEnumerable<IFormulaToken> EmptyChild = Enumerable.Empty<IFormulaToken>();

        #region Tokenize string

        private readonly IDictionary<string, UserVariable> _localVariables = null;
        private readonly IDictionary<string, UserVariable> _globalVariables = null;

        public FormulaTokenizer(IEnumerable<UserVariable> localVariables, IEnumerable<UserVariable> globalVariable)
        {
            _localVariables = localVariables.ToDictionary(variable => variable.Name);
            _globalVariables = globalVariable.ToDictionary(variable => variable.Name);
        }

        public bool Tokenize(string input, out IEnumerable<IFormulaToken> tokens, out IEnumerable<string> parsingErrors)
        {
            if (input == null)
            {
                tokens = null;
                parsingErrors = Enumerable.Empty<string>();
                return true;
            }

            var tokens2 = new List<IFormulaToken>();
            tokens = tokens2;
            var parsingErrors2 = new List<string>();
            parsingErrors = parsingErrors2;
            return Tokenize(input, ref tokens2, ref parsingErrors2);
        }

        private bool Tokenize(string input, ref List<IFormulaToken> tokens, ref List<string> parsingErrors)
        {
            var decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            var negativeSign = CultureInfo.CurrentCulture.NumberFormat.NegativeSign;
            var index = 0;
            while (index < input.Length)
            {
                // ignore whitespace
                if (char.IsWhiteSpace(input[index])) index++;

                // constants
                else if (TokenizeDigit(input, ref index, ref tokens)) { }
                else if (Tokenize(input, ref index, decimalSeparator, FormulaTokenFactory.CreateDecimalSeparatorToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "pi", FormulaTokenFactory.CreatePiToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "true", FormulaTokenFactory.CreateTrueToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "false", FormulaTokenFactory.CreateFalseToken, ref tokens)) { }

                // operators
                else if (Tokenize(input, ref index, "+", FormulaTokenFactory.CreatePlusToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "-", FormulaTokenFactory.CreateMinusToken, ref tokens)) { }
                else if (Tokenize(input, ref index, negativeSign, FormulaTokenFactory.CreateMinusToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "*", FormulaTokenFactory.CreateMultiplyToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "/", FormulaTokenFactory.CreateDivideToken, ref tokens)) { }
                else if (Tokenize(input, ref index, ":", FormulaTokenFactory.CreateDivideToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "^", FormulaTokenFactory.CreateCaretToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "==", FormulaTokenFactory.CreateEqualsToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "=", FormulaTokenFactory.CreateEqualsToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "≠", FormulaTokenFactory.CreateNotEqualsToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "<>", FormulaTokenFactory.CreateNotEqualsToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "!=", FormulaTokenFactory.CreateNotEqualsToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "≤", FormulaTokenFactory.CreateLessEqualToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "<=", FormulaTokenFactory.CreateLessEqualToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "<", FormulaTokenFactory.CreateLessToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "≥", FormulaTokenFactory.CreateGreaterEqualToken, ref tokens)) { }
                else if (Tokenize(input, ref index, ">=", FormulaTokenFactory.CreateGreaterEqualToken, ref tokens)) { }
                else if (Tokenize(input, ref index, ">", FormulaTokenFactory.CreateGreaterToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "and", FormulaTokenFactory.CreateAndToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "or", FormulaTokenFactory.CreateOrToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "not", FormulaTokenFactory.CreateNotToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "mod", FormulaTokenFactory.CreateModToken, ref tokens)) { }

                // functions
                else if (Tokenize(input, ref index, ",", FormulaTokenFactory.CreateParameterSeparatorToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "exp", FormulaTokenFactory.CreateExpToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "e^", FormulaTokenFactory.CreateExpToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "log", FormulaTokenFactory.CreateLogToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "ln", FormulaTokenFactory.CreateLnToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "min", FormulaTokenFactory.CreateMinToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "max", FormulaTokenFactory.CreateMaxToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "sin", FormulaTokenFactory.CreateSinToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "cos", FormulaTokenFactory.CreateCosToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "tan", FormulaTokenFactory.CreateTanToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "arcsin", FormulaTokenFactory.CreateArcsinToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "arccos", FormulaTokenFactory.CreateArccosToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "arctan", FormulaTokenFactory.CreateArctanToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "sqrt", FormulaTokenFactory.CreateSqrtToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "abs", FormulaTokenFactory.CreateAbsToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "round", FormulaTokenFactory.CreateRoundToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "random", FormulaTokenFactory.CreateRandomToken, ref tokens)) { }

                // sensors
                // TODO: translate sensors
                else if (Tokenize(input, ref index, "accelerationx", FormulaTokenFactory.CreateAccelerationXToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "accelerationy", FormulaTokenFactory.CreateAccelerationYToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "accelerationz", FormulaTokenFactory.CreateAccelerationZToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "compass", FormulaTokenFactory.CreateCompassToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "inclinationx", FormulaTokenFactory.CreateInclinationXToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "inclinationy", FormulaTokenFactory.CreateInclinationYToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "loudness", FormulaTokenFactory.CreateLoudnessToken, ref tokens)) { }

                // properties
                // TODO: translate sensors
                else if (Tokenize(input, ref index, "brightness", FormulaTokenFactory.CreateAccelerationXToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "layer", FormulaTokenFactory.CreateAccelerationXToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "positionx", FormulaTokenFactory.CreateAccelerationXToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "positiony", FormulaTokenFactory.CreateAccelerationXToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "rotation", FormulaTokenFactory.CreateAccelerationXToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "size", FormulaTokenFactory.CreateAccelerationXToken, ref tokens)) { }
                else if (Tokenize(input, ref index, "transparency", FormulaTokenFactory.CreateAccelerationXToken, ref tokens)) { }

                // brackets
                else if (Tokenize(input, ref index, "(", () => FormulaTokenFactory.CreateParenthesisToken(true), ref tokens)) { }
                else if (Tokenize(input, ref index, ")", () => FormulaTokenFactory.CreateParenthesisToken(false), ref tokens)) { }

                // user variables
                else if (TokenizeVariable(input, ref index, ref tokens)) { }

                else
                {
                    // TODO: add parsing error like "Unknown token at ..."
                    parsingErrors.Add("Unknown token. ");
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
                        provider: ServiceLocator.CultureService.GetCulture(),
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

        private bool TokenizeDigit(string input, ref int startIndex, ref List<IFormulaToken> tokens)
        {
            for (var digit = 0; digit <= 9; digit++)
            {
                // access to modified closure
                var digit2 = digit;
                if (Tokenize(input, ref startIndex, 
                    tokenValue: digit.ToString(), 
                    constructor: () => FormulaTokenFactory.CreateDigitToken(digit2), 
                    tokens: ref tokens)) return true;
            }
            return false;
        }

        private bool TokenizeVariable(string input, ref int startIndex, ref List<IFormulaToken> tokens)
        {
            foreach (var entry in _localVariables)
            {
                // access to modified closure
                var entry2 = entry;
                if (Tokenize(input, ref startIndex,
                    tokenValue: entry.Key,
                    constructor: () => FormulaTokenFactory.CreateLocalVariableToken(entry2.Value), 
                    tokens: ref tokens)) return true;
            }
            foreach (var entry in _globalVariables)
            {
                // access to modified closure
                var entry2 = entry;
                if (Tokenize(input, ref startIndex,
                    tokenValue: entry.Key,
                    constructor: () => FormulaTokenFactory.CreateGlobalVariableToken(entry2.Value),
                    tokens: ref tokens)) return true;
            }
            return false;
        }

        #endregion

        #region Tokenize formula

        public static IEnumerable<IFormulaToken> Tokenize(IFormulaTree formula)
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
