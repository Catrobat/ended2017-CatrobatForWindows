using System.Collections.Generic;
using System.Linq;

namespace Catrobat.IDE.Core.Models.Formulas.Tree
{
    public abstract partial class UnaryFormulaTree : FormulaTree
    {
        #region Properties

        private FormulaTree _child;
        public FormulaTree Child

        {
            get { return _child; }
            set { _child = value; }
        }

        #endregion

        #region Inherits FormulaTree

        public override IEnumerable<FormulaTree> Children
        {
            get
            {
                return Enumerable.Repeat(Child, 1);
            }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(FormulaTree other)
        {
            return TestEquals((UnaryFormulaTree) other);
        }

        protected bool TestEquals(UnaryFormulaTree other)
        {
            return TestEquals(Child, other.Child);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance()
        {
            var result = (UnaryFormulaTree) base.CloneInstance();
            CloneMember(ref result._child);
            return result;
        }

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (UnaryFormulaTree) base.CloneInstance(context);
            CloneMember(ref result._child, context);
            return result;
        }

        #endregion
    }
}
