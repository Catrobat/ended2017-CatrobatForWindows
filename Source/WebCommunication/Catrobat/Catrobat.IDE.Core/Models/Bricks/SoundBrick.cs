using Catrobat.IDE.Core.Models.Formulas.Tree;

namespace Catrobat.IDE.Core.Models.Bricks
{
    public abstract partial class SoundBrick : Brick
    {
    }

    #region Implementations

    public partial class PlaySoundBrick : SoundBrick
    {
        #region Properties

        private Sound _value;
        public Sound Value
        {
            get { return _value; }
            set { Set(ref _value, value); }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(Brick other)
        {
            return base.TestEquals(other) && TestEquals((PlaySoundBrick) other);
        }

        protected bool TestEquals(PlaySoundBrick other)
        {
            return TestEquals(_value, other._value);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (PlaySoundBrick) base.CloneInstance(context);
            CloneMember(ref result._value, context);
            return result;
        }

        #endregion
    }

    public partial class SpeakBrick : SoundBrick
    {
        #region Properties

        private string _value;
        public string Value
        {
            get { return _value; }
            set { Set(ref _value, value); }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(Brick other)
        {
            return base.TestEquals(other) && TestEquals((SpeakBrick) other);
        }

        protected bool TestEquals(SpeakBrick other)
        {
            return string.Equals(_value, other._value);
        }

        #endregion
    }

    public partial class StopSoundsBrick : SoundBrick
    {
    }

    public partial class SetVolumeBrick : SoundBrick
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
            return base.TestEquals(other) && TestEquals((SetVolumeBrick) other);
        }

        protected bool TestEquals(SetVolumeBrick other)
        {
            return TestEquals(_percentage, other._percentage);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (SetVolumeBrick) base.CloneInstance(context);
            CloneMember(ref result._percentage, context);
            return result;
        }

        #endregion
    }

    public partial class ChangeVolumeBrick : SoundBrick
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
            return base.TestEquals(other) && TestEquals((ChangeVolumeBrick) other);
        }

        protected bool TestEquals(ChangeVolumeBrick other)
        {
            return TestEquals(_relativePercentage, other._relativePercentage);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (ChangeVolumeBrick) base.CloneInstance(context);
            CloneMember(ref result._relativePercentage, context);
            return result;
        }

        #endregion
    }

    #endregion
}
