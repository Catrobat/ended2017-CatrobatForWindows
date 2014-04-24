using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;
using Catrobat.IDE.Core.FormulaEditor;
using Catrobat.IDE.Core.Services;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree
{
    abstract partial class FormulaNodePrefixOperator
    {
        #region Implements IFormulaTokenizer

        public override IEnumerable<IFormulaToken> Tokenize()
        {
            return Enumerable.Repeat(CreateToken(), 1)
                .Concat(Child == null ? FormulaTokenizer.EmptyChild : Child.Tokenize());
        }

        #endregion

        #region Implements IFormulaSerialization

        internal override void Serialize(StringBuilder sb)
        {
            SerializeToken(sb);
            var child = Child as BaseFormulaTree;
            if (child == null) sb.Append(FormulaSerializer.EmptyChild); else child.Serialize(sb);
        }

        #endregion
    }

    #region Implementations

    partial class FormulaNodeNot
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateNotToken();
        }

        public override double EvaluateNumber()
        {
            return ~((long) Child.EvaluateNumber());
        }

        public override bool EvaluateLogic()
        {
            return !Child.EvaluateLogic();
        }

        protected override void SerializeToken(StringBuilder sb)
        {
            // TODO: translate
            sb.Append("Not ");
        }

        public override bool IsNumber()
        {
            return IsNumberT1T();
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateNotNode(child);
        }
    }

    partial class FormulaNodeNegativeSign
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateMinusToken();
        }

        public override double EvaluateNumber()
        {
            return -Child.EvaluateNumber();
        }

        protected override void SerializeToken(StringBuilder sb)
        {
            // TODO: translate
            sb.Append(ServiceLocator.CultureService.GetCulture().NumberFormat.NegativeSign);
        }

        public override bool IsNumber()
        {
            return IsNumberN1N();
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateNegativeSignNode(child);
        }
    }

    #endregion
}