namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken
{
    public abstract partial class FormulaTokenBracket : IFormulaToken
    {
        public virtual bool IsOpening { get; set; }

        public virtual bool IsClosing
        {
            get { return !IsOpening; }
            set { IsOpening = !value; }
        }
    }

    #region Implementations

    public partial class FormulaTokenParenthesis : FormulaTokenBracket
    {
    }

    #endregion
}
