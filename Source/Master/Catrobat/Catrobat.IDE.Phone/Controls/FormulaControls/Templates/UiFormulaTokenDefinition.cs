using System;
using System.ComponentModel;
using Catrobat.IDE.Phone.Controls.FormulaControls.PartControls;
using Catrobat.IDE.Phone.Converters;

namespace Catrobat.IDE.Phone.Controls.FormulaControls.Templates
{
    public class UiFormulaTokenDefinition
    {
        [TypeConverter(typeof(NamespaceTypeConverter))]  
        public Type TokenType { get; set; }

        public FormulaPartControl Template { get; set; }
    }
}
