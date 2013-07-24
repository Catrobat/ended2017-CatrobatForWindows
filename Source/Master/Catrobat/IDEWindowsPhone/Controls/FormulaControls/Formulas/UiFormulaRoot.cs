using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Catrobat.Core.Objects.Formulas;
using IDEWindowsPhone;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls.Formulas
{
    public class UiFormulaRoot
    {
        public UiFormula UiFormula { get; set; }

        public DataTemplate Template
        {
            get
            {
                return Application.Current.Resources["UiFormulaRootTemplate"] as DataTemplate;
            }
        }
    }
}
