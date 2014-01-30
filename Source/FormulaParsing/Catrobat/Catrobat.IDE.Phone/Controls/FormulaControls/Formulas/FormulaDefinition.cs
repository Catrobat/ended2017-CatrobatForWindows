using System;
using Catrobat.IDE.Phone.Controls.FormulaControls.PartControls;

namespace Catrobat.IDE.Phone.Controls.FormulaControls.Formulas
{
    [Obsolete]
    public class FormulaDefinition
    {
        public string Type { get; set; }

        public string Value { get; set; }

        public StringList Values { get; set; }

        public FormulaPartControlList Template { get; set; }
    }
}
