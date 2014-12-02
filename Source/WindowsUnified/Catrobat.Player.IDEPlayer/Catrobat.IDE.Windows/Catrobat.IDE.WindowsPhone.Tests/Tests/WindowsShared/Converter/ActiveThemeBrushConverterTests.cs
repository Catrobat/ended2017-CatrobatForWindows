using Windows.UI.Core;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.UI.Converters;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Catrobat.IDE.WindowsPhone.Tests.Tests.WindowsShared.Converter
{
    [TestClass]
    public class ActiveThemeBrushConverterTests
    {
        [TestMethod]
        public void TestConversion()
        {
            var dispatcher = Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher;

            dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
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
            var dispatcher = Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher;

            dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
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
            var dispatcher = Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher;

            dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
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
