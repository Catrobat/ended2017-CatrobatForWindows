using System.Collections.Generic;

namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree
{
    public abstract partial class BinaryFormulaTree : BaseFormulaTree
    {
        public IFormulaTree FirstChild { get; set; }
        public IFormulaTree SecondChild { get; set; }

        #region Implements IFormulaTree

        public override IEnumerable<IFormulaTree> Children
        {
            get
            {
                return new[] { FirstChild, SecondChild };
            }
        }

        #endregion

        #region Implements ICloneable

        public override object Clone()
        {
            var clone = (BinaryFormulaTree) base.Clone();
            clone.FirstChild = (IFormulaTree) FirstChild.Clone();
            clone.SecondChild = (IFormulaTree) SecondChild.Clone();
            return clone;
        }

        #endregion

        #region Overrides Equals

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
            return Equals(FirstChild, other.FirstChild) && Equals(SecondChild, other.SecondChild);
        }

        public override int GetHashCode()
        {
            // auto-implemented by ReSharper
            unchecked
            {
                return ((FirstChild != null ? FirstChild.GetHashCode() : 0) * 397) ^ (SecondChild != null ? SecondChild.GetHashCode() : 0);
            }
        }

        #endregion
    }
}
