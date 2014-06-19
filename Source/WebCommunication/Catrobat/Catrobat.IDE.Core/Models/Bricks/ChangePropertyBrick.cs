using Catrobat.IDE.Core.Models.Formulas.Tree;

namespace Catrobat.IDE.Core.Models.Bricks
{
    public abstract partial class ChangePropertyBrick : Brick
    {
    }

    #region Implementations

    public partial class ChangePositionXBrick : ChangePropertyBrick
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

        protected override bool TestEquals(Brick other)
        {
            return base.TestEquals(other) && TestEquals((ChangePositionXBrick)other);
        }

        protected bool TestEquals(ChangePositionXBrick other)
        {
            return TestEquals(_relativeValue, other._relativeValue);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance()
        {
            var result = (ChangePositionXBrick) base.CloneInstance();
            CloneMember(ref result._relativeValue);
            return result;
        }

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (ChangePositionXBrick)base.CloneInstance(context);
            CloneMember(ref result._relativeValue, context);
            return result;
        }

        #endregion
    }

    public partial class ChangePositionYBrick : ChangePropertyBrick
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

        protected override bool TestEquals(Brick other)
        {
            return base.TestEquals(other) && TestEquals((ChangePositionYBrick)other);
        }

        protected bool TestEquals(ChangePositionYBrick other)
        {
            return TestEquals(_relativeValue, other._relativeValue);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance()
        {
            var result = (ChangePositionYBrick) base.CloneInstance();
            CloneMember(ref result._relativeValue);
            return result;
        }

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (ChangePositionYBrick) base.CloneInstance(context);
            CloneMember(ref result._relativeValue, context);
            return result;
        }

        #endregion
    }

    public partial class MoveBrick : ChangePropertyBrick
    {
        #region Properties

        private FormulaTree _steps;
        public FormulaTree Steps
        {
            get { return _steps; }
            set { Set(ref _steps, value); }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(Brick other)
        {
            return base.TestEquals(other) && TestEquals((MoveBrick) other);
        }

        protected bool TestEquals(MoveBrick other)
        {
            return TestEquals(_steps, other._steps);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance()
        {
            var result = (MoveBrick) base.CloneInstance();
            CloneMember(ref result._steps);
            return result;
        }

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (MoveBrick) base.CloneInstance(context);
            CloneMember(ref result._steps, context);
            return result;
        }

        #endregion
    }

    public partial class ChangeSizeBrick : ChangePropertyBrick
    {
        #region Properties

        private FormulaTree _relativePercentage;
        public FormulaTree RelativePercentage
        {
            get { return _relativePercentage; }
            set { Set(ref _relativePercentage, value); }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(Brick other)
        {
            return base.TestEquals(other) && TestEquals((ChangeSizeBrick)other);
        }

        protected bool TestEquals(ChangeSizeBrick other)
        {
            return TestEquals(_relativePercentage, other._relativePercentage);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance()
        {
            var result = (ChangeSizeBrick) base.CloneInstance();
            CloneMember(ref result._relativePercentage);
            return result;
        }

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (ChangeSizeBrick) base.CloneInstance(context);
            CloneMember(ref result._relativePercentage, context);
            return result;
        }

        #endregion
    }

    public abstract partial class ChangeRotationBrick : ChangePropertyBrick
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

        protected override bool TestEquals(Brick other)
        {
            return base.TestEquals(other) && TestEquals((ChangeRotationBrick) other);
        }

        protected bool TestEquals(ChangeRotationBrick other)
        {
            return TestEquals(_relativeValue, other._relativeValue);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance()
        {
            var result = (ChangeRotationBrick) base.CloneInstance();
            CloneMember(ref result._relativeValue);
            return result;
        }

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (ChangeRotationBrick) base.CloneInstance(context);
            CloneMember(ref result._relativeValue, context);
            return result;
        }

        #endregion
    }

    public partial class TurnLeftBrick : ChangeRotationBrick
    {
    }

    public partial class TurnRightBrick : ChangeRotationBrick
    {
    }

    public partial class ChangeBrightnessBrick : ChangePropertyBrick
    {
        #region Properties

        private FormulaTree _relativePercentage;
        public FormulaTree RelativePercentage
        {
            get { return _relativePercentage; }
            set { Set(ref _relativePercentage, value); }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(Brick other)
        {
            return base.TestEquals(other) && TestEquals((ChangeBrightnessBrick) other);
        }

        protected bool TestEquals(ChangeBrightnessBrick other)
        {
            return TestEquals(_relativePercentage, other._relativePercentage);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance()
        {
            var result = (ChangeBrightnessBrick) base.CloneInstance();
            CloneMember(ref result._relativePercentage);
            return result;
        }

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (ChangeBrightnessBrick) base.CloneInstance(context);
            CloneMember(ref result._relativePercentage, context);
            return result;
        }

        #endregion
    }

    public partial class ChangeTransparencyBrick : ChangePropertyBrick
    {
        #region Properties

        private FormulaTree _relativePercentage;
        public FormulaTree RelativePercentage
        {
            get { return _relativePercentage; }
            set { Set(ref _relativePercentage, value); }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(Brick other)
        {
            return base.TestEquals(other) && TestEquals((ChangeTransparencyBrick) other);
        }

        protected bool TestEquals(ChangeTransparencyBrick other)
        {
            return TestEquals(_relativePercentage, other._relativePercentage);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance()
        {
            var result = (ChangeTransparencyBrick) base.CloneInstance();
            CloneMember(ref result._relativePercentage);
            return result;
        }

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (ChangeTransparencyBrick) base.CloneInstance(context);
            CloneMember(ref result._relativePercentage, context);
            return result;
        }

        #endregion
    }

    public partial class DecreaseZOrderBrick : ChangePropertyBrick
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

        protected override bool TestEquals(Brick other)
        {
            return base.TestEquals(other) && TestEquals((DecreaseZOrderBrick) other);
        }

        protected bool TestEquals(DecreaseZOrderBrick other)
        {
            return TestEquals(_relativeValue, other._relativeValue);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance()
        {
            var result = (DecreaseZOrderBrick) base.CloneInstance();
            CloneMember(ref result._relativeValue);
            return result;
        }

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (DecreaseZOrderBrick) base.CloneInstance(context);
            CloneMember(ref result._relativeValue, context);
            return result;
        }

        #endregion
    }

    #endregion
}
