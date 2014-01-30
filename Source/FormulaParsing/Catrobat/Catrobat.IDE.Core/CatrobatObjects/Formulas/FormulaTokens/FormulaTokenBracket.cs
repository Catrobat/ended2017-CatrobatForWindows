namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTokens
{
    public abstract class FormulaTokenBracket : IFormulaToken
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
}
