using System;
using System.Windows.Controls;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree;

namespace Catrobat.IDE.Phone.Controls.FormulaControls.PartControls
{
    public class FormulaPartControlVariable : FormulaPartControl
    {
        protected override Grid CreateControls(double fontSize, bool isParentSelected, bool isSelected, bool isError)
        {
            var grid = new Grid { DataContext = this };
            grid.Children.Add(new TextBlock
            {
                Text = GetText() ?? "?",
                FontSize = fontSize
            });
            return grid;
        }

        private string GetText()
        {
            var node = Token as FormulaNodeVariable;
            return node == null ? null : node.Variable.Name;
        }

        public override int GetCharacterWidth()
        {
            var text = GetText();
            return text == null ? 1 : text.Length;
        }

        public override FormulaPartControl Copy()
        {
            return new FormulaPartControlVariable
            {
                Token = Token, 
                Style = Style
            };
        }
    }
}
