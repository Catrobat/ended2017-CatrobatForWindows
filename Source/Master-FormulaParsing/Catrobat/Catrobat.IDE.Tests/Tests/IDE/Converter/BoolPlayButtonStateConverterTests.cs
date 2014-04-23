using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.UI.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Tests.IDE.Converter
{
    [TestClass]
    public class BoolPlayButtonStateConverterTests
    {
        [TestMethod, TestCategory("GatedTests")]
        public void TestConversion()
        {
            var conv = new BoolPlayButtonStateConverter();
            object output = conv.Convert(false, null, null, null);
            Assert.IsNotNull(output);
            Assert.AreEqual(PlayPauseButtonState.Pause, output);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void TestBackConversion()
        {
            var conv = new BoolPlayButtonStateConverter();
            object output = conv.ConvertBack(PlayPauseButtonState.Play, null, null, null);
            Assert.IsNotNull(output);
            Assert.AreEqual(true, output);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void TestFaultyConversion()
        {
            var conv = new BoolPlayButtonStateConverter();
            object output = conv.Convert("NotValid", null, null, null);
            Assert.IsNotNull(output);
            Assert.AreEqual(PlayPauseButtonState.Pause, output);
        }
    }
}
