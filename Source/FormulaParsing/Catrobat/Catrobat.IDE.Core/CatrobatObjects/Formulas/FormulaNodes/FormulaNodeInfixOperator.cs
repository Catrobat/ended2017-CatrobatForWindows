using System;

namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaNodes
{
    abstract class FormulaNodeInfixOperator : BinaryFormulaTree
    {
        #region rename First- and SecondChild to Left- and RightChild

        public IFormulaTree LeftChild
        {
            get
            {
                return base.FirstChild;
            }
            set
            {
                base.FirstChild = value;
            }
        }

        [Obsolete("Use LeftChild instead. ", false)]
        public new IFormulaTree FirstChild
        {
            get
            {
                return base.FirstChild;
            }
            set
            {
                base.FirstChild = value;
            }
        }

        public IFormulaTree RightChild
        {
            get
            {
                return base.SecondChild;
            }
            set
            {
                base.SecondChild = value;
            }
        }

        [Obsolete("Use RightChild instead. ", false)]
        public new IFormulaTree SecondChild
        {
            get
            {
                return base.SecondChild;
            }
            set
            {
                base.SecondChild = value;
            }
        }

        #endregion

    }
}
