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

    #endregion
}
