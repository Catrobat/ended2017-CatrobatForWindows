using System.Linq;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Tests.Extensions;
using Catrobat.IDE.Core.Tests.Misc;
using Catrobat.IDE.Core.Tests.SampleData.ProgramGenerators;
using Catrobat.IDE.Core.Tests.Services.Storage;
using Catrobat.IDE.Core.XmlModelConvertion.Converters;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.Data
{
    [TestClass]
    public class DataReadingTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }

        [TestMethod, TestCategory("Data")]
        public async Task DataReadingTest_References()
        {
            IProgramGenerator projectGenerator = new ProgramGeneratorWhackAMole();
            var program = await projectGenerator.GenerateProgram("TestProgram", false);

            var programConverter = new ProgramConverter();
            var xmlProgram = programConverter.Convert(program);

            CheckProgram(xmlProgram);
            var newProgram = programConverter.Convert(xmlProgram);
            CheckProgram(newProgram);
        }

        [TestMethod, TestCategory("Data")]
        public async Task DataReadingTest_References1()
        {
            IProgramGenerator projectGenerator = new ProgramGeneratorWhackAMole();
            var program = await projectGenerator.GenerateProgram("TestProgram", false);

            var programConverter = new ProgramConverter();
            var xmlProgram = programConverter.Convert(program);

            //CheckProgram(program);
            //CheckProgram(xmlProgram);

            var xmlString = xmlProgram.ToXmlString();

            var newProgram = new XmlProgram(xmlString);
            CheckProgram(newProgram);
        }

        public void CheckProgram(Program program)
        {
            for (var moleIndex = 1; moleIndex < 5; moleIndex++)
            {
                var foreverBegin = program.Sprites[moleIndex].Scripts[0].Bricks.FirstOrDefault(
                    a => a is ForeverBrick) as ForeverBrick;
                var foreverEnd = program.Sprites[moleIndex].Scripts[0].Bricks.FirstOrDefault(
                    a => a is EndForeverBrick) as EndForeverBrick;

                Assert.IsNotNull(foreverBegin);
                Assert.IsNotNull(foreverEnd);

                Assert.IsTrue(foreverBegin.End == foreverEnd);
                Assert.IsTrue(foreverEnd.Begin == foreverBegin);
            }
        }

        public void CheckProgram(XmlProgram program)
        {
            var foreverBegin1 = program.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks.FirstOrDefault(
                    a => a is XmlForeverBrick) as XmlForeverBrick;
            var foreverEnd1 = program.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks.FirstOrDefault(
                a => a is XmlForeverLoopEndBrick) as XmlForeverLoopEndBrick;

            var foreverBegin2 = program.SpriteList.Sprites[2].Scripts.Scripts[0].Bricks.Bricks.FirstOrDefault(
                    a => a is XmlForeverBrick) as XmlForeverBrick;
            var foreverEnd2 = program.SpriteList.Sprites[2].Scripts.Scripts[0].Bricks.Bricks.FirstOrDefault(
                a => a is XmlForeverLoopEndBrick) as XmlForeverLoopEndBrick;

            Assert.IsNotNull(foreverBegin1);
            Assert.IsNotNull(foreverEnd1);

            Assert.IsFalse(foreverBegin1.LoopEndBrick != foreverEnd2);
            Assert.IsFalse(foreverEnd1.LoopBeginBrick != foreverBegin2);


            for (var moleIndex = 1; moleIndex < 5; moleIndex++)
            {
                var foreverBegin = program.SpriteList.Sprites[moleIndex].Scripts.Scripts[0].Bricks.Bricks.FirstOrDefault(
                    a => a is XmlForeverBrick) as XmlForeverBrick;
                var foreverEnd = program.SpriteList.Sprites[moleIndex].Scripts.Scripts[0].Bricks.Bricks.FirstOrDefault(
                    a => a is XmlForeverLoopEndBrick) as XmlForeverLoopEndBrick;

                Assert.IsNotNull(foreverBegin);
                Assert.IsNotNull(foreverEnd);

                Assert.IsTrue(foreverBegin.LoopEndBrick == foreverEnd);
                Assert.IsTrue(foreverEnd.LoopBeginBrick == foreverBegin);
            }
        }
    }
}
