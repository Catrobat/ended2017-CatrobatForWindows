using System.Windows.Controls;
using Catrobat.IDE.Core.Models.Formulas.FormulaTree;

namespace Catrobat.IDE.Phone.Controls.FormulaControls.PartControls
{
    public class FormulaPartControlVariable : FormulaPartControl
    {
        protected override Grid CreateControls(double fontSize, bool isParentSelected, bool isSelected, bool isError)
        {
            var grid = new Grid { DataContext = this };
            grid.Children.Add(new TextBlock
            {
                Text = Text,
                FontSize = fontSize
            });
            return grid;
        }

        private string Text
        {
            get
            {
                var node = Token as FormulaNodeVariable;
                return node == null || node.Variable.Name == null ? "" : node.Variable.Name;
            }
        }

        public override int GetCharacterWidth()
        {
            return Text.Length;
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
