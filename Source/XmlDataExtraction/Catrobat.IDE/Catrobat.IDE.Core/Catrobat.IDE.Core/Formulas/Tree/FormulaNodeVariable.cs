using Catrobat.IDE.Core.Formulas;
using Catrobat.IDE.Core.Models.Formulas.Tokens;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.Tree
{
    partial class FormulaNodeVariable
    {
        #region Implements IFormulaEvaluation

        public override double EvaluateNumber()
        {
            return 0;
        }

        #endregion

        #region Implements IStringSerializable

        public override string Serialize()
        {
            return Variable == null || Variable.Name == null
                ? FormulaSerializer.EmptyChild
                : Variable.Name;
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
    }

    partial class FormulaNodeGlobalVariable
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateGlobalVariableToken(Variable);
        }
    }

    #endregion
}
