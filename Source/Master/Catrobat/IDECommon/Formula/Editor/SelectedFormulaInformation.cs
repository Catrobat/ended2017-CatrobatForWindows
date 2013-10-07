using Catrobat.Core.CatrobatObjects.Formulas;

namespace Catrobat.IDECommon.Formula.Editor
{
    public class SelectedFormulaInformation
    {
        public Core.CatrobatObjects.Formulas.Formula FormulaRoot { get; set; }

        public FormulaTree SelectedFormula { get; set; }

        public FormulaTree SelectedFormulaParent { get; set; }
    }
}
