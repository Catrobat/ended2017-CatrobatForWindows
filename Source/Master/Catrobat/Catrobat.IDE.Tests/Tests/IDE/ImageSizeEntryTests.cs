using Catrobat.IDE.Core.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Tests.IDE
{
    [TestClass]
    public class ImageSizeEntrytests
    {
        [TestMethod, TestCategory("GuardedTests")]
        public void GetNewImageWidthTest()
        {
            Assert.AreEqual(400, ImageSizeEntry.GetNewImageWidth(ImageSize.Small, 900, 600));

            Assert.AreEqual(800, ImageSizeEntry.GetNewImageWidth(ImageSize.Medium, 900, 500));

            Assert.AreEqual(1200, ImageSizeEntry.GetNewImageWidth(ImageSize.Large, 3600, 900));

            Assert.AreEqual(3600, ImageSizeEntry.GetNewImageWidth(ImageSize.FullSize, 3600, 900));
        }

        [TestMethod, TestCategory("GuardedTests")]
        public void GetNewImageHeightTest()
        {
            Assert.AreEqual(222, ImageSizeEntry.GetNewImageHeight(ImageSize.Small, 900, 500));

            Assert.AreEqual(444, ImageSizeEntry.GetNewImageHeight(ImageSize.Medium, 900, 500));

            Assert.AreEqual(1200, ImageSizeEntry.GetNewImageHeight(ImageSize.Large, 600, 3600));

            Assert.AreEqual(900, ImageSizeEntry.GetNewImageHeight(ImageSize.FullSize, 3600, 900));
        }
    }
}
