using System.Diagnostics;

namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree
{
    /// <remarks>See /catroid/src/org/catrobat/catroid/formulaeditor/Functions.java</remarks>
    public abstract partial class FormulaNodeUnaryFunction : UnaryFormulaTree, IFormulaFunction
    {
    }

    #region Implementations

    [DebuggerDisplay("exp({Child})")]
    public partial class FormulaNodeExp : FormulaNodeUnaryFunction
    {
    }

    [DebuggerDisplay("log({Child})")]
    public partial class FormulaNodeLog : FormulaNodeUnaryFunction
    {
    }

    [DebuggerDisplay("ln({Child})")]
    public partial class FormulaNodeLn : FormulaNodeUnaryFunction
    {
    }

    [DebuggerDisplay("sin({Child})")]
    public partial class FormulaNodeSin : FormulaNodeUnaryFunction
    {
    }

    [DebuggerDisplay("cos({Child})")]
    public partial class FormulaNodeCos : FormulaNodeUnaryFunction
    {
    }

    [DebuggerDisplay("tan({Child})")]
    public partial class FormulaNodeTan : FormulaNodeUnaryFunction
    {
    }

    [DebuggerDisplay("arcsin({Child})")]
    public partial class FormulaNodeArcsin : FormulaNodeUnaryFunction
    {
    }

    [DebuggerDisplay("arccos({Child})")]
    public partial class FormulaNodeArccos : FormulaNodeUnaryFunction
    {
    }

    [DebuggerDisplay("arctan({Child})")]
    public partial class FormulaNodeArctan : FormulaNodeUnaryFunction
    {
    }

    public partial class FormulaNodeAbs : FormulaNodeUnaryFunction
    {
    }

    public partial class FormulaNodeSqrt : FormulaNodeUnaryFunction
    {
    }

    public partial class FormulaNodeRound : FormulaNodeUnaryFunction
    {
    }


    #endregion
}
