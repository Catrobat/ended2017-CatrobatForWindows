using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls.PartControls
{
    public class FormulaPartControlPlaceholderRight : FormulaPartControl
    {
        protected override Grid CreateControls(int fontSize)
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
