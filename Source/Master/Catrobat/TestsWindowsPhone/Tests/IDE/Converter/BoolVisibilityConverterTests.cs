using System.Windows;
using System.Windows.Media;
using Catrobat.IDE.Phone.Controls.Buttons;
using Catrobat.IDE.Phone.Converters;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Catrobat.IDE.Phone.Tests.Tests.IDE.Converter
{
    [TestClass]
    public class BoolVisibilityConverterTests
    {
        [TestMethod]
        public void TestConversion()
        {
            var conv = new BoolVisibilityConverter();
            object output = conv.Convert(false, null, null, null);
            Assert.IsNotNull(output);
            Assert.AreEqual(Visibility.Collapsed, output);
        }

        [TestMethod]
        public void TestBackConversion()
        {
            var conv = new BoolVisibilityConverter();
            object output = conv.ConvertBack(Visibility.Visible, null, null, null);
            Assert.AreEqual(null, output);
        }

        [TestMethod]
        public void TestFaultyConversion()
        {
            var conv = new BoolVisibilityConverter();
            object output = conv.Convert("NotValid", null, null, null);
            Assert.IsNotNull(output);
            Assert.AreEqual(Visibility.Collapsed, output);
        }
    }
}
