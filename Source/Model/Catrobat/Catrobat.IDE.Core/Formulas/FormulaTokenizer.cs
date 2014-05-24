using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Formulas.Tokens;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace Catrobat.IDE.Core.Formulas
{
    public class FormulaTokenizer
    {
        public static IEnumerable<IFormulaToken> EmptyChild = Enumerable.Empty<IFormulaToken>();

        #region Tokenize string

        private static CultureInfo _cultureSpecificTokenMappingsCulture;
        private static Dictionary<string, Func<IFormulaToken>> _cultureSpecificTokenMappings;
        private static Dictionary<string, Func<IFormulaToken>> _localizationSpecificTokenMappings;
        private static Dictionary<string, Func<IFormulaToken>> _invariantTokenMappings;
        private Dictionary<string, Func<IFormulaToken>> _instanceSpecificTokenMappings;
        private readonly IDictionary<string, LocalVariable> _localVariables;
        private readonly IDictionary<string, GlobalVariable> _globalVariables;

        private static void InitCultureSpecificTokenMappings(CultureInfo culture)
        {
            _cultureSpecificTokenMappings = new Dictionary<string, Func<IFormulaToken>>();

            // constants
            AddTokenMapping(_cultureSpecificTokenMappings, FormulaTokenFactory.CreateDecimalSeparatorToken);
            AddTokenMapping(_cultureSpecificTokenMappings, culture.NumberFormat.NegativeSign, FormulaTokenFactory.CreateMinusToken);
            foreach (var digit in Enumerable.Range(0, 10))
            {
                // access to foreach variable in closure
                var value = digit;
                AddTokenMapping(_cultureSpecificTokenMappings, digit.ToString(culture), () => FormulaTokenFactory.CreateDigitToken(value));
            }

            // functions
            var parameterSeparatorValue = culture.NumberFormat.NumberDecimalSeparator == "," ? ", " : ",";
            AddTokenMapping(_cultureSpecificTokenMappings, parameterSeparatorValue, FormulaTokenFactory.CreateParameterSeparatorToken);

            _cultureSpecificTokenMappingsCulture = culture;
        }

        private static void InitLocalizationSpecificTokenMappings()
        {
            _localizationSpecificTokenMappings = new Dictionary<string, Func<IFormulaToken>>();

            // constants
            AddTokenMapping(_invariantTokenMappings, FormulaTokenFactory.CreateTrueToken);
            AddTokenMapping(_invariantTokenMappings, FormulaTokenFactory.CreateFalseToken);

            // operators
            AddTokenMapping(_localizationSpecificTokenMappings, FormulaTokenFactory.CreateAndToken);
            AddTokenMapping(_localizationSpecificTokenMappings, FormulaTokenFactory.CreateOrToken);
            AddTokenMapping(_localizationSpecificTokenMappings, FormulaTokenFactory.CreateNotToken);
            AddTokenMapping(_localizationSpecificTokenMappings, FormulaTokenFactory.CreateModToken);

             // functions
            AddTokenMapping(_localizationSpecificTokenMappings, FormulaTokenFactory.CreateMinToken);
            AddTokenMapping(_localizationSpecificTokenMappings, FormulaTokenFactory.CreateMaxToken);
            AddTokenMapping(_localizationSpecificTokenMappings, FormulaTokenFactory.CreateSqrtToken);
            AddTokenMapping(_localizationSpecificTokenMappings, FormulaTokenFactory.CreateAbsToken);
            AddTokenMapping(_localizationSpecificTokenMappings, FormulaTokenFactory.CreateRoundToken);
            AddTokenMapping(_localizationSpecificTokenMappings, FormulaTokenFactory.CreateRandomToken);

            // sensors
            AddTokenMapping(_localizationSpecificTokenMappings, FormulaTokenFactory.CreateAccelerationXToken);
            AddTokenMapping(_localizationSpecificTokenMappings, FormulaTokenFactory.CreateAccelerationYToken);
            AddTokenMapping(_localizationSpecificTokenMappings, FormulaTokenFactory.CreateAccelerationZToken);
            AddTokenMapping(_localizationSpecificTokenMappings, FormulaTokenFactory.CreateCompassToken);
            AddTokenMapping(_localizationSpecificTokenMappings, FormulaTokenFactory.CreateInclinationXToken);
            AddTokenMapping(_localizationSpecificTokenMappings, FormulaTokenFactory.CreateInclinationYToken);
            AddTokenMapping(_localizationSpecificTokenMappings, FormulaTokenFactory.CreateLoudnessToken);

            // properties
            AddTokenMapping(_cultureSpecificTokenMappings, FormulaTokenFactory.CreateBrightnessToken);
            AddTokenMapping(_cultureSpecificTokenMappings, FormulaTokenFactory.CreateLayerToken);
            AddTokenMapping(_cultureSpecificTokenMappings, FormulaTokenFactory.CreatePositionXToken);
            AddTokenMapping(_cultureSpecificTokenMappings, FormulaTokenFactory.CreatePositionYToken);
            AddTokenMapping(_cultureSpecificTokenMappings, FormulaTokenFactory.CreateRotationToken);
            AddTokenMapping(_cultureSpecificTokenMappings, FormulaTokenFactory.CreateSizeToken);
            AddTokenMapping(_cultureSpecificTokenMappings, FormulaTokenFactory.CreateTransparencyToken);
       }

        // Tokenize\(.*, (FormulaTokenFactory.\w+), .*

        private static void InitInvariantTokenMappings()
        {
            _invariantTokenMappings = new Dictionary<string, Func<IFormulaToken>>();

            // constants
            AddTokenMapping(_invariantTokenMappings, FormulaTokenFactory.CreatePiToken);

            // operators
            AddTokenMapping(_invariantTokenMappings, FormulaTokenFactory.CreatePlusToken);
            AddTokenMapping(_invariantTokenMappings, FormulaTokenFactory.CreateMinusToken);
            AddTokenMapping(_invariantTokenMappings, FormulaTokenFactory.CreateMultiplyToken);
            AddTokenMapping(_invariantTokenMappings, FormulaTokenFactory.CreateDivideToken);
            AddTokenMapping(_invariantTokenMappings, FormulaTokenFactory.CreateCaretToken);
            AddTokenMapping(_invariantTokenMappings, FormulaTokenFactory.CreateEqualsToken);
            AddTokenMapping(_invariantTokenMappings, FormulaTokenFactory.CreateNotEqualsToken);
            AddTokenMapping(_invariantTokenMappings, FormulaTokenFactory.CreateLessToken);
            AddTokenMapping(_invariantTokenMappings, FormulaTokenFactory.CreateLessEqualToken);
            AddTokenMapping(_invariantTokenMappings, FormulaTokenFactory.CreateGreaterToken);
            AddTokenMapping(_invariantTokenMappings, FormulaTokenFactory.CreateGreaterEqualToken);


            // functions
            AddTokenMapping(_invariantTokenMappings, FormulaTokenFactory.CreateExpToken);
            AddTokenMapping(_invariantTokenMappings, FormulaTokenFactory.CreateLogToken);
            AddTokenMapping(_invariantTokenMappings, FormulaTokenFactory.CreateLnToken);
            AddTokenMapping(_invariantTokenMappings, FormulaTokenFactory.CreateSinToken);
            AddTokenMapping(_invariantTokenMappings, FormulaTokenFactory.CreateCosToken);
            AddTokenMapping(_invariantTokenMappings, FormulaTokenFactory.CreateTanToken);
            AddTokenMapping(_invariantTokenMappings, FormulaTokenFactory.CreateArcsinToken);
            AddTokenMapping(_invariantTokenMappings, FormulaTokenFactory.CreateArccosToken);
            AddTokenMapping(_invariantTokenMappings, FormulaTokenFactory.CreateArctanToken);

            // brackets
            AddTokenMapping(_invariantTokenMappings, () => FormulaTokenFactory.CreateParenthesisToken(true));
            AddTokenMapping(_invariantTokenMappings, () => FormulaTokenFactory.CreateParenthesisToken(false));
        }

        private void InitInstanceSpecificTokenMappings()
        {
            _instanceSpecificTokenMappings = new Dictionary<string, Func<IFormulaToken>>();

            // variables
            foreach (var variable in _globalVariables.Select(entry => entry.Value))
            {
                var variable1 = variable;
                AddTokenMapping(_instanceSpecificTokenMappings, () => FormulaTokenFactory.CreateGlobalVariableToken(variable1));
            }
            foreach (var variable in _localVariables.Select(entry => entry.Value))
            {
                var variable1 = variable;
                AddTokenMapping(_instanceSpecificTokenMappings, () => FormulaTokenFactory.CreateLocalVariableToken(variable1));
            }
        }

        private static void AddTokenMapping(IDictionary<string, Func<IFormulaToken>> tokenMappings, Func<IFormulaToken> tokenCreator)
        {
            AddTokenMapping(tokenMappings, FormulaSerializer.Serialize(tokenCreator.Invoke()), tokenCreator);
        }

        private static void AddTokenMapping(IDictionary<string, Func<IFormulaToken>> tokenMappings, string value, Func<IFormulaToken> tokenCreator)
        {
            if (!string.IsNullOrWhiteSpace(value)) tokenMappings[value] = tokenCreator;
        }

        private void InitTokenMappings(CultureInfo culture)
        {
            if (_invariantTokenMappings == null) InitInvariantTokenMappings();
            if (_instanceSpecificTokenMappings == null) InitInstanceSpecificTokenMappings();
            var cultureChanged = !Equals(_cultureSpecificTokenMappingsCulture, culture);
            if (_cultureSpecificTokenMappings == null || cultureChanged) InitCultureSpecificTokenMappings(culture);
            if (_localizationSpecificTokenMappings == null || cultureChanged) InitLocalizationSpecificTokenMappings();
        }

        public FormulaTokenizer(IEnumerable<LocalVariable> localVariables, IEnumerable<GlobalVariable> globalVariable)
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

            InitTokenMappings(ServiceLocator.CultureService.GetCulture());

            var tokens = new List<IFormulaToken>();
            for (var index = 0; index < input.Length; )
            {
                // ignore whitespace
                if (char.IsWhiteSpace(input[index]))
                {
                    index++;
                    continue;
                }

                var match = Tokenize(_invariantTokenMappings, input, index)
                            ?? Tokenize(_cultureSpecificTokenMappings, input, index)
                            ?? Tokenize(_localizationSpecificTokenMappings, input, index)
                            ?? Tokenize(_instanceSpecificTokenMappings, input, index);
                if (match == null)
                {
                    // TODO: translate parsing error
                    parsingError = new ParsingError("Unknown token. ", index, 0);
                    return null;
                }
                tokens.Add(match.Item2);
                index += match.Item1.Length;
            }

            parsingError = null;
            return tokens;
        }

        private static Tuple<string, IFormulaToken> Tokenize(IEnumerable<KeyValuePair<string, Func<IFormulaToken>>> tokenMappings, string input, int startIndex)
        {
            var matches = tokenMappings
                .Where(entry => input.StartsWith(entry.Key, startIndex, StringComparison.OrdinalIgnoreCase))
                .Argmax(entry => entry.Key.Length)
                .Take(1)
                .ToList();
            return matches.Count == 0 ? null : Tuple.Create(matches[0].Key, matches[0].Value.Invoke());
        }

        #endregion

        #region Tokenize formula

        public static IEnumerable<IFormulaToken> Tokenize(FormulaTree formula)
        {
            if (formula == null) return null;
    
            return formula.Tokenize();
        }

        #endregion
    }

}
