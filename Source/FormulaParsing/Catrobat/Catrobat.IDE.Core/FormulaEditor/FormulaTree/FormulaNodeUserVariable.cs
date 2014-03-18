using System;
using System.Text;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree
{
    partial class FormulaNodeUserVariable
    {
        #region Implements IFormulaTokenizer

        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateUserVariableToken(Variable);
        }

        #endregion

        #region Implements IFormulaEvaluation

        public override double EvaluateNumber()
        {
            // TODO: evaluate object variables
            throw new NotImplementedException();
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
            sb.Append(Variable.Name);
        }

        #endregion

        #region Implements IFormulaInterpreter

        public override bool IsNumber()
        {
            return IsNumberN();
        }

        #endregion

        #region Implements IXmlFormulaConvertible

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateUserVariableNode(Variable);
        }

        #endregion
    }
}
