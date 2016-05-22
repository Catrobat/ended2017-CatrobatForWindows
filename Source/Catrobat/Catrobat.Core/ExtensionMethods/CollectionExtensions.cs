using System.Collections.Generic;
using System.Linq;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Models;

namespace Catrobat.IDE.Core.ExtensionMethods
{
    public static class CollectionExtensions
    {
        public static bool Equals<T>(ICollection<T> first, ICollection<T> second)
        {
            return first == null || second == null
                ? first == null && second == null
                : first.Count == second.Count && first.SequenceEqual(second);
        }

        public static bool TestEquals<T>(ICollection<T> first, ICollection<T> second) where T : class, ITestEquatable<T>
        {
            return first == null || second == null
                ? first == null && second == null
                : first.Count == second.Count && first.SequenceEqual(second, new TestEqualityComparer<T>());
        }
    }
}
