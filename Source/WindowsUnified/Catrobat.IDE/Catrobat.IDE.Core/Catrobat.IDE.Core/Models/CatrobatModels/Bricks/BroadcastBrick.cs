namespace Catrobat.IDE.Core.Models.Bricks
{
    public abstract partial class BroadcastBrick : Brick
    {
        #region Properties

        private BroadcastMessage _message;
        public BroadcastMessage Message
        {
            get { return _message; }
            set { Set(ref _message, value); }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(Brick other)
        {
            return base.TestEquals(other) && TestEquals((BroadcastBrick) other);
        }

        protected virtual bool TestEquals(BroadcastBrick other)
        {
            return TestEquals(_message, other._message);
        }

        #endregion
    }

    #region Implementations

    public partial class BroadcastSendBrick : BroadcastBrick
    {
    }

    public partial class BroadcastSendBlockingBrick : BroadcastBrick
    {
    }

    #endregion
}
