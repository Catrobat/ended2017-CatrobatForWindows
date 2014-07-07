using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Catrobat.IDE.Core.CatrobatObjects
{
    /// <summary>
    /// Compares two instances of <typeparam name="TClass" /> by using <see cref="object.ReferenceEquals"/> and 
    /// thus ignoring any <see cref="IEquatable{T}"/> or <see cref="object.Equals(object)"/>  implementations. </summary>
    /// <remarks>
    /// See <see cref="http://stackoverflow.com/questions/1890058/iequalitycomparert-that-uses-referenceequals"/>
    /// </remarks>
    internal class ReferenceEqualityComparer<TClass> : EqualityComparer<TClass> where TClass : class
    {
        #region Inherits EqualityComparer

        public override bool Equals(TClass x, TClass y)
        {
            return ReferenceEquals(x, y);
        }

        public override int GetHashCode(TClass obj)
        {
            return RuntimeHelpers.GetHashCode(obj);
        }

        #endregion
    }
}
