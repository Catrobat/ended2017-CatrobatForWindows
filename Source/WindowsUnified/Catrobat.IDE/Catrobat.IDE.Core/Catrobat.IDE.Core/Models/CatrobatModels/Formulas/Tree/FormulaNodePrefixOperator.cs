using System.Diagnostics;

namespace Catrobat.IDE.Core.Models.Formulas.Tree
{
    /// <remarks>See /catroid/src/org/catrobat/catroid/formulaeditor/Operators.java</remarks>
    public abstract partial class FormulaNodePrefixOperator : UnaryFormulaTree, IFormulaOperator
    {
        public abstract int Order { get; }
    }

    #region Implementations

    [DebuggerDisplay("not {Child}")]
    public partial class FormulaNodeNot : FormulaNodePrefixOperator
    {
        public override int Order
        {
            get { return 4; }
        }
    }

    [DebuggerDisplay("-{Child}")]
    public partial class FormulaNodeNegativeSign : FormulaNodePrefixOperator
    {
        /// <remarks>Must be the same as <see cref="FormulaNodeSubtract.Order"/></remarks>
        public override int Order
        {
            get { return 5; }
        }
    }

    #endregion
}