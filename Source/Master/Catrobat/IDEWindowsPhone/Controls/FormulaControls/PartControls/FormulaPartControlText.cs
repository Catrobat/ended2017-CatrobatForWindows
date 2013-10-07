using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Catrobat.Core.CatrobatObjects.Variables;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls.PartControls
{
    public class FormulaPartControlText : FormulaPartControl
    {
        public string GetText()
        {
            if (string.IsNullOrEmpty(UiFormula.FormulaValue))
            {
                return " ";
            }
            else
            {
                return UiFormula.FormulaValue;
            }
        }

        protected override Grid CreateControls(int fontSize, bool isParentSelected, bool isSelected, bool isError)
        {
            var textBlock = new TextBlock
            {
                Text = GetText(),
                FontSize = fontSize
            };

            var grid = new Grid {DataContext = this};
            grid.Children.Add(textBlock);

            return grid;
        }

        public override int GetCharacterWidth()
        {
            return GetText().Length;
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
