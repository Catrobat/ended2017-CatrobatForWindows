namespace Catrobat.IDE.Core.Models.Bricks
{
    public abstract partial class LookBrick : Brick
    {
    }

    #region Implementations

    public partial class SetLookBrick : LookBrick
    {
        #region Properties

        private Look _value;
        public Look Value
        {
            get { return _value; }
            set { Set(ref _value, value); }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(Brick other)
        {
            return base.TestEquals(other) && TestEquals((SetLookBrick) other);
        }

        protected bool TestEquals(SetLookBrick other)
        {
            return TestEquals(_value, other._value);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (SetLookBrick) base.CloneInstance(context);
            result.Value = context.Looks[Value];
            return result;
        }

        #endregion
    }

    public partial class NextLookBrick : LookBrick
    {
    }

    #endregion
}
