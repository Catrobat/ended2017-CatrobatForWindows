using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Formulas.FormulaToken;
using Catrobat.IDE.Core.Models.Formulas.FormulaTree;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catrobat.IDE.Core.Formulas
{
    class FormulaTokenizer
    {
        public static IEnumerable<IFormulaToken> EmptyChild = Enumerable.Empty<IFormulaToken>();

        #region Tokenize string

        private readonly IDictionary<string, UserVariable> _localVariables;
        private readonly IDictionary<string, UserVariable> _globalVariables;

        [Obsolete("Cache AppResources_Formula, digits, negative sign and decimal separator and refresh when culture changes. ")]
        public FormulaTokenizer(IEnumerable<UserVariable> localVariables, IEnumerable<UserVariable> globalVariable)
        {
            _localVariables = localVariables.ToDictionary(variable => variable.Name);
            _globalVariables = globalVariable.ToDictionary(variable => variable.Name);
        }

        public IEnumerable<IFormulaToken> Tokenize(string input, out ParsingError parsingError)
        {
            if (input == null)
            {
                parsingError = null;
                return null;
            }

            var tokens2 = new List<IFormulaToken>();
            Tokenize(input, ref tokens2, out parsingError);
            if (parsingError != null) return null;
            
            return tokens2;
        }

        private void Tokenize(string input, ref List<IFormulaToken> tokens, out ParsingError parsingError)
        {
            parsingError = null;

            for (var index = 0; index < input.Length; )
            {
                // ignore whitespace
                if (char.IsWhiteSpace(input[index]))
                {
                    index++;
                    continue;
                }

                if (!TokenizeOperator(input, ref index, ref tokens) &&
                    !TokenizeFunction(input, ref index, ref tokens) &&
                    !TokenizeSensor(input, ref index, ref tokens) &&
                    !TokenizeProperty(input, ref index, ref tokens) &&
                    !TokenizeBracket(input, ref index, ref tokens) &&
                    !TokenizeConstant(input, ref index, ref tokens) &&
                    !TokenizeVariable(input, ref index, ref tokens))
                {
                    // TODO: translate parsing error
                    parsingError = new ParsingError("Unknown token. ", index, 0);
                    return;
                }
            }
        }

        private bool TokenizeConstant(string input, ref int startIndex, ref List<IFormulaToken> tokens)
        {
            var decimalSeparator = ServiceLocator.CultureService.GetCulture().NumberFormat.NumberDecimalSeparator;
            return
                TokenizeDigit(input, ref startIndex, ref tokens) ||
                Tokenize(input, ref startIndex, decimalSeparator, FormulaTokenFactory.CreateDecimalSeparatorToken, ref tokens) ||
                Tokenize(input, ref startIndex, "π", FormulaTokenFactory.CreatePiToken, ref tokens) ||
                Tokenize(input, ref startIndex, AppResources.Formula_Constant_True, FormulaTokenFactory.CreateTrueToken, ref tokens) ||
                Tokenize(input, ref startIndex, AppResources.Formula_Constant_False, FormulaTokenFactory.CreateFalseToken, ref tokens);
        }

        private bool TokenizeDigit(string input, ref int startIndex, ref List<IFormulaToken> tokens)
        {
            var culture = ServiceLocator.CultureService.GetCulture();
            for (var digit = 0; digit <= 9; digit++)
            {
                // access to modified closure
                var digit2 = digit;
                if (Tokenize(input, ref startIndex,
                    tokenValue: digit.ToString(culture), 
                    token: FormulaTokenFactory.CreateDigitToken(digit2), 
                    tokens: ref tokens)) return true;
            }
            return false;
        }

        private bool TokenizeOperator(string input, ref int startIndex, ref List<IFormulaToken> tokens)
        {
            var negativeSign = ServiceLocator.CultureService.GetCulture().NumberFormat.NegativeSign;
            return
                Tokenize(input, ref startIndex, "+", FormulaTokenFactory.CreatePlusToken, ref tokens) ||
                Tokenize(input, ref startIndex, "-", FormulaTokenFactory.CreateMinusToken, ref tokens) ||
                Tokenize(input, ref startIndex, negativeSign, FormulaTokenFactory.CreateMinusToken, ref tokens) ||
                Tokenize(input, ref startIndex, "*", FormulaTokenFactory.CreateMultiplyToken, ref tokens) ||
                Tokenize(input, ref startIndex, "/", FormulaTokenFactory.CreateDivideToken, ref tokens) ||
                Tokenize(input, ref startIndex, ":", FormulaTokenFactory.CreateDivideToken, ref tokens) ||
                Tokenize(input, ref startIndex, "^", FormulaTokenFactory.CreateCaretToken, ref tokens) ||
                Tokenize(input, ref startIndex, "==", FormulaTokenFactory.CreateEqualsToken, ref tokens) ||
                Tokenize(input, ref startIndex, "=", FormulaTokenFactory.CreateEqualsToken, ref tokens) ||
                Tokenize(input, ref startIndex, "≠", FormulaTokenFactory.CreateNotEqualsToken, ref tokens) ||
                Tokenize(input, ref startIndex, "<>", FormulaTokenFactory.CreateNotEqualsToken, ref tokens) ||
                Tokenize(input, ref startIndex, "!=", FormulaTokenFactory.CreateNotEqualsToken, ref tokens) ||
                Tokenize(input, ref startIndex, "≤", FormulaTokenFactory.CreateLessEqualToken, ref tokens) ||
                Tokenize(input, ref startIndex, "<=", FormulaTokenFactory.CreateLessEqualToken, ref tokens) ||
                Tokenize(input, ref startIndex, "<", FormulaTokenFactory.CreateLessToken, ref tokens) ||
                Tokenize(input, ref startIndex, "≥", FormulaTokenFactory.CreateGreaterEqualToken, ref tokens) ||
                Tokenize(input, ref startIndex, ">=", FormulaTokenFactory.CreateGreaterEqualToken, ref tokens) ||
                Tokenize(input, ref startIndex, ">", FormulaTokenFactory.CreateGreaterToken, ref tokens) ||
                Tokenize(input, ref startIndex, AppResources.Formula_Operator_And, FormulaTokenFactory.CreateAndToken, ref tokens) ||
                Tokenize(input, ref startIndex, AppResources.Formula_Operator_Or, FormulaTokenFactory.CreateOrToken, ref tokens) ||
                Tokenize(input, ref startIndex, AppResources.Formula_Operator_Not, FormulaTokenFactory.CreateNotToken, ref tokens) ||
                Tokenize(input, ref startIndex, AppResources.Formula_Operator_Mod, FormulaTokenFactory.CreateModToken, ref tokens);
        }

        private bool TokenizeFunction(string input, ref int startIndex, ref List<IFormulaToken> tokens)
        {
            return
                TokenizeParameterSeparator(input, ref startIndex, ref tokens) ||
                Tokenize(input, ref startIndex, "exp", FormulaTokenFactory.CreateExpToken, ref tokens) ||
                Tokenize(input, ref startIndex, "log", FormulaTokenFactory.CreateLogToken, ref tokens) ||
                Tokenize(input, ref startIndex, "ln", FormulaTokenFactory.CreateLnToken, ref tokens) ||
                Tokenize(input, ref startIndex, AppResources.Formula_Function_Min, FormulaTokenFactory.CreateMinToken, ref tokens) ||
                Tokenize(input, ref startIndex, AppResources.Formula_Function_Max, FormulaTokenFactory.CreateMaxToken, ref tokens) ||
                Tokenize(input, ref startIndex, "sin", FormulaTokenFactory.CreateSinToken, ref tokens) ||
                Tokenize(input, ref startIndex, "cos", FormulaTokenFactory.CreateCosToken, ref tokens) ||
                Tokenize(input, ref startIndex, "tan", FormulaTokenFactory.CreateTanToken, ref tokens) ||
                Tokenize(input, ref startIndex, "arcsin", FormulaTokenFactory.CreateArcsinToken, ref tokens) ||
                Tokenize(input, ref startIndex, "arccos", FormulaTokenFactory.CreateArccosToken, ref tokens) ||
                Tokenize(input, ref startIndex, "arctan", FormulaTokenFactory.CreateArctanToken, ref tokens) ||
                Tokenize(input, ref startIndex, AppResources.Formula_Function_Sqrt, FormulaTokenFactory.CreateSqrtToken, ref tokens) ||
                Tokenize(input, ref startIndex, AppResources.Formula_Function_Abs, FormulaTokenFactory.CreateAbsToken, ref tokens) ||
                Tokenize(input, ref startIndex, AppResources.Formula_Function_Round, FormulaTokenFactory.CreateRoundToken, ref tokens) ||
                Tokenize(input, ref startIndex, AppResources.Formula_Function_Random, FormulaTokenFactory.CreateRandomToken, ref tokens);
        }

        private bool TokenizeParameterSeparator(string input, ref int startIndex, ref List<IFormulaToken> tokens)
        {
            var culture = ServiceLocator.CultureService.GetCulture();
            var tokenValue = culture.NumberFormat.NumberDecimalSeparator == ","
                ? ", "
                : ",";
            return Tokenize(input, ref startIndex, tokenValue, FormulaTokenFactory.CreateParameterSeparatorToken, ref tokens);
        }

        private bool TokenizeSensor(string input, ref int startIndex, ref List<IFormulaToken> tokens)
        {
            return
                Tokenize(input, ref startIndex, AppResources.Formula_Sensor_AccelerationX, FormulaTokenFactory.CreateAccelerationXToken, ref tokens) ||
                Tokenize(input, ref startIndex, AppResources.Formula_Sensor_AccelerationY, FormulaTokenFactory.CreateAccelerationYToken, ref tokens) ||
                Tokenize(input, ref startIndex, AppResources.Formula_Sensor_AccelerationZ, FormulaTokenFactory.CreateAccelerationZToken, ref tokens) ||
                Tokenize(input, ref startIndex, AppResources.Formula_Sensor_Compass, FormulaTokenFactory.CreateCompassToken, ref tokens) ||
                Tokenize(input, ref startIndex, AppResources.Formula_Sensor_InclinationX, FormulaTokenFactory.CreateInclinationXToken, ref tokens) ||
                Tokenize(input, ref startIndex, AppResources.Formula_Sensor_InclinationY, FormulaTokenFactory.CreateInclinationYToken, ref tokens) ||
                Tokenize(input, ref startIndex, AppResources.Formula_Sensor_Loudness, FormulaTokenFactory.CreateLoudnessToken, ref tokens);
        }


        private bool TokenizeProperty(string input, ref int startIndex, ref List<IFormulaToken> tokens)
        {
            return
                Tokenize(input, ref startIndex, AppResources.Formula_Property_Brightness, FormulaTokenFactory.CreateBrightnessToken, ref tokens) ||
                Tokenize(input, ref startIndex, AppResources.Formula_Property_Layer, FormulaTokenFactory.CreateLayerToken, ref tokens) ||
                Tokenize(input, ref startIndex, AppResources.Formula_Property_PositionX, FormulaTokenFactory.CreatePositionXToken, ref tokens) ||
                Tokenize(input, ref startIndex, AppResources.Formula_Property_PositionY, FormulaTokenFactory.CreatePositionYToken, ref tokens) ||
                Tokenize(input, ref startIndex, AppResources.Formula_Property_Rotation, FormulaTokenFactory.CreateRotationToken, ref tokens) ||
                Tokenize(input, ref startIndex, AppResources.Formula_Property_Size, FormulaTokenFactory.CreateSizeToken, ref tokens) ||
                Tokenize(input, ref startIndex, AppResources.Formula_Property_Transparency, FormulaTokenFactory.CreateTransparencyToken, ref tokens);
        }

        private bool TokenizeBracket(string input, ref int startIndex, ref List<IFormulaToken> tokens)
        {
            return
                Tokenize(input, ref startIndex, "(", FormulaTokenFactory.CreateParenthesisToken(true), ref tokens) ||
                Tokenize(input, ref startIndex, ")", FormulaTokenFactory.CreateParenthesisToken(false), ref tokens);
        }

        private bool TokenizeVariable(string input, ref int startIndex, ref List<IFormulaToken> tokens)
        {
            foreach (var entry in _localVariables)
            {
                // access to modified closure
                var entry2 = entry;
                if (Tokenize(input, ref startIndex,
                    tokenValue: entry.Key,
                    tokenCreator: () => FormulaTokenFactory.CreateLocalVariableToken(entry2.Value), 
                    tokens: ref tokens)) return true;
            }
            foreach (var entry in _globalVariables)
            {
                // access to modified closure
                var entry2 = entry;
                if (Tokenize(input, ref startIndex,
                    tokenValue: entry.Key,
                    tokenCreator: () => FormulaTokenFactory.CreateGlobalVariableToken(entry2.Value),
                    tokens: ref tokens)) return true;
            }
            return false;
        }

        private static bool Tokenize(string input, ref int startIndex, string tokenValue, Func<IFormulaToken> tokenCreator, ref List<IFormulaToken> tokens)
        {
            return Tokenize(input,ref  startIndex, tokenValue, tokenCreator.Invoke(), ref tokens);
        }

        private static bool Tokenize(string input, ref int startIndex, string tokenValue, IFormulaToken token, ref List<IFormulaToken> tokens)
        {
            if (!input.StartsWith(tokenValue, startIndex, StringComparison.CurrentCultureIgnoreCase)) return false;
            tokens.Add(token);
            startIndex += tokenValue.Length;
            return true;
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
