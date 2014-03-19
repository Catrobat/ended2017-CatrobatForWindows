namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree
{
    public abstract partial class FormulaNodePrefixOperator : UnaryFormulaTree, IFormulaOperator
    {
        public abstract int Order { get; }
    }

    #region Implementations

    public partial class FormulaNodeNot : FormulaNodePrefixOperator
    {
        public override int Order
        {
            get { return 1; }
        }
    }

    public partial class FormulaNodeNegativeSign : FormulaNodePrefixOperator
    {
        /// <remarks>Must be the same as <see cref="FormulaNodeSubtract.Order"/></remarks>
        public override int Order
        {
            get { return 1; }
        }
    }

    #endregion
}