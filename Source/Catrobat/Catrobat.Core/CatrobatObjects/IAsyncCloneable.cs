using System;
using System.Threading.Tasks;
using Catrobat.IDE.Core.ExtensionMethods;

namespace Catrobat.IDE.Core.CatrobatObjects
{
    public interface IAsyncCloneable<in TContext>
    {
        /// <summary>Creates a new object that is a copy of the current instance. </summary>
        /// <returns>A new object that is a copy of this instance. </returns>
        /// <remarks>
        /// <example>Usage: await <c>obj.CloneAsync()</c> via <see cref="CloneableExtensions"/></example>
        /// <para>The resulting clone must be of the same type as, or compatible with, the original instance. </para>
        /// <para>See <see cref="Object.MemberwiseClone"/> for more information on cloning, deep versus shallow copies, and examples. </para>
        /// </remarks>
        Task<object> CloneInstance(TContext context);
    }
}
