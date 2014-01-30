using Catrobat.IDE.Core.CatrobatObjects.Variables;

namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaNodes
{
    public abstract class FormulaNodeObjectVariable : FormulaNodeVariable
    {
        public ObjectVariableEntry Variable { get; set; }
    }
}
