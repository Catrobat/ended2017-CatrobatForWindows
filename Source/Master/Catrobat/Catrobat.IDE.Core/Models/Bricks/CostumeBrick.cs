namespace Catrobat.IDE.Core.Models.Bricks
{
    public abstract partial class CostumeBrick : Brick
    {
    }

    #region Implementations

    public partial class SetCostumeBrick : CostumeBrick
    {
        #region Properties

        private Costume _value;
        public Costume Value
        {
            get { return _value; }
            set { Set(ref _value, value); }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(Brick other)
        {
            return base.TestEquals(other) && TestEquals((SetCostumeBrick) other);
        }

        protected bool TestEquals(SetCostumeBrick other)
        {
            return TestEquals(_value, other._value);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (SetCostumeBrick) base.CloneInstance(context);
            result.Value = context.Costumes[Value];
            return result;
        }

        #endregion
    }

    public partial class NextCostumeBrick : CostumeBrick
    {
    }

    #endregion
}
