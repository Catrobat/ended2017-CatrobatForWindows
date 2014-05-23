using System.Diagnostics;

namespace Catrobat.IDE.Core.Models.Formulas.Tokens
{
    public abstract partial class FormulaTokenBracket : FormulaToken
    {
        public virtual bool IsOpening { get; set; }

        public virtual bool IsClosing
        {
            get { return !IsOpening; }
            set { IsOpening = !value; }
        }

        #region Implements ITestEquatable

        protected override bool TestEquals(FormulaToken other)
        {
            return base.TestEquals(other) && TestEquals((FormulaTokenBracket) other);
        }

        protected bool TestEquals(FormulaTokenBracket other)
        {
            return Equals(IsOpening, other.IsOpening);
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
