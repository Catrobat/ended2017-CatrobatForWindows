using Catrobat.IDE.Core.UI.Converters;
using Catrobat.IDE.Core.UI.PortableUI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Tests.IDE.Converter
{
    [TestClass]
    public class BoolVisibilityConverterTests
    {
        [TestMethod, TestCategory("GuardedTests")]
        public void TestConversion()
        {
            var conv = new BoolVisibilityConverter();
            object output = conv.Convert(false, null, null, null);
            Assert.IsNotNull(output);
            Assert.AreEqual(PortableVisibility.Collapsed, output);
        }

        [TestMethod, TestCategory("GuardedTests")]
        public void TestBackConversion()
        {
            var conv = new BoolVisibilityConverter();
            object output = conv.ConvertBack(PortableVisibility.Visible, null, null, null);
            Assert.AreEqual(null, output);
        }

        [TestMethod, TestCategory("GuardedTests")]
        public void TestFaultyConversion()
        {
            var conv = new BoolVisibilityConverter();
            object output = conv.Convert("NotValid", null, null, null);
            Assert.IsNotNull(output);
            Assert.AreEqual(PortableVisibility.Collapsed, output);
        }
    }
}
