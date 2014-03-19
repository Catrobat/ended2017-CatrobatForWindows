namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken
{
    public abstract partial class FormulaTokenBracket : IFormulaToken
    {
        public bool IsOpening { get; set; }

        public bool IsClosing {
            get
            {
                return !IsOpening;
            }
            set
            {
                IsOpening = !value;
            }
        }
    }

    #region Imeplementations

    public partial class FormulaTokenParenthesis : FormulaTokenBracket
    {
    }

    #endregion
}
