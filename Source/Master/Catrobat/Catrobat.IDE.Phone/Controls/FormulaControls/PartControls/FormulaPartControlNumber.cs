using System.Windows.Controls;
using Catrobat.IDE.Core.Models.Formulas.FormulaToken;
using Catrobat.IDE.Core.Models.Formulas.FormulaTree;

namespace Catrobat.IDE.Phone.Controls.FormulaControls.PartControls
{
    public class FormulaPartControlNumber : FormulaPartControl
    {
        public string GetText()
        {
            var node = Token as FormulaNodeNumber;
            if (node == null) return string.Empty;

            return node.Value.ToString();
        }

        #region FormulaPartControl

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

        public override int GetCharacterWidth()
        {
            return GetText().Length;
        }

        public override FormulaPartControl Copy()
        {
            return new FormulaPartControlNumber
            {
                Style = Style
            };
        }

        public override FormulaPartControl CreateUiTokenTemplate(IFormulaToken token)
        {
            return new FormulaPartControlNumber
            {
                Style = Style, 
                Token = token
            };
        }

        #endregion
    }
}
