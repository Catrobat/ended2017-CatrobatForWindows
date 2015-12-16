using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Catrobat.IDE.Core.Models.Formulas.Tokens;
using Catrobat.IDE.Core.Services;
using Catrobat.Core.Resources.Localization;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.Tree
{
    abstract partial class ConstantFormulaTree
    {
        #region Implements IFormulaTokenizer

        public override IEnumerable<IFormulaToken> Tokenize()
        {
            return Enumerable.Repeat<IFormulaToken>(CreateToken(), 1);
        }

        #endregion

        #region Implements IStringBuilderSerializable

        public override void Append(StringBuilder sb)
        {
            sb.Append((string) Serialize());
        }

        #endregion
    }

    #region Implementations

    partial class FormulaNodeNumber
    {
        #region Implements IFormulaTokenizer

        private static CultureInfo _tokenMappingsCulture;
        private static List<KeyValuePair<string, Func<IFormulaToken>>> _tokenMappings;
        protected static IEnumerable<KeyValuePair<string, Func<IFormulaToken>>> TokenMappings
        {
            get
            {
                var culture = ServiceLocator.CultureService.GetCulture();
                if (_tokenMappings == null || !Equals(_tokenMappingsCulture, culture))
                {
                    _tokenMappings = new List<KeyValuePair<string, Func<IFormulaToken>>>
                    {
                        // add decimal separator mapping
                        new KeyValuePair<string, Func<IFormulaToken>>(
                            key: culture.NumberFormat.NumberDecimalSeparator,
                            value: FormulaTokenFactory.CreateDecimalSeparatorToken),

                        // add minus sign mapping
                        new KeyValuePair<string, Func<IFormulaToken>>(
                            key: culture.NumberFormat.NegativeSign,
                            value: FormulaTokenFactory.CreateMinusToken)
                    };

                    // add digits mapping
                    for (var i = 0; i <= 9; i++)
                    {
                        var digit = i;
                        _tokenMappings.Add(new KeyValuePair<string, Func<IFormulaToken>>(
                            key: i.ToString(culture),
                            value: () => FormulaTokenFactory.CreateDigitToken(digit)));
                    }
                    _tokenMappingsCulture = culture;
                }
                return _tokenMappings;
            }
        }

        protected override IFormulaToken CreateToken()
        {
            // not used
            throw new NotImplementedException();
        }

        public override IEnumerable<IFormulaToken> Tokenize()
        {
            // split off tokens from the string representation
            var tokens = new List<IFormulaToken>();
            var value = Value.ToString("R", ServiceLocator.CultureService.GetCulture());
            while (value.Length != 0)
            {
                var mapping = TokenMappings.First(kvp => value.StartsWith(kvp.Key));
                tokens.Add(mapping.Value.Invoke());
                value = value.Remove(0, mapping.Key.Length);
            }
            return tokens;
        }

        #endregion

        public override double EvaluateNumber()
        {
            return Value;
        }

        public override string Serialize()
        {
            return Value.ToString("R", ServiceLocator.CultureService.GetCulture());
        }

        public override bool IsNumber()
        {
            return true;
        }
    }

    partial class FormulaNodePi
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreatePiToken();
        }

        public override double EvaluateNumber()
        {
            return Math.PI;
        }

        public override string Serialize()
        {
            return "π";
        }

        public override bool IsNumber()
        {
            return true;
        }
    }

    partial class FormulaNodeTrue
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateTrueToken();
        }

        public override bool EvaluateLogic()
        {
            return true;
        }

        public override string Serialize()
        {
            return AppResources.Formula_Constant_True;
        }

        public override bool IsNumber()
        {
            return false;
        }
    }

    partial class FormulaNodeFalse
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateFalseToken();
        }
        
        public override bool EvaluateLogic()
        {
            return false;
        }

        public override string Serialize()
        {
            return AppResources.Formula_Constant_False;
        }

        public override bool IsNumber()
        {
            return false;
        }
    }

    #endregion

}
