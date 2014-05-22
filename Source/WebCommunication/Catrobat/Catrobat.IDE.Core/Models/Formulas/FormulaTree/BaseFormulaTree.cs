using System.Collections.Generic;

namespace Catrobat.IDE.Core.Models.Formulas.FormulaTree
{
    public abstract partial class BaseFormulaTree : IFormulaTree
    {
        #region Implements IFormulaTree

        public abstract IEnumerable<IFormulaTree> Children { get; }

        #endregion

        #region Implements ICloneable

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        #endregion

        #region Overrides Equals

        public override abstract bool Equals(object other);

        public override abstract int GetHashCode();

        #endregion

    }
}
