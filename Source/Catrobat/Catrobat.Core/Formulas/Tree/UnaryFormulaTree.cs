using Catrobat.IDE.Core.Formulas;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.Tree
{
    abstract partial class UnaryFormulaTree
    {
        #region Implements IFormulaInterpreter

        protected bool IsNumberN1N()
        {
            // TODO: meaningful (translated?) error message
            if (!Child.IsNumber()) throw new SemanticsErrorException(this, "Child must be number");
            return true;
        }

        protected bool IsNumberL1L()
        {
            // TODO: meaningful (translated?) error message
            if (Child.IsNumber()) throw new SemanticsErrorException(this, "Child must be logic value");
            return true;
        }

        protected bool IsNumberT1T()
        {
            return Child.IsNumber();
        }

        #endregion
    }
}
