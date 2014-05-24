using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities;
using Catrobat.IDE.Core.VersionConverter;
using Catrobat.IDE.Core.Xml.Converter;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Tests.Misc;
using Catrobat.IDE.Tests.SampleData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Catrobat.IDE.Tests.Tests.IDE.Xml
{
    /// <summary>Tests <see cref="XmlProjectConverter" />. </summary>
    [TestClass]
    public class XmlProjectConverterTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            //  needed for SampleLoader.LoadSampleXDocument
            TestHelper.InitializeTests();
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Xml"), TestCategory("GatedTests.Obsolete")]
        public void TestPocketCodeProjects()
        {
            var documents = Enumerable.Range(1, 16)
                .Select(i => "Converter/091_Win091/PracticalTests/Test" + i + "Input")
                .Select(SampleLoader.LoadSampleXDocument);
            foreach (var document in documents)
            {
                CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", document);
                var xml = document.ToString();

                var project = new XmlProject(xml);
                TestConvert(new XmlProjectConverter(), project);
            }
        }

        #region Helpers

        private static void TestConvert(XmlProjectConverter converter, XmlProject project)
        {
            var actual = converter.ConvertBack(converter.Convert(project));

            var expectedDocument = project.CreateXML();

            var actualDocument = actual.CreateXML();

            var writer = new XmlStringWriter();
            expectedDocument.Save(writer, SaveOptions.None);
            var expectedXml = writer.GetStringBuilder().ToString();

            writer = new XmlStringWriter();
            actualDocument.Save(writer, SaveOptions.None);
            var actualXml = writer.GetStringBuilder().ToString();

            Assert.AreEqual(expectedXml, actualXml);
        }

        #endregion
    }
}
