namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree
{
    /// <remarks>See /catroid/src/org/catrobat/catroid/formulaeditor/Functions.java</remarks>
    public abstract partial class FormulaNodeBinaryFunction : BinaryFormulaTree, IFormulaFunction
    {
    }

    #region Implementations

    public partial class FormulaNodeMin : FormulaNodeBinaryFunction
    {
    }

    public partial class FormulaNodeMax : FormulaNodeBinaryFunction
    {
    }

    public partial class FormulaNodeRandom : FormulaNodeBinaryFunction
    {
    }

    #endregion
}
