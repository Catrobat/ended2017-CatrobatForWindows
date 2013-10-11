using System.Windows;
using System.Windows.Media;
using Catrobat.IDE.Phone.Converters;
using Catrobat.IDE.Phone.Themes;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Catrobat.TestsWindowsPhone.Tests.IDE.Converter
{
    [TestClass]
    public class ActiveThemeBrushConverterTests
    {
        [TestMethod]
        public void TestConversion()
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                var theme = new Theme("", "");

                var conv = new ActiveThemeBrushConverter();
                object output = conv.Convert((object)theme, null, null, null);
                Assert.IsNotNull(output);
                Assert.IsTrue(output is Theme);
                Assert.AreEqual(theme, (Theme)output);
            });
        }

        [TestMethod]
        public void TestBackConversion()
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                var conv = new ActiveThemeBrushConverter();
                object output = conv.ConvertBack(null, null, null, null);
                Assert.IsNotNull(output);
                Assert.IsTrue(output is string);
                Assert.AreEqual(null, (string)output);
            });
        }

        [TestMethod]
        public void TestFaultyConversion()
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                var conv = new ActiveThemeBrushConverter();
                object output = conv.Convert((object)"NotATheme", null, null, null);
                Assert.IsNotNull(output);
                Assert.IsTrue(output is double);
                Assert.AreEqual(null, (double)output);
            });
        }
    }
}
