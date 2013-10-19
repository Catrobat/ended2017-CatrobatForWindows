using System.Windows.Controls;

namespace Catrobat.IDE.Phone.Controls.FormulaControls.PartControls
{
    public class FormulaPartControlPlaceholderLeft : FormulaPartControl
    {
        protected override Grid CreateControls(int fontSize, bool isParentSelected, bool isSelected, bool isError)
        {
            return null;
        }

        public override int GetCharacterWidth()
        {
            return 0;
        }

        public override FormulaPartControl Copy()
        {
            return new FormulaPartControlPlaceholderLeft();
        }
    }
}
