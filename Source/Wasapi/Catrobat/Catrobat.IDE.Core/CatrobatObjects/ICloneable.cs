using System;
using Catrobat.IDE.Core.ExtensionMethods;

namespace Catrobat.IDE.Core.CatrobatObjects
{
    /// <summary>Supports cloning, which creates a new instance of a class with the same value as an existing instance. </summary>
    /// <remarks><see cref="http://msdn.microsoft.com/en-us/library/system.icloneable.aspx"/></remarks>
    public interface ICloneable
    {
        /// <summary>Creates a new object that is a copy of the current instance. </summary>
        /// <returns>A new object that is a copy of this instance. </returns>
        /// <remarks>
        /// <example>Usage: <c>obj.Clone()</c> via <see cref="CloneableExtensions"/></example>
        /// <para>The resulting clone must be of the same type as, or compatible with, the original instance. </para>
        /// <para>See <see cref="Object.MemberwiseClone"/> for more information on cloning, deep versus shallow copies, and examples. </para>
        /// </remarks>
        object CloneInstance();
    }

    /// <summary>Supports cloning, which creates a new instance of a class with the same value as an existing instance. </summary>
    /// <remarks><see cref="http://msdn.microsoft.com/en-us/library/system.icloneable.aspx"/></remarks>
    public interface ICloneable<in TContext>
    {
        /// <summary>Creates a new object that is a copy of the current instance. </summary>
        /// <returns>A new object that is a copy of this instance. </returns>
        /// <remarks>
        /// <example>Usage: <c>obj.Clone()</c> via <see cref="CloneableExtensions"/></example>
        /// <para>The resulting clone must be of the same type as, or compatible with, the original instance. </para>
        /// <para>See <see cref="Object.MemberwiseClone"/> for more information on cloning, deep versus shallow copies, and examples. </para>
        /// </remarks>
        object CloneInstance(TContext context);
    }
}
