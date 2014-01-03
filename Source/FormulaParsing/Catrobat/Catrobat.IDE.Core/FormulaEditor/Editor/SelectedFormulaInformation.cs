using System;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;

namespace Catrobat.IDE.Core.FormulaEditor.Editor
{
    [Obsolete]
    public class SelectedFormulaInformation
    {
        public Formula FormulaRoot { get; set; }

        public XmlFormulaTree SelectedFormula { get; set; }

        public XmlFormulaTree SelectedFormulaParent { get; set; }
    }
}
