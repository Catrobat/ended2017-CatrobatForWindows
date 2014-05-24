using System.Collections.Generic;
using Catrobat.IDE.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Extensions
{
    public static class EnumerableAssert
    {
        public static void AreTestEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual) where T : class, ITestEquatable<T>
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
                    ModelAssert.AreTestEqual(expectedElement, actualEnumerator.Current);
                }
                Assert.IsFalse(actualEnumerator.MoveNext());
            }
        }
    }
}
