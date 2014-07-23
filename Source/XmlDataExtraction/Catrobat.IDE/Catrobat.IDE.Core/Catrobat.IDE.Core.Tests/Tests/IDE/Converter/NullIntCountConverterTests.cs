using Catrobat.IDE.Core.UI.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.IDE.Converter
{
    [TestClass]
    public class NullIntCountConverterTests
    {
        [TestMethod, TestCategory("GatedTests")]
        public void TestConversion1()
        {
            var conv = new NullIntCountConverter();
            object output = conv.Convert(null, null, null, null);
            Assert.IsNotNull(output);
            Assert.AreEqual(0, output);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void TestConversion2()
        {
            var conv = new NullIntCountConverter();
            object output = conv.Convert(new object(), null, null, null);
            Assert.IsNotNull(output);
            Assert.AreEqual(1, output);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void TestBackConversion()
        {
            var conv = new NullIntCountConverter();
            object output = conv.ConvertBack(0, null, null, null);
            Assert.AreEqual(null, output);
        }
    }
}
