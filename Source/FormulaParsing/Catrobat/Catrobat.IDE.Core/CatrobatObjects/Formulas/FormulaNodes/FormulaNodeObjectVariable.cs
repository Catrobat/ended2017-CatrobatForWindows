using Catrobat.IDE.Core.CatrobatObjects.Variables;

namespace Catrobat.IDE.Core.CatrobatObjects.Formulas
{
    abstract class FormulaNodeObjectVariable : FormulaNodeVariable
    {
        public ObjectVariableEntry Variable { get; set; }
    }
}
