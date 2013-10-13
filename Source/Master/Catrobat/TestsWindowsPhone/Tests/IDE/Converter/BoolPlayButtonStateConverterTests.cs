using System.Windows;
using System.Windows.Media;
using Catrobat.IDE.Phone.Controls.Buttons;
using Catrobat.IDE.Phone.Converters;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Catrobat.IDE.Phone.Tests.Tests.IDE.Converter
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
            Assert.AreEqual(PlayPauseButtonState.Pause, output);
        }
    }
}
