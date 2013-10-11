using System.Windows;
using System.Windows.Media;
using Catrobat.IDE.Phone.Controls.Buttons;
using Catrobat.IDE.Phone.Converters;
using Catrobat.IDE.Phone.Themes;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Catrobat.TestsWindowsPhone.Tests.IDE.Converter
{
    [TestClass]
    public class BoolVisibilityNegativeConverterTests
    {
        [TestMethod]
        public void TestConversion()
        {
            var conv = new BoolVisibilityNegativeConverter();
            object output = conv.Convert(false, null, null, null);
            Assert.AreEqual(Visibility.Visible, output);
        }

        [TestMethod]
        public void TestBackConversion()
        {
            var conv = new BoolVisibilityNegativeConverter();
            object output = conv.ConvertBack(Visibility.Collapsed, null, null, null);
            Assert.AreEqual(null, output);
        }

        [TestMethod]
        public void TestFaultyConversion()
        {
            var conv = new BoolVisibilityNegativeConverter();
            object output = conv.Convert("NotValid", null, null, null);
            Assert.AreEqual(Visibility.Collapsed, output);
        }
    }
}
