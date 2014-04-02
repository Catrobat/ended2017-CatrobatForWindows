using System.Collections.Generic;

namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken
{
    public abstract partial class FormulaTokenBrackets : BaseFormulaToken
    {
        public List<IFormulaToken> Children { get; set; }

        #region overrides Equals

        protected bool Equals(FormulaTokenBrackets other)
        {
            // auto-implemented by ReSharper
            return base.Equals(other) && Equals(Children, other.Children);
        }

        public override int GetHashCode()
        {
            // auto-implemented by ReSharper
            unchecked
            {
                return (base.GetHashCode()*397) ^ (Children != null ? Children.GetHashCode() : 0);
            }
        }

        #endregion
    }

    #region Imeplementations

    public partial class FormulaTokenParentheses : FormulaTokenBrackets
    {
    }

    #endregion
}
