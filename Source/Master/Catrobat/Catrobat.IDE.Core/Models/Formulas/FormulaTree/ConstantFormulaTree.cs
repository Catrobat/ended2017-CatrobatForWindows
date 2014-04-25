using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Catrobat.IDE.Core.Models.Formulas.FormulaTree
{
    /// <remarks>See /catroid/src/org/catrobat/catroid/formulaeditor/Functions.java</remarks>
    public abstract partial class ConstantFormulaTree : BaseFormulaTree
    {
        #region Implements IFormulaTree

        public override IEnumerable<IFormulaTree> Children
        {
            get
            {
                return Enumerable.Empty<IFormulaTree>();
            }
        }

        #endregion

        #region overrides Equals

        public override bool Equals(object obj)
        {
            // auto-implemented by ReSharper
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ConstantFormulaTree) obj);
        }

        protected bool Equals(ConstantFormulaTree other)
        {
            return true;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        #endregion
    }

    [DebuggerDisplay("Value = {Value}")]
    public abstract partial class ConstantFormulaTree<TValue> : ConstantFormulaTree
    {
        public TValue Value { get; set; }

        #region overrides Equals

        public override bool Equals(object obj)
        {
            // auto-implemented by ReSharper
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ConstantFormulaTree<TValue>) obj);
        }

        protected bool Equals(ConstantFormulaTree<TValue> other)
        {
            // auto-implemented by ReSharper
            return base.Equals(other) && EqualityComparer<TValue>.Default.Equals(Value, other.Value);
        }

        public override int GetHashCode()
        {
            // auto-implemented by ReSharper
            unchecked
            {
                return (base.GetHashCode()*397) ^ EqualityComparer<TValue>.Default.GetHashCode(Value);
            }
        }

        #endregion
    }

    #region Implementations

    [DebuggerDisplay("{Value}")]
    public partial class FormulaNodeNumber : ConstantFormulaTree<double>
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
