using Catrobat.IDE.Core.UI.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.UI.Converters
{
    [TestClass]
    public class FloatStringConverterTests
    {
        [TestMethod, TestCategory("UI.Converters")]
        public void TestStringToFloatConversion()
        {
            var conv = new FloatStringConverter();
            object output = conv.ConvertBack((object)"4.2", null, null, null);
            Assert.IsNotNull(output);
            Assert.IsTrue(output is float);
            Assert.AreEqual(4.2f, (float)output);
        }

        [TestMethod, TestCategory("UI.Converters")]
        public void TestFloatToStringConversion()
        {
            var conv = new FloatStringConverter();
            object output = conv.Convert((object)4.2f, null, null, null);
            Assert.IsNotNull(output);
            Assert.IsTrue(output is string);
            Assert.AreEqual("4.2", (string)output);
        }

        [TestMethod, TestCategory("UI.Converters")]
        public void TestFaultyStringToFloatConversion()
        {
            var conv = new FloatStringConverter();
            object output = conv.ConvertBack((object)"4d2", null, 42f, null);
            Assert.IsNotNull(output);
            Assert.IsTrue(output is float);
            Assert.AreEqual(42, (float)output);
        }
    }
}
