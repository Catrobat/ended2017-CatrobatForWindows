using Catrobat.IDE.Core.UI.Converters;
using Catrobat.IDE.Core.UI.PortableUI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.UI.Converters
{
    [TestClass]
    public class BoolVisibilityNegativeConverterTests
    {
        [TestMethod, TestCategory("UI.Converters")]
        public void TestConversion()
        {
            var conv = new BoolVisibilityNegativeConverter();
            object output = conv.Convert(false, null, null, null);
            Assert.AreEqual(PortableVisibility.Visible, output);
        }

        [TestMethod, TestCategory("UI.Converters")]
        public void TestBackConversion()
        {
            var conv = new BoolVisibilityNegativeConverter();
            object output = conv.ConvertBack(PortableVisibility.Collapsed, null, null, null);
            Assert.AreEqual(null, output);
        }

        [TestMethod, TestCategory("UI.Converters")]
        public void TestFaultyConversion()
        {
            var conv = new BoolVisibilityNegativeConverter();
            object output = conv.Convert("NotValid", null, null, null);
            Assert.AreEqual(PortableVisibility.Collapsed, output);
        }
    }
}
