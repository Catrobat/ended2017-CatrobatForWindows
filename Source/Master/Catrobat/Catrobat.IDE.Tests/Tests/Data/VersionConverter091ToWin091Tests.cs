using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using System.Collections.Generic;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.VersionConverter;
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
            TestSampleData("Converter/091_Win091/VersionConverterTest_08_to_Win08_ObjectReferences");
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_SoundReferences()
        {
            TestSampleData("Converter/091_Win091/VersionConverterTest_08_to_Win08_SoundReferences");
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_LookReferences()
        {
            TestSampleData("Converter/091_Win091/VersionConverterTest_08_to_Win08_LookReferences");
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_GlobalVariableReferences()
        {
            TestSampleData("Converter/091_Win091/VersionConverterTest_08_to_Win08_GlobalVariableReferences");
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_LocalVariableReferences()
        {
            TestSampleData("Converter/091_Win091/VersionConverterTest_08_to_Win08_LocalVariableReferences");
        }

        #region References in Bricks

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_PointToBrickReferences()
        {
            TestSampleData("Converter/091_Win091/VersionConverterTest_08_to_Win08_PointTo");
        }


        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_ForeverBrickReferences()
        {
            TestSampleData("Converter/091_Win091/VersionConverterTest_08_to_Win08_Forever");
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_RepeatBrickReferences()
        {
            TestSampleData("Converter/091_Win091/VersionConverterTest_08_to_Win08_Repeat");
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_Convert_IfLoginBeginBrickReferences()
        {
            TestSampleData("Converter/091_Win091/VersionConverterTest_08_to_Win08_IfLogicBegin");
        }

        #endregion

        private void TestSampleData(string path)
        {
            var actualDocument = SampleLoader.LoadSampleXDocument(path + "_Input");
            var expectedDocument = SampleLoader.LoadSampleXDocument(path + "_Output");

            var error = CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            Assert.AreEqual(CatrobatVersionConverter.VersionConverterError.NoError, error);

            XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

    }
}
