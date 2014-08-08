using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Models.Scripts;
using Catrobat.IDE.Core.Tests.Misc;
using Catrobat.IDE.Core.Tests.SampleData;
using Catrobat.IDE.Core.Tests.SampleData.ProgramGenerators;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Looks;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Variables;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.Data
{
    [TestClass]
    public class ReferenceHelperTests
    {
        public static ITestProgramGenerator ProjectGenerator = new ProgramGeneratorForReferenceHelperTests();

        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }

        [TestMethod]
        public void GetLookObjectTest()
        {
            var program = CreateProgram();

            var sprite = program.SpriteList.Sprites[0];
            var setLookBrick = sprite.Scripts.Scripts[0].Bricks.Bricks[0] as XmlSetLookBrick;
            
            Assert.IsNotNull(setLookBrick);
            Assert.AreEqual(sprite.Looks.Looks[0], setLookBrick.Look);
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void GetSoundObjectTest()
        {
            var program = CreateProgram();

            var sprite = program.SpriteList.Sprites[1];
            var playSoundBrick = sprite.Scripts.Scripts[0].Bricks.Bricks[1] as XmlPlaySoundBrick;

            Assert.IsNotNull(playSoundBrick);
            Assert.AreEqual(sprite.Sounds.Sounds[0], playSoundBrick.Sound);
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void GetSpriteObjectTest()
        {
            var program = CreateProgram().ToModel();
            var sprite = program.Sprites[1];
            var pointToBrick = sprite.Scripts[0].Bricks[2] as LookAtBrick;

            Assert.IsNotNull(pointToBrick);
            Assert.AreEqual(program.Sprites[1], pointToBrick.Target);
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void GetUserVariableObjectTest()
        {
            var program = CreateProgram();
            var sprite1 = program.SpriteList.Sprites[0];
            var setVariableBrick = sprite1.Scripts.Scripts[0].Bricks.Bricks[1] as XmlSetVariableBrick;

            var entries = program.VariableList.ObjectVariableList.ObjectVariableEntries;

            Assert.IsNotNull(setVariableBrick);
            Assert.AreEqual(entries[0].VariableList.UserVariables[0], setVariableBrick.UserVariable);

            var sprite2 = program.SpriteList.Sprites[1];
            var changeVariableBrick = sprite2.Scripts.Scripts[1].Bricks.Bricks[1] as XmlChangeVariableBrick;

            Assert.IsNotNull(changeVariableBrick);
            Assert.AreEqual(program.VariableList.ProgramVariableList.UserVariables[0], changeVariableBrick.UserVariable);
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void GetForeverBrickObjectTest()
        {
            var program = CreateProgram();
            var sprite = program.SpriteList.Sprites[1];
            var foreverBrick = sprite.Scripts.Scripts[0].Bricks.Bricks[4] as XmlForeverLoopEndBrick;

            Assert.IsNotNull(foreverBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[3], foreverBrick.LoopBeginBrick);
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void GetRepeatBrickObjectTest()
        {
            var program = CreateProgram();
            var sprite = program.SpriteList.Sprites[1];
            var repeatBrick = sprite.Scripts.Scripts[0].Bricks.Bricks[6] as XmlRepeatLoopEndBrick;

            Assert.IsNotNull(repeatBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[5], repeatBrick.LoopBeginBrick);
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void GetForeverLoopEndBrickObjectTest()
        {
            var program = CreateProgram();
            var sprite = program.SpriteList.Sprites[1];
            var foreverBrick = sprite.Scripts.Scripts[0].Bricks.Bricks[3] as XmlForeverBrick;

            Assert.IsNotNull(foreverBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[4], foreverBrick.LoopEndBrick);
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void GetRepeatLoopEndBrickObjectTest()
        {
            var program = CreateProgram();
            var sprite = program.SpriteList.Sprites[1];
            var repeatBrick = sprite.Scripts.Scripts[0].Bricks.Bricks[5] as XmlRepeatBrick;

            Assert.IsNotNull(repeatBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[6], repeatBrick.LoopEndBrick);
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void GetLoopEndBrickObjectTest()
        {
            var program = CreateProgram();
            var sprite = program.SpriteList.Sprites[1];
            var loopEndBrick = sprite.Scripts.Scripts[0].Bricks.Bricks[4] as XmlLoopEndBrick;

            Assert.IsNotNull(loopEndBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[3], loopEndBrick.LoopBeginBrick);
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void GetIfLogicBeginBrickObjectTest()
        {
            var program = CreateProgram();
            var sprite = program.SpriteList.Sprites[0];
            var ifLogicBeginBrick1 = sprite.Scripts.Scripts[0].Bricks.Bricks[2] as XmlIfLogicBeginBrick;

            Assert.IsNotNull(ifLogicBeginBrick1);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[6], ifLogicBeginBrick1.IfLogicElseBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[7], ifLogicBeginBrick1.IfLogicEndBrick);

            var ifLogicBeginBrick2 = sprite.Scripts.Scripts[0].Bricks.Bricks[3] as XmlIfLogicBeginBrick;

            Assert.IsNotNull(ifLogicBeginBrick2);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[4], ifLogicBeginBrick2.IfLogicElseBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[5], ifLogicBeginBrick2.IfLogicEndBrick);
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void GetIfLogicElseBrickObjectTest()
        {
            var program = CreateProgram();
            var sprite = program.SpriteList.Sprites[0];
            var ifLogicElseBrick1 = sprite.Scripts.Scripts[0].Bricks.Bricks[4] as XmlIfLogicElseBrick;

            Assert.IsNotNull(ifLogicElseBrick1);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[3], ifLogicElseBrick1.IfLogicBeginBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[5], ifLogicElseBrick1.IfLogicEndBrick);

            var ifLogicElseBrick2 = sprite.Scripts.Scripts[0].Bricks.Bricks[6] as XmlIfLogicElseBrick;

            Assert.IsNotNull(ifLogicElseBrick2);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[2], ifLogicElseBrick2.IfLogicBeginBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[7], ifLogicElseBrick2.IfLogicEndBrick);
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void GetIfLogicEndBrickObjectTest()
        {
            var program = CreateProgram();
            var sprite = program.SpriteList.Sprites[0];
            var ifLogicEndBrick1 = sprite.Scripts.Scripts[0].Bricks.Bricks[5] as XmlIfLogicEndBrick;

            Assert.IsNotNull(ifLogicEndBrick1);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[3], ifLogicEndBrick1.IfLogicBeginBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[4], ifLogicEndBrick1.IfLogicElseBrick);

            var ifLogicEndBrick2 = sprite.Scripts.Scripts[0].Bricks.Bricks[7] as XmlIfLogicEndBrick;

            Assert.IsNotNull(ifLogicEndBrick2);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[2], ifLogicEndBrick2.IfLogicBeginBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[6], ifLogicEndBrick2.IfLogicElseBrick);
        }


        [TestMethod, TestCategory("ExcludeGated")]
        public void GetLookReferenceStringTest()
        {
            var program = CreateProgram();
            var setLookBrick = program.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[0] as XmlSetLookBrick;
            Assert.IsNotNull(setLookBrick);
            var lookReference = setLookBrick.XmlLookReference;

            Assert.IsNotNull(lookReference);

            var reference = ReferenceHelper.GetReferenceString(lookReference);

            Assert.AreEqual("../../../../../lookList/look[1]", reference);
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void GetSoundReferenceStringTest()
        {
            var program = CreateProgram();
            var playSoundBrick = program.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks[1] as XmlPlaySoundBrick;
            Debug.Assert(playSoundBrick != null);
            var soundReference = playSoundBrick.XmlSoundReference;

            Assert.IsNotNull(soundReference);

            var reference = ReferenceHelper.GetReferenceString(soundReference);

            Assert.AreEqual("../../../../../soundList/sound[1]", reference);
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void GetSpriteReferenceStringTest()
        {
            var program = CreateProgram();
            var pointToBrick = program.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks[2] as XmlPointToBrick;
            Debug.Assert(pointToBrick != null);
            var pointedSpriteReference = pointToBrick.PointedXmlSpriteReference;

            Assert.IsNotNull(pointedSpriteReference);

            var reference = ReferenceHelper.GetReferenceString(pointedSpriteReference);

            Assert.AreEqual("../../../../../../object[2]", reference);
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void GetVariableReferenceStringTest()
        {
            var program = CreateProgram();
            var setVariableBrick = program.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[1] as XmlSetVariableBrick;
            Debug.Assert(setVariableBrick != null);
            var userVariableReference = setVariableBrick.UserVariableReference;

            Assert.IsNotNull(userVariableReference);

            var reference = ReferenceHelper.GetReferenceString(userVariableReference);

            Assert.AreEqual("../../../../../../../variables/objectVariableList/entry[1]/list/userVariable[1]", reference);

            var changeVariableBrick = program.SpriteList.Sprites[1].Scripts.Scripts[1].Bricks.Bricks[1] as XmlChangeVariableBrick;
            Debug.Assert(changeVariableBrick != null);
            userVariableReference = changeVariableBrick.UserVariableReference;

            Assert.IsNotNull(userVariableReference);

            reference = ReferenceHelper.GetReferenceString(userVariableReference);

            Assert.AreEqual("../../../../../../../variables/programVariableList/userVariable[1]", reference);
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void GetForeverBrickReferenceStringTest()
        {
            var program = CreateProgram();
            var loopEndBrick = program.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks[4] as XmlLoopEndBrick;
            Debug.Assert(loopEndBrick != null);
            var loopBeginBrickReference = loopEndBrick.LoopBeginBrickReference;

            Assert.IsNotNull(loopBeginBrickReference);

            var reference = ReferenceHelper.GetReferenceString(loopBeginBrickReference);

            Assert.AreEqual("../../foreverBrick[1]", reference);
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void GetRepeatBrickReferenceStringTest()
        {
            var program = CreateProgram();
            var loopEndBrick = program.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks[6] as XmlLoopEndBrick;
            Debug.Assert(loopEndBrick != null);
            var loopBeginBrickReference = loopEndBrick.LoopBeginBrickReference;

            Assert.IsNotNull(loopBeginBrickReference);

            var reference = ReferenceHelper.GetReferenceString(loopBeginBrickReference);

            Assert.AreEqual("../../repeatBrick[1]", reference);
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void GetForeverLoopEndBrickReferenceStringTest()
        {
            var program = CreateProgram();
            var foreverBrick = program.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks[3] as XmlForeverBrick;
            Debug.Assert(foreverBrick != null);
            var loopEndBrickReference = foreverBrick.LoopEndBrickReference;

            Assert.IsNotNull(loopEndBrickReference);

            var reference = ReferenceHelper.GetReferenceString(loopEndBrickReference);

            Assert.AreEqual("../../loopEndlessBrick[1]", reference);
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void GetRepeatLoopEndBrickReferenceStringTest()
        {
            var program = CreateProgram();
            var repeatBrick = program.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks[5] as XmlRepeatBrick;
            Debug.Assert(repeatBrick != null);
            var loopEndBrickReference = repeatBrick.LoopEndBrickReference;

            Assert.IsNotNull(loopEndBrickReference);

            var reference = ReferenceHelper.GetReferenceString(loopEndBrickReference);

            Assert.AreEqual("../../loopEndBrick[1]", reference);
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void GetIfLogicBeginBrickReferenceStringTest()
        {
            var program = CreateProgram();

            var ifLogicElseBrick = program.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[4] as XmlIfLogicElseBrick;
            Debug.Assert(ifLogicElseBrick != null);
            var ifLogicBeginBrickReference = ifLogicElseBrick.IfLogicBeginBrickReference;
            Assert.IsNotNull(ifLogicBeginBrickReference);
            var reference = ReferenceHelper.GetReferenceString(ifLogicBeginBrickReference);
            Assert.AreEqual("../../ifLogicBeginBrick[2]", reference);

            var ifLogicEndBrick = program.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[5] as XmlIfLogicEndBrick;
            Debug.Assert(ifLogicEndBrick != null);
            ifLogicBeginBrickReference = ifLogicEndBrick.IfLogicBeginBrickReference;
            Assert.IsNotNull(ifLogicBeginBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicBeginBrickReference);
            Assert.AreEqual("../../ifLogicBeginBrick[2]", reference);

            ifLogicElseBrick = program.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[6] as XmlIfLogicElseBrick;
            Debug.Assert(ifLogicElseBrick != null);
            ifLogicBeginBrickReference = ifLogicElseBrick.IfLogicBeginBrickReference;
            Assert.IsNotNull(ifLogicBeginBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicBeginBrickReference);
            Assert.AreEqual("../../ifLogicBeginBrick[1]", reference);

            ifLogicEndBrick = program.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[7] as XmlIfLogicEndBrick;
            Debug.Assert(ifLogicEndBrick != null);
            ifLogicBeginBrickReference = ifLogicEndBrick.IfLogicBeginBrickReference;
            Assert.IsNotNull(ifLogicBeginBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicBeginBrickReference);
            Assert.AreEqual("../../ifLogicBeginBrick[1]", reference);
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void GetIfLogicElseBrickReferenceStringTest()
        {
            var program = CreateProgram();

            var ifLogicBeginBrick = program.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[2] as XmlIfLogicBeginBrick;
            Debug.Assert(ifLogicBeginBrick != null);
            var ifLogicElseBrickReference = ifLogicBeginBrick.IfLogicElseBrickReference;
            Assert.IsNotNull(ifLogicElseBrickReference);
            var reference = ReferenceHelper.GetReferenceString(ifLogicElseBrickReference);
            Assert.AreEqual("../../ifLogicElseBrick[2]", reference);

            var ifLogicEndBrick = program.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[5] as XmlIfLogicEndBrick;
            Debug.Assert(ifLogicEndBrick != null);
            ifLogicElseBrickReference = ifLogicEndBrick.IfLogicElseBrickReference;
            Assert.IsNotNull(ifLogicElseBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicElseBrickReference);
            Assert.AreEqual("../../ifLogicElseBrick[1]", reference);

            ifLogicBeginBrick = program.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[3] as XmlIfLogicBeginBrick;
            Debug.Assert(ifLogicBeginBrick != null);
            ifLogicElseBrickReference = ifLogicBeginBrick.IfLogicElseBrickReference;
            Assert.IsNotNull(ifLogicElseBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicElseBrickReference);
            Assert.AreEqual("../../ifLogicElseBrick[1]", reference);

            ifLogicEndBrick = program.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[7] as XmlIfLogicEndBrick;
            Debug.Assert(ifLogicEndBrick != null);
            ifLogicElseBrickReference = ifLogicEndBrick.IfLogicElseBrickReference;
            Assert.IsNotNull(ifLogicElseBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicElseBrickReference);
            Assert.AreEqual("../../ifLogicElseBrick[2]", reference);
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void GetIfLogicEndBrickReferenceStringTest()
        {
            var program = CreateProgram();

            var ifLogicBeginBrick = program.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[2] as XmlIfLogicBeginBrick;
            Debug.Assert(ifLogicBeginBrick != null);
            var ifLogicEndBrickReference = ifLogicBeginBrick.IfLogicEndBrickReference;
            Assert.IsNotNull(ifLogicEndBrickReference);
            var reference = ReferenceHelper.GetReferenceString(ifLogicEndBrickReference);
            Assert.AreEqual("../../ifLogicEndBrick[2]", reference);

            var ifLogicElseBrick = program.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[4] as XmlIfLogicElseBrick;
            Debug.Assert(ifLogicElseBrick != null);
            ifLogicEndBrickReference = ifLogicElseBrick.IfLogicEndBrickReference;
            Assert.IsNotNull(ifLogicEndBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicEndBrickReference);
            Assert.AreEqual("../../ifLogicEndBrick[1]", reference);

            ifLogicBeginBrick = program.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[3] as XmlIfLogicBeginBrick;
            Debug.Assert(ifLogicBeginBrick != null);
            ifLogicEndBrickReference = ifLogicBeginBrick.IfLogicEndBrickReference;
            Assert.IsNotNull(ifLogicEndBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicEndBrickReference);
            Assert.AreEqual("../../ifLogicEndBrick[1]", reference);

            ifLogicElseBrick = program.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[6] as XmlIfLogicElseBrick;
            Debug.Assert(ifLogicElseBrick != null);
            ifLogicEndBrickReference = ifLogicElseBrick.IfLogicEndBrickReference;
            Assert.IsNotNull(ifLogicEndBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicEndBrickReference);
            Assert.AreEqual("../../ifLogicEndBrick[2]", reference);
        }


        //[TestMethod] // Test for Bug 511
        //public void VariableReferenceTest()
        //{
        //    var programGenerator = new ProgramGeneratorTestSimple();

        //    var program = programGenerator.GenerateProgram();

        //    var globatVariable = new GlobalVariable { Name = "GlobalVariable1" };
        //    var localVariable = new LocalVariable { Name = "LocalVariable1" };
        //    program.GlobalVariables.Add(globatVariable);
        //    program.Sprites[0].LocalVariables.Add(localVariable);

        //    var startScript = new StartScript();
        //    var localSetVariableBrick = new SetVariableBrick { Value = new FormulaNodeNumber { Value = 0.0 }, Variable = localVariable };
        //    var globalSetVariableBrick = new SetVariableBrick { Value = new FormulaNodeNumber { Value = 0.0 }, Variable = globatVariable };
        //    startScript.Bricks.Add(localSetVariableBrick);
        //    startScript.Bricks.Add(globalSetVariableBrick);

        //    program.Sprites[0].Scripts.Add(startScript);


        //    var xmlString = program.ToXmlObject().ToXmlString();

        //    var document = XDocument.Load(new StringReader(xmlString));

        //    var localVariableElement = document.Descendants("userVariable").ElementAt(0);
        //    var localVariablePath = localVariableElement.Attributes("reference").ElementAt(0).Value;
        //    Assert.AreEqual("../../../../../../../variables/objectVariableList/entry[1]/list/userVariable[1]", localVariablePath);


        //    var globalVariableElement = document.Descendants("userVariable").ElementAt(1);
        //    var globalVariablePath = globalVariableElement.Attributes("reference").ElementAt(0).Value;
        //    Assert.AreEqual("../../../../../../../variables/programVariableList/userVariable[1]", globalVariablePath);

        //}

        private static XmlProgram CreateProgram()
        {
            var originalProgram = ProjectGenerator.GenerateProgram();
            var projectString = originalProgram.ToXmlObject().ToXmlString();
            return new XmlProgram(projectString);
        }
    }
}
