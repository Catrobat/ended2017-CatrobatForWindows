using System.Windows;
using System.Windows.Media;
using Catrobat.IDEWindowsPhone.Converters;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Catrobat.TestsWindowsPhone.Tests.IDE.Converter
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
            Assert.AreEqual((int)output, 4200);
        }

        [TestMethod]
        public void TestMillisecondToSecondStringConversion()
        {
            var conv = new SecondStringMillisecondConverter();
            object output = conv.Convert((object)4200, null, null, null);
            Assert.IsNotNull(output);
            Assert.IsTrue(output is string);
            Assert.AreEqual((string)output, "4.2");
        }

        [TestMethod]
        public void TestFaultySecondStringToMillisecondConversion()
        {
            var conv = new SecondStringMillisecondConverter();
            object output = conv.ConvertBack((object)"4d2", null, 4200, null);
            Assert.IsNotNull(output);
            Assert.IsTrue(output is int);
            Assert.AreEqual((int)output, 4200);
        }
    }
}
