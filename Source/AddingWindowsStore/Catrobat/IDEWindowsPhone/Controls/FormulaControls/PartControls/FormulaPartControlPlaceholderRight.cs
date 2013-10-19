using System.Windows.Controls;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls.PartControls
{
    public class FormulaPartControlPlaceholderRight : FormulaPartControl
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
            return new FormulaPartControlPlaceholderRight();
        }
    }
}
