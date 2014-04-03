using System.Diagnostics;

namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken
{
    public abstract partial class FormulaTokenBracket : BaseFormulaToken
    {
        public virtual bool IsOpening { get; set; }

        public virtual bool IsClosing
        {
            get { return !IsOpening; }
            set { IsOpening = !value; }
        }

        #region overrides Equals

        protected bool Equals(FormulaTokenBracket other)
        {
            // auto-implemented by ReSharper
            return base.Equals(other) && IsOpening.Equals(other.IsOpening);
        }

        public override int GetHashCode()
        {
            // auto-implemented by ReSharper
            unchecked
            {
                return (base.GetHashCode() * 397) ^ IsOpening.GetHashCode();
            }
        }

        #endregion
    }

    #region Implementations

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public partial class FormulaTokenParenthesis : FormulaTokenBracket
    {
        protected string DebuggerDisplay
        {
            get { return IsOpening ? "(" : ")"; }
        }
    }

    #endregion
}
