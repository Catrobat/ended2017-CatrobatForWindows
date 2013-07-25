using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls.Formulas.Number
{
    public class UiFormulaNumber: UiFormula
    {
        public UiFormulaNumber()
        {

        }

        public override System.Windows.DataTemplate Template
        {
            get { return Application.Current.Resources["UiFormulaNumber"] as DataTemplate; }
        }
    }
}
