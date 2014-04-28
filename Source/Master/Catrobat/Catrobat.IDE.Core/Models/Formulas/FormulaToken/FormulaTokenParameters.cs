using Catrobat.IDE.Core.Models.Formulas.FormulaTree;

namespace Catrobat.IDE.Core.Models.Formulas.FormulaToken
{
    internal abstract partial class FormulaTokenParameter : BaseFormulaToken
    {
    }

    #region Implementations

    internal partial class FormulaTokenUnaryParameter : FormulaTokenParameter
    {
        public IFormulaTree Parameter { get; set; }
        
        #region Overrides Equals

        public override bool Equals(object obj)
        {
            // auto-implemented by ReSharper
            return base.Equals(obj) && Equals((FormulaTokenUnaryParameter) obj);
        }

        protected bool Equals(FormulaTokenUnaryParameter other)
        {
            // auto-implemented by ReSharper
            return base.Equals(other) && Equals(Parameter, other.Parameter);
        }

        public override int GetHashCode()
        {
            // auto-implemented by ReSharper
            unchecked
            {
                return (base.GetHashCode()*397) ^ (Parameter != null ? Parameter.GetHashCode() : 0);
            }
        }

        #endregion
    }

    internal partial class FormulaTokenBinaryParameter : FormulaTokenParameter
    {
        public IFormulaTree FirstParameter { get; set; }
        public IFormulaTree SecondParameter { get; set; }

        #region Overrides Equals

        public override bool Equals(object obj)
        {
            // auto-implemented by ReSharper
            return base.Equals(obj) && Equals((FormulaTokenBinaryParameter) obj);
        }

        protected bool Equals(FormulaTokenBinaryParameter other)
        {
            // auto-implemented by ReSharper
            return base.Equals(other) && Equals(FirstParameter, other.FirstParameter) && Equals(SecondParameter, other.SecondParameter);
        }

        public override int GetHashCode()
        {
            // auto-implemented by ReSharper
            unchecked
            {
                return ((FirstParameter != null ? FirstParameter.GetHashCode() : 0) * 397) ^ (SecondParameter != null ? SecondParameter.GetHashCode() : 0);
            }
        }

        #endregion
    }
    
    #endregion
}
