﻿using System.Threading.Tasks;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Xml.Converter;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Tests.Extensions;
using Catrobat.IDE.Tests.Misc;
using Catrobat.IDE.Tests.Services.Storage;
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

        [TestMethod, TestCategory("GatedTests")]
        public async Task ReflectionWriteReadTest1()
        {
            const string savePath = "/ReflectionWriteReadTest1/project.xml";

            ITestProjectGenerator projectgenerator = new ProjectGeneratorReflection();
            var project1 = projectgenerator.GenerateProject();

            await project1.Save(savePath);

            string xml1;
            using (IStorage storage = new StorageTest())
            {
                xml1 = storage.ReadTextFile(savePath);
            }

            var project2 = new XmlProjectConverter().Convert(new XmlProject(xml1));

            ModelAssert.AreTestEqual(project1, project2);
        }

        
    }
}