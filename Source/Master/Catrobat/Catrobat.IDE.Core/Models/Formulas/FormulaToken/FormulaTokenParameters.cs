using Catrobat.IDE.Core.Models.Formulas.FormulaTree;

namespace Catrobat.IDE.Core.Models.Formulas.FormulaToken
{
    internal class FormulaTokenParameters : BaseFormulaToken
    {
        public IFormulaTree FirstParameter { get; set; }
        public IFormulaTree SecondParameter { get; set; }

        #region Overrides Equals

        public override bool Equals(object obj)
        {
            // auto-implemented by ReSharper
            return base.Equals(obj) && Equals((FormulaTokenParameters) obj);
        }

        protected bool Equals(FormulaTokenParameters other)
        {
            // auto-implemented by ReSharper
            return Equals(FirstParameter, other.FirstParameter) && Equals(SecondParameter, other.SecondParameter);
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
}
