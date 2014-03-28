using System.Collections.Generic;
using System.Diagnostics;
using Catrobat.IDE.Core.CatrobatObjects.Variables;

namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public abstract partial class FormulaNodeVariable : ConstantFormulaTree
    {
        public UserVariable Variable { get; set; }

        protected string DebuggerDisplay
        {
            get { return Variable != null ? "Name = " + Variable.Name : "Variable = " + Variable; }
        }

        #region overrides Equals

        public override bool Equals(object obj)
        {
            // auto-implemented by ReSharper
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FormulaNodeVariable)obj);
        }

        protected bool Equals(FormulaNodeVariable other)
        {
            // auto-implemented by ReSharper
            return base.Equals(other) && EqualityComparer<UserVariable>.Default.Equals(Variable, other.Variable);
        }

        public override int GetHashCode()
        {
            // auto-implemented by ReSharper
            unchecked
            {
                return (base.GetHashCode() * 397) ^ EqualityComparer<UserVariable>.Default.GetHashCode(Variable);
            }
        }

        #endregion
    }

    #region Implementations

    public partial class FormulaNodeLocalVariable : FormulaNodeVariable
    {
    }

    public partial class FormulaNodeGlobalVariable : FormulaNodeVariable
    {
    }

    #endregion
}
