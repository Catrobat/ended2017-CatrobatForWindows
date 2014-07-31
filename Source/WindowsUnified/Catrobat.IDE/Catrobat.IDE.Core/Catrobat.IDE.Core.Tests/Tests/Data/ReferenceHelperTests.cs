using System.Diagnostics;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Tests.Misc;
using Catrobat.IDE.Core.Tests.SampleData;
using Catrobat.IDE.Core.Utilities.Helpers;
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
        public static ITestProjectGenerator ProjectGenerator = new ProjectGeneratorForReferenceHelperTests();

        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }

        [TestMethod, TestCategory("GatedTests")]
        public void GetLookObjectTest()
        {
            var project = ProjectGenerator.GenerateProject();

            var sprite = project.Sprites[0];
            var setLookBrick = sprite.Scripts[0].Bricks[0] as SetLookBrick;
            
            Assert.IsNotNull(setLookBrick);
            Assert.AreEqual(sprite.Looks[0], setLookBrick.Value);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetSoundObjectTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var sprite = project.SpriteList.Sprites[1];
            var playSoundBrick = sprite.Scripts.Scripts[0].Bricks.Bricks[1] as XmlPlaySoundBrick;

            Assert.IsNotNull(playSoundBrick);
            Assert.AreEqual(sprite.Sounds.Sounds[0], playSoundBrick.Sound);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetSpriteObjectTest()
        {
            var project = await SampleLoader.LoadSampleProject("default.catroid", "default");
            var sprite = project.Sprites[1];
            var pointToBrick = sprite.Scripts[0].Bricks[2] as LookAtBrick;

            Assert.IsNotNull(pointToBrick);
            Assert.AreEqual(project.Sprites[1], pointToBrick.Target);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetUserVariableObjectTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var sprite1 = project.SpriteList.Sprites[0];
            var setVariableBrick = sprite1.Scripts.Scripts[0].Bricks.Bricks[1] as XmlSetVariableBrick;

            var entries = project.VariableList.ObjectVariableList.ObjectVariableEntries;

            Assert.IsNotNull(setVariableBrick);
            Assert.AreEqual(entries[0].VariableList.UserVariables[0], setVariableBrick.UserVariable);

            var sprite2 = project.SpriteList.Sprites[1];
            var changeVariableBrick = sprite2.Scripts.Scripts[1].Bricks.Bricks[5] as XmlChangeVariableBrick;

            Assert.IsNotNull(changeVariableBrick);
            Assert.AreEqual(project.VariableList.ProgramVariableList.UserVariables[0], changeVariableBrick.UserVariable);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetForeverBrickObjectTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var sprite = project.SpriteList.Sprites[1];
            var foreverBrick = sprite.Scripts.Scripts[0].Bricks.Bricks[4] as XmlForeverLoopEndBrick;

            Assert.IsNotNull(foreverBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[3], foreverBrick.LoopBeginBrick);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetRepeatBrickObjectTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var sprite = project.SpriteList.Sprites[1];
            var repeatBrick = sprite.Scripts.Scripts[0].Bricks.Bricks[6] as XmlRepeatLoopEndBrick;

            Assert.IsNotNull(repeatBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[5], repeatBrick.LoopBeginBrick);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetForeverLoopEndBrickObjectTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var sprite = project.SpriteList.Sprites[1];
            var foreverBrick = sprite.Scripts.Scripts[0].Bricks.Bricks[3] as XmlForeverBrick;

            Assert.IsNotNull(foreverBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[4], foreverBrick.LoopEndBrick);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetRepeatLoopEndBrickObjectTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var sprite = project.SpriteList.Sprites[1];
            var repeatBrick = sprite.Scripts.Scripts[0].Bricks.Bricks[5] as XmlRepeatBrick;

            Assert.IsNotNull(repeatBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[6], repeatBrick.LoopEndBrick);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetLoopEndBrickObjectTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var sprite = project.SpriteList.Sprites[1];
            var loopEndBrick = sprite.Scripts.Scripts[0].Bricks.Bricks[4] as XmlLoopEndBrick;

            Assert.IsNotNull(loopEndBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[3], loopEndBrick.LoopBeginBrick);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetIfLogicBeginBrickObjectTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var sprite = project.SpriteList.Sprites[0];
            var ifLogicBeginBrick1 = sprite.Scripts.Scripts[0].Bricks.Bricks[2] as XmlIfLogicBeginBrick;

            Assert.IsNotNull(ifLogicBeginBrick1);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[6], ifLogicBeginBrick1.IfLogicElseBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[7], ifLogicBeginBrick1.IfLogicEndBrick);

            var ifLogicBeginBrick2 = sprite.Scripts.Scripts[0].Bricks.Bricks[3] as XmlIfLogicBeginBrick;

            Assert.IsNotNull(ifLogicBeginBrick2);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[4], ifLogicBeginBrick2.IfLogicElseBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[5], ifLogicBeginBrick2.IfLogicEndBrick);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetIfLogicElseBrickObjectTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var sprite = project.SpriteList.Sprites[0];
            var ifLogicElseBrick1 = sprite.Scripts.Scripts[0].Bricks.Bricks[4] as XmlIfLogicElseBrick;

            Assert.IsNotNull(ifLogicElseBrick1);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[3], ifLogicElseBrick1.IfLogicBeginBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[5], ifLogicElseBrick1.IfLogicEndBrick);

            var ifLogicElseBrick2 = sprite.Scripts.Scripts[0].Bricks.Bricks[6] as XmlIfLogicElseBrick;

            Assert.IsNotNull(ifLogicElseBrick2);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[2], ifLogicElseBrick2.IfLogicBeginBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[7], ifLogicElseBrick2.IfLogicEndBrick);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetIfLogicEndBrickObjectTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var sprite = project.SpriteList.Sprites[0];
            var ifLogicEndBrick1 = sprite.Scripts.Scripts[0].Bricks.Bricks[5] as XmlIfLogicEndBrick;

            Assert.IsNotNull(ifLogicEndBrick1);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[3], ifLogicEndBrick1.IfLogicBeginBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[4], ifLogicEndBrick1.IfLogicElseBrick);

            var ifLogicEndBrick2 = sprite.Scripts.Scripts[0].Bricks.Bricks[7] as XmlIfLogicEndBrick;

            Assert.IsNotNull(ifLogicEndBrick2);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[2], ifLogicEndBrick2.IfLogicBeginBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[6], ifLogicEndBrick2.IfLogicElseBrick);
        }


        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetLookReferenceStringTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var setLookBrick = project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[0] as XmlSetLookBrick;
            Assert.IsNotNull(setLookBrick);
            var lookReference = setLookBrick.XmlLookReference;

            Assert.IsNotNull(lookReference);

            var reference = ReferenceHelper.GetReferenceString(lookReference);

            Assert.AreEqual("../../../../../lookList/look[1]", reference);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetSoundReferenceStringTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var playSoundBrick = project.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks[1] as XmlPlaySoundBrick;
            Debug.Assert(playSoundBrick != null);
            var soundReference = playSoundBrick.XmlSoundReference;

            Assert.IsNotNull(soundReference);

            var reference = ReferenceHelper.GetReferenceString(soundReference);

            Assert.AreEqual("../../../../../soundList/sound[1]", reference);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetSpriteReferenceStringTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var pointToBrick = project.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks[2] as XmlPointToBrick;
            Debug.Assert(pointToBrick != null);
            var pointedSpriteReference = pointToBrick.PointedXmlSpriteReference;

            Assert.IsNotNull(pointedSpriteReference);

            var reference = ReferenceHelper.GetReferenceString(pointedSpriteReference);

            Assert.AreEqual("../../../../../../object[2]", reference);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetVariableReferenceStringTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var setVariableBrick = project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[1] as XmlSetVariableBrick;
            Debug.Assert(setVariableBrick != null);
            var userVariableReference = setVariableBrick.UserVariableReference;

            Assert.IsNotNull(userVariableReference);

            var reference = ReferenceHelper.GetReferenceString(userVariableReference);

            Assert.AreEqual("../../../../../variables/objectVariableList/entry[1]/list/userVariable[1]", reference);

            var changeVariableBrick = project.SpriteList.Sprites[1].Scripts.Scripts[1].Bricks.Bricks[5] as XmlChangeVariableBrick;
            Debug.Assert(changeVariableBrick != null);
            userVariableReference = changeVariableBrick.UserVariableReference;

            Assert.IsNotNull(userVariableReference);

            reference = ReferenceHelper.GetReferenceString(userVariableReference);

            Assert.AreEqual("../../../../../variables/programVariableList/userVariable[1]", reference);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetForeverBrickReferenceStringTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var loopEndBrick = project.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks[4] as XmlLoopEndBrick;
            Debug.Assert(loopEndBrick != null);
            var loopBeginBrickReference = loopEndBrick.LoopBeginBrickReference;

            Assert.IsNotNull(loopBeginBrickReference);

            var reference = ReferenceHelper.GetReferenceString(loopBeginBrickReference);

            Assert.AreEqual("../../foreverBrick[1]", reference);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetRepeatBrickReferenceStringTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var loopEndBrick = project.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks[6] as XmlLoopEndBrick;
            Debug.Assert(loopEndBrick != null);
            var loopBeginBrickReference = loopEndBrick.LoopBeginBrickReference;

            Assert.IsNotNull(loopBeginBrickReference);

            var reference = ReferenceHelper.GetReferenceString(loopBeginBrickReference);

            Assert.AreEqual("../../repeatBrick[1]", reference);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetForeverLoopEndBrickReferenceStringTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var foreverBrick = project.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks[3] as XmlForeverBrick;
            Debug.Assert(foreverBrick != null);
            var loopEndBrickReference = foreverBrick.LoopEndBrickReference;

            Assert.IsNotNull(loopEndBrickReference);

            var reference = ReferenceHelper.GetReferenceString(loopEndBrickReference);

            Assert.AreEqual("../../loopEndlessBrick[1]", reference);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetRepeatLoopEndBrickReferenceStringTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var repeatBrick = project.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks[5] as XmlRepeatBrick;
            Debug.Assert(repeatBrick != null);
            var loopEndBrickReference = repeatBrick.LoopEndBrickReference;

            Assert.IsNotNull(loopEndBrickReference);

            var reference = ReferenceHelper.GetReferenceString(loopEndBrickReference);

            Assert.AreEqual("../../loopEndBrick[1]", reference);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetIfLogicBeginBrickReferenceStringTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");

            var ifLogicElseBrick = project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[4] as XmlIfLogicElseBrick;
            Debug.Assert(ifLogicElseBrick != null);
            var ifLogicBeginBrickReference = ifLogicElseBrick.IfLogicBeginBrickReference;
            Assert.IsNotNull(ifLogicBeginBrickReference);
            var reference = ReferenceHelper.GetReferenceString(ifLogicBeginBrickReference);
            Assert.AreEqual("../../ifLogicBeginBrick[2]", reference);

            var ifLogicEndBrick = project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[5] as XmlIfLogicEndBrick;
            Debug.Assert(ifLogicEndBrick != null);
            ifLogicBeginBrickReference = ifLogicEndBrick.IfLogicBeginBrickReference;
            Assert.IsNotNull(ifLogicBeginBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicBeginBrickReference);
            Assert.AreEqual("../../ifLogicBeginBrick[2]", reference);

            ifLogicElseBrick = project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[6] as XmlIfLogicElseBrick;
            Debug.Assert(ifLogicElseBrick != null);
            ifLogicBeginBrickReference = ifLogicElseBrick.IfLogicBeginBrickReference;
            Assert.IsNotNull(ifLogicBeginBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicBeginBrickReference);
            Assert.AreEqual("../../ifLogicBeginBrick[1]", reference);

            ifLogicEndBrick = project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[7] as XmlIfLogicEndBrick;
            Debug.Assert(ifLogicEndBrick != null);
            ifLogicBeginBrickReference = ifLogicEndBrick.IfLogicBeginBrickReference;
            Assert.IsNotNull(ifLogicBeginBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicBeginBrickReference);
            Assert.AreEqual("../../ifLogicBeginBrick[1]", reference);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetIfLogicElseBrickReferenceStringTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");

            var ifLogicBeginBrick = project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[2] as XmlIfLogicBeginBrick;
            Debug.Assert(ifLogicBeginBrick != null);
            var ifLogicElseBrickReference = ifLogicBeginBrick.IfLogicElseBrickReference;
            Assert.IsNotNull(ifLogicElseBrickReference);
            var reference = ReferenceHelper.GetReferenceString(ifLogicElseBrickReference);
            Assert.AreEqual("../../ifLogicElseBrick[2]", reference);

            var ifLogicEndBrick = project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[5] as XmlIfLogicEndBrick;
            Debug.Assert(ifLogicEndBrick != null);
            ifLogicElseBrickReference = ifLogicEndBrick.IfLogicElseBrickReference;
            Assert.IsNotNull(ifLogicElseBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicElseBrickReference);
            Assert.AreEqual("../../ifLogicElseBrick[1]", reference);

            ifLogicBeginBrick = project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[3] as XmlIfLogicBeginBrick;
            Debug.Assert(ifLogicBeginBrick != null);
            ifLogicElseBrickReference = ifLogicBeginBrick.IfLogicElseBrickReference;
            Assert.IsNotNull(ifLogicElseBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicElseBrickReference);
            Assert.AreEqual("../../ifLogicElseBrick[1]", reference);

            ifLogicEndBrick = project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[7] as XmlIfLogicEndBrick;
            Debug.Assert(ifLogicEndBrick != null);
            ifLogicElseBrickReference = ifLogicEndBrick.IfLogicElseBrickReference;
            Assert.IsNotNull(ifLogicElseBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicElseBrickReference);
            Assert.AreEqual("../../ifLogicElseBrick[2]", reference);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetIfLogicEndBrickReferenceStringTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");

            var ifLogicBeginBrick = project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[2] as XmlIfLogicBeginBrick;
            Debug.Assert(ifLogicBeginBrick != null);
            var ifLogicEndBrickReference = ifLogicBeginBrick.IfLogicEndBrickReference;
            Assert.IsNotNull(ifLogicEndBrickReference);
            var reference = ReferenceHelper.GetReferenceString(ifLogicEndBrickReference);
            Assert.AreEqual("../../ifLogicEndBrick[2]", reference);

            var ifLogicElseBrick = project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[4] as XmlIfLogicElseBrick;
            Debug.Assert(ifLogicElseBrick != null);
            ifLogicEndBrickReference = ifLogicElseBrick.IfLogicEndBrickReference;
            Assert.IsNotNull(ifLogicEndBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicEndBrickReference);
            Assert.AreEqual("../../ifLogicEndBrick[1]", reference);

            ifLogicBeginBrick = project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[3] as XmlIfLogicBeginBrick;
            Debug.Assert(ifLogicBeginBrick != null);
            ifLogicEndBrickReference = ifLogicBeginBrick.IfLogicEndBrickReference;
            Assert.IsNotNull(ifLogicEndBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicEndBrickReference);
            Assert.AreEqual("../../ifLogicEndBrick[1]", reference);

            ifLogicElseBrick = project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[6] as XmlIfLogicElseBrick;
            Debug.Assert(ifLogicElseBrick != null);
            ifLogicEndBrickReference = ifLogicElseBrick.IfLogicEndBrickReference;
            Assert.IsNotNull(ifLogicEndBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicEndBrickReference);
            Assert.AreEqual("../../ifLogicEndBrick[2]", reference);
        }

    }
}
