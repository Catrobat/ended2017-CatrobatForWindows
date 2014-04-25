using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Extensions
{
    public static class EnumerableAssert
    {
        public static void AreEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            if (expected == null)
            {
                Assert.IsNull(actual);
            }
            else
            {
                Assert.IsNotNull(actual);
                var actualEnumerator = actual.GetEnumerator();
                foreach (var expectedElement in expected)
                {
                    Assert.IsTrue(actualEnumerator.MoveNext());
                    Assert.AreEqual(expectedElement, actualEnumerator.Current);
                }
                Assert.IsFalse(actualEnumerator.MoveNext());
            }
        }

        public static void AreEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual, string message)
        {
            if (expected == null)
            {
                Assert.IsNull(actual);
            }
            else
            {
                Assert.IsNotNull(actual);
                var actualEnumerator = actual.GetEnumerator();
                foreach (var expectedElement in expected)
                {
                    Assert.IsTrue(actualEnumerator.MoveNext(), message);
                    Assert.AreEqual(expectedElement, actualEnumerator.Current, message);
                }
                Assert.IsFalse(actualEnumerator.MoveNext(), message);
            }
        }
    }
}
