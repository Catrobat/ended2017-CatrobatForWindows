using System;
using System.IO;
using System.Xml.Linq;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Tests.Misc;
using Catrobat.IDE.Tests.Misc.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Tests.Data
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
        public void ReflectionWriteReadTest1()
        {
            const string savePath = "/ReflectionWriteReadTest1/project.xml";

            var project1 = ProjectGenerator.GenerateProject();

            project1.Save(savePath);

            string xml1;
            using (IStorage storage = new StorageTest())
            {
                xml1 = storage.ReadTextFile(savePath);
            }

            var project2 = new Project(xml1);
            Assert.IsTrue(project1.Equals(project2));
        }

        
    }
}
