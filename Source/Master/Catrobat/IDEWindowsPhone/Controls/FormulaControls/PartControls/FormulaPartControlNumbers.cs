using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls.PartControls
{
    public class FormulaPartControlNumbers : FormulaPartControl
    {
        protected override Grid CreateControls(int fontSize)
        {
            var control = new TextBlock
            {
                Text = UiFormula.FormulaValue,
                FontSize = fontSize
            };

            var grid = new Grid();
            grid.Children.Add(control);

            return grid;
        }

        public override int GetCharacterWidth()
        {
            return UiFormula.FormulaValue.Length;
        }

        public override FormulaPartControl Copy()
        {
            return new FormulaPartControlNumbers();
        }
    }
}
