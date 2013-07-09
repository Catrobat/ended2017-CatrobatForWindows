using Catrobat.IDEWindowsPhone.Converters;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Catrobat.TestsWindowsPhone.Tests.IDE.Converter
{
    [TestClass]
    public class IntStringConverterTests
    {
        // ####### IntStringConverter #############################################
        [TestMethod]
        public void TestStringToIntConversion()
        {
            var conv = new IntStringConverter();
            object output = conv.ConvertBack((object)"12", null, null, null);
            Assert.IsNotNull(output);
            Assert.IsTrue(output is int);
            Assert.AreEqual((int)output, 12);
        }

        [TestMethod]
        public void TestIntToStringConversion()
        {
            var conv = new IntStringConverter();
            object output = conv.Convert((object)42, null, null, null);
            Assert.IsNotNull(output);
            Assert.IsTrue(output is string);
            Assert.AreEqual((string)output, "42");
        }

        [TestMethod]
        public void TestFaultyStringToIntConversion()
        {
            var conv = new IntStringConverter();
            object output = conv.ConvertBack((object)"4d2", null, 42, null);
            Assert.IsNotNull(output);
            Assert.IsTrue(output is int);
            Assert.AreEqual((int)output, 42);
        }

    }
}
