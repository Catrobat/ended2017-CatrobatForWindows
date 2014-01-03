using System;
using System.Windows.Controls;

namespace Catrobat.IDE.Phone.Controls.FormulaControls.PartControls
{
    [Obsolete]
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
