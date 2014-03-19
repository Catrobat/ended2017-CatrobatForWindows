using Catrobat.IDE.Core.CatrobatObjects.Formulas;

namespace Catrobat.IDE.Core.FormulaEditor.Editor
{
    public class SelectedFormulaInformation
    {
        public Formula FormulaRoot { get; set; }

        public FormulaTree SelectedFormula { get; set; }

        public FormulaTree SelectedFormulaParent { get; set; }
    }
}
