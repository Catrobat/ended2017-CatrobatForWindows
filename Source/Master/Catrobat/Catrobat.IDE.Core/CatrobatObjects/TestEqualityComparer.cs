using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Catrobat.IDE.Core.Models;

namespace Catrobat.IDE.Core.CatrobatObjects
{
    /// <summary>
    /// Compares two instances of <typeparam name="T" /> by using <see cref="ITestEquatable{T}.TestEquals(T)"/>. 
    /// </summary>
    internal class TestEqualityComparer<T> : EqualityComparer<T> where T : class, ITestEquatable<T>
    {
        #region Inherits EqualityComparer

        public override bool Equals(T x, T y)
        {
            return x == null 
                ? y == null 
                : y != null && x.TestEquals(y);
        }

        public override int GetHashCode(T obj)
        {
            return RuntimeHelpers.GetHashCode(obj);
        }

        #endregion
    }
}
