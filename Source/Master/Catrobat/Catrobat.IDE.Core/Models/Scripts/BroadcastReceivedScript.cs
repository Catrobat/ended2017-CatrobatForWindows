namespace Catrobat.IDE.Core.Models.Scripts
{
    public partial class BroadcastReceivedScript : Script
    {
        #region Properties

        private BroadcastMessage _mesage;
        public BroadcastMessage Message
        {
            get { return _mesage; }
            set { Set(ref _mesage, value); }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(Script other)
        {
             return base.TestEquals(other) && TestEquals((BroadcastReceivedScript) other);
        }

        private bool TestEquals(BroadcastReceivedScript other)
        {
            return TestEquals(_mesage, other._mesage);
        }

        #endregion
    }
}
