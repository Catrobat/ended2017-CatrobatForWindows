using System.Collections.Generic;
using System.Linq;

namespace Catrobat.IDE.Core.CatrobatObjects.Formulas
{
    abstract class ConstantFormulaTree : IFormulaTree
    {

        #region implements IFormulaTree

        IEnumerable<IFormulaTree> IFormulaTree.Children
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

    class ConstantFormulaTree<TValue> : ConstantFormulaTree
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

}
