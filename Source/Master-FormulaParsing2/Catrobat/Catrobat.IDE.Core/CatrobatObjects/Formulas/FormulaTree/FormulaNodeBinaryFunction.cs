using System.Diagnostics;

namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree
{
    /// <remarks>See /catroid/src/org/catrobat/catroid/formulaeditor/Functions.java</remarks>
    public abstract partial class FormulaNodeBinaryFunction : BinaryFormulaTree, IFormulaFunction
    {
    }

    #region Implementations

    [DebuggerDisplay("Min({FirstChild}, {SecondChild})")]
    public partial class FormulaNodeMin : FormulaNodeBinaryFunction
    {
    }

    [DebuggerDisplay("Max({FirstChild}, {SecondChild})")]
    public partial class FormulaNodeMax : FormulaNodeBinaryFunction
    {
    }

    public partial class FormulaNodeRandom : FormulaNodeBinaryFunction
    {
    }

    #endregion
}
