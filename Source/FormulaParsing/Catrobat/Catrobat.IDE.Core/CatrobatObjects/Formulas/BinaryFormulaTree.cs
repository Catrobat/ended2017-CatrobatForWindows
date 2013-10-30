using System.Collections.Generic;

namespace Catrobat.IDE.Core.CatrobatObjects.Formulas
{
    abstract class BinaryFormulaTree : IFormulaTree
    {

        public IFormulaTree LeftChild { get; set; }

        public IFormulaTree RightChild { get; set; }

        #region implements IFormulaTree

        IEnumerable<IFormulaTree> IFormulaTree.Children
        {
            get
            {
                return new[] { LeftChild, RightChild };
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
            return Equals((BinaryFormulaTree)obj);
        }

        protected bool Equals(BinaryFormulaTree other)
        {
            // auto-implemented by ReSharper
            return Equals(LeftChild, other.LeftChild) && Equals(RightChild, other.RightChild);
        }

        public override int GetHashCode()
        {
            // auto-implemented by ReSharper
            unchecked
            {
                return ((LeftChild != null ? LeftChild.GetHashCode() : 0) * 397) ^ (RightChild != null ? RightChild.GetHashCode() : 0);
            }
        }

        #endregion
    }
}
