using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Tests.Misc;
using Catrobat.IDE.Tests.Misc.Storage;
using Catrobat.IDE.Tests.Tests.Misc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Tests.Data
{
    [TestClass]
    public class CopyTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }


        [TestMethod, TestCategory("GatedTests")]
        public void CopySprite()
        {
            const string savePath1 = "/CopyTest1/project.xml";
            const string savePath2 = "/CopyTest1/project.xml";

            ITestProjectGenerator projectgenerator = new ProjectGeneratorReflection();
            var project1 = projectgenerator.GenerateProject();

            var project2 = new Project();
            project2.ProjectHeader = project1.ProjectHeader;
            project2.SpriteList = project1.SpriteList;

            var sprites = project1.SpriteList.Sprites;
            var spritesToAdd = new List<Sprite>();
            foreach (var sprite in sprites)
                spritesToAdd.Add(sprite.Copy() as Sprite);

            foreach (var sprite in spritesToAdd)
                project2.SpriteList.Sprites.Add(sprite);

            project2.VariableList = project1.VariableList;

            XmlParserTempProjectHelper.Project = project1;
            project1.Save(savePath1);
            XmlParserTempProjectHelper.Project = project2;
            project2.Save(savePath2);

            string xml1;
            string xml2;
            using (IStorage storage = new StorageTest())
            {
                xml1 = storage.ReadTextFile(savePath1);
                xml2 = storage.ReadTextFile(savePath2);
            }

            var document1 = XDocument.Load(new StringReader(xml1));
            var document2 = XDocument.Load(new StringReader(xml2));

            XmlDocumentCompare.Compare(document1, document2);
        }
    }
}
