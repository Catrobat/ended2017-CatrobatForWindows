using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDEWindowsPhone.Controls.FormulaControls.PartControls;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls.Formulas
{
    public class FormulaDefinition
    {
        public string Type { get; set; }

        public string Value { get; set; }

        public StringList Values { get; set; }

        public FormulaPartControlList Template { get; set; }
    }
}
