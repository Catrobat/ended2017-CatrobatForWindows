using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.Core.Objects.Formulas;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls.Formulas
{
    public class SelectedFormulaInformation
    {
        public UiFormula SelectedUiFormula { get; set; }

        public FormulaTree SelectedFormula { get; set; }

        public FormulaTree SelectedFormulaParent { get; set; }
    }
}
