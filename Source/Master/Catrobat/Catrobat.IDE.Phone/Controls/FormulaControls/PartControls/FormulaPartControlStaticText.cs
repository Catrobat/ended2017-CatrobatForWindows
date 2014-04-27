using System.Windows.Controls;

namespace Catrobat.IDE.Phone.Controls.FormulaControls.PartControls
{
    public class FormulaPartControlStaticText : FormulaPartControl
    {
        public string Text { get; set; }

        protected override Grid CreateControls(double fontSize, bool isParentSelected, bool isSelected, bool isError)
        {
            var textBlock = new TextBlock
            {
                Text = Text,
                FontSize = fontSize
            };

            var grid = new Grid { DataContext = this };
            grid.Children.Add(textBlock);

            return grid;
        }

        public override int GetCharacterWidth()
        {
            return Text.Length;
        }

        public override FormulaPartControl Copy()
        {
            return new FormulaPartControlStaticText
            {
                Style = Style,
                Text = this.Text
            };
        }
    }
}
