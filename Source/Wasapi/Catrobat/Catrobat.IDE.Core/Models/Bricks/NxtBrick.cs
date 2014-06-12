using Catrobat.IDE.Core.Models.Formulas.Tree;

namespace Catrobat.IDE.Core.Models.Bricks
{
    public abstract partial class NxtBrick : Brick
    {
    }

    public abstract partial class NxtMotorBrick : Brick
    {
        #region Properties

        private string _motor;
        public string Motor
        {
            get { return _motor; }
            set { Set(ref _motor, value); }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(Brick other)
        {
            return base.TestEquals(other) && TestEquals((NxtMotorBrick) other);
        }

        protected bool TestEquals(NxtMotorBrick other)
        {
            return string.Equals(_motor, other._motor);
        }

        #endregion
    }

    #region Implementations


    public partial class PlayNxtToneBrick : NxtBrick
    {
        #region Properties

        private FormulaTree _frequency;
        public FormulaTree Frequency
        {
            get { return _frequency; }
            set { Set(ref _frequency, value); }
        }

        private FormulaTree _duration;
        public FormulaTree Duration
        {
            get { return _duration; }
            set { Set(ref _duration, value); }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(Brick other)
        {
            return base.TestEquals(other) && TestEquals((PlayNxtToneBrick) other);
        }

        protected bool TestEquals(PlayNxtToneBrick other)
        {
            return TestEquals(_frequency, other._frequency) && TestEquals(_duration, other._duration);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance()
        {
            var result = (PlayNxtToneBrick) base.CloneInstance();
            CloneMember(ref result._frequency);
            CloneMember(ref result._duration);
            return result;
        }

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (PlayNxtToneBrick) base.CloneInstance(context);
            CloneMember(ref result._frequency, context);
            CloneMember(ref result._duration, context);
            return result;
        }

        #endregion
    }

    public partial class SetNxtMotorSpeedBrick : NxtMotorBrick
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
            return base.TestEquals(other) && TestEquals((SetNxtMotorSpeedBrick) other);
        }

        protected bool TestEquals(SetNxtMotorSpeedBrick other)
        {
            return TestEquals(_percentage, other._percentage);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance()
        {
            var result = (SetNxtMotorSpeedBrick) base.CloneInstance();
            CloneMember(ref result._percentage);
            return result;
        }

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (SetNxtMotorSpeedBrick) base.CloneInstance(context);
            CloneMember(ref result._percentage, context);
            return result;
        }

        #endregion
    }

    public partial class ChangeNxtMotorAngleBrick : NxtMotorBrick
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
            return base.TestEquals(other) && TestEquals((ChangeNxtMotorAngleBrick) other);
        }

        protected bool TestEquals(ChangeNxtMotorAngleBrick other)
        {
            return TestEquals(_relativeValue, other._relativeValue);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance()
        {
            var result = (ChangeNxtMotorAngleBrick) base.CloneInstance();
            CloneMember(ref result._relativeValue);
            return result;
        }

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (ChangeNxtMotorAngleBrick) base.CloneInstance(context);
            CloneMember(ref result._relativeValue, context);
            return result;
        }

        #endregion
    }

    public partial class StopNxtMotorBrick : NxtMotorBrick
    {
    }

    #endregion
}
