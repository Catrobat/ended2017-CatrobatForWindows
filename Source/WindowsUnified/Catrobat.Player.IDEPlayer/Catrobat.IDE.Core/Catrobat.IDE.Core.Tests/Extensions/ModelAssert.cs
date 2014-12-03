using Catrobat.IDE.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Extensions
{
    internal static class ModelAssert
    {
        public static void AreTestEqual<T>(T expected, T actual) where T : class, ITestEquatable<T>
        {
            if (expected == null)
            {
                Assert.IsNull(actual);
            }
            else
            {
                Assert.IsTrue(expected.TestEquals(actual));
            }
        }
    }
}
