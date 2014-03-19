using System.Collections.Generic;

namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree
{
    public abstract partial class BaseFormulaTree : IFormulaTree
    {
        #region implements IFormulaTree

        public abstract IEnumerable<IFormulaTree> Children { get; }

        #endregion

        #region overrides Equals

        public override abstract bool Equals(object other);

        public override abstract int GetHashCode();

        #endregion

    }
}
