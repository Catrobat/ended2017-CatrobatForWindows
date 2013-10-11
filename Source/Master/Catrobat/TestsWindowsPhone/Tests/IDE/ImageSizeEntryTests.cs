using Catrobat.IDE.Phone.Controls.Misc;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Catrobat.IDE.Phone.Tests.Tests.IDE
{
    [TestClass]
    public class ImageSizeEntrytests
    {
        [TestMethod]
        public void GetNewImageWidthTest()
        {
            Assert.AreEqual(500, ImageSizeEntry.GetNewImageWidth(ImageSize.Small, 900, 600));

            Assert.AreEqual(900, ImageSizeEntry.GetNewImageWidth(ImageSize.Medium, 900, 500));

            Assert.AreEqual(1500, ImageSizeEntry.GetNewImageWidth(ImageSize.Large, 3600, 900));

            Assert.AreEqual(3600, ImageSizeEntry.GetNewImageWidth(ImageSize.FullSize, 3600, 900));
        }

        [TestMethod]
        public void GetNewImageHeightTest()
        {
            Assert.AreEqual(277, ImageSizeEntry.GetNewImageHeight(ImageSize.Small, 900, 500));

            Assert.AreEqual(500, ImageSizeEntry.GetNewImageHeight(ImageSize.Medium, 900, 500));

            Assert.AreEqual(1500, ImageSizeEntry.GetNewImageHeight(ImageSize.Large, 600, 3600));

            Assert.AreEqual(900, ImageSizeEntry.GetNewImageHeight(ImageSize.FullSize, 3600, 900));
        }


    }
}
