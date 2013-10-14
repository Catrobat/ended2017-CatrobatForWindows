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
    public class VersionConverter091ToWin091PracticalTests
    {
        [ClassInitialize()]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }

        #region Examples from PocketCode.com

        //[TestMethod]
        //public void CatrobatVersionConverterTest_Convert_Compass()
        //{
        //    XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/VersionConverterTest_08_to_Win08_Compass_Input");
        //    XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/VersionConverterTest_08_to_Win08_Compass_Output");

        //    CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
        //    XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        //}

        //[TestMethod]
        //public void CatrobatVersionConverterTest_Convert_Wake_Up()
        //{
        //    XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/VersionConverterTest_08_to_Win08_Wake_Up_Input");
        //    XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/VersionConverterTest_08_to_Win08_Wake_Up_Output");

        //    CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
        //    XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        //}

        [TestMethod]
        public void CatrobatVersionConverterTest_PocketCode1()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test1Input");
            //XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test1Output");

            var error = CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            Assert.AreEqual(CatrobatVersionConverter.VersionConverterError.NoError ,error);
            var xml = actualDocument.ToString();
            var project = new Project(xml);
            //XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }


        [TestMethod]
        public void CatrobatVersionConverterTest_PocketCode2()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test2Input");
            //XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test2Output");

            var error = CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            Assert.AreEqual(CatrobatVersionConverter.VersionConverterError.NoError, error);
            var xml = actualDocument.ToString();
            var project = new Project(xml);
            //XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_PocketCode3()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test3Input");
            //XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test3Output");

            var error = CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            Assert.AreEqual(CatrobatVersionConverter.VersionConverterError.NoError, error);
            var xml = actualDocument.ToString();
            var project = new Project(xml);
            //XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_PocketCode4()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test4Input");
            //XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test4Output");

            var error = CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            Assert.AreEqual(CatrobatVersionConverter.VersionConverterError.NoError, error);
            var xml = actualDocument.ToString();
            var project = new Project(xml);
            //XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_PocketCode5()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test5Input");
            //XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test5Output");

            var error = CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            Assert.AreEqual(CatrobatVersionConverter.VersionConverterError.NoError, error);
            var xml = actualDocument.ToString();
            var project = new Project(xml);
            //XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_PocketCode6()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test6Input");
            //XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test6Output");

            var error = CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            Assert.AreEqual(CatrobatVersionConverter.VersionConverterError.NoError, error);
            var xml = actualDocument.ToString();
            var project = new Project(xml);
            //XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_PocketCode7()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test7Input");
            //XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test7Output");

            var error = CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            Assert.AreEqual(CatrobatVersionConverter.VersionConverterError.NoError, error);
            var xml = actualDocument.ToString();
            var project = new Project(xml);
            //XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_PocketCode8()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test8Input");
            //XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test8Output");

            var error = CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            Assert.AreEqual(CatrobatVersionConverter.VersionConverterError.NoError, error);
            var xml = actualDocument.ToString();
            var project = new Project(xml);
            //XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_PocketCode9()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test9Input");
            //XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test9Output");

            var error = CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            Assert.AreEqual(CatrobatVersionConverter.VersionConverterError.NoError, error);
            var xml = actualDocument.ToString();
            var project = new Project(xml);
            //XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_PocketCode10()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test10Input");
            //XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test10Output");

            var error = CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            Assert.AreEqual(CatrobatVersionConverter.VersionConverterError.NoError, error);
            var xml = actualDocument.ToString();
            var project = new Project(xml);
            //XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_PocketCode11()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test11Input");
            //XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test11Output");

            var error = CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            Assert.AreEqual(CatrobatVersionConverter.VersionConverterError.NoError, error);
            var xml = actualDocument.ToString();
            var project = new Project(xml);
            //XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_PocketCode12()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test12Input");
            //XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test12Output");

            var error = CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            Assert.AreEqual(CatrobatVersionConverter.VersionConverterError.NoError, error);
            var xml = actualDocument.ToString();
            var project = new Project(xml);
            //XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_PocketCode13()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test13Input");
            //XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test13Output");

            var error = CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            Assert.AreEqual(CatrobatVersionConverter.VersionConverterError.NoError, error);
            var xml = actualDocument.ToString();
            var project = new Project(xml);
            //XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_PocketCode14()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test14Input");
            //XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test14Output");

            var error = CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            Assert.AreEqual(CatrobatVersionConverter.VersionConverterError.NoError, error);
            var xml = actualDocument.ToString();
            var project = new Project(xml);
            //XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_PocketCode15()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test15Input");
            //XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test15Output");

            var error = CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            Assert.AreEqual(CatrobatVersionConverter.VersionConverterError.NoError, error);
            var xml = actualDocument.ToString();
            var project = new Project(xml);
            //XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        [TestMethod]
        public void CatrobatVersionConverterTest_PocketCode16()
        {
            XDocument actualDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test16Input");
            //XDocument expectedDocument = SampleLoader.LoadSampleXDocument("Converter/091_Win091/PracticalTests/Test16Output");

            var error = CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            Assert.AreEqual(CatrobatVersionConverter.VersionConverterError.NoError, error);
            var xml = actualDocument.ToString();
            var project = new Project(xml);
            //XmlDocumentCompare.Compare(expectedDocument, actualDocument);
        }

        #endregion

    }
}
