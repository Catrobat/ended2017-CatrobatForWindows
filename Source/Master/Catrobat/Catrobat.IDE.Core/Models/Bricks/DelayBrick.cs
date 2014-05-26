using Catrobat.IDE.Core.Models.Formulas.Tree;

namespace Catrobat.IDE.Core.Models.Bricks
{
    public partial class DelayBrick : Brick
    {
        #region Properties

        private FormulaTree _duration;
        /// <summary>The duration to wait in seconds</summary>
        public FormulaTree Duration
        {
            get { return _duration; }
            set { Set(ref _duration, value); }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(Brick other)
        {
            return base.TestEquals(other) && TestEquals((DelayBrick) other);
        }

        protected bool TestEquals(DelayBrick other)
        {
            return TestEquals(_duration, other._duration);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance()
        {
            var result = (DelayBrick) base.CloneInstance();
            CloneMember(ref result._duration);
            return result;
        }

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (DelayBrick)base.CloneInstance(context);
            CloneMember(ref result._duration, context);
            return result;
        }

        #endregion
    }
}
