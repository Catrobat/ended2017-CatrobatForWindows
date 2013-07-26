using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Catrobat.IDEWindowsPhone.Controls.FormulaControls.PartControls;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls.Formulas.Number
{
    public class UiFormulaNumber: UiFormula
    {
        public UiFormulaNumber()
        {

        }

        public override FormulaPartControlList Template
        {
            get { return Application.Current.Resources["UiFormulaNumber"] as FormulaPartControlList; }
        }
    }
}
