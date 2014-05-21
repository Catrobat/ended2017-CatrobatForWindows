using System;

namespace Catrobat.IDE.Core.CatrobatObjects
{
    /// <remarks>
    /// Workaround to <see cref="System.Tuple"/> is not serializable (see <see cref="http://stackoverflow.com/a/13739409"/>). 
    /// </remarks>
    public struct SerializableTuple<T1, T2>
    {
        public T1 Item1;

        public T2 Item2;

        public static implicit operator SerializableTuple<T1, T2>(Tuple<T1, T2> tuple)
        {
            return new SerializableTuple<T1, T2> {Item1 = tuple.Item1, Item2 = tuple.Item2};
        }

        public static implicit operator Tuple<T1, T2>(SerializableTuple<T1, T2> tuple)
        {
            return new Tuple<T1, T2>(tuple.Item1, tuple.Item2);
        }
    }
}