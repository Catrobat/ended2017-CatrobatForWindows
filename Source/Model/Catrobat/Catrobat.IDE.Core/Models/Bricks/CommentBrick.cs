namespace Catrobat.IDE.Core.Models.Bricks
{
    public partial class CommentBrick : Brick
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
            return base.TestEquals(other) && TestEquals((CommentBrick) other);
        }

        protected bool TestEquals(CommentBrick other)
        {
            return string.Equals(_value, other._value);
        }

        #endregion
     }
}
