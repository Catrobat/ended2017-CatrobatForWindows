using Catrobat.IDE.Core.UI.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.IDE.Converter
{
    [TestClass]
    public class DoubleStringConverterTests
    {
        [TestMethod, TestCategory("GatedTests")]
        public void TestStringToDoubleConversion()
        {
            var conv = new DoubleStringConverter();
            object output = conv.ConvertBack((object)"4.2", null, null, null);
            Assert.IsNotNull(output);
            Assert.IsTrue(output is double);
            Assert.AreEqual(4.2d, (double)output);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void TestDoubleToStringConversion()
        {
            var conv = new DoubleStringConverter();
            object output = conv.Convert((object)4.2d, null, null, null);
            Assert.IsNotNull(output);
            Assert.IsTrue(output is string);
            Assert.AreEqual("4.2", (string)output);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void TestFaultyStringToDoubleConversion()
        {
            var conv = new DoubleStringConverter();
            object output = conv.ConvertBack((object)"4d2", null, 42d, null);
            Assert.IsNotNull(output);
            Assert.IsTrue(output is double);
            Assert.AreEqual(42d, (double)output);
        }
    }
}
