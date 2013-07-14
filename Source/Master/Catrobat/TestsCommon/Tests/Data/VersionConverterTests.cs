using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using System.Collections.Generic;
using Catrobat.Core.Converter;
using Catrobat.Core.VersionConverter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.TestsCommon.Misc;
using Catrobat.TestsCommon.SampleData;

namespace Catrobat.TestsCommon.Tests.Data
{
    [TestClass]
    public class VersionConverterTests
    {
        [ClassInitialize()]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert1Test()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/VersionConverterTest1Input");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/VersionConverterTest1Output");

            CatrobatVersionConverter.Convert("1.0", "Win1.0", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_ConvertBack1Test()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/VersionConverterTest1Output");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/VersionConverterTest1Input");

            CatrobatVersionConverter.Convert("Win1.0", "1.0", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }
    }
}
