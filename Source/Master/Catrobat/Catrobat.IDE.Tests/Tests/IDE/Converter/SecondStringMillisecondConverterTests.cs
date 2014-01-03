using Catrobat.IDE.Core.UI.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Tests.IDE.Converter
{
    [TestClass]
    public class SecondStringMillisecondConverterTests
    {
        [TestMethod, TestCategory("GatedTests")]
        public void TestSecondStringToMillisecondConversion()
        {
            var conv = new SecondStringMillisecondConverter();
            var output = conv.ConvertBack((object)"4.2", null, null, null);
            Assert.IsNotNull(output);
            Assert.IsTrue(output is int);
            Assert.AreEqual(4200, (int)output);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void TestMillisecondToSecondStringConversion()
        {
            var conv = new SecondStringMillisecondConverter();
            var output = conv.Convert((object)4200, null, null, null);
            Assert.IsNotNull(output);
            Assert.IsTrue(output is string);
            Assert.AreEqual("4.2", (string)output);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void TestFaultySecondStringToMillisecondConversion()
        {
            var conv = new SecondStringMillisecondConverter();
            var output = conv.ConvertBack((object)"4d2", null, 4200, null);
            Assert.IsNotNull(output);
            Assert.IsTrue(output is int);
            Assert.AreEqual(4200, (int)output);
        }
    }
}
