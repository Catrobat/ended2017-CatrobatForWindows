using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls.Formulas.Math
{
    public class UiFormulaMathRandom : UiFormula
    {
        public UiFormulaMathRandom()
        {

        }

        public override System.Windows.DataTemplate Template
        {
            get { return Application.Current.Resources["UiFormulaTemplateMathRandom"] as DataTemplate; }
        }
    }
}
