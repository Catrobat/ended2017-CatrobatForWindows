using System;
using System.Collections.Generic;

namespace Catrobat.IDE.Core.ExtensionMethods
{
    public static class ListExtensions
    {
        /// <summary>Removes the specified number of items at the specified index. </summary>
        /// <param name="index">The zero-based index of the first item to remove. </param>
        /// <param name="count">The number of items to remove. </param>
        /// <exception cref="ArgumentOutOfRangeException">index is not a valid index in the <see cref="IList{T}" />. </exception>
        /// <remarks>See <see cref="List{T}.RemoveRange"/></remarks>
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
        public static void ReplaceAt<T>(this IList<T> source, int index, T item)
        {
            source.RemoveAt(index);
            source.Insert(index, item);
        }

        /// <summary>Replaces the specified number of items at the specified index with the specified item. </summary>
        /// <param name="index">The zero-based index of the item to be replaced. </param>
        /// <param name="count">The number of items to replace. </param>
        /// <exception cref="ArgumentOutOfRangeException">index is not a valid index in the <see cref="IList{T}" />. </exception>
        public static void ReplaceRange<T>(this IList<T> source, int index, int count, T item)
        {
            source.RemoveRange(index, count);
            source.Insert(index, item);
        }


    }
}
