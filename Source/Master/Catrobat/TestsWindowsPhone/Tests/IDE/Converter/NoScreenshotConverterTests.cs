using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Catrobat.IDEWindowsPhone.Controls.Buttons;
using Catrobat.IDEWindowsPhone.Converters;
using Catrobat.IDEWindowsPhone.Themes;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Catrobat.TestsWindowsPhone.Tests.IDE.Converter
{
    [TestClass]
    public class NoScreenshotConverterTests
    {
        [TestMethod]
        public void TestConversion()
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                var conv = new NoScreenshotConverter();
                object output = conv.Convert(null, null, null, null);
                Assert.IsNotNull(output);
            });
        }

        [TestMethod]
        public void TestBackConversion()
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                var image = new BitmapImage();
                var conv = new NoScreenshotConverter();
                object output = conv.ConvertBack(image, null, null, null);
                Assert.IsNotNull(output);
                Assert.AreEqual(image, output);
            });
        }

        [TestMethod]
        public void TestFaultyConversion()
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                var conv = new NoScreenshotConverter();
                object output = conv.Convert("NotValid", null, null, null);
                Assert.IsNotNull(output);
                Assert.AreEqual(null, output);
            });
        }
    }
}
