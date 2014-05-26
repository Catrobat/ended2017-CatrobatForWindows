using Catrobat.IDE.Core.Models.Formulas.Tree;

namespace Catrobat.IDE.Core.Models.Formulas.Tokens
{
    internal abstract partial class FormulaTokenParameter : FormulaToken
    {
    }

    #region Implementations

    internal partial class FormulaTokenUnaryParameter : FormulaTokenParameter
    {
        public FormulaTree Parameter { get; set; }
        
        #region Implements ITestEquatable

        protected override bool TestEquals(FormulaToken other)
        {
            return base.TestEquals(other) && TestEquals((FormulaTokenUnaryParameter) other);
        }

        protected bool TestEquals(FormulaTokenUnaryParameter other)
        {
            return TestEquals(Parameter, other.Parameter);
        }

        #endregion
    }

    internal partial class FormulaTokenBinaryParameter : FormulaTokenParameter
    {
        public FormulaTree FirstParameter { get; set; }
        public FormulaTree SecondParameter { get; set; }

        #region Implements ITestEquatable

        protected override bool TestEquals(FormulaToken other)
        {
            return base.TestEquals(other) && TestEquals((FormulaTokenBinaryParameter) other);
        }

        protected bool TestEquals(FormulaTokenBinaryParameter other)
        {
            return TestEquals(FirstParameter, other.FirstParameter) && Equals(SecondParameter, other.SecondParameter);
        }

        #endregion
    }
    
    #endregion
}
