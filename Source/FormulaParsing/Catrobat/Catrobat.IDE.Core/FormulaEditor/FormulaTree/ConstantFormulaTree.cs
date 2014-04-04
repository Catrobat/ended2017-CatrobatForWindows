using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree
{
    abstract partial class ConstantFormulaTree
    {
        #region Implements IFormulaTokenizer

        public override IEnumerable<IFormulaToken> Tokenize()
        {
            return Enumerable.Repeat(CreateToken(), 1);
        }

        protected override void SerializeToken(StringBuilder sb)
        {
            // not used
            throw new NotImplementedException();
        }

        #endregion

        #region Implements IFormulaInterpreter

        public override void ClearChildren()
        {
            // nothing to do
        }

        #endregion
    }

    #region Implementations

    partial class FormulaNodeNumber
    {
        #region Implements IFormulaTokenizer

        private static List<KeyValuePair<string, Func<IFormulaToken>>> _tokenMappings;
        protected static IEnumerable<KeyValuePair<string, Func<IFormulaToken>>> TokenMappings
        {
            get
            {
                if (_tokenMappings == null)
                {
                    _tokenMappings = new List<KeyValuePair<string, Func<IFormulaToken>>>
                    {
                        // add decimal separator mapping
                        new KeyValuePair<string, Func<IFormulaToken>>(
                            key: CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator,
                            value: FormulaTokenFactory.CreateDecimalSeparatorToken),

                        // add minus sign mapping
                        new KeyValuePair<string, Func<IFormulaToken>>(
                            key: CultureInfo.CurrentCulture.NumberFormat.NegativeSign,
                            value: FormulaTokenFactory.CreateNegativeSignToken)
                    };

                    // add digits mapping
                    for (var i = 0; i <= 9; i++)
                    {
                        var digit = i;
                        _tokenMappings.Add(new KeyValuePair<string, Func<IFormulaToken>>(
                            key: i.ToString(),
                            value: () => FormulaTokenFactory.CreateDigitToken(digit)));
                    }
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
            var value = Value.ToString("R");
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

        internal override void Serialize(StringBuilder sb)
        {
            sb.Append(Value);
        }

        public override bool IsNumber()
        {
            return true;
        }

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateNumberNode(Value);
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

        internal override void Serialize(StringBuilder sb)
        {
            // TODO: translate
            sb.Append("pi");
        }

        public override bool IsNumber()
        {
            return true;
        }

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreatePiNode();
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

        internal override void Serialize(StringBuilder sb)
        {
            // TODO: translate
            sb.Append("True");
        }

        public override bool IsNumber()
        {
            return false;
        }

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateTrueNode();
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

        internal override void Serialize(StringBuilder sb)
        {
            // TODO: translate
            sb.Append("False");
        }

        public override bool IsNumber()
        {
            return false;
        }

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateFalseNode();
        }
    }

    #endregion

}
