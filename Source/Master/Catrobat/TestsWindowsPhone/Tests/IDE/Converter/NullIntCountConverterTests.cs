using System.Windows;
using System.Windows.Media;
using Catrobat.IDEWindowsPhone.Controls.Buttons;
using Catrobat.IDEWindowsPhone.Converters;
using Catrobat.IDEWindowsPhone.Themes;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Catrobat.TestsWindowsPhone.Tests.IDE.Converter
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
            Assert.IsNotNull(output);
            Assert.AreEqual(null, output);
        }
    }
}
