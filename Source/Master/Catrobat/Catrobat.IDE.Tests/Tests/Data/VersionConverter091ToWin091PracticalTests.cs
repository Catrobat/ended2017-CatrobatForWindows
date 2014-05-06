using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.VersionConverter;
using Catrobat.IDE.Tests.Misc;
using Catrobat.IDE.Tests.SampleData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Tests.Data
{
    [TestClass]
    public class VersionConverter091ToWin091PracticalTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }

        #region Examples from PocketCode.com

        [TestMethod] //  TestCategory("GatedTests") TODO: Configure TFS
        public void PocketCode01()
        {
            TestSampleData("Converter/091_Win091/PracticalTests/Test1Input");
        }


        [TestMethod] //  TestCategory("GatedTests") TODO: Configure TFS
        public void PocketCode02()
        {
            TestSampleData("Converter/091_Win091/PracticalTests/Test2Input");
        }

        [TestMethod] //  TestCategory("GatedTests") TODO: Configure TFS
        public void PocketCode03()
        {
            TestSampleData("Converter/091_Win091/PracticalTests/Test3Input");
        }

        [TestMethod] //  TestCategory("GatedTests") TODO: Configure TFS
        public void PocketCode04()
        {
            TestSampleData("Converter/091_Win091/PracticalTests/Test4Input");
        }

        [TestMethod] //  TestCategory("GatedTests") TODO: Configure TFS
        public void PocketCode05()
        {
            TestSampleData("Converter/091_Win091/PracticalTests/Test5Input");
        }

        [TestMethod] //  TestCategory("GatedTests") TODO: Configure TFS
        public void PocketCode06()
        {
            TestSampleData("Converter/091_Win091/PracticalTests/Test6Input");
        }

        [TestMethod] //  TestCategory("GatedTests") TODO: Configure TFS
        public void PocketCode07()
        {
            TestSampleData("Converter/091_Win091/PracticalTests/Test7Input");
        }

        [TestMethod] //  TestCategory("GatedTests") TODO: Configure TFS
        public void PocketCode08()
        {
            TestSampleData("Converter/091_Win091/PracticalTests/Test8Input");
        }

        [TestMethod] //  TestCategory("GatedTests") TODO: Configure TFS
        public void PocketCode09()
        {
            TestSampleData("Converter/091_Win091/PracticalTests/Test9Input");
        }

        [TestMethod] //  TestCategory("GatedTests") TODO: Configure TFS
        public void PocketCode10()
        {
            TestSampleData("Converter/091_Win091/PracticalTests/Test10Input");
        }

        [TestMethod] //  TestCategory("GatedTests") TODO: Configure TFS
        public void PocketCode11()
        {
            TestSampleData("Converter/091_Win091/PracticalTests/Test11Input");
        }

        [TestMethod] //  TestCategory("GatedTests") TODO: Configure TFS
        public void PocketCode12()
        {
            TestSampleData("Converter/091_Win091/PracticalTests/Test12Input");
        }

        [TestMethod] //  TestCategory("GatedTests") TODO: Configure TFS
        public void PocketCode13()
        {
            TestSampleData("Converter/091_Win091/PracticalTests/Test13Input");
        }

        [TestMethod] //  TestCategory("GatedTests") TODO: Configure TFS
        public void PocketCode14()
        {
            TestSampleData("Converter/091_Win091/PracticalTests/Test14Input");
        }

        [TestMethod] //  TestCategory("GatedTests") TODO: Configure TFS
        public void PocketCode15()
        {
            TestSampleData("Converter/091_Win091/PracticalTests/Test15Input");
        }

        [TestMethod] //  TestCategory("GatedTests") TODO: Configure TFS
        public void PocketCode16()
        {
            TestSampleData("Converter/091_Win091/PracticalTests/Test16Input");
        }

        private void TestSampleData(string input, string output = null)
        {
            var actualDocument = SampleLoader.LoadSampleXDocument(input);

            var error = CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", actualDocument);
            Assert.AreEqual(CatrobatVersionConverter.VersionConverterError.NoError, error);

            var xml = actualDocument.ToString();
            var project = new Project(xml); // must not throw an exception

            if (output != null)
            {
                var expectedDocument = SampleLoader.LoadSampleXDocument(output);
                XmlDocumentComparer.Compare(expectedDocument, actualDocument);
            }
        }

        #endregion

    }
}
