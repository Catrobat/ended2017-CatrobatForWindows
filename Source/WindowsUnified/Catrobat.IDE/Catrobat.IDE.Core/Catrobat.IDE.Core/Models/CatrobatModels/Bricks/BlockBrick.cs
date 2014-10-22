using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Formulas.Tree;

namespace Catrobat.IDE.Core.Models.Bricks
{
    public abstract partial class BlockBrick : Brick
    {
    }

    /// <summary>Helper class because <c>BlockBeginBrick{dynamic, dynamic} doesn't work properly</c> as needed for <see cref="BlockBeginBrick{TBegin, TEnd}"/>. </summary>
    public abstract partial class BlockBeginBrick : BlockBrick
    {
        #region Properties

        private BlockEndBrick _end;
        public BlockEndBrick End
        {
            get { return _end; }
            set { Set(ref _end, value); }
        }

        private bool _isGrouped;
        public bool IsGrouped
        {
            get { return _isGrouped; }
            set { _isGrouped = value; }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(Brick other)
        {
            return base.TestEquals(other) && TestEquals((BlockBeginBrick) other);
        }

        protected virtual bool TestEquals(BlockBeginBrick other)
        {
            // prevent endless loops
            return ObjectExtensions.TypeEquals(_end, other._end);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (BlockBeginBrick) base.CloneInstance(context);
            CloneMember(ref result._end, context);
            return result;
        }

        #endregion
    }

    /// <summary>Helper class because <c>BlockEndBrick{dynamic, dynamic} doesn't work properly</c> as needed for <see cref="BlockEndBrick{TBegin, TEnd}"/>. </summary>
    public abstract partial class BlockEndBrick : BlockBrick
    {
        #region Properties

        private BlockBeginBrick _begin;
        public BlockBeginBrick Begin
        {
            get { return _begin; }
            set { Set(ref _begin, value); }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(Brick other)
        {
            return base.TestEquals(other) && TestEquals((BlockEndBrick) other);
        }

        protected virtual bool TestEquals(BlockEndBrick other)
        {
            // prevent endless loops
            return ObjectExtensions.TypeEquals(_begin, other._begin);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (BlockEndBrick) base.CloneInstance(context);
            CloneMember(ref result._begin, context);
            return result;
        }

        #endregion
    }

    public abstract partial class BlockBeginBrick<TBegin, TEnd> : BlockBeginBrick 
        where TBegin : BlockBeginBrick<TBegin, TEnd> 
        where TEnd : BlockEndBrick<TBegin, TEnd>
    {
        #region Properties

        public new TEnd End
        {
            get { return (TEnd) base.End; }
            set { base.End = value; }
        }

        #endregion
    }

    public abstract partial class BlockEndBrick<TBegin, TEnd> : BlockEndBrick
        where TBegin : BlockBeginBrick<TBegin, TEnd>
        where TEnd : BlockEndBrick<TBegin, TEnd>
    {
        #region Properties

        public new TBegin Begin
        {
            get { return (TBegin) base.Begin; }
            set { base.Begin = value; }
        }

        #endregion
    }

    #region Implementations

    public partial class ForeverBrick : BlockBeginBrick<ForeverBrick, EndForeverBrick>
    {
    }

    public partial class EndForeverBrick : BlockEndBrick<ForeverBrick, EndForeverBrick>
    {
    }

    public partial class RepeatBrick : BlockBeginBrick<RepeatBrick, EndRepeatBrick>
    {
        #region Properties

        private FormulaTree _count;
        public FormulaTree Count
        {
            get { return _count; }
            set { Set(ref _count, value); }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(BlockBeginBrick other)
        {
            return base.TestEquals(other) && TestEquals((RepeatBrick) other);
        }

        protected bool TestEquals(RepeatBrick other)
        {
            return TestEquals(_count, other._count);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance()
        {
            var result = (RepeatBrick) base.CloneInstance();
            CloneMember(ref result._count);
            return result;
        }

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (RepeatBrick) base.CloneInstance(context);
            CloneMember(ref result._count, context);
            return result;
        }

        #endregion
    }

    public partial class EndRepeatBrick : BlockEndBrick<RepeatBrick, EndRepeatBrick>
    {
    }

    public partial class IfBrick : BlockBeginBrick<IfBrick, EndIfBrick>
    {
        #region Properties

        private FormulaTree _condition;
        public FormulaTree Condition
        {
            get { return _condition; }
            set { Set(ref _condition, value); }
        }

        private ElseBrick _else;
        public ElseBrick Else
        {
            get { return _else; }
            set { Set(ref _else, value); }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(BlockBeginBrick other)
        {
            return base.TestEquals(other) && TestEquals((IfBrick) other);
        }

        protected bool TestEquals(IfBrick other)
        {
            // prevent endless loops
            return TestEquals(_condition, other._condition) && ObjectExtensions.TypeEquals(_else, other._else);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance()
        {
            var result = (IfBrick) base.CloneInstance();
            CloneMember(ref result._condition);
            return result;
        }

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (IfBrick) base.CloneInstance(context);
            CloneMember(ref result._condition, context);
            CloneMember(ref result._else, context);
            return result;
        }

        #endregion
    }

    public partial class ElseBrick : BlockBeginBrick<IfBrick, EndIfBrick>
    {
        #region Properties

        private IfBrick _begin;
        public IfBrick Begin
        {
            get { return _begin; }
            set { Set(ref _begin, value); }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(BlockBeginBrick other)
        {
            return base.TestEquals(other) && TestEquals((ElseBrick) other);
        }

        protected bool TestEquals(ElseBrick other)
        {
            // prevent endless loops
            return ObjectExtensions.TypeEquals(_begin, other._begin);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (ElseBrick) base.CloneInstance(context);
            CloneMember(ref result._begin, context);
            return result;
        }

        #endregion
    }

    public partial class EndIfBrick : BlockEndBrick<IfBrick, EndIfBrick>
    {
        #region Properties

        private ElseBrick _else;
        public ElseBrick Else
        {
            get { return _else; }
            set { Set(ref _else, value); }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(BlockEndBrick other)
        {
            return base.TestEquals(other) && TestEquals((EndIfBrick) other);
        }

        protected bool TestEquals(EndIfBrick other)
        {
            // prevent endless loops
            return ObjectExtensions.TypeEquals(_else, other._else);
        }

        #endregion

        #region Implements ICloneable

        internal override object CloneInstance(CloneSpriteContext context)
        {
            var result = (EndIfBrick) base.CloneInstance(context);
            CloneMember(ref result._else, context);
            return result;
        }

        #endregion
    }

    #endregion
}
