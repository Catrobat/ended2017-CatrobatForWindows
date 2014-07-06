using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Catrobat.IDE.Core.Models.Formulas.Tokens;

namespace Catrobat.IDE.Core.Models.Formulas.Tree
{
    /// <remarks>See /catroid/src/org/catrobat/catroid/formulaeditor/Functions.java</remarks>
    public abstract partial class ConstantFormulaTree : FormulaTree
    {
        #region Inherits FormulaTree

        public override IEnumerable<FormulaTree> Children
        {
            get
            {
                return Enumerable.Empty<FormulaTree>();
            }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(FormulaTree other)
        {
            return TestEquals((ConstantFormulaTree) other);
        }

        protected virtual bool TestEquals(ConstantFormulaTree other)
        {
            return true;
        }

        #endregion
    }

    [DebuggerDisplay("Value = {Value}")]
    public abstract partial class ConstantFormulaTree<TValue> : ConstantFormulaTree
    {
        public TValue Value { get; set; }

        #region Implements ITestEquatable

        protected override bool TestEquals(ConstantFormulaTree other)
        {
            return base.TestEquals(other) && TestEquals((ConstantFormulaTree<TValue>) other);
        }

        protected bool TestEquals(ConstantFormulaTree<TValue> other)
        {
            return base.TestEquals(other) && EqualityComparer<TValue>.Default.Equals(Value, other.Value);
        }

        #endregion
    }

    #region Implementations

    [DebuggerDisplay("{Value}")]
    public partial class FormulaNodeNumber : ConstantFormulaTree<double>, IFormulaNumber
    {
    }

    [DebuggerDisplay("pi")]
    public partial class FormulaNodePi : ConstantFormulaTree
    {
    }

    public partial class FormulaNodeTrue : ConstantFormulaTree
    {
    }

    public partial class FormulaNodeFalse : ConstantFormulaTree
    {
    }

    #endregion
}
