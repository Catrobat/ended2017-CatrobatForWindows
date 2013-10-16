using System;
using System.IO;
using System.Xml.Linq;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.Utilities;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.VersionConverter;
using Catrobat.IDE.Tests.Misc;
using Catrobat.IDE.Tests.Misc.Storage;
using Catrobat.IDE.Tests.SampleData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Tests.Data
{
    [TestClass]
    public class DataReadingTests
    {
                [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }

        //[TestMethod]
        //public void DataReadingTest1()
        //{
        //    XDocument xDocument = SampleLoader.LoadSampleXDocument("DataReadingTests/test_code1");
        //    CatrobatVersionConverter.ConvertVersions("0.8", "Win0.80", xDocument);

        //    var writer = new XmlStringWriter();
        //    xDocument.Save(writer, SaveOptions.None);

        //    var xml = writer.GetStringBuilder().ToString();
        //    var project = new Project(xml);

        //    // TODO: check project if it is correct!
        //}

        //[TestMethod]
        //public void DataReadingTestCatrobatV091()
        //{
        //    XDocument xDocument = SampleLoader.LoadSampleXDocument("DataReadingTests/testCatrobat091");
        //    CatrobatVersionConverter.ConvertVersions("0.91", "Win0.80", xDocument);

        //    var writer = new XmlStringWriter();
        //    xDocument.Save(writer, SaveOptions.None);

        //    var xml = writer.GetStringBuilder().ToString();
        //    var project = new Project(xml);

        //    // TODO: check project if it is correct!
        //}
        
    }
}
