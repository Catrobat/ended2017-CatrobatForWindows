using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Catrobat.IDE.Core.ExtensionMethods
{
    public static class LinqExtensions
    {

        /// <remarks>Inspired by <see cref="http://code.google.com/p/morelinq/source/browse/MoreLinq/DistinctBy.cs"/>. </remarks>
        public static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
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
        public static IEnumerable<TSource[]> WithContext<TSource>(this IEnumerable<TSource> source)
            where TSource : class
        {
            var previousElement = (TSource) null;
            foreach (var element in source)
            {
                yield return new[] {previousElement, element};
                previousElement = element;
            }
            if (previousElement != null)
            {
                yield return new[] {previousElement, null};
            }
        }

        /// <summary>
        /// Returns the first element of the sequence that satisfies a condition 
        /// or a default value if no such element is found. 
        ///</summary>
        /// <param name="source">An <see cref="System.Collections.Generic.IEnumerator{T}" /> to return an element from. </param>
        /// <param name="predicate"> A function to test each element for a condition.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <returns>
        /// <c>default(<typeparamref name="TSource" />)</c> if <paramref name="source"/> is empty 
        /// or if no element passes the test specified by predicate; 
        /// otherwise, the first element in <paramref name="source"/> 
        /// that passes the test specified by <paramref name="predicate"/>.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        public static TSource FirstOrDefault<TSource>(this IEnumerator<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (predicate == null) throw new ArgumentNullException("predicate");

            while (source.MoveNext())
            {
                if (predicate.Invoke(source.Current)) return source.Current;
            }
            return default(TSource);
        }

        public static int IndexOf<TSource>(this IEnumerable<TSource> source, TSource element) where TSource : class
        {
            var index = 0;
            foreach (var element2 in source)
            {
                if (element == element2) return index;
                index++;
            }
            return -1;
        }

        public static IEnumerable<TSource> Argmax<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) where TKey : IComparable
        {
            var result = new List<TSource>();
            var maxKey = default(TKey);
            foreach (var element in source)
            {
                var key = keySelector(element);
                var comparison = key.CompareTo(maxKey);
                if (result.Count == 0)
                {
                    result.Add(element);
                    maxKey = key;
                }
                else
                {
                    if (comparison > 0)
                    {
                        result.Clear();
                        maxKey = key;
                    }
                    if (comparison >= 0)
                    {
                        result.Add(element);
                    }
                }
            }
            return result;
        }

        public static ObservableCollection<TSource> ToObservableCollection<TSource>(this IEnumerable<TSource> source)
        {
            return new ObservableCollection<TSource>(source);
        }

        public static ISet<TSource> ToSet<TSource>(this IEnumerable<TSource> source)
        {
            return new HashSet<TSource>(source);
        }

    }
}
