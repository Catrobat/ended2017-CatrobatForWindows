using Catrobat.IDE.Core.Models.Formulas.FormulaToken;
using System.Windows.Controls;

namespace Catrobat.IDE.Phone.Controls.FormulaControls.PartControls
{
    public class FormulaPartControlParenthesis : FormulaPartControl
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
                var node = Token as FormulaTokenParenthesis;
                if (node == null) return "";
                return node.IsOpening ? "(" : ")";
            }
        }

        public override int GetCharacterWidth()
        {
            return Text .Length;
        }

        public override FormulaPartControl Copy()
        {
            return new FormulaPartControlParenthesis
            {
                Token = Token, 
                Style = Style
            };
        }

        public override FormulaPartControl CreateUiTokenTemplate(IFormulaToken token)
        {
            return new FormulaPartControlParenthesis
            {
                Style = Style,
                Token = token
            };
        }
    }
}
