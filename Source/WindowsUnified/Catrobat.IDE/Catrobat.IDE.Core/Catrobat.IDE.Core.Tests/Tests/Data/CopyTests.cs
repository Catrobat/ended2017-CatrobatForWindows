using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Tests.Misc;
using Catrobat.IDE.Core.Tests.Services.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.Data
{
    [TestClass]
    public class CopyTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }


        [Obsolete("This test adds cloned sprites to both projects (useless)!")]
        [TestMethod]
        public async Task CopySprite()
        {
            const string savePath1 = "/CopyTest1/project.xml";
            const string savePath2 = "/CopyTest1/project.xml";

            ITestProgramGenerator projectgenerator = new ProjectGeneratorReflection();
            var project1 = projectgenerator.GenerateProgram();

            var project2 = new Program();
            project2.Name = project1.Name;
            project2.Description = project1.Description;
            project2.UploadHeader = project1.UploadHeader;
            project2.Sprites = project1.Sprites;

            var sprites = project1.Sprites;
            var spritesToAdd = new List<Sprite>();
            foreach (var sprite in sprites)
                spritesToAdd.Add(await sprite.CloneAsync(project1));

            foreach (var sprite in spritesToAdd)
                project2.Sprites.Add(sprite);

            project2.GlobalVariables = project1.GlobalVariables;

            await project1.Save(savePath1);
            await project2.Save(savePath2);

            string xml1;
            string xml2;
            using (IStorage storage = new StorageTest())
            {
                xml1 = storage.ReadTextFile(savePath1);
                xml2 = storage.ReadTextFile(savePath2);
            }

            var document1 = XDocument.Load(new StringReader(xml1));
            var document2 = XDocument.Load(new StringReader(xml2));

            XmlDocumentComparer.Compare(document1, document2);
        }
    }
}
