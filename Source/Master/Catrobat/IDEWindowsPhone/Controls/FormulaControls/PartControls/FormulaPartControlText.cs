using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Catrobat.Core.Objects.Variables;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls.PartControls
{
    public class FormulaPartControlText : FormulaPartControl
    {
        protected override Grid CreateControls(int fontSize, bool isParentSelected, bool isSelected)
        {
            var textBlock = new TextBlock
            {
                Text = UiFormula.FormulaValue,
                FontSize = fontSize
            };

 
            var grid = new Grid {DataContext = this};
            grid.Children.Add(textBlock);

            return grid;
        }

        public override int GetCharacterWidth()
        {
            return UiFormula.FormulaValue.Length;
        }

        public override FormulaPartControl Copy()
        {
            return new FormulaPartControlText
            {
                Style = Style
            };
        }
    }
}
