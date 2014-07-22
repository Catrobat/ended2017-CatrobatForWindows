using Catrobat.IDE.Core.Models.Formulas.Tree;

namespace Catrobat.IDE.Core.Models.Bricks
{
    public abstract partial class VariableBrick : Brick
    {
        #region Properties

        private Variable _variable;
        public Variable Variable
        {
            get { return _variable; }
            set { Set(ref _variable, value); }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(Brick other)
        {
            return base.TestEquals(other) && TestEquals((VariableBrick) other);
        }

        protected virtual bool TestEquals(VariableBrick other)
        {
            return TestEquals(_variable, other._variable);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (VariableBrick) base.CloneInstance(context);
            CloneMember(ref result._variable, context);
            return result;
        }

        #endregion
    }

    #region Implementations

    public partial class SetVariableBrick : VariableBrick
    {
        #region Properties

        private FormulaTree _value;
        public FormulaTree Value
        {
            get { return _value; }
            set { Set(ref _value, value); }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(VariableBrick other)
        {
            return base.TestEquals(other) && TestEquals((SetVariableBrick) other);
        }

        protected bool TestEquals(SetVariableBrick other)
        {
            return TestEquals(_value, other._value);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance()
        {
            var result = (SetVariableBrick) base.CloneInstance();
            CloneMember(ref result._value);
            return result;
        }

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (SetVariableBrick) base.CloneInstance(context);
            CloneMember(ref result._value, context);
            return result;
        }

        #endregion
    }

    public partial class ChangeVariableBrick : VariableBrick
    {
        #region Properties

        private FormulaTree _relativeValue;
        public FormulaTree RelativeValue
        {
            get { return _relativeValue; }
            set { Set(ref _relativeValue, value); }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(VariableBrick other)
        {
            return base.TestEquals(other) && TestEquals((ChangeVariableBrick) other);
        }

        protected bool TestEquals(ChangeVariableBrick other)
        {
            return TestEquals(_relativeValue, other._relativeValue);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance()
        {
            var result = (ChangeVariableBrick) base.CloneInstance();
            CloneMember(ref result._relativeValue);
            return result;
        }

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (ChangeVariableBrick) base.CloneInstance(context);
            CloneMember(ref result._relativeValue, context);
            return result;
        }

        #endregion
    }

    #endregion
}
