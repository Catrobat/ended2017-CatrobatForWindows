using System.Collections.Generic;
using System.Linq;

namespace Catrobat.IDE.Core.CatrobatObjects.Formulas
{
    public abstract class UnaryFormulaTree : IFormulaTree
    {

        public IFormulaTree Child { get; set; }

        #region implements IFormulaTree

        IEnumerable<IFormulaTree> IFormulaTree.Children
        {
            get
            {
                return Enumerable.Repeat(Child, 1);
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
            return Equals((UnaryFormulaTree) obj);
        }

        protected bool Equals(UnaryFormulaTree other)
        {
            // auto-implemented by ReSharper
            return Equals(Child, other.Child);
        }

        public override int GetHashCode()
        {
            // auto-implemented by ReSharper
            return (Child != null ? Child.GetHashCode() : 0);
        }

        #endregion
    }
}
