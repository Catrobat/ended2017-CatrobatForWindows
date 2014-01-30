using System.Collections.Generic;

namespace Catrobat.IDE.Core.CatrobatObjects.Formulas
{
    public abstract class BinaryFormulaTree : IFormulaTree
    {

        public IFormulaTree FirstChild { get; set; }

        public IFormulaTree SecondChild { get; set; }

        #region implements IFormulaTree

        IEnumerable<IFormulaTree> IFormulaTree.Children
        {
            get
            {
                return new[] { FirstChild, SecondChild };
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
