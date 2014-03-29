using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;
using Catrobat.IDE.Core.FormulaEditor;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree
{
    abstract partial class FormulaNodeBinaryFunction
    {
        #region Implements IFormulaTokenizer

        public override IEnumerable<IFormulaToken> Tokenize()
        {
            return Enumerable.Repeat(CreateToken(), 1)
                .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateParenthesisToken(true), 1))
                .Concat(FirstChild.Tokenize())
                .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateArgumentSeparatorToken(), 1))
                .Concat(SecondChild.Tokenize())
                .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateParenthesisToken(false), 1));

        }

        #endregion

        #region Implements IFormulaSerialization

        internal override void Serialize(StringBuilder sb)
        {
            SerializeToken(sb);
            sb.Append("(");
            var firstChild = FirstChild as BaseFormulaTree;
            if (firstChild == null) sb.Append(FormulaSerializer.EmptyChild); else firstChild.Serialize(sb);
            // TODO: translate argument separator
            sb.Append(", ");
            var secondChild = SecondChild as BaseFormulaTree;
            if (secondChild == null) sb.Append(FormulaSerializer.EmptyChild); else secondChild.Serialize(sb);
            sb.Append(")");
        }

        #endregion

        #region Implements IFormulaInterpreter

        public override bool IsNumber()
        {
            return IsNumberN2N();
        }

        #endregion
    }

    #region Implementations

    partial class FormulaNodeMin
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateMinToken();
        }

        public override double EvaluateNumber()
        {
            return Math.Min(FirstChild.EvaluateNumber(), SecondChild.EvaluateNumber());
        }

        protected override void SerializeToken(StringBuilder sb)
        {
            // TODO: translate
            sb.Append("min");
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree firstChild, XmlFormulaTree secondChild)
        {
            return XmlFormulaTreeFactory.CreateMinNode(firstChild, secondChild);
        }
    }

    partial class FormulaNodeMax
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateMaxToken();
        }

        public override double EvaluateNumber()
        {
            return Math.Max(FirstChild.EvaluateNumber(), SecondChild.EvaluateNumber());
        }

        protected override void SerializeToken(StringBuilder sb)
        {
            // TODO: translate
            sb.Append("max");
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree firstChild, XmlFormulaTree secondChild)
        {
            return XmlFormulaTreeFactory.CreateMaxNode(firstChild, secondChild);
        }
    }

    partial class FormulaNodeRandom
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateRandomToken();
        }

        private static Random _random;
        protected static Random Random
        {
            get
            {
                if (_random == null)
                {
                    _random = new Random();
                }
                return _random;
            }
        }

        public override double EvaluateNumber()
        {
            var from = FirstChild.EvaluateNumber();
            var to = SecondChild.EvaluateNumber();
            return from + Random.NextDouble()*(to - from);
        }

        protected override void SerializeToken(StringBuilder sb)
        {
            // TODO: translate
            sb.Append("rand");
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree firstChild, XmlFormulaTree secondChild)
        {
            return XmlFormulaTreeFactory.CreateRandomNode(firstChild, secondChild);
        }
    }

    #endregion
}
