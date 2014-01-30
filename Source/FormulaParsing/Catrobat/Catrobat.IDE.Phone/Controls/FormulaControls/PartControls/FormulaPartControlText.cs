using System;
using System.Windows.Controls;

namespace Catrobat.IDE.Phone.Controls.FormulaControls.PartControls
{
    [Obsolete]
    public class FormulaPartControlText : FormulaPartControl
    {
        protected override Grid CreateControls(double fontSize, bool isParentSelected, bool isSelected, bool isError)
        {
            var grid = new Grid {DataContext = this};
            grid.Children.Add(new TextBlock
            {
                Text = GetText(),
                FontSize = fontSize
            });

            return grid;
        }

        public string GetText()
        {
            return string.IsNullOrEmpty(UiFormula.FormulaValue) ? " " : UiFormula.FormulaValue;
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
