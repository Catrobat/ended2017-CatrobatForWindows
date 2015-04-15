using GalaSoft.MvvmLight;

namespace Catrobat.IDE.Core.Models
{
    public abstract class ModelBase : ObservableObject, ITestEquatable<ModelBase>
    {
        #region Implements ITestEquatable

        bool ITestEquatable<ModelBase>.TestEquals(ModelBase other)
        {
            return TestEquals(other);
        }

        protected virtual bool TestEquals(ModelBase other)
        {
            return other.GetType() == GetType();
        }

        internal static bool TestEquals(ModelBase x, ModelBase y)
        {
            return x == null
                ? y == null
                : y != null && x.TestEquals(y);
        }

        #endregion
    }
}
