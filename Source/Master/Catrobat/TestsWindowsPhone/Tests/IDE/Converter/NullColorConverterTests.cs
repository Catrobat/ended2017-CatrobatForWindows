using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Catrobat.IDE.Phone.Converters;
using Catrobat.IDE.Phone.Converters.NativeConverters;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Catrobat.IDE.Phone.Tests.Tests.IDE.Converter
{
    [TestClass]
    public class NullColorConverterTests
    {
        [TestMethod]
        public void TestNullColorConverter1()
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                var conv = new NullColorConverter();
                object output = conv.ConvertBack(null, null, null, null);
                Assert.IsNull(output);
            });
        }

        [TestMethod]
        public void TestNullColorConverter2()
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                var conv = new NullColorConverter();
                object output = conv.Convert(null, null, null, null);
                Assert.IsNotNull(output);
                Assert.IsTrue(output is Brush);
                Assert.AreEqual(Application.Current.Resources["PhoneAccentBrush"], output);
            });
        }

        [TestMethod]
        public void TestNullColorConverter3()
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                var conv = new NullColorConverter();
                object output = conv.Convert(this, null, null, null);
                Assert.IsNotNull(output);
                Assert.IsTrue(output is Brush);
                Assert.AreEqual(Application.Current.Resources["PhoneTextBoxForegroundColor"], output);
            });
        }
    }
}
