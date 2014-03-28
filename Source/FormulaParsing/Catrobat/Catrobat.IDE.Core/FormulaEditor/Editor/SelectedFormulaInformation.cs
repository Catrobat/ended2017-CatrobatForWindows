using System;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;

namespace Catrobat.IDE.Core.FormulaEditor.Editor
{
    [Obsolete("Use FormulaEditor3 instead. ", true)]
    public class SelectedFormulaInformation
    {
        public Formula FormulaRoot { get; set; }

        public XmlFormulaTree SelectedFormula { get; set; }

        public XmlFormulaTree SelectedFormulaParent { get; set; }
    }
}
