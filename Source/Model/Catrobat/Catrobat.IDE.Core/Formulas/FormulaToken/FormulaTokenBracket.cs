// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaToken
{
    public abstract partial class FormulaTokenBracket
    {
    }

    #region Implementations

    public partial class FormulaTokenParenthesis
    {
        #region Implements IStringSerializable

        public override string Serialize()
        {
            return IsOpening ? "(" : ")";
        }

        #endregion
    }

    #endregion
}
