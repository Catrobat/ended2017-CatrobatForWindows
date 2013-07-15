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
    public class VersionConverter08ToWin08Tests
    {
        [ClassInitialize()]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_ObjectReferences()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/08_Win08/VersionConverterTest_08_to_Win08_ObjectReferences_Input");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/08_Win08/VersionConverterTest_08_to_Win08_ObjectReferences_Output");

            CatrobatVersionConverter.Convert("0.8", "Win0.8", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_ConvertBack_ObjectReferences()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/08_Win08/VersionConverterTest_08_to_Win08_ObjectReferences_Output");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/08_Win08/VersionConverterTest_08_to_Win08_ObjectReferences_Input");

            CatrobatVersionConverter.Convert("Win0.8", "0.8", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }
    }
}
