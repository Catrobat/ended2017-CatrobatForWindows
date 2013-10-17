using Catrobat.IDE.Core.UI.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Tests.IDE.Converter
{
    [TestClass]
    public class IntStringConverterTests
    {
        // ####### IntStringConverter #############################################
        [TestMethod, TestCategory("GatedTests")]
        public void TestStringToIntConversion()
        {
            var conv = new IntStringConverter();
            object output = conv.ConvertBack((object)"12", null, null, null);
            Assert.IsNotNull(output);
            Assert.IsTrue(output is int);
            Assert.AreEqual(12, (int)output);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void TestIntToStringConversion()
        {
            var conv = new IntStringConverter();
            object output = conv.Convert((object)42, null, null, null);
            Assert.IsNotNull(output);
            Assert.IsTrue(output is string);
            Assert.AreEqual("42", (string)output);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void TestFaultyStringToIntConversion()
        {
            var conv = new IntStringConverter();
            object output = conv.ConvertBack((object)"4d2", null, 42, null);
            Assert.IsNotNull(output);
            Assert.IsTrue(output is int);
            Assert.AreEqual(42, (int)output);
        }

    }
}
