using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;
using Catrobat.IDE.Core.Formulas;
using Catrobat.IDE.Core.Models.Formulas.FormulaToken;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaTree
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

        #region Implements IStringBuilderSerializable

        public override void Append(StringBuilder sb)
        {
            AppendToken(sb);
            if (Child == null)
            {
                sb.Append(FormulaSerializer.EmptyChild);
            }
            else
            {
                Child.Append(sb);
            }
        }

        protected virtual void AppendToken(StringBuilder sb)
        {
            sb.Append(Serialize());
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

        protected override void AppendToken(StringBuilder sb)
        {
            base.AppendToken(sb);
            sb.Append(" ");
        }

        public override string Serialize()
        {
            return AppResources.Formula_Operator_Not;
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

        public override string Serialize()
        {
            return ServiceLocator.CultureService.GetCulture().NumberFormat.NegativeSign;
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
