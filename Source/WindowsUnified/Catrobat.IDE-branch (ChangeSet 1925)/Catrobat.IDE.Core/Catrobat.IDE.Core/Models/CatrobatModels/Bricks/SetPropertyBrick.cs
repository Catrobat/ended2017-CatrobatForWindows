using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Formulas.Tree;

namespace Catrobat.IDE.Core.Models.Bricks
{
    public abstract partial class SetPropertyBrick : Brick
    {
    }

    #region Implementations

    public partial class SetPositionXBrick : SetPropertyBrick
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

        protected override bool TestEquals(Brick other)
        {
            return base.TestEquals(other) && TestEquals((SetPositionXBrick) other);
        }

        protected bool TestEquals(SetPositionXBrick other)
        {
            return TestEquals(_value, other._value);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance()
        {
            var result = (SetPositionXBrick) base.CloneInstance();
            CloneMember(ref result._value);
            return result;
        }

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (SetPositionXBrick) base.CloneInstance(context);
            CloneMember(ref result._value, context);
            return result;
        }

        #endregion
    }

    public partial class SetPositionYBrick : SetPropertyBrick
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

        protected override bool TestEquals(Brick other)
        {
            return base.TestEquals(other) && TestEquals((SetPositionYBrick) other);
        }

        protected bool TestEquals(SetPositionYBrick other)
        {
            return TestEquals(_value, other._value);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance()
        {
            var result = (SetPositionYBrick) base.CloneInstance();
            CloneMember(ref result._value);
            return result;
        }

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (SetPositionYBrick) base.CloneInstance(context);
            CloneMember(ref result._value, context);
            return result;
        }

        #endregion
    }

    public partial class SetPositionBrick : SetPropertyBrick
    {
        #region Properties

        private FormulaTree _valueX;
        public FormulaTree ValueX
        {
            get { return _valueX; }
            set { Set(ref _valueX, value); }
        }

        private FormulaTree _valueY;
        public FormulaTree ValueY
        {
            get { return _valueY; }
            set { Set(ref _valueY, value); }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(Brick other)
        {
            return base.TestEquals(other) && TestEquals((SetPositionBrick) other);
        }

        protected bool TestEquals(SetPositionBrick other)
        {
            return TestEquals(_valueX, other._valueX) && TestEquals(_valueY, other._valueY);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance()
        {
            var result = (SetPositionBrick) base.CloneInstance();
            CloneMember(ref result._valueX);
            CloneMember(ref result._valueY);
            return result;
        }

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (SetPositionBrick) base.CloneInstance(context);
            CloneMember(ref result._valueX, context);
            CloneMember(ref result._valueY, context);
            return result;
        }

        #endregion
    }

    public partial class BounceBrick : SetPropertyBrick
    {
    }

    public partial class SetSizeBrick : SetPropertyBrick
    {
        #region Properties

        private FormulaTree _percentage;
        public FormulaTree Percentage
        {
            get { return _percentage; }
            set { Set(ref _percentage, value); }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(Brick other)
        {
            return base.TestEquals(other) && TestEquals((SetSizeBrick) other);
        }

        protected bool TestEquals(SetSizeBrick other)
        {
            return TestEquals(_percentage, other._percentage);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance()
        {
            var result = (SetSizeBrick) base.CloneInstance();
            CloneMember(ref result._percentage);
            return result;
        }

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (SetSizeBrick) base.CloneInstance(context);
            CloneMember(ref result._percentage, context);
            return result;
        }

        #endregion
    }

    public partial class SetRotationBrick : SetPropertyBrick
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

        protected override bool TestEquals(Brick other)
        {
            return base.TestEquals(other) && TestEquals((SetRotationBrick) other);
        }

        protected bool TestEquals(SetRotationBrick other)
        {
            return TestEquals(_value, other._value);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance()
        {
            var result = (SetRotationBrick) base.CloneInstance();
            CloneMember(ref result._value);
            return result;
        }

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (SetRotationBrick) base.CloneInstance(context);
            CloneMember(ref result._value, context);
            return result;
        }

        #endregion
    }

    public partial class LookAtBrick : SetPropertyBrick
    {
        #region Properties

        private Sprite _target;
        public Sprite Target
        {
            get { return _target; }
            set { Set(ref _target, value); }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(Brick other)
        {
            return base.TestEquals(other) && TestEquals((LookAtBrick) other);
        }

        protected bool TestEquals(LookAtBrick other)
        {
            // prevent endless loops
            return ObjectExtensions.TypeEquals(_target, other._target);
        }

        #endregion
    }

    public partial class SetBrightnessBrick : SetPropertyBrick
    {
        #region Properties

        private FormulaTree _percentage;
        public FormulaTree Percentage
        {
            get { return _percentage; }
            set { Set(ref _percentage, value); }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(Brick other)
        {
            return base.TestEquals(other) && TestEquals((SetBrightnessBrick) other);
        }

        protected bool TestEquals(SetBrightnessBrick other)
        {
            return TestEquals(_percentage, other._percentage);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance()
        {
            var result = (SetBrightnessBrick) base.CloneInstance();
            CloneMember(ref result._percentage);
            return result;
        }

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (SetBrightnessBrick) base.CloneInstance(context);
            CloneMember(ref result._percentage, context);
            return result;
        }

        #endregion
    }

    public partial class SetTransparencyBrick : SetPropertyBrick
    {
        #region Properties

        private FormulaTree _percentage;
        public FormulaTree Percentage
        {
            get { return _percentage; }
            set { Set(ref _percentage, value); }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(Brick other)
        {
            return base.TestEquals(other) && TestEquals((SetTransparencyBrick) other);
        }

        protected bool TestEquals(SetTransparencyBrick other)
        {
            return TestEquals(_percentage, other._percentage);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance()
        {
            var result = (SetTransparencyBrick) base.CloneInstance();
            CloneMember(ref result._percentage);
            return result;
        }

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (SetTransparencyBrick) base.CloneInstance(context);
            CloneMember(ref result._percentage, context);
            return result;
        }

        #endregion
    }

    public partial class ResetGraphicPropertiesBrick : SetPropertyBrick
    {
    }

    public partial class BringToFrontBrick : SetPropertyBrick
    {
    }

    #endregion
}
