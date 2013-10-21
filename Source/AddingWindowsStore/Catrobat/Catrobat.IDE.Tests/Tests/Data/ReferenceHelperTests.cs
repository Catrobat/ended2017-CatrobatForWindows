using System;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Bricks;
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

            var sprite = project.SpriteList.Sprites[0];
            var setCostumeBrick = sprite.Scripts.Scripts[0].Bricks.Bricks[0] as SetCostumeBrick;
            
            Assert.IsNotNull(setCostumeBrick);
            Assert.AreEqual(sprite.Costumes.Costumes[0], setCostumeBrick.Costume);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void GetSoundObjectTest()
        {
            var project = SampleLoader.LoadSampleProject("default.catroid", "default");
            var sprite = project.SpriteList.Sprites[1];
            var playSoundBrick = sprite.Scripts.Scripts[0].Bricks.Bricks[1] as PlaySoundBrick;

            Assert.IsNotNull(playSoundBrick);
            Assert.AreEqual(sprite.Sounds.Sounds[0], playSoundBrick.Sound);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void GetSpriteObjectTest()
        {
            var project = SampleLoader.LoadSampleProject("default.catroid", "default");
            var sprite = project.SpriteList.Sprites[1];
            var pointToBrick = sprite.Scripts.Scripts[0].Bricks.Bricks[2] as PointToBrick;

            Assert.IsNotNull(pointToBrick);
            Assert.AreEqual(project.SpriteList.Sprites[1], pointToBrick.PointedSprite);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void GetUserVariableObjectTest()
        {
            var project = SampleLoader.LoadSampleProject("default.catroid", "default");
            var sprite1 = project.SpriteList.Sprites[0];
            var setVariableBrick = sprite1.Scripts.Scripts[0].Bricks.Bricks[1] as SetVariableBrick;

            var entries = project.VariableList.ObjectVariableList.ObjectVariableEntries;

            Assert.IsNotNull(setVariableBrick);
            Assert.AreEqual(entries[0].VariableList.UserVariables[0], setVariableBrick.UserVariable);

            var sprite2 = project.SpriteList.Sprites[1];
            var changeVariableBrick = sprite2.Scripts.Scripts[1].Bricks.Bricks[5] as ChangeVariableBrick;

            Assert.IsNotNull(changeVariableBrick);
            Assert.AreEqual(project.VariableList.ProgramVariableList.UserVariables[0], changeVariableBrick.UserVariable);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void GetForeverBrickObjectTest()
        {
            var project = SampleLoader.LoadSampleProject("default.catroid", "default");
            var sprite = project.SpriteList.Sprites[1];
            var foreverBrick = sprite.Scripts.Scripts[0].Bricks.Bricks[4] as ForeverLoopEndBrick;

            Assert.IsNotNull(foreverBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[3], foreverBrick.LoopBeginBrick);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void GetRepeatBrickObjectTest()
        {
            var project = SampleLoader.LoadSampleProject("default.catroid", "default");
            var sprite = project.SpriteList.Sprites[1];
            var repeatBrick = sprite.Scripts.Scripts[0].Bricks.Bricks[6] as RepeatLoopEndBrick;

            Assert.IsNotNull(repeatBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[5], repeatBrick.LoopBeginBrick);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void GetForeverLoopEndBrickObjectTest()
        {
            var project = SampleLoader.LoadSampleProject("default.catroid", "default");
            var sprite = project.SpriteList.Sprites[1];
            var foreverBrick = sprite.Scripts.Scripts[0].Bricks.Bricks[3] as ForeverBrick;

            Assert.IsNotNull(foreverBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[4], foreverBrick.LoopEndBrick);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void GetRepeatLoopEndBrickObjectTest()
        {
            var project = SampleLoader.LoadSampleProject("default.catroid", "default");
            var sprite = project.SpriteList.Sprites[1];
            var repeatBrick = sprite.Scripts.Scripts[0].Bricks.Bricks[5] as RepeatBrick;

            Assert.IsNotNull(repeatBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[6], repeatBrick.LoopEndBrick);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void GetLoopEndBrickObjectTest()
        {
            var project = SampleLoader.LoadSampleProject("default.catroid", "default");
            var sprite = project.SpriteList.Sprites[1];
            var loopEndBrick = sprite.Scripts.Scripts[0].Bricks.Bricks[4] as LoopEndBrick;

            Assert.IsNotNull(loopEndBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[3], loopEndBrick.LoopBeginBrick);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void GetIfLogicBeginBrickObjectTest()
        {
            var project = SampleLoader.LoadSampleProject("default.catroid", "default");
            var sprite = project.SpriteList.Sprites[0];
            var ifLogicBeginBrick1 = sprite.Scripts.Scripts[0].Bricks.Bricks[2] as IfLogicBeginBrick;

            Assert.IsNotNull(ifLogicBeginBrick1);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[6], ifLogicBeginBrick1.IfLogicElseBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[7], ifLogicBeginBrick1.IfLogicEndBrick);

            var ifLogicBeginBrick2 = sprite.Scripts.Scripts[0].Bricks.Bricks[3] as IfLogicBeginBrick;

            Assert.IsNotNull(ifLogicBeginBrick2);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[4], ifLogicBeginBrick2.IfLogicElseBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[5], ifLogicBeginBrick2.IfLogicEndBrick);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void GetIfLogicElseBrickObjectTest()
        {
            var project = SampleLoader.LoadSampleProject("default.catroid", "default");
            var sprite = project.SpriteList.Sprites[0];
            var ifLogicElseBrick1 = sprite.Scripts.Scripts[0].Bricks.Bricks[4] as IfLogicElseBrick;

            Assert.IsNotNull(ifLogicElseBrick1);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[3], ifLogicElseBrick1.IfLogicBeginBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[5], ifLogicElseBrick1.IfLogicEndBrick);

            var ifLogicElseBrick2 = sprite.Scripts.Scripts[0].Bricks.Bricks[6] as IfLogicElseBrick;

            Assert.IsNotNull(ifLogicElseBrick2);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[2], ifLogicElseBrick2.IfLogicBeginBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[7], ifLogicElseBrick2.IfLogicEndBrick);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void GetIfLogicEndBrickObjectTest()
        {
            var project = SampleLoader.LoadSampleProject("default.catroid", "default");
            var sprite = project.SpriteList.Sprites[0];
            var ifLogicEndBrick1 = sprite.Scripts.Scripts[0].Bricks.Bricks[5] as IfLogicEndBrick;

            Assert.IsNotNull(ifLogicEndBrick1);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[3], ifLogicEndBrick1.IfLogicBeginBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[4], ifLogicEndBrick1.IfLogicElseBrick);

            var ifLogicEndBrick2 = sprite.Scripts.Scripts[0].Bricks.Bricks[7] as IfLogicEndBrick;

            Assert.IsNotNull(ifLogicEndBrick2);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[2], ifLogicEndBrick2.IfLogicBeginBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[6], ifLogicEndBrick2.IfLogicElseBrick);
        }


        [TestMethod, TestCategory("GatedTests")]
        public void GetCostumeReferenceStringTest()
        {
            var project = SampleLoader.LoadSampleProject("default.catroid", "default");
            var costumeReference = (project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[0] as SetCostumeBrick).CostumeReference;

            Assert.IsNotNull(costumeReference);

            var reference = ReferenceHelper.GetReferenceString(costumeReference);

            Assert.AreEqual("../../../../../lookList/look[1]", reference);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void GetSoundReferenceStringTest()
        {
            var project = SampleLoader.LoadSampleProject("default.catroid", "default");
            var soundReference = (project.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks[1] as PlaySoundBrick).SoundReference;

            Assert.IsNotNull(soundReference);

            var reference = ReferenceHelper.GetReferenceString(soundReference);

            Assert.AreEqual("../../../../../soundList/sound[1]", reference);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void GetSpriteReferenceStringTest()
        {
            var project = SampleLoader.LoadSampleProject("default.catroid", "default");
            var pointedSpriteReference = (project.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks[2] as PointToBrick).PointedSpriteReference;

            Assert.IsNotNull(pointedSpriteReference);

            var reference = ReferenceHelper.GetReferenceString(pointedSpriteReference);

            Assert.AreEqual("../../../../../../object[2]", reference);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void GetVariableReferenceStringTest()
        {
            var project = SampleLoader.LoadSampleProject("default.catroid", "default");
            var userVariableReference = (project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[1] as SetVariableBrick).UserVariableReference;

            Assert.IsNotNull(userVariableReference);

            var reference = ReferenceHelper.GetReferenceString(userVariableReference);

            Assert.AreEqual("../../../../../variables/objectVariableList/entry[1]/list/userVariable[1]", reference);

            userVariableReference = (project.SpriteList.Sprites[1].Scripts.Scripts[1].Bricks.Bricks[5] as ChangeVariableBrick).UserVariableReference;

            Assert.IsNotNull(userVariableReference);

            reference = ReferenceHelper.GetReferenceString(userVariableReference);

            Assert.AreEqual("../../../../../variables/programVariableList/userVariable[1]", reference);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void GetForeverBrickReferenceStringTest()
        {
            var project = SampleLoader.LoadSampleProject("default.catroid", "default");
            var loopBeginBrickReference = (project.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks[4] as LoopEndBrick).LoopBeginBrickReference;

            Assert.IsNotNull(loopBeginBrickReference);

            var reference = ReferenceHelper.GetReferenceString(loopBeginBrickReference);

            Assert.AreEqual("../../foreverBrick[1]", reference);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void GetRepeatBrickReferenceStringTest()
        {
            var project = SampleLoader.LoadSampleProject("default.catroid", "default");
            var loopBeginBrickReference = (project.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks[6] as LoopEndBrick).LoopBeginBrickReference;

            Assert.IsNotNull(loopBeginBrickReference);

            var reference = ReferenceHelper.GetReferenceString(loopBeginBrickReference);

            Assert.AreEqual("../../repeatBrick[1]", reference);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void GetForeverLoopEndBrickReferenceStringTest()
        {
            var project = SampleLoader.LoadSampleProject("default.catroid", "default");
            var loopEndBrickReference = (project.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks[3] as ForeverBrick).LoopEndBrickReference;

            Assert.IsNotNull(loopEndBrickReference);

            var reference = ReferenceHelper.GetReferenceString(loopEndBrickReference);

            Assert.AreEqual("../../loopEndlessBrick[1]", reference);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void GetRepeatLoopEndBrickReferenceStringTest()
        {
            var project = SampleLoader.LoadSampleProject("default.catroid", "default");
            var loopEndBrickReference = (project.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks[5] as RepeatBrick).LoopEndBrickReference;

            Assert.IsNotNull(loopEndBrickReference);

            var reference = ReferenceHelper.GetReferenceString(loopEndBrickReference);

            Assert.AreEqual("../../loopEndBrick[1]", reference);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void GetIfLogicBeginBrickReferenceStringTest()
        {
            var project = SampleLoader.LoadSampleProject("default.catroid", "default");

            var ifLogicBeginBrickReference = (project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[4] as IfLogicElseBrick).IfLogicBeginBrickReference;
            Assert.IsNotNull(ifLogicBeginBrickReference);
            var reference = ReferenceHelper.GetReferenceString(ifLogicBeginBrickReference);
            Assert.AreEqual("../../ifLogicBeginBrick[2]", reference);

            ifLogicBeginBrickReference = (project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[5] as IfLogicEndBrick).IfLogicBeginBrickReference;
            Assert.IsNotNull(ifLogicBeginBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicBeginBrickReference);
            Assert.AreEqual("../../ifLogicBeginBrick[2]", reference);

            ifLogicBeginBrickReference = (project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[6] as IfLogicElseBrick).IfLogicBeginBrickReference;
            Assert.IsNotNull(ifLogicBeginBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicBeginBrickReference);
            Assert.AreEqual("../../ifLogicBeginBrick[1]", reference);

            ifLogicBeginBrickReference = (project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[7] as IfLogicEndBrick).IfLogicBeginBrickReference;
            Assert.IsNotNull(ifLogicBeginBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicBeginBrickReference);
            Assert.AreEqual("../../ifLogicBeginBrick[1]", reference);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void GetIfLogicElseBrickReferenceStringTest()
        {
            var project = SampleLoader.LoadSampleProject("default.catroid", "default");

            var ifLogicElseBrickReference = (project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[2] as IfLogicBeginBrick).IfLogicElseBrickReference;
            Assert.IsNotNull(ifLogicElseBrickReference);
            var reference = ReferenceHelper.GetReferenceString(ifLogicElseBrickReference);
            Assert.AreEqual("../../ifLogicElseBrick[2]", reference);

            ifLogicElseBrickReference = (project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[5] as IfLogicEndBrick).IfLogicElseBrickReference;
            Assert.IsNotNull(ifLogicElseBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicElseBrickReference);
            Assert.AreEqual("../../ifLogicElseBrick[1]", reference);

            ifLogicElseBrickReference = (project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[3] as IfLogicBeginBrick).IfLogicElseBrickReference;
            Assert.IsNotNull(ifLogicElseBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicElseBrickReference);
            Assert.AreEqual("../../ifLogicElseBrick[1]", reference);

            ifLogicElseBrickReference = (project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[7] as IfLogicEndBrick).IfLogicElseBrickReference;
            Assert.IsNotNull(ifLogicElseBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicElseBrickReference);
            Assert.AreEqual("../../ifLogicElseBrick[2]", reference);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void GetIfLogicEndBrickReferenceStringTest()
        {
            var project = SampleLoader.LoadSampleProject("default.catroid", "default");

            var ifLogicEndBrickReference = (project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[2] as IfLogicBeginBrick).IfLogicEndBrickReference;
            Assert.IsNotNull(ifLogicEndBrickReference);
            var reference = ReferenceHelper.GetReferenceString(ifLogicEndBrickReference);
            Assert.AreEqual("../../ifLogicEndBrick[2]", reference);

            ifLogicEndBrickReference = (project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[4] as IfLogicElseBrick).IfLogicEndBrickReference;
            Assert.IsNotNull(ifLogicEndBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicEndBrickReference);
            Assert.AreEqual("../../ifLogicEndBrick[1]", reference);

            ifLogicEndBrickReference = (project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[3] as IfLogicBeginBrick).IfLogicEndBrickReference;
            Assert.IsNotNull(ifLogicEndBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicEndBrickReference);
            Assert.AreEqual("../../ifLogicEndBrick[1]", reference);

            ifLogicEndBrickReference = (project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[6] as IfLogicElseBrick).IfLogicEndBrickReference;
            Assert.IsNotNull(ifLogicEndBrickReference);
            reference = ReferenceHelper.GetReferenceString(ifLogicEndBrickReference);
            Assert.AreEqual("../../ifLogicEndBrick[2]", reference);
        }


        [TestMethod, TestCategory("GatedTests")]
        public void UpdateCostumeReferenceTest()
        {
            var project = SampleLoader.LoadSampleProject("default.catroid", "default");
            var oldSprite = project.SpriteList.Sprites[0];
            var newSprite = oldSprite.Copy() as Sprite;
            Assert.IsNotNull(newSprite);

            var oldCostume = oldSprite.Costumes.Costumes[0];
            var newCostume = newSprite.Costumes.Costumes[0];

            var oldCostumeBrick = oldSprite.Scripts.Scripts[0].Bricks.Bricks[0] as SetCostumeBrick;
            var newCostumeBrick = newSprite.Scripts.Scripts[0].Bricks.Bricks[0] as SetCostumeBrick;
            Assert.IsNotNull(oldCostumeBrick);
            Assert.IsNotNull(newCostumeBrick);
            Assert.AreNotEqual(oldCostumeBrick.Costume, newCostumeBrick.Costume);
            Assert.AreEqual(oldCostume, oldCostumeBrick.Costume);
            Assert.AreEqual(newCostume, newCostumeBrick.Costume);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void UpdateSoundReferenceTest()
        {
            var project = SampleLoader.LoadSampleProject("default.catroid", "default");
            var oldSprite = project.SpriteList.Sprites[1];
            var newSprite = oldSprite.Copy() as Sprite;
            Assert.IsNotNull(newSprite);

            var oldSound = oldSprite.Sounds.Sounds[0];
            var newSound = newSprite.Sounds.Sounds[0];

            var oldPlaySoundBrick = oldSprite.Scripts.Scripts[0].Bricks.Bricks[1] as PlaySoundBrick;
            var newPlaySoundBrick = newSprite.Scripts.Scripts[0].Bricks.Bricks[1] as PlaySoundBrick;
            Assert.IsNotNull(oldPlaySoundBrick);
            Assert.IsNotNull(newPlaySoundBrick);
            Assert.AreNotEqual(oldPlaySoundBrick.Sound, newPlaySoundBrick.Sound);
            Assert.AreEqual(oldSound, oldPlaySoundBrick.Sound);
            Assert.AreEqual(newSound, newPlaySoundBrick.Sound);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void CopyVariableOnSpriteCopyTest()
        {
            var project = SampleLoader.LoadSampleProject("default.catroid", "default");
            var oldSprite = project.SpriteList.Sprites[0];
            var newSprite = oldSprite.Copy() as Sprite;
            Assert.IsNotNull(newSprite);

            var oldVariable = project.VariableList.ObjectVariableList.ObjectVariableEntries[0].VariableList.UserVariables[0];
            var newVariable = project.VariableList.ObjectVariableList.ObjectVariableEntries[2].VariableList.UserVariables[0];

            var oldBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[1] as SetVariableBrick;
            var newBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[1] as SetVariableBrick;
            Assert.IsNotNull(oldBrick1);
            Assert.IsNotNull(newBrick1);
            Assert.AreNotEqual(oldBrick1.UserVariable, newBrick1.UserVariable);
            Assert.AreEqual(oldVariable, oldBrick1.UserVariable);
            Assert.AreEqual(newVariable, newBrick1.UserVariable);


            oldSprite = project.SpriteList.Sprites[1];
            newSprite = oldSprite.Copy() as Sprite;
            Assert.IsNotNull(newSprite);

            oldVariable = project.VariableList.ProgramVariableList.UserVariables[0];
            newVariable = project.VariableList.ProgramVariableList.UserVariables[0];

            var oldBrick2 = oldSprite.Scripts.Scripts[1].Bricks.Bricks[5] as ChangeVariableBrick;
            var newBrick2 = newSprite.Scripts.Scripts[1].Bricks.Bricks[5] as ChangeVariableBrick;
            Assert.IsNotNull(oldBrick2);
            Assert.IsNotNull(newBrick2);
            Assert.AreEqual(oldBrick2.UserVariable, newBrick2.UserVariable);
            Assert.AreEqual(oldVariable, oldBrick2.UserVariable);
            Assert.AreEqual(newVariable, newBrick2.UserVariable);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void UpdateLoopBeginBrickReferenceTest()
        {
            var project = SampleLoader.LoadSampleProject("default.catroid", "default");
            var oldSprite = project.SpriteList.Sprites[1];
            var newSprite = oldSprite.Copy() as Sprite;

            Assert.IsNotNull(newSprite);

            var oldLoopBeginBrick = oldSprite.Scripts.Scripts[0].Bricks.Bricks[3] as LoopBeginBrick;
            var newLoopBeginBrick = newSprite.Scripts.Scripts[0].Bricks.Bricks[3] as LoopBeginBrick;

            var oldBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[4] as LoopEndBrick;
            var newBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[4] as LoopEndBrick;
            Assert.IsNotNull(oldBrick1);
            Assert.IsNotNull(newBrick1);
            Assert.AreNotEqual(oldBrick1.LoopBeginBrick, newBrick1.LoopBeginBrick);
            Assert.AreEqual(oldLoopBeginBrick, oldBrick1.LoopBeginBrick);
            Assert.AreEqual(newLoopBeginBrick, newBrick1.LoopBeginBrick);


            oldLoopBeginBrick = oldSprite.Scripts.Scripts[0].Bricks.Bricks[5] as LoopBeginBrick;
            newLoopBeginBrick = newSprite.Scripts.Scripts[0].Bricks.Bricks[5] as LoopBeginBrick;

            var oldBrick2 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[6] as LoopEndBrick;
            var newBrick2 = newSprite.Scripts.Scripts[0].Bricks.Bricks[6] as LoopEndBrick;
            Assert.IsNotNull(oldBrick2);
            Assert.IsNotNull(newBrick2);
            Assert.AreNotEqual(oldBrick2.LoopBeginBrick, newBrick2.LoopBeginBrick);
            Assert.AreEqual(oldLoopBeginBrick, oldBrick2.LoopBeginBrick);
            Assert.AreEqual(newLoopBeginBrick, newBrick2.LoopBeginBrick);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void UpdateLoopEndBrickReferenceTest()
        {
            var project = SampleLoader.LoadSampleProject("default.catroid", "default");
            var oldSprite = project.SpriteList.Sprites[1];
            var newSprite = oldSprite.Copy() as Sprite;

            Assert.IsNotNull(newSprite);

            var oldLoopEndBrick = oldSprite.Scripts.Scripts[0].Bricks.Bricks[4] as LoopEndBrick;
            var newLoopEndBrick = newSprite.Scripts.Scripts[0].Bricks.Bricks[4] as LoopEndBrick;

            var oldBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[3] as ForeverBrick;
            var newBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[3] as ForeverBrick;
            Assert.IsNotNull(oldBrick1);
            Assert.IsNotNull(newBrick1);
            Assert.AreNotEqual(oldBrick1.LoopEndBrick, newBrick1.LoopEndBrick);
            Assert.AreEqual(oldLoopEndBrick, oldBrick1.LoopEndBrick);
            Assert.AreEqual(newLoopEndBrick, newBrick1.LoopEndBrick);


            oldLoopEndBrick = oldSprite.Scripts.Scripts[0].Bricks.Bricks[6] as LoopEndBrick;
            newLoopEndBrick = newSprite.Scripts.Scripts[0].Bricks.Bricks[6] as LoopEndBrick;

            var oldBrick2 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[5] as RepeatBrick;
            var newBrick2 = newSprite.Scripts.Scripts[0].Bricks.Bricks[5] as RepeatBrick;
            Assert.IsNotNull(oldBrick2);
            Assert.IsNotNull(newBrick2);
            Assert.AreNotEqual(oldBrick2.LoopEndBrick, newBrick2.LoopEndBrick);
            Assert.AreEqual(oldLoopEndBrick, oldBrick2.LoopEndBrick);
            Assert.AreEqual(newLoopEndBrick, newBrick2.LoopEndBrick);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void UpdateIfLogicBeginBrickReferenceTest()
        {
            var project = SampleLoader.LoadSampleProject("default.catroid", "default");
            var oldSprite = project.SpriteList.Sprites[0];
            var newSprite = oldSprite.Copy() as Sprite;

            Assert.IsNotNull(newSprite);

            var oldIfLogicBeginBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[3] as IfLogicBeginBrick;
            var newIfLogicBeginBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[3] as IfLogicBeginBrick;

            var oldIfLogicElseBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[4] as IfLogicElseBrick;
            var newIfLogicElseBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[4] as IfLogicElseBrick;
            Assert.IsNotNull(oldIfLogicElseBrick1);
            Assert.IsNotNull(newIfLogicElseBrick1);
            Assert.AreNotEqual(oldIfLogicElseBrick1.IfLogicBeginBrick, newIfLogicElseBrick1.IfLogicBeginBrick);
            Assert.AreEqual(oldIfLogicBeginBrick1, oldIfLogicElseBrick1.IfLogicBeginBrick);
            Assert.AreEqual(newIfLogicBeginBrick1, newIfLogicElseBrick1.IfLogicBeginBrick);

            var oldIfLogicEndBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[5] as IfLogicEndBrick;
            var newIfLogicEndBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[5] as IfLogicEndBrick;
            Assert.IsNotNull(oldIfLogicEndBrick1);
            Assert.IsNotNull(newIfLogicEndBrick1);
            Assert.AreNotEqual(oldIfLogicEndBrick1.IfLogicBeginBrick, newIfLogicEndBrick1.IfLogicBeginBrick);
            Assert.AreEqual(oldIfLogicBeginBrick1, oldIfLogicEndBrick1.IfLogicBeginBrick);
            Assert.AreEqual(newIfLogicBeginBrick1, newIfLogicEndBrick1.IfLogicBeginBrick);



            oldIfLogicBeginBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[2] as IfLogicBeginBrick;
            newIfLogicBeginBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[2] as IfLogicBeginBrick;

            oldIfLogicElseBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[6] as IfLogicElseBrick;
            newIfLogicElseBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[6] as IfLogicElseBrick;
            Assert.IsNotNull(oldIfLogicElseBrick1);
            Assert.IsNotNull(newIfLogicElseBrick1);
            Assert.AreNotEqual(oldIfLogicElseBrick1.IfLogicBeginBrick, newIfLogicElseBrick1.IfLogicBeginBrick);
            Assert.AreEqual(oldIfLogicBeginBrick1, oldIfLogicElseBrick1.IfLogicBeginBrick);
            Assert.AreEqual(newIfLogicBeginBrick1, newIfLogicElseBrick1.IfLogicBeginBrick);

            oldIfLogicEndBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[7] as IfLogicEndBrick;
            newIfLogicEndBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[7] as IfLogicEndBrick;
            Assert.IsNotNull(oldIfLogicEndBrick1);
            Assert.IsNotNull(newIfLogicEndBrick1);
            Assert.AreNotEqual(oldIfLogicEndBrick1.IfLogicBeginBrick, newIfLogicEndBrick1.IfLogicBeginBrick);
            Assert.AreEqual(oldIfLogicBeginBrick1, oldIfLogicEndBrick1.IfLogicBeginBrick);
            Assert.AreEqual(newIfLogicBeginBrick1, newIfLogicEndBrick1.IfLogicBeginBrick);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void UpdateIfLogicElseBrickReferenceTest()
        {
            var project = SampleLoader.LoadSampleProject("default.catroid", "default");
            var oldSprite = project.SpriteList.Sprites[0];
            var newSprite = oldSprite.Copy() as Sprite;

            Assert.IsNotNull(newSprite);

            var oldIfLogicElseBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[6] as IfLogicElseBrick;
            var newIfLogicElseBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[6] as IfLogicElseBrick;

            var oldIfLogicBeginBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[2] as IfLogicBeginBrick;
            var newIfLogicBeginBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[2] as IfLogicBeginBrick;
            Assert.IsNotNull(oldIfLogicBeginBrick1);
            Assert.IsNotNull(newIfLogicBeginBrick1);
            Assert.AreNotEqual(oldIfLogicBeginBrick1.IfLogicElseBrick, newIfLogicBeginBrick1.IfLogicElseBrick);
            Assert.AreEqual(oldIfLogicElseBrick1, oldIfLogicBeginBrick1.IfLogicElseBrick);
            Assert.AreEqual(newIfLogicElseBrick1, newIfLogicBeginBrick1.IfLogicElseBrick);

            var oldIfLogicEndBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[7] as IfLogicEndBrick;
            var newIfLogicEndBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[7] as IfLogicEndBrick;
            Assert.IsNotNull(oldIfLogicEndBrick1);
            Assert.IsNotNull(newIfLogicEndBrick1);
            Assert.AreNotEqual(oldIfLogicEndBrick1.IfLogicElseBrick, newIfLogicEndBrick1.IfLogicElseBrick);
            Assert.AreEqual(oldIfLogicElseBrick1, oldIfLogicEndBrick1.IfLogicElseBrick);
            Assert.AreEqual(newIfLogicElseBrick1, newIfLogicEndBrick1.IfLogicElseBrick);



            oldIfLogicElseBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[4] as IfLogicElseBrick;
            newIfLogicElseBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[4] as IfLogicElseBrick;

            oldIfLogicBeginBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[3] as IfLogicBeginBrick;
            newIfLogicBeginBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[3] as IfLogicBeginBrick;
            Assert.IsNotNull(oldIfLogicBeginBrick1);
            Assert.IsNotNull(newIfLogicBeginBrick1);
            Assert.AreNotEqual(oldIfLogicBeginBrick1.IfLogicElseBrick, newIfLogicBeginBrick1.IfLogicElseBrick);
            Assert.AreEqual(oldIfLogicElseBrick1, oldIfLogicBeginBrick1.IfLogicElseBrick);
            Assert.AreEqual(newIfLogicElseBrick1, newIfLogicBeginBrick1.IfLogicElseBrick);

            oldIfLogicEndBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[5] as IfLogicEndBrick;
            newIfLogicEndBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[5] as IfLogicEndBrick;
            Assert.IsNotNull(oldIfLogicEndBrick1);
            Assert.IsNotNull(newIfLogicEndBrick1);
            Assert.AreNotEqual(oldIfLogicEndBrick1.IfLogicElseBrick, newIfLogicEndBrick1.IfLogicElseBrick);
            Assert.AreEqual(oldIfLogicElseBrick1, oldIfLogicEndBrick1.IfLogicElseBrick);
            Assert.AreEqual(newIfLogicElseBrick1, newIfLogicEndBrick1.IfLogicElseBrick);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void UpdateIfLogicEndBrickReferenceTest()
        {
            var project = SampleLoader.LoadSampleProject("default.catroid", "default");
            var oldSprite = project.SpriteList.Sprites[0];
            var newSprite = oldSprite.Copy() as Sprite;

            Assert.IsNotNull(newSprite);

            var oldIfLogicEndBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[5] as IfLogicEndBrick;
            var newIfLogicEndBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[5] as IfLogicEndBrick;

            var oldIfLogicBeginBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[3] as IfLogicBeginBrick;
            var newIfLogicBeginBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[3] as IfLogicBeginBrick;
            Assert.IsNotNull(oldIfLogicBeginBrick1);
            Assert.IsNotNull(newIfLogicBeginBrick1);
            Assert.AreNotEqual(oldIfLogicBeginBrick1.IfLogicEndBrick, newIfLogicBeginBrick1.IfLogicEndBrick);
            Assert.AreEqual(oldIfLogicEndBrick1, oldIfLogicBeginBrick1.IfLogicEndBrick);
            Assert.AreEqual(newIfLogicEndBrick1, newIfLogicBeginBrick1.IfLogicEndBrick);

            var oldIfLogicElseBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[4] as IfLogicElseBrick;
            var newIfLogicElseBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[4] as IfLogicElseBrick;
            Assert.IsNotNull(oldIfLogicElseBrick1);
            Assert.IsNotNull(newIfLogicElseBrick1);
            Assert.AreNotEqual(oldIfLogicElseBrick1.IfLogicEndBrick, newIfLogicElseBrick1.IfLogicEndBrick);
            Assert.AreEqual(oldIfLogicEndBrick1, oldIfLogicElseBrick1.IfLogicEndBrick);
            Assert.AreEqual(newIfLogicEndBrick1, newIfLogicElseBrick1.IfLogicEndBrick);



            oldIfLogicEndBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[7] as IfLogicEndBrick;
            newIfLogicEndBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[7] as IfLogicEndBrick;

            oldIfLogicBeginBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[2] as IfLogicBeginBrick;
            newIfLogicBeginBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[2] as IfLogicBeginBrick;
            Assert.IsNotNull(oldIfLogicBeginBrick1);
            Assert.IsNotNull(newIfLogicBeginBrick1);
            Assert.AreNotEqual(oldIfLogicBeginBrick1.IfLogicEndBrick, newIfLogicBeginBrick1.IfLogicEndBrick);
            Assert.AreEqual(oldIfLogicEndBrick1, oldIfLogicBeginBrick1.IfLogicEndBrick);
            Assert.AreEqual(newIfLogicEndBrick1, newIfLogicBeginBrick1.IfLogicEndBrick);

            oldIfLogicElseBrick1 = oldSprite.Scripts.Scripts[0].Bricks.Bricks[6] as IfLogicElseBrick;
            newIfLogicElseBrick1 = newSprite.Scripts.Scripts[0].Bricks.Bricks[6] as IfLogicElseBrick;
            Assert.IsNotNull(oldIfLogicElseBrick1);
            Assert.IsNotNull(newIfLogicElseBrick1);
            Assert.AreNotEqual(oldIfLogicElseBrick1.IfLogicEndBrick, newIfLogicElseBrick1.IfLogicEndBrick);
            Assert.AreEqual(oldIfLogicEndBrick1, oldIfLogicElseBrick1.IfLogicEndBrick);
            Assert.AreEqual(newIfLogicEndBrick1, newIfLogicElseBrick1.IfLogicEndBrick);
        }
    }
}
