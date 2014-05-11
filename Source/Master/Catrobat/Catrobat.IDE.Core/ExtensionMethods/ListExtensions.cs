using System;
using System.Collections.Generic;

namespace Catrobat.IDE.Core.ExtensionMethods
{
    public static class ListExtensions
    {
        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="System.Collections.Generic.IList{T}" />. 
        /// </summary>
        /// <param name="items">The collection whose elements should be added to the end of the <see cref="System.Collections.Generic.IList{T}" />. </param>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null</exception>
        // ReSharper disable once CSharpWarnings::CS1573
        public static void AddRange<T>(this IList<T> source, IEnumerable<T> items)
        {
            var list = source as List<T>;
            if (list != null)
            {
                list.AddRange(items);
            }
            else
            {
                foreach (var element in items)
                {
                    source.Add(element);
                }
            }
        }

        /// <summary>
        /// Inserts the elements of a collection into the <see cref="System.Collections.Generic.IList{T}" /> 
        /// at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which the new elements should be inserted. </param>
        /// <param name="items">The elements who should be inserted into the <see cref="System.Collections.Generic.IList{T}" />. </param>
        /// <exception cref="System.ArgumentNullException"><paramref name="items"/> is null. </exception> 
        /// <exception cref="System.ArgumentOutOfRangeException"><paramref name="items"/> is null. 
        /// <paramref name="index"/> is less than 0.-or-<paramref name="index"/> is greater than <see cref="System.Collections.Generic.IList{T}.Count" />
        /// </exception> 
        /// <remarks>See <see cref="List{T}.InsertRange"/></remarks>
        // ReSharper disable once CSharpWarnings::CS1573
        public static void InsertRange<T>(this IList<T> source, int index, IEnumerable<T> items)
        {
            if (items == null) throw new ArgumentNullException("items");
            if (index < 0 || index > source.Count) throw new ArgumentOutOfRangeException("index");

            var list = source as List<T>;
            if (list != null)
            {
                list.InsertRange(index, items);
            }
            else
            {
                var index2 = index;
                foreach (var item in items)
                {
                    source.Insert(index2, item);
                    index2++;
                }
            }

        }

        /// <summary>Removes the specified number of items at the specified index. </summary>
        /// <param name="index">The zero-based index of the first item to remove. </param>
        /// <param name="count">The number of items to remove. </param>
        /// <exception cref="ArgumentOutOfRangeException">index is not a valid index in the <see cref="IList{T}" />. </exception>
        /// <remarks>See <see cref="List{T}.RemoveRange"/></remarks>
        // ReSharper disable once CSharpWarnings::CS1573
        public static void RemoveRange<T>(this IList<T> source, int index, int count)
        {
            var list = source as List<T>;
            if (list != null)
            {
                list.RemoveRange(index, count);
            }
            else
            {
                for (var i = 1; i <= count; i++)
                {
                    source.RemoveAt(index);
                }
            }
        }

        /// <summary>Replaces the item at the specified index with the specified item. </summary>
        /// <param name="index">The zero-based index of the item to replace. </param>
        /// <exception cref="ArgumentOutOfRangeException">index is not a valid index in the <see cref="IList{T}" />. </exception>
        // ReSharper disable once CSharpWarnings::CS1573
        public static void ReplaceAt<T>(this IList<T> source, int index, T item)
        {
            source.RemoveAt(index);
            source.Insert(index, item);
        }

        /// <summary>Replaces the specified number of items at the specified index with the specified items. </summary>
        /// <param name="index">The zero-based index of the first item to be replaced. </param>
        /// <param name="count">The number of items to replace. </param>
        /// <exception cref="ArgumentOutOfRangeException">index is not a valid index in the <see cref="IList{T}" />. </exception>
        // ReSharper disable once CSharpWarnings::CS1573
        public static void ReplaceRange<T>(this IList<T> source, int index, int count, IEnumerable<T> items)
        {
            source.RemoveRange(index, count);
            source.InsertRange(index, items);
        }

        public static void RemoveAll<T>(this IList<T> source, Func<T, bool> predicate)
        {
            for (var index = 0; index < source.Count; index++)
            {
                if (predicate(source[index])) source.RemoveAt(index);
            }
        }
    }
}
