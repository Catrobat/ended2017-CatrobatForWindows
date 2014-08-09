using Catrobat.IDE.Core.Tests.Misc;
using Catrobat.IDE.Core.Tests.SampleData;
using Catrobat.IDE.Core.Xml.VersionConverter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.Data
{
    [TestClass]
    public class VersionConverter091ToWin091Tests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }

        [TestMethod, TestCategory("Data")]
        public void ObjectReferences()
        {
            TestSampleData("Converter/091_Win091/VersionConverterTest_08_to_Win08_ObjectReferences");
        }

        [TestMethod, TestCategory("Data")]
        public void SoundReferences()
        {
            TestSampleData("Converter/091_Win091/VersionConverterTest_08_to_Win08_SoundReferences");
        }

        [TestMethod, TestCategory("Data")]
        public void LookReferences()
        {
            TestSampleData("Converter/091_Win091/VersionConverterTest_08_to_Win08_LookReferences");
        }

        [TestMethod, TestCategory("Data")]
        public void GlobalVariableReferences()
        {
            TestSampleData("Converter/091_Win091/VersionConverterTest_08_to_Win08_GlobalVariableReferences");
        }

        [TestMethod, TestCategory("Data")]
        public void LocalVariableReferences()
        {
            TestSampleData("Converter/091_Win091/VersionConverterTest_08_to_Win08_LocalVariableReferences");
        }

        [TestMethod, TestCategory("Data")]
        public void PointToBrickReferences()
        {
            TestSampleData("Converter/091_Win091/VersionConverterTest_08_to_Win08_PointTo");
        }


        [TestMethod, TestCategory("Data")]
        public void ForeverBrickReferences()
        {
            TestSampleData("Converter/091_Win091/VersionConverterTest_08_to_Win08_Forever");
        }

        [TestMethod, TestCategory("Data")]
        public void RepeatBrickReferences()
        {
            TestSampleData("Converter/091_Win091/VersionConverterTest_08_to_Win08_Repeat");
        }

        [TestMethod, TestCategory("Data")]
        public void IfLoginBeginBrickReferences()
        {
            TestSampleData("Converter/091_Win091/VersionConverterTest_08_to_Win08_IfLogicBegin");
        }

        private void TestSampleData(string path)
        {
            var actualDocument = SampleLoader.LoadSampleXDocument(path + "_Input");
            var expectedDocument = SampleLoader.LoadSampleXDocument(path + "_Output");

            var error = CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            Assert.AreEqual(CatrobatVersionConverter.VersionConverterStatus.NoError, error);

            XmlDocumentComparer.Compare(expectedDocument, actualDocument);
        }

    }
}
