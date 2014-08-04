using Catrobat.IDE.Core.UI.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.UI.Converters
{
    [TestClass]
    public class NullIntCountConverterTests
    {
        [TestMethod]
        public void TestConversion1()
        {
            var conv = new NullIntCountConverter();
            object output = conv.Convert(null, null, null, null);
            Assert.IsNotNull(output);
            Assert.AreEqual(0, output);
        }

        [TestMethod]
        public void TestConversion2()
        {
            var conv = new NullIntCountConverter();
            object output = conv.Convert(new object(), null, null, null);
            Assert.IsNotNull(output);
            Assert.AreEqual(1, output);
        }

        [TestMethod]
        public void TestBackConversion()
        {
            var conv = new NullIntCountConverter();
            object output = conv.ConvertBack(0, null, null, null);
            Assert.AreEqual(null, output);
        }
    }
}
