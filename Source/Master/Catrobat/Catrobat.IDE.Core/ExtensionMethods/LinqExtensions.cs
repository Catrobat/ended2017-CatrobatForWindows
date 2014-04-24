using System;
using System.Collections.Generic;
using System.Linq;

namespace Catrobat.IDE.Core.ExtensionMethods
{
    public static class LinqExtensions
    {

        /// <see cref="http://code.google.com/p/morelinq/source/browse/MoreLinq/DistinctBy.cs"/>
        public static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.GroupBy(keySelector).Select(group => group.First());
        }

        /// <summary>Includes the previous element to each element.</summary>
        /// <remarks>
        /// <para>
        /// Usage:
        /// <example>
        /// <code>
        /// foreach (var context in source.WithContext())
        /// {
        ///     var previousElement = context[0];
        ///     var element = context[1];
        /// 
        ///     if (previousElement == null)
        ///     {
        ///         var firstElement = element;
        ///     }
        /// 
        ///     if (element == null)
        ///     {
        ///         var lastElement = previousElement;
        ///     }
        ///     
        ///     if (element != null) yield return element;
        /// }
        /// </code>
        /// </example>
        /// </para>
        /// <para>
        /// Inspired by <see cref="http://stackoverflow.com/a/8760569/1220972"/>. 
        /// </para>
        /// </remarks>
        public static IEnumerable<TSource[]> WithContext<TSource>(this IEnumerable<TSource> source) where TSource : class
        {
            var previousElement = (TSource) null;
            foreach (var element in source)
            {
                yield return new[] { previousElement, element };
                previousElement = element;
            }
            if (previousElement != null) 
            {
                yield return new[] { previousElement, null };
            }
        }
    }
}
