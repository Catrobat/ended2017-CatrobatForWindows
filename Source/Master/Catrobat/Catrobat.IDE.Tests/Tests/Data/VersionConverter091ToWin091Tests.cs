using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using System.Collections.Generic;
using Catrobat.Core.CatrobatObjects;
using Catrobat.Core.VersionConverter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Tests.Misc;
using Catrobat.IDE.Tests.SampleData;

namespace Catrobat.IDE.Tests.Tests.Data
{
    [TestClass]
    public class VersionConverter091ToWin091Tests
    {
        [ClassInitialize()]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_ObjectReferences()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_ObjectReferences_Input");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_ObjectReferences_Output");

            CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_SoundReferences()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_SoundReferences_Input");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_SoundReferences_Output");

            CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_LookReferences()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_LookReferences_Input");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_LookReferences_Output");

            CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_GlobalVariableReferences()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_GlobalVariableReferences_Input");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_GlobalVariableReferences_Output");

            CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_LocalVariableReferences()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_LocalVariableReferences_Input");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_LocalVariableReferences_Output");

            CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        #region References in Bricks

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_PointToBrickReferences()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_PointTo_Input");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_PointTo_Output");

            CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }


        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_ForeverBrickReferences()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_Forever_Input");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_Forever_Output");

            CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_RepeatBrickReferences()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_Repeat_Input");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_Repeat_Output");

            CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_IfLoginBeginBrickReferences()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_IfLogicBegin_Input");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/VersionConverterTest_08_to_Win08_IfLogicBegin_Output");

            CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        #endregion
    }
}
