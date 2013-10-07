using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Catrobat.Core;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Misc.Storage;
using Catrobat.Core.CatrobatObjects;
using Catrobat.Core.Services.Common;
using Catrobat.TestsCommon.Misc;
using Catrobat.TestsCommon.Misc.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.TestsCommon.Tests.Data
{
    [TestClass]
    public class CopyTests
    {
        [ClassInitialize()]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }

        [TestMethod]
        public void CopySprite()
        {
            const string savePath1 = "/CopyTest1/project.xml";
            const string savePath2 = "/CopyTest1/project.xml";

            var project1 = ProjectGenerator.GenerateProject();

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
