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

        /// <see cref="http://stackoverflow.com/a/8760569/1220972"/>
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
