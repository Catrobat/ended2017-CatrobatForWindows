using System.Linq;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.TestsCommon.Misc;
using Catrobat.TestsCommon.SampleData;

namespace Catrobat.TestsCommon.Tests.Data
{
    [TestClass]
    public class XPathHelperTests
    {
        [ClassInitialize()]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }

        [TestMethod]
        public void GetElementTest()
        {
            XDocument document = SampleLoader.LoadSampleXDocument("Converter/XPathHelperTestInput");

            const string path1 = "../../../element1/element11[2]";

            var start1 = (from a in document.Descendants()
                          where a.Attribute("start") != null && a.Attribute("start").Value == "1"
                          select a).Single();

            var destination1 = (from a in document.Descendants()
                                where a.Attribute("destination") != null && a.Attribute("destination").Value == "1"
                                select a).Single();


            var foundElement = Catrobat.Core.Misc.XPathHelper.GetElement(start1, path1);
            Assert.AreEqual(destination1, foundElement);
        }

        [TestMethod]
        public void GetXPathTest()
        {
            XDocument document = SampleLoader.LoadSampleXDocument("Converter/XPathHelperTestInput");

            const string path1 = "../../../element1/element11[2]";

            var start1 = (from a in document.Descendants()
                          where a.Attribute("start") != null && a.Attribute("start").Value == "1"
                          select a).Single();

            var destination1 = (from a in document.Descendants()
                                where a.Attribute("destination") != null && a.Attribute("destination").Value == "1"
                                select a).Single();


            var foundPath = Catrobat.Core.Misc.XPathHelper.GetXPath(start1, destination1);
            Assert.AreEqual(path1, foundPath);
        }

        public void XPathEqualsTest1()
        {
            const string path1 = "../../../element1[1]/element11[2]";
            const string path2 = "../../../element1[1]/element11[2]";

            Assert.IsTrue(Catrobat.Core.Misc.XPathHelper.XPathEquals(path1, path2));
        }

        public void XPathEqualsTest2()
        {
            const string path1 = "../../../element2[1]/element11[2]";
            const string path2 = "../../../element1[1]/element11[2]";

            Assert.IsFalse(Catrobat.Core.Misc.XPathHelper.XPathEquals(path1, path2));
        }
    }
}
