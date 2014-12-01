// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.Tokens
{
    public abstract partial class FormulaTokenBracket
    {
    }

    #region Implementations

    public partial class FormulaTokenParenthesis
    {
        public override string Serialize()
        {
            return IsOpening ? "(" : ")";
        }
    }

    #endregion
}
