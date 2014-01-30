using System.Diagnostics;

namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaNodes
{
    [DebuggerDisplay("{LeftChild} + {RightChild}")]
    public class FormulaNodeAdd : FormulaNodeInfixOperator
    {
    }
}
