using Catrobat.IDE.Core.VersionConverter;
using Catrobat.IDE.Tests.Misc;
using Catrobat.IDE.Tests.SampleData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Tests.Data
{
    [TestClass]
    public class VersionConverter091ToWin091Tests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }

        [TestMethod, TestCategory("GatedTests")]
        public void ObjectReferences()
        {
            TestSampleData("Converter/091_Win091/VersionConverterTest_08_to_Win08_ObjectReferences");
        }

        [TestMethod, TestCategory("GatedTests")]
        public void SoundReferences()
        {
            TestSampleData("Converter/091_Win091/VersionConverterTest_08_to_Win08_SoundReferences");
        }

        [TestMethod, TestCategory("GatedTests")]
        public void LookReferences()
        {
            TestSampleData("Converter/091_Win091/VersionConverterTest_08_to_Win08_LookReferences");
        }

        [TestMethod, TestCategory("GatedTests")]
        public void GlobalVariableReferences()
        {
            TestSampleData("Converter/091_Win091/VersionConverterTest_08_to_Win08_GlobalVariableReferences");
        }

        [TestMethod, TestCategory("GatedTests")]
        public void LocalVariableReferences()
        {
            TestSampleData("Converter/091_Win091/VersionConverterTest_08_to_Win08_LocalVariableReferences");
        }

        #region References in Bricks

        [TestMethod, TestCategory("GatedTests")]
        public void PointToBrickReferences()
        {
            TestSampleData("Converter/091_Win091/VersionConverterTest_08_to_Win08_PointTo");
        }


        [TestMethod, TestCategory("GatedTests")]
        public void ForeverBrickReferences()
        {
            TestSampleData("Converter/091_Win091/VersionConverterTest_08_to_Win08_Forever");
        }

        [TestMethod, TestCategory("GatedTests")]
        public void RepeatBrickReferences()
        {
            TestSampleData("Converter/091_Win091/VersionConverterTest_08_to_Win08_Repeat");
        }

        [TestMethod, TestCategory("GatedTests")]
        public void IfLoginBeginBrickReferences()
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
