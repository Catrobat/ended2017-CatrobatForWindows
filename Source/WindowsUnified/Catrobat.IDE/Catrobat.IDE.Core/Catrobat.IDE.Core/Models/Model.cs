using GalaSoft.MvvmLight;

namespace Catrobat.IDE.Core.Models
{
    public abstract class Model : ObservableObject, ITestEquatable<Model>
    {
        #region Implements ITestEquatable

        bool ITestEquatable<Model>.TestEquals(Model other)
        {
            return TestEquals(other);
        }

        protected virtual bool TestEquals(Model other)
        {
            return other.GetType() == GetType();
        }

        internal static bool TestEquals(Model x, Model y)
        {
            return x == null
                ? y == null
                : y != null && x.TestEquals(y);
        }

        #endregion
    }
}
