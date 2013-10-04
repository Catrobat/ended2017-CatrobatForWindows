using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using System.Collections.Generic;
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

            CatrobatVersionConverter.SetConverterDirections("0.80", "Win0.80", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_SoundReferences()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/08_Win08/VersionConverterTest_08_to_Win08_SoundReferences_Input");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/08_Win08/VersionConverterTest_08_to_Win08_SoundReferences_Output");

            CatrobatVersionConverter.SetConverterDirections("0.80", "Win0.80", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_LookReferences()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/08_Win08/VersionConverterTest_08_to_Win08_LookReferences_Input");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/08_Win08/VersionConverterTest_08_to_Win08_LookReferences_Output");

            CatrobatVersionConverter.SetConverterDirections("0.80", "Win0.80", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_GlobalVariableReferences()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/08_Win08/VersionConverterTest_08_to_Win08_GlobalVariableReferences_Input");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/08_Win08/VersionConverterTest_08_to_Win08_GlobalVariableReferences_Output");

            CatrobatVersionConverter.SetConverterDirections("0.80", "Win0.80", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_LocalVariableReferences()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/08_Win08/VersionConverterTest_08_to_Win08_LocalVariableReferences_Input");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/08_Win08/VersionConverterTest_08_to_Win08_LocalVariableReferences_Output");

            CatrobatVersionConverter.SetConverterDirections("0.80", "Win0.80", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        #region References in Bricks

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_PointToBrickReferences()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/08_Win08/VersionConverterTest_08_to_Win08_PointTo_Input");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/08_Win08/VersionConverterTest_08_to_Win08_PointTo_Output");

            CatrobatVersionConverter.SetConverterDirections("0.80", "Win0.80", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }


        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_ForeverBrickReferences()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/08_Win08/VersionConverterTest_08_to_Win08_Forever_Input");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/08_Win08/VersionConverterTest_08_to_Win08_Forever_Output");

            CatrobatVersionConverter.SetConverterDirections("0.80", "Win0.80", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_RepeatBrickReferences()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/08_Win08/VersionConverterTest_08_to_Win08_Repeat_Input");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/08_Win08/VersionConverterTest_08_to_Win08_Repeat_Output");

            CatrobatVersionConverter.SetConverterDirections("0.80", "Win0.80", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_IfLoginBeginBrickReferences()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/08_Win08/VersionConverterTest_08_to_Win08_IfLogicBegin_Input");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/08_Win08/VersionConverterTest_08_to_Win08_IfLogicBegin_Output");

            CatrobatVersionConverter.SetConverterDirections("0.80", "Win0.80", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        #endregion

        #region Examples from PocketCode.com

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_Compass()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/08_Win08/PracticalTests/VersionConverterTest_08_to_Win08_Compass_Input");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/08_Win08/PracticalTests/VersionConverterTest_08_to_Win08_Compass_Output");

            CatrobatVersionConverter.SetConverterDirections("0.80", "Win0.80", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_Wake_Up()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/08_Win08/PracticalTests/VersionConverterTest_08_to_Win08_Wake_Up_Input");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/08_Win08/PracticalTests/VersionConverterTest_08_to_Win08_Wake_Up_Output");

            CatrobatVersionConverter.SetConverterDirections("0.80", "Win0.80", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_Drums()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/08_Win08/PracticalTests/VersionConverterTest_08_to_Win08_Drums_Input");
            XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/08_Win08/PracticalTests/VersionConverterTest_08_to_Win08_Drums_Output");

            CatrobatVersionConverter.SetConverterDirections("0.80", "Win0.80", actualDocument);
            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }


        #endregion

    }
}
