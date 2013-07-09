using System.Windows;
using System.Windows.Media;
using Catrobat.IDEWindowsPhone.Controls.Buttons;
using Catrobat.IDEWindowsPhone.Converters;
using Catrobat.IDEWindowsPhone.Themes;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Catrobat.TestsWindowsPhone.Tests.IDE.Converter
{
    [TestClass]
    public class BoolPlayButtonStateConverterTests
    {
        [TestMethod]
        public void TestConversion()
        {
            var conv = new BoolPlayButtonStateConverter();
            object output = conv.Convert(false, null, null, null);
            Assert.IsNotNull(output);
            Assert.AreEqual(PlayPauseButtonState.Pause, output);
        }

        [TestMethod]
        public void TestBackConversion()
        {
            var conv = new BoolPlayButtonStateConverter();
            object output = conv.ConvertBack(PlayPauseButtonState.Play, null, null, null);
            Assert.IsNotNull(output);
            Assert.AreEqual(true, output);
        }

        [TestMethod]
        public void TestFaultyConversion()
        {
            var conv = new BoolPlayButtonStateConverter();
            object output = conv.Convert("NotValid", null, null, null);
            Assert.IsNotNull(output);
            Assert.AreEqual(output, PlayPauseButtonState.Pause);
        }
    }
}
