using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Formulas.Tree;

namespace Catrobat.IDE.Core.Models.Bricks
{
    public abstract partial class AnimatePropertyBrick : Brick
    {
        #region Properties

        private FormulaTree _duration;
        /// <summary>The animation's duration in seconds</summary>
        public FormulaTree Duration
        {
            get { return _duration; }
            set { Set(ref _duration, value); }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(Brick other)
        {
            return base.TestEquals(other) && TestEquals((AnimatePropertyBrick) other);
        }

        protected virtual bool TestEquals(AnimatePropertyBrick other)
        {
            return TestEquals(_duration, other._duration);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance()
        {
            var result = (AnimatePropertyBrick) base.CloneInstance();
            CloneMember(ref result._duration);
            return result;
        }

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (AnimatePropertyBrick) base.CloneInstance(context);
            CloneMember(ref result._duration, context);
            return result;
        }

        #endregion
    }

    #region Implementations

    public partial class AnimatePositionBrick : AnimatePropertyBrick
    {
        #region Properties

        private FormulaTree _toX;
        public FormulaTree ToX
        {
            get { return _toX; }
            set { Set(ref _toX, value); }
        }

        private FormulaTree _toY;
        public FormulaTree ToY
        {
            get { return _toY; }
            set { Set(ref _toY, value); }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(AnimatePropertyBrick other)
        {
            return base.TestEquals(other) && TestEquals((AnimatePositionBrick) other);
        }

        protected bool TestEquals(AnimatePositionBrick other)
        {
            return TestEquals(_toX, other._toX) && TestEquals(_toY, other._toY);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance()
        {
            var result = (AnimatePositionBrick) base.CloneInstance();
            CloneMember(ref result._toX);
            CloneMember(ref result._toY);
            return result;
        }

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (AnimatePositionBrick) base.CloneInstance(context);
            CloneMember(ref result._toX, context);
            CloneMember(ref result._toY, context);
            return result;
        }

        #endregion
    }

    #endregion
}
