using System.Collections.Generic;

namespace Catrobat.IDE.Core.Models.Formulas.Tree
{
    public abstract partial class BinaryFormulaTree : FormulaTree
    {
        #region Properties

        private FormulaTree _firstChild;

        public FormulaTree FirstChild
        {
            get { return _firstChild; }
            set { _firstChild = value; }
        }

        private FormulaTree _secondChild;
        public FormulaTree SecondChild
        {
            get { return _secondChild; }
            set { _secondChild = value; }
        }

        #endregion

        #region Inherits FormulaTree

        public override IEnumerable<FormulaTree> Children
        {
            get
            {
                return new[] { FirstChild, SecondChild };
            }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(FormulaTree other)
        {
            return TestEquals((BinaryFormulaTree) other);
        }

        protected bool TestEquals(BinaryFormulaTree other)
        {
            return TestEquals(FirstChild, other.FirstChild) && TestEquals(SecondChild, other.SecondChild);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance()
        {
            var result = (BinaryFormulaTree) base.CloneInstance();
            CloneMember(ref result._firstChild);
            CloneMember(ref result._secondChild);
            return result;
        }

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (BinaryFormulaTree) base.CloneInstance();
            CloneMember(ref result._firstChild, context);
            CloneMember(ref result._secondChild, context);
            return result;
        }

        #endregion
    }
}
