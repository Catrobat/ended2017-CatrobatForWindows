using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Catrobat.IDEWindowsPhone.Controls.FormulaControls.Formulas;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls.PartControls
{
    public abstract class FormulaPartControl
    {
        public Grid CreateUiControls(int fontSize)
        {
            var control = CreateControls(fontSize);

            if (control != null)
            {
                control.Tap += ControlOnTap;
                UiFormula.UiControls.Add(control);
            }

            return control;
        }

        protected abstract Grid CreateControls(int fontSize);

        protected void ControlOnTap(object sender, GestureEventArgs gestureEventArgs)
        {

            if (UiFormula.IsEditEnabled)
            {
                UiFormula.ClearAllSelection();
                UiFormula.ClearAllBackground();
                UiFormula.IsSelected = !UiFormula.IsSelected;
                if (UiFormula.IsSelected)
                    UiFormula.SetBackground(true);
            }

            gestureEventArgs.Handled = true;
        }

        public abstract int GetCharacterWidth();

        public UiFormula UiFormula { get; set; }

        public abstract FormulaPartControl Copy();
    }
}
