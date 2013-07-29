using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Catrobat.IDEWindowsPhone.Controls.FormulaControls.Formulas;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls.PartControls
{
    public abstract class FormulaPartControl
    {
        public FormulaPartStyleCollection Style { get; set; }

        public Grid CreateUiControls(int fontSize, bool isSelected, bool isParentSelected, bool isError)
        {
            var control = CreateControls(fontSize, isSelected, isParentSelected, isError);

            if (control != null)
            {
                control.Tap += ControlOnTap;
                UiFormula.UiControls.Add(control);
            }

            return control;
        }

        protected abstract Grid CreateControls(int fontSize, bool isParentSelected, bool isSelected, bool isError);

        protected void ControlOnTap(object sender, GestureEventArgs gestureEventArgs)
        {
            if (UiFormula.IsEditEnabled)
            {
                bool wasSelected = UiFormula.IsSelected;

                UiFormula.ClearAllSelection();
                UiFormula.ClearAllBackground();
                UiFormula.IsSelected = !wasSelected;
                
                if(UiFormula.IsSelected)
                    UiFormula.Viewer.SelectedFormulaChanged(UiFormula.TreeItem);
                else
                    UiFormula.Viewer.SelectedFormulaChanged(null);

                if (UiFormula.IsSelected)
                    UiFormula.SetStyle(true, false);
            }

            gestureEventArgs.Handled = true;
        }

        public abstract int GetCharacterWidth();

        public UiFormula UiFormula { get; set; }

        public abstract FormulaPartControl Copy();
    }
}
