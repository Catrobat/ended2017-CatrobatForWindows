using System;
using System.Diagnostics;

namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree
{
    public abstract partial class FormulaNodeInfixOperator : BinaryFormulaTree, IFormulaOperator
    {
        #region rename First- and SecondChild to Left- and RightChild

        public IFormulaTree LeftChild
        {
            get { return base.FirstChild; }
            set { base.FirstChild = value; }
        }

        [Obsolete("Use LeftChild instead. ", false)]
        public new IFormulaTree FirstChild
        {
            get { return base.FirstChild; }
            set { base.FirstChild = value; }
        }

        public IFormulaTree RightChild
        {
            get { return base.SecondChild; }
            set { base.SecondChild = value; }
        }

        [Obsolete("Use RightChild instead. ", false)]
        public new IFormulaTree SecondChild
        {
            get { return base.SecondChild; }
            set { base.SecondChild = value; }
        }

        #endregion

        public abstract int Order { get; }
    }

    #region Implementations

    [DebuggerDisplay("{LeftChild} + {RightChild}")]
    public partial class FormulaNodeAdd : FormulaNodeInfixOperator
    {
        public override int Order
        {
            get { return 1; }
        }
    }

    [DebuggerDisplay("{LeftChild} - {RightChild}")]
    public partial class FormulaNodeSubtract : FormulaNodeInfixOperator
    {
        /// <remarks>Must be the same as <see cref="FormulaNodeNegativeSign.Order"/></remarks>
        public override int Order
        {
            get { return 1; }
        }
    }

    [DebuggerDisplay("{LeftChild} * {RightChild}")]
    public partial class FormulaNodeMultiply : FormulaNodeInfixOperator
    {
        public override int Order
        {
            get { return 2; }
        }
    }

    [DebuggerDisplay("{LeftChild} / {RightChild}")]
    public partial class FormulaNodeDivide : FormulaNodeInfixOperator
    {
        public override int Order
        {
            get { return 2; }
        }
    }

    [DebuggerDisplay("{LeftChild} = {RightChild}")]
    public partial class FormulaNodeEquals : FormulaNodeInfixOperator
    {
        public override int Order
        {
            get { return 0; }
        }
    }

    [DebuggerDisplay("{LeftChild} != {RightChild}")]
    public partial class FormulaNodeNotEquals : FormulaNodeInfixOperator
    {
        public override int Order
        {
            get { return 0; }
        }
    }

    [DebuggerDisplay("{LeftChild} > {RightChild}")]
    public partial class FormulaNodeGreater : FormulaNodeInfixOperator
    {
        public override int Order
        {
            get { return 0; }
        }
    }

    [DebuggerDisplay("{LeftChild} >= {RightChild}")]
    public partial class FormulaNodeGreaterEqual : FormulaNodeInfixOperator
    {
        public override int Order
        {
            get { return 0; }
        }
    }

    [DebuggerDisplay("{LeftChild} < {RightChild}")]
    public partial class FormulaNodeLess : FormulaNodeInfixOperator
    {
        public override int Order
        {
            get { return 0; }
        }
    }

    [DebuggerDisplay("{LeftChild} <= {RightChild}")]
    public partial class FormulaNodeLessEqual : FormulaNodeInfixOperator
    {
        public override int Order
        {
            get { return 0; }
        }
    }

    [DebuggerDisplay("{LeftChild} and {RightChild}")]
    public partial class FormulaNodeAnd : FormulaNodeInfixOperator
    {
        public override int Order
        {
            get { return 0; }
        }
    }

    [DebuggerDisplay("{LeftChild} or {RightChild}")]
    public partial class FormulaNodeOr : FormulaNodeInfixOperator
    {
        public override int Order
        {
            get { return 0; }
        }
    }

    [DebuggerDisplay("{LeftChild} mod {RightChild}")]
    public partial class FormulaNodeMod : FormulaNodeInfixOperator
    {
        public override int Order
        {
            get { return 0; }
        }
    }

    #endregion

}
