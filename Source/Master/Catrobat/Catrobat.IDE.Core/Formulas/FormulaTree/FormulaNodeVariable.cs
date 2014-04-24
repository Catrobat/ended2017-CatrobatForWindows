using System;
using System.Text;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;
using Catrobat.IDE.Core.Formulas;
using Catrobat.IDE.Core.Models.Formulas.FormulaToken;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaTree
{
    partial class FormulaNodeVariable
    {
        #region Implements IFormulaEvaluation

        public override double EvaluateNumber()
        {
            return 0;
        }

        #endregion

        #region Implements IFormulaSerialization

        protected override void SerializeToken(StringBuilder sb)
        {
            // not used
            throw new NotImplementedException();
        }

        internal override void Serialize(StringBuilder sb)
        {
            sb.Append(Variable == null ? FormulaSerializer.EmptyChild : Variable.Name);
        }

        #endregion

        #region Implements IFormulaInterpreter

        public override bool IsNumber()
        {
            return true;
        }

        #endregion
    }

    #region Implementations

    partial class FormulaNodeLocalVariable
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateLocalVariableToken(Variable);
        }

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateLocalVariableNode(Variable);
        }
    }

    partial class FormulaNodeGlobalVariable
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateGlobalVariableToken(Variable);
        }

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateGlobalVariableNode(Variable);
        }
    }

    #endregion
}
