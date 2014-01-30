using System.Diagnostics;

namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaNodes
{
    [DebuggerDisplay("{Value}")]
    public class FormulaNodeNumber : ConstantFormulaTree<double>
    {
    }
}
