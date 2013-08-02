using Catrobat.Core.Objects.Formulas;

namespace Catrobat.IDECommon.Formula.Editor
{
    public class SelectedFormulaInformation
    {
        public Core.Objects.Formulas.Formula FormulaRoot { get; set; }

        public FormulaTree SelectedFormula { get; set; }

        public FormulaTree SelectedFormulaParent { get; set; }
    }
}
