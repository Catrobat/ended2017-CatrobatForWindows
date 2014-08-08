using System.Threading.Tasks;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Tests.Extensions;
using Catrobat.IDE.Core.Tests.Misc;
using Catrobat.IDE.Core.Tests.SampleData.ProgramGenerators;
using Catrobat.IDE.Core.Tests.Services.Storage;
using Catrobat.IDE.Core.Xml.Converter;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.Data
{
    [TestClass]
    public class DataReflectionTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }

        [TestMethod]
        public async Task ReflectionWriteReadTest1()
        {
            const string savePath = "/ReflectionWriteReadTest1/project.xml";

            ITestProgramGenerator projectgenerator = new ProgramGeneratorReflection();
            var project1 = projectgenerator.GenerateProgram();

            await project1.Save(savePath);

            string xml1;
            using (IStorage storage = new StorageTest())
            {
                xml1 = storage.ReadTextFile(savePath);
            }

            var project2 = new XmlProjectConverter().Convert(new XmlProgram(xml1));

            ModelAssert.AreTestEqual(project1, project2);
        }

        
    }
}
