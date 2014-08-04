using System;
using Catrobat.IDE.Core.UI.Converters;
using Catrobat.IDE.Core.UI.PortableUI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.UI.Converters
{
    [TestClass]
    public class UnixTimeDateTimeConverterTests
    {
        [TestMethod]
        public void TestUnixTimeToDateTimeConversion()
        {
            var conv = new UnixTimeDateTimeConverter();
            object output = conv.Convert((object)1395419262d, null, null, null);
            Assert.IsNotNull(output);
            var date = new DateTime(2014, 3, 21, 16, 27, 42, 0);
            Assert.AreEqual(date, output);
        }

        [TestMethod]
        public void TestDateTimeToUnixTimeConversion()
        {
            var conv = new UnixTimeDateTimeConverter();
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            object output = conv.ConvertBack(origin, null, null, null);
            Assert.AreEqual(null, output);
        }

        [TestMethod]
        public void TestFaultyUnixTimeToDateTimeConversion()
        {
            var conv = new UnixTimeDateTimeConverter();
            object output = conv.Convert((object)1395419262f, null, null, null);
            Assert.IsNotNull(output);
            var date = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            Assert.AreEqual(date, output);
        }
    }
}
