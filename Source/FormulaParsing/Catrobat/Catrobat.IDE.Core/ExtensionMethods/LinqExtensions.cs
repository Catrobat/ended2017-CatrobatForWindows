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
 

    }
}
