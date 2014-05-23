using System.Threading.Tasks;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Costumes;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Variables;
using Catrobat.IDE.Tests.Misc;
using Catrobat.IDE.Tests.SampleData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Tests.Data
{
    [TestClass]
    public class ReferenceHelperTests
    {
        public static ITestProjectGenerator ProjectGenerator = new ProjectGeneratorForReferenceHelperTests();

        [ClassInitialize()]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }

        [TestMethod, TestCategory("GatedTests")]
        public void GetCostumeObjectTest()
        {
            var project = ProjectGenerator.GenerateProject();

            var sprite = project.Sprites[0];
            var setCostumeBrick = sprite.Scripts[0].Bricks[0] as SetCostumeBrick;
            
            Assert.IsNotNull(setCostumeBrick);
            Assert.AreEqual(sprite.Costumes[0], setCostumeBrick.Value);
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
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var sprite = project.SpriteList.Sprites[1];
            var pointToBrick = sprite.Scripts.Scripts[0].Bricks.Bricks[2] as XmlPointToBrick;

            Assert.IsNotNull(pointToBrick);
            Assert.AreEqual(project.SpriteList.Sprites[1], pointToBrick.PointedSprite);
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
        public async Task GetCostumeReferenceStringTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var costumeReference = (project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[0] as XmlSetCostumeBrick).XmlCostumeReference;

            Assert.IsNotNull(costumeReference);

            var reference = ReferenceHelper.GetReferenceString(costumeReference);

            Assert.AreEqual("../../../../../lookList/look[1]", reference);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetSoundReferenceStringTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var soundReference = (project.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks[1] as XmlPlaySoundBrick).XmlSoundReference;

            Assert.IsNotNull(soundReference);

            var reference = ReferenceHelper.GetReferenceString(soundReference);

            Assert.AreEqual("../../../../../soundList/sound[1]", reference);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetSpriteReferenceStringTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var pointedSpriteReference = (project.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks[2] as XmlPointToBrick).PointedXmlSpriteReference;

            Assert.IsNotNull(pointedSpriteReference);

            var reference = ReferenceHelper.GetReferenceString(pointedSpriteReference);

            Assert.AreEqual("../../../../../../object[2]", reference);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetVariableReferenceStringTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var userVariableReference = (project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[1] as XmlSetVariableBrick).UserVariableReference;

            Assert.IsNotNull(userVariableReference);

            var reference = ReferenceHelper.GetReferenceString(userVariableReference);

            Assert.AreEqual("../../../../../variables/objectVariableList/entry[1]/list/userVariable[1]", reference);

            userVariableReference = (project.SpriteList.Sprites[1].Scripts.Scripts[1].Bricks.Bricks[5] as XmlChangeVariableBrick).UserVariableReference;

            Assert.IsNotNull(userVariableReference);

            reference = ReferenceHelper.GetReferenceString(userVariableReference);

            Assert.AreEqual("../../../../../variables/programVariableList/userVariable[1]", reference);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetForeverBrickReferenceStringTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var loopBeginBrickReference = (project.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks[4] as XmlLoopEndBrick).LoopBeginBrickReference;

            Assert.IsNotNull(loopBeginBrickReference);

            var reference = ReferenceHelper.GetReferenceString(loopBeginBrickReference);

            Assert.AreEqual("../../foreverBrick[1]", reference);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetRepeatBrickReferenceStringTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var loopBeginBrickReference = (project.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks[6] as XmlLoopEndBrick).LoopBeginBrickReference;

            Assert.IsNotNull(loopBeginBrickReference);

            var reference = ReferenceHelper.GetReferenceString(loopBeginBrickReference);

            Assert.AreEqual("../../repeatBrick[1]", reference);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetForeverLoopEndBrickReferenceStringTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var loopEndBrickReference = (project.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks[3] as XmlForeverBrick).LoopEndBrickReference;

            Assert.IsNotNull(loopEndBrickReference);

            var reference = ReferenceHelper.GetReferenceString(loopEndBrickReference);

            Assert.AreEqual("../../loopEndlessBrick[1]", reference);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetRepeatLoopEndBrickReferenceStringTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var loopEndBrickReference = (project.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks[5] as XmlRepeatBrick).LoopEndBrickReference;

            Assert.IsNotNull(loopEndBrickReference);

            var reference = ReferenceHelper.GetReferenceString(loopEndBrickReference);

            Assert.AreEqual("../../loopEndBrick[1]", reference);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetIfLogicBeginBrickReferenceStringTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");

            var ifLogicBeginBrickReference = (project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[4] as XmlIfLogicElseBrick).IfLogicBeginBrickReference;
            Assert.IsNotNull(ifLogicBeginBrickReference);
            var reference = ReferenceHelper.GetReferenceString(ifLogicBeginBrickReference);
            Assert.AreEqual("../../ifLogicBeginBrick[2]", reference);

            ifLogicBeginBrickReference = (project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[5] as XmlIfLogicEndBrick).IfLogicBeginBrickReference;
            Assert.IsNotNull(ifLogicBeginBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicBeginBrickReference);
            Assert.AreEqual("../../ifLogicBeginBrick[2]", reference);

            ifLogicBeginBrickReference = (project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[6] as XmlIfLogicElseBrick).IfLogicBeginBrickReference;
            Assert.IsNotNull(ifLogicBeginBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicBeginBrickReference);
            Assert.AreEqual("../../ifLogicBeginBrick[1]", reference);

            ifLogicBeginBrickReference = (project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[7] as XmlIfLogicEndBrick).IfLogicBeginBrickReference;
            Assert.IsNotNull(ifLogicBeginBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicBeginBrickReference);
            Assert.AreEqual("../../ifLogicBeginBrick[1]", reference);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetIfLogicElseBrickReferenceStringTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");

            var ifLogicElseBrickReference = (project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[2] as XmlIfLogicBeginBrick).IfLogicElseBrickReference;
            Assert.IsNotNull(ifLogicElseBrickReference);
            var reference = ReferenceHelper.GetReferenceString(ifLogicElseBrickReference);
            Assert.AreEqual("../../ifLogicElseBrick[2]", reference);

            ifLogicElseBrickReference = (project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[5] as XmlIfLogicEndBrick).IfLogicElseBrickReference;
            Assert.IsNotNull(ifLogicElseBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicElseBrickReference);
            Assert.AreEqual("../../ifLogicElseBrick[1]", reference);

            ifLogicElseBrickReference = (project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[3] as XmlIfLogicBeginBrick).IfLogicElseBrickReference;
            Assert.IsNotNull(ifLogicElseBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicElseBrickReference);
            Assert.AreEqual("../../ifLogicElseBrick[1]", reference);

            ifLogicElseBrickReference = (project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[7] as XmlIfLogicEndBrick).IfLogicElseBrickReference;
            Assert.IsNotNull(ifLogicElseBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicElseBrickReference);
            Assert.AreEqual("../../ifLogicElseBrick[2]", reference);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task GetIfLogicEndBrickReferenceStringTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");

            var ifLogicEndBrickReference = (project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[2] as XmlIfLogicBeginBrick).IfLogicEndBrickReference;
            Assert.IsNotNull(ifLogicEndBrickReference);
            var reference = ReferenceHelper.GetReferenceString(ifLogicEndBrickReference);
            Assert.AreEqual("../../ifLogicEndBrick[2]", reference);

            ifLogicEndBrickReference = (project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[4] as XmlIfLogicElseBrick).IfLogicEndBrickReference;
            Assert.IsNotNull(ifLogicEndBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicEndBrickReference);
            Assert.AreEqual("../../ifLogicEndBrick[1]", reference);

            ifLogicEndBrickReference = (project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[3] as XmlIfLogicBeginBrick).IfLogicEndBrickReference;
            Assert.IsNotNull(ifLogicEndBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicEndBrickReference);
            Assert.AreEqual("../../ifLogicEndBrick[1]", reference);

            ifLogicEndBrickReference = (project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[6] as XmlIfLogicElseBrick).IfLogicEndBrickReference;
            Assert.IsNotNull(ifLogicEndBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicEndBrickReference);
            Assert.AreEqual("../../ifLogicEndBrick[2]", reference);
        }


        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task UpdateCostumeReferenceTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var oldSprite = project.SpriteList.Sprites[0];
            var newSprite = await oldSprite.Copy() as XmlSprite;
            Assert.IsNotNull(newSprite);

            var oldCostume = oldSprite.Costumes.Costumes[0];
            var newCostume = newSprite.Costumes.Costumes[0];

            var oldCostumeBrick = oldSprite.Scripts.Scripts[0].Bricks.Bricks[0] as XmlSetCostumeBrick;
            var newCostumeBrick = newSprite.Scripts.Scripts[0].Bricks.Bricks[0] as XmlSetCostumeBrick;
            Assert.IsNotNull(oldCostumeBrick);
            Assert.IsNotNull(newCostumeBrick);
            Assert.AreNotEqual(oldCostumeBrick.Costume, newCostumeBrick.Costume);
            Assert.AreEqual(oldCostume, oldCostumeBrick.Costume);
            Assert.AreEqual(newCostume, newCostumeBrick.Costume);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task UpdateSoundReferenceTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var oldSprite = project.SpriteList.Sprites[1];
            var newSprite = await oldSprite.Copy() as XmlSprite;
            Assert.IsNotNull(newSprite);

            var oldSound = oldSprite.Sounds.Sounds[0];
            var newSound = newSprite.Sounds.Sounds[0];

            var oldPlaySoundBrick = oldSprite.Scripts.Scripts[0].Bricks.Bricks[1] as XmlPlaySoundBrick;
            var newPlaySoundBrick = newSprite.Scripts.Scripts[0].Bricks.Bricks[1] as XmlPlaySoundBrick;
            Assert.IsNotNull(oldPlaySoundBrick);
            Assert.IsNotNull(newPlaySoundBrick);
            Assert.AreNotEqual(oldPlaySoundBrick.Sound, newPlaySoundBrick.Sound);
            Assert.AreEqual(oldSound, oldPlaySoundBrick.Sound);
            Assert.AreEqual(newSound, newPlaySoundBrick.Sound);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task CopyVariableOnSpriteCopyTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var oldSprite = project.SpriteList.Sprites[0];
            var newSprite = await oldSprite.Copy() as XmlSprite;
            Assert.IsNotNull(newSprite);

            var oldVariable = project.VariableList.ObjectVariableList.ObjectVariableEntries[0].VariableList.UserVariables[0];
            var newVariable = project.VariableList.ObjectVariableList.ObjectVariableEntries[2].VariableList.UserVariables[0];

            var oldBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[1] as XmlSetVariableBrick;
            var newBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[1] as XmlSetVariableBrick;
            Assert.IsNotNull(oldBrick1);
            Assert.IsNotNull(newBrick1);
            Assert.AreNotSame(oldBrick1.UserVariable, newBrick1.UserVariable);
            Assert.AreSame(oldVariable, oldBrick1.UserVariable);
            Assert.AreSame(newVariable, newBrick1.UserVariable);


            oldSprite = project.SpriteList.Sprites[1];
            newSprite = await oldSprite.Copy() as XmlSprite;
            Assert.IsNotNull(newSprite);

            oldVariable = project.VariableList.ProgramVariableList.UserVariables[0];
            newVariable = project.VariableList.ProgramVariableList.UserVariables[0];

            var oldBrick2 = oldSprite.Scripts.Scripts[1].Bricks.Bricks[5] as XmlChangeVariableBrick;
            var newBrick2 = newSprite.Scripts.Scripts[1].Bricks.Bricks[5] as XmlChangeVariableBrick;
            Assert.IsNotNull(oldBrick2);
            Assert.IsNotNull(newBrick2);
            Assert.AreSame(oldBrick2.UserVariable, newBrick2.UserVariable);
            Assert.AreSame(oldVariable, oldBrick2.UserVariable);
            Assert.AreSame(newVariable, newBrick2.UserVariable);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task UpdateLoopBeginBrickReferenceTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var oldSprite = project.SpriteList.Sprites[1];
            var newSprite = await oldSprite.Copy() as XmlSprite;

            Assert.IsNotNull(newSprite);

            var oldLoopBeginBrick = oldSprite.Scripts.Scripts[0].Bricks.Bricks[3] as XmlLoopBeginBrick;
            var newLoopBeginBrick = newSprite.Scripts.Scripts[0].Bricks.Bricks[3] as XmlLoopBeginBrick;

            var oldBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[4] as XmlLoopEndBrick;
            var newBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[4] as XmlLoopEndBrick;
            Assert.IsNotNull(oldBrick1);
            Assert.IsNotNull(newBrick1);
            Assert.AreNotEqual(oldBrick1.LoopBeginBrick, newBrick1.LoopBeginBrick);
            Assert.AreEqual(oldLoopBeginBrick, oldBrick1.LoopBeginBrick);
            Assert.AreEqual(newLoopBeginBrick, newBrick1.LoopBeginBrick);


            oldLoopBeginBrick = oldSprite.Scripts.Scripts[0].Bricks.Bricks[5] as XmlLoopBeginBrick;
            newLoopBeginBrick = newSprite.Scripts.Scripts[0].Bricks.Bricks[5] as XmlLoopBeginBrick;

            var oldBrick2 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[6] as XmlLoopEndBrick;
            var newBrick2 = newSprite.Scripts.Scripts[0].Bricks.Bricks[6] as XmlLoopEndBrick;
            Assert.IsNotNull(oldBrick2);
            Assert.IsNotNull(newBrick2);
            Assert.AreNotEqual(oldBrick2.LoopBeginBrick, newBrick2.LoopBeginBrick);
            Assert.AreEqual(oldLoopBeginBrick, oldBrick2.LoopBeginBrick);
            Assert.AreEqual(newLoopBeginBrick, newBrick2.LoopBeginBrick);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task UpdateLoopEndBrickReferenceTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var oldSprite = project.SpriteList.Sprites[1];
            var newSprite = await oldSprite.Copy() as XmlSprite;

            Assert.IsNotNull(newSprite);

            var oldLoopEndBrick = oldSprite.Scripts.Scripts[0].Bricks.Bricks[4] as XmlLoopEndBrick;
            var newLoopEndBrick = newSprite.Scripts.Scripts[0].Bricks.Bricks[4] as XmlLoopEndBrick;

            var oldBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[3] as XmlForeverBrick;
            var newBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[3] as XmlForeverBrick;
            Assert.IsNotNull(oldBrick1);
            Assert.IsNotNull(newBrick1);
            Assert.AreNotEqual(oldBrick1.LoopEndBrick, newBrick1.LoopEndBrick);
            Assert.AreEqual(oldLoopEndBrick, oldBrick1.LoopEndBrick);
            Assert.AreEqual(newLoopEndBrick, newBrick1.LoopEndBrick);


            oldLoopEndBrick = oldSprite.Scripts.Scripts[0].Bricks.Bricks[6] as XmlLoopEndBrick;
            newLoopEndBrick = newSprite.Scripts.Scripts[0].Bricks.Bricks[6] as XmlLoopEndBrick;

            var oldBrick2 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[5] as XmlRepeatBrick;
            var newBrick2 = newSprite.Scripts.Scripts[0].Bricks.Bricks[5] as XmlRepeatBrick;
            Assert.IsNotNull(oldBrick2);
            Assert.IsNotNull(newBrick2);
            Assert.AreNotEqual(oldBrick2.LoopEndBrick, newBrick2.LoopEndBrick);
            Assert.AreEqual(oldLoopEndBrick, oldBrick2.LoopEndBrick);
            Assert.AreEqual(newLoopEndBrick, newBrick2.LoopEndBrick);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task UpdateIfLogicBeginBrickReferenceTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var oldSprite = project.SpriteList.Sprites[0];
            var newSprite = await oldSprite.Copy() as XmlSprite;

            Assert.IsNotNull(newSprite);

            var oldIfLogicBeginBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[3] as XmlIfLogicBeginBrick;
            var newIfLogicBeginBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[3] as XmlIfLogicBeginBrick;

            var oldIfLogicElseBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[4] as XmlIfLogicElseBrick;
            var newIfLogicElseBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[4] as XmlIfLogicElseBrick;
            Assert.IsNotNull(oldIfLogicElseBrick1);
            Assert.IsNotNull(newIfLogicElseBrick1);
            Assert.AreNotEqual(oldIfLogicElseBrick1.IfLogicBeginBrick, newIfLogicElseBrick1.IfLogicBeginBrick);
            Assert.AreEqual(oldIfLogicBeginBrick1, oldIfLogicElseBrick1.IfLogicBeginBrick);
            Assert.AreEqual(newIfLogicBeginBrick1, newIfLogicElseBrick1.IfLogicBeginBrick);

            var oldIfLogicEndBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[5] as XmlIfLogicEndBrick;
            var newIfLogicEndBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[5] as XmlIfLogicEndBrick;
            Assert.IsNotNull(oldIfLogicEndBrick1);
            Assert.IsNotNull(newIfLogicEndBrick1);
            Assert.AreNotEqual(oldIfLogicEndBrick1.IfLogicBeginBrick, newIfLogicEndBrick1.IfLogicBeginBrick);
            Assert.AreEqual(oldIfLogicBeginBrick1, oldIfLogicEndBrick1.IfLogicBeginBrick);
            Assert.AreEqual(newIfLogicBeginBrick1, newIfLogicEndBrick1.IfLogicBeginBrick);



            oldIfLogicBeginBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[2] as XmlIfLogicBeginBrick;
            newIfLogicBeginBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[2] as XmlIfLogicBeginBrick;

            oldIfLogicElseBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[6] as XmlIfLogicElseBrick;
            newIfLogicElseBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[6] as XmlIfLogicElseBrick;
            Assert.IsNotNull(oldIfLogicElseBrick1);
            Assert.IsNotNull(newIfLogicElseBrick1);
            Assert.AreNotEqual(oldIfLogicElseBrick1.IfLogicBeginBrick, newIfLogicElseBrick1.IfLogicBeginBrick);
            Assert.AreEqual(oldIfLogicBeginBrick1, oldIfLogicElseBrick1.IfLogicBeginBrick);
            Assert.AreEqual(newIfLogicBeginBrick1, newIfLogicElseBrick1.IfLogicBeginBrick);

            oldIfLogicEndBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[7] as XmlIfLogicEndBrick;
            newIfLogicEndBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[7] as XmlIfLogicEndBrick;
            Assert.IsNotNull(oldIfLogicEndBrick1);
            Assert.IsNotNull(newIfLogicEndBrick1);
            Assert.AreNotEqual(oldIfLogicEndBrick1.IfLogicBeginBrick, newIfLogicEndBrick1.IfLogicBeginBrick);
            Assert.AreEqual(oldIfLogicBeginBrick1, oldIfLogicEndBrick1.IfLogicBeginBrick);
            Assert.AreEqual(newIfLogicBeginBrick1, newIfLogicEndBrick1.IfLogicBeginBrick);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task UpdateIfLogicElseBrickReferenceTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var oldSprite = project.SpriteList.Sprites[0];
            var newSprite = await oldSprite.Copy() as XmlSprite;

            Assert.IsNotNull(newSprite);

            var oldIfLogicElseBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[6] as XmlIfLogicElseBrick;
            var newIfLogicElseBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[6] as XmlIfLogicElseBrick;

            var oldIfLogicBeginBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[2] as XmlIfLogicBeginBrick;
            var newIfLogicBeginBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[2] as XmlIfLogicBeginBrick;
            Assert.IsNotNull(oldIfLogicBeginBrick1);
            Assert.IsNotNull(newIfLogicBeginBrick1);
            Assert.AreNotEqual(oldIfLogicBeginBrick1.IfLogicElseBrick, newIfLogicBeginBrick1.IfLogicElseBrick);
            Assert.AreEqual(oldIfLogicElseBrick1, oldIfLogicBeginBrick1.IfLogicElseBrick);
            Assert.AreEqual(newIfLogicElseBrick1, newIfLogicBeginBrick1.IfLogicElseBrick);

            var oldIfLogicEndBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[7] as XmlIfLogicEndBrick;
            var newIfLogicEndBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[7] as XmlIfLogicEndBrick;
            Assert.IsNotNull(oldIfLogicEndBrick1);
            Assert.IsNotNull(newIfLogicEndBrick1);
            Assert.AreNotEqual(oldIfLogicEndBrick1.IfLogicElseBrick, newIfLogicEndBrick1.IfLogicElseBrick);
            Assert.AreEqual(oldIfLogicElseBrick1, oldIfLogicEndBrick1.IfLogicElseBrick);
            Assert.AreEqual(newIfLogicElseBrick1, newIfLogicEndBrick1.IfLogicElseBrick);



            oldIfLogicElseBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[4] as XmlIfLogicElseBrick;
            newIfLogicElseBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[4] as XmlIfLogicElseBrick;

            oldIfLogicBeginBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[3] as XmlIfLogicBeginBrick;
            newIfLogicBeginBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[3] as XmlIfLogicBeginBrick;
            Assert.IsNotNull(oldIfLogicBeginBrick1);
            Assert.IsNotNull(newIfLogicBeginBrick1);
            Assert.AreNotEqual(oldIfLogicBeginBrick1.IfLogicElseBrick, newIfLogicBeginBrick1.IfLogicElseBrick);
            Assert.AreEqual(oldIfLogicElseBrick1, oldIfLogicBeginBrick1.IfLogicElseBrick);
            Assert.AreEqual(newIfLogicElseBrick1, newIfLogicBeginBrick1.IfLogicElseBrick);

            oldIfLogicEndBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[5] as XmlIfLogicEndBrick;
            newIfLogicEndBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[5] as XmlIfLogicEndBrick;
            Assert.IsNotNull(oldIfLogicEndBrick1);
            Assert.IsNotNull(newIfLogicEndBrick1);
            Assert.AreNotEqual(oldIfLogicEndBrick1.IfLogicElseBrick, newIfLogicEndBrick1.IfLogicElseBrick);
            Assert.AreEqual(oldIfLogicElseBrick1, oldIfLogicEndBrick1.IfLogicElseBrick);
            Assert.AreEqual(newIfLogicElseBrick1, newIfLogicEndBrick1.IfLogicElseBrick);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task UpdateIfLogicEndBrickReferenceTest()
        {
            var project = await SampleLoader.LoadSampleXmlProject("default.catroid", "default");
            var oldSprite = project.SpriteList.Sprites[0];
            var newSprite = await oldSprite.Copy() as XmlSprite;

            Assert.IsNotNull(newSprite);

            var oldIfLogicEndBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[5] as XmlIfLogicEndBrick;
            var newIfLogicEndBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[5] as XmlIfLogicEndBrick;

            var oldIfLogicBeginBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[3] as XmlIfLogicBeginBrick;
            var newIfLogicBeginBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[3] as XmlIfLogicBeginBrick;
            Assert.IsNotNull(oldIfLogicBeginBrick1);
            Assert.IsNotNull(newIfLogicBeginBrick1);
            Assert.AreNotEqual(oldIfLogicBeginBrick1.IfLogicEndBrick, newIfLogicBeginBrick1.IfLogicEndBrick);
            Assert.AreEqual(oldIfLogicEndBrick1, oldIfLogicBeginBrick1.IfLogicEndBrick);
            Assert.AreEqual(newIfLogicEndBrick1, newIfLogicBeginBrick1.IfLogicEndBrick);

            var oldIfLogicElseBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[4] as XmlIfLogicElseBrick;
            var newIfLogicElseBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[4] as XmlIfLogicElseBrick;
            Assert.IsNotNull(oldIfLogicElseBrick1);
            Assert.IsNotNull(newIfLogicElseBrick1);
            Assert.AreNotEqual(oldIfLogicElseBrick1.IfLogicEndBrick, newIfLogicElseBrick1.IfLogicEndBrick);
            Assert.AreEqual(oldIfLogicEndBrick1, oldIfLogicElseBrick1.IfLogicEndBrick);
            Assert.AreEqual(newIfLogicEndBrick1, newIfLogicElseBrick1.IfLogicEndBrick);



            oldIfLogicEndBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[7] as XmlIfLogicEndBrick;
            newIfLogicEndBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[7] as XmlIfLogicEndBrick;

            oldIfLogicBeginBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[2] as XmlIfLogicBeginBrick;
            newIfLogicBeginBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[2] as XmlIfLogicBeginBrick;
            Assert.IsNotNull(oldIfLogicBeginBrick1);
            Assert.IsNotNull(newIfLogicBeginBrick1);
            Assert.AreNotEqual(oldIfLogicBeginBrick1.IfLogicEndBrick, newIfLogicBeginBrick1.IfLogicEndBrick);
            Assert.AreEqual(oldIfLogicEndBrick1, oldIfLogicBeginBrick1.IfLogicEndBrick);
            Assert.AreEqual(newIfLogicEndBrick1, newIfLogicBeginBrick1.IfLogicEndBrick);

            oldIfLogicElseBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[6] as XmlIfLogicElseBrick;
            newIfLogicElseBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[6] as XmlIfLogicElseBrick;
            Assert.IsNotNull(oldIfLogicElseBrick1);
            Assert.IsNotNull(newIfLogicElseBrick1);
            Assert.AreNotEqual(oldIfLogicElseBrick1.IfLogicEndBrick, newIfLogicElseBrick1.IfLogicEndBrick);
            Assert.AreEqual(oldIfLogicEndBrick1, oldIfLogicElseBrick1.IfLogicEndBrick);
            Assert.AreEqual(newIfLogicEndBrick1, newIfLogicElseBrick1.IfLogicEndBrick);
        }
    }
}
