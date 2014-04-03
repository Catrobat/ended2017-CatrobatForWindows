using System.Diagnostics;

namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree
{
    /// <remarks>See /catroid/src/org/catrobat/catroid/formulaeditor/Functions.java</remarks>
    public abstract partial class FormulaNodeUnaryFunction : UnaryFormulaTree, IFormulaFunction
    {
    }

    #region Implementations

    [DebuggerDisplay("exp()")]
    public partial class FormulaNodeExp : FormulaNodeUnaryFunction
    {
    }

    [DebuggerDisplay("log()")]
    public partial class FormulaNodeLog : FormulaNodeUnaryFunction
    {
    }

    [DebuggerDisplay("ln()")]
    public partial class FormulaNodeLn : FormulaNodeUnaryFunction
    {
    }

    [DebuggerDisplay("sin()")]
    public partial class FormulaNodeSin : FormulaNodeUnaryFunction
    {
    }

    [DebuggerDisplay("cos()")]
    public partial class FormulaNodeCos : FormulaNodeUnaryFunction
    {
    }

    [DebuggerDisplay("tan()")]
    public partial class FormulaNodeTan : FormulaNodeUnaryFunction
    {
    }

    [DebuggerDisplay("arcsin()")]
    public partial class FormulaNodeArcsin : FormulaNodeUnaryFunction
    {
    }

    [DebuggerDisplay("arccos()")]
    public partial class FormulaNodeArccos : FormulaNodeUnaryFunction
    {
    }

    [DebuggerDisplay("arctan()")]
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
