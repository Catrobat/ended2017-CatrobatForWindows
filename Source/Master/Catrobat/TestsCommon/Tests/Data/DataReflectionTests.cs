using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Catrobat.Core.Objects.Costumes;
using Catrobat.Core.Objects.Formulas;
using Catrobat.Core.Objects.Scripts;
using Catrobat.Core.Objects.Sounds;
using Catrobat.Core.Objects.Variables;
using Catrobat.Core.Storage;
using Catrobat.TestsCommon.Misc;
using Catrobat.TestsCommon.Misc.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.TestsCommon.Tests.Data
{
    [TestClass]
    public class DataReflectionTests
    {
        [ClassInitialize()]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }

        [TestMethod]
        public void ReflectionWriteReadTest1()
        {
            const string savePath1 = "/ReflectionWriteReadTest1/project.xml";
            const string savePath2 = "/ReflectionWriteReadTest1/project.xml";

            var project1 = ProjectGenerator.GenerateProject();

            project1.Save(savePath1);

            string xml1 = null;
            using (IStorage storage = new StorageTest())
            {
                xml1 = storage.ReadTextFile(savePath1);
            }

            var project2 = new Project(xml1);
            project2.Save(savePath2);
            
            string xml2 = null;
            using (IStorage storage = new StorageTest())
            {
                xml2 = storage.ReadTextFile(savePath1);
            }

            var document1 = XDocument.Load(new StringReader(xml1));

            var document2 = XDocument.Load(new StringReader(xml2));

            XmlDocumentCompare.Compare(document1, document2);
        }

        
    }
}
