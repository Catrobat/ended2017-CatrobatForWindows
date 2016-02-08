using System.Diagnostics;

namespace Catrobat.IDE.Core.Models.Formulas.Tree
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public abstract partial class FormulaNodeVariable : ConstantFormulaTree
    {

        #region Properties

        private Variable _variable;
        public Variable Variable
        {
            get { return _variable; }
            set { _variable = value; }
        }

        #endregion

        protected string DebuggerDisplay
        {
            get { return Variable != null ? "Name = " + Variable.Name : "Variable = " + Variable; }
        }

        #region Implements ITestEquatable

        protected override bool TestEquals(ConstantFormulaTree other)
        {
            return base.TestEquals(other) && TestEquals((FormulaNodeVariable) other);
        }

        protected bool TestEquals(FormulaNodeVariable other)
        {
            return TestEquals(Variable, other.Variable);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (FormulaNodeLocalVariable) base.CloneInstance(context);
            CloneMember(ref result._variable, context);
            return result;
        }

        #endregion
    }

    #region Implementations

    public partial class FormulaNodeLocalVariable : FormulaNodeVariable
    {
        public new LocalVariable Variable
        {
            get { return (LocalVariable) base.Variable; }
            set { base.Variable = value; }
        }
    }

    public partial class FormulaNodeGlobalVariable : FormulaNodeVariable
    {
        public new GlobalVariable Variable
        {
            get { return (GlobalVariable) base.Variable; }
            set { base.Variable = value; }
        }
    }

    #endregion
}
