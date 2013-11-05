namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken
{
    abstract class FormulaTokenBracket : IFormulaToken
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
