using Catrobat.IDE.Core.UI.Converters;
using Catrobat.IDE.Core.UI.PortableUI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.IDE.Converter
{
    [TestClass]
    public class StringVisibilityConverterTests
    {
        [TestMethod]
        public void TestStringVisibilityConversion()
        {
            var conv = new StringVisibilityConverter();
            object output = conv.Convert("", null, null, null);
            Assert.IsNotNull(output);
            Assert.AreEqual(PortableVisibility.Visible, output);
        }

        [TestMethod]
        public void TestVisibilityStringConversion()
        {
            var conv = new StringVisibilityConverter();
            object output = conv.ConvertBack(PortableVisibility.Visible, null, null, null);
            Assert.AreEqual(null, output);
        }

        [TestMethod]
        public void TestFaultyStringVisibilityConversion()
        {
            var conv = new StringVisibilityConverter();
            object output = conv.Convert("NotEmptyString", null, null, null);
            Assert.IsNotNull(output);
            Assert.AreEqual(PortableVisibility.Collapsed, output);
        }
    }
}
