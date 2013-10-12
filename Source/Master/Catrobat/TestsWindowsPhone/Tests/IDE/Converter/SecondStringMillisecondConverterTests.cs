using System.Windows;
using System.Windows.Media;
using Catrobat.IDE.Phone.Converters;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Catrobat.IDE.Phone.Tests.Tests.IDE.Converter
{
    [TestClass]
    public class SecondStringMillisecondConverterTests
    {
        [TestMethod]
        public void TestSecondStringToMillisecondConversion()
        {
            var conv = new SecondStringMillisecondConverter();
            object output = conv.ConvertBack((object)"4.2", null, null, null);
            Assert.IsNotNull(output);
            Assert.IsTrue(output is int);
            Assert.AreEqual(4200, (int)output);
        }

        [TestMethod]
        public void TestMillisecondToSecondStringConversion()
        {
            var conv = new SecondStringMillisecondConverter();
            object output = conv.Convert((object)4200, null, null, null);
            Assert.IsNotNull(output);
            Assert.IsTrue(output is string);
            Assert.AreEqual("4.2", (string)output);
        }

        [TestMethod]
        public void TestFaultySecondStringToMillisecondConversion()
        {
            var conv = new SecondStringMillisecondConverter();
            object output = conv.ConvertBack((object)"4d2", null, 4200, null);
            Assert.IsNotNull(output);
            Assert.IsTrue(output is int);
            Assert.AreEqual(4200, (int)output);
        }
    }
}
