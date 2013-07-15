using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Objects.Bricks;
using Catrobat.TestsCommon.Misc;
using Catrobat.TestsCommon.SampleData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.Core.Objects;

namespace Catrobat.TestsCommon.Tests.Data
{
    [TestClass]
    public class ReferenceHelperTests
    {
        [ClassInitialize()]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }

        [TestMethod]
        public void GetCostumeObjectTest()
        {
            var project = (SampleLoader.LoadSampleProject("default.catroid","default")).CurrentProject;
            var sprite = project.SpriteList.Sprites[0];
            var setCostumeBrick = sprite.Scripts.Scripts[0].Bricks.Bricks[0] as SetCostumeBrick;
            
            Assert.IsNotNull(setCostumeBrick);
            Assert.AreEqual(sprite.Costumes.Costumes[0], setCostumeBrick.Costume);
        }

        [TestMethod]
        public void GetSoundObjectTest()
        {
            var project = (SampleLoader.LoadSampleProject("default.catroid", "default")).CurrentProject;
            var sprite = project.SpriteList.Sprites[1];
            var playSoundBrick = sprite.Scripts.Scripts[0].Bricks.Bricks[1] as PlaySoundBrick;

            Assert.IsNotNull(playSoundBrick);
            Assert.AreEqual(sprite.Sounds.Sounds[0], playSoundBrick.Sound);
        }

        [TestMethod]
        public void GetSpriteObjectTest()
        {
            var project = (SampleLoader.LoadSampleProject("default.catroid", "default")).CurrentProject;
            var sprite = project.SpriteList.Sprites[1];
            var pointToBrick = sprite.Scripts.Scripts[0].Bricks.Bricks[2] as PointToBrick;

            Assert.IsNotNull(pointToBrick);
            Assert.AreEqual(project.SpriteList.Sprites[1], pointToBrick.PointedSprite);
        }

        [TestMethod]
        public void GetUserVariableObjectTest()
        {
            //var project = (SampleLoader.LoadSampleProject("default.catroid", "default")).CurrentProject;
            
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetForeverBrickObjectTest()
        {
            var project = (SampleLoader.LoadSampleProject("default.catroid", "default")).CurrentProject;
            var sprite = project.SpriteList.Sprites[1];
            var foreverBrick = sprite.Scripts.Scripts[0].Bricks.Bricks[3] as ForeverBrick;

            Assert.IsNotNull(foreverBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[4], foreverBrick.LoopEndBrick);
        }

        [TestMethod]
        public void GetRepeatBrickObjectTest()
        {
            var project = (SampleLoader.LoadSampleProject("default.catroid", "default")).CurrentProject;
            var sprite = project.SpriteList.Sprites[1];
            var repeatBrick = sprite.Scripts.Scripts[0].Bricks.Bricks[5] as RepeatBrick;

            Assert.IsNotNull(repeatBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[6], repeatBrick.LoopEndBrick);
        }

        [TestMethod]
        public void GetLoopEndBrickObjectTest()
        {
            var project = (SampleLoader.LoadSampleProject("default.catroid", "default")).CurrentProject;
            var sprite = project.SpriteList.Sprites[1];
            var loopEndBrick = sprite.Scripts.Scripts[0].Bricks.Bricks[4] as LoopEndBrick;

            Assert.IsNotNull(loopEndBrick);
            Assert.AreEqual(sprite.Scripts.Scripts[0].Bricks.Bricks[3], loopEndBrick.LoopBeginBrick);
        }

        [TestMethod]
        public void GetCostumeReferenceStringTest()
        {
            var project = (SampleLoader.LoadSampleProject("default.catroid", "default")).CurrentProject;
            ReferenceHelper.Project = project;

            var costumeReference = (project.SpriteList.Sprites[0].Scripts.Scripts[0].Bricks.Bricks[0] as SetCostumeBrick).CostumeReference;

            Assert.IsNotNull(costumeReference);

            var reference = ReferenceHelper.GetReferenceString(costumeReference);

            Assert.AreEqual("../../../../../lookList/look[1]", reference);

            ReferenceHelper.Project = null;
        }

        [TestMethod]
        public void GetSoundReferenceStringTest()
        {
            var project = (SampleLoader.LoadSampleProject("default.catroid", "default")).CurrentProject;
            ReferenceHelper.Project = project;

            var soundReference = (project.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks[1] as PlaySoundBrick).SoundReference;

            Assert.IsNotNull(soundReference);

            var reference = ReferenceHelper.GetReferenceString(soundReference);

            Assert.AreEqual("../../../../../soundList/sound[1]", reference);

            ReferenceHelper.Project = null;
        }

        [TestMethod]
        public void GetObjectReferenceStringTest()
        {
            var project = (SampleLoader.LoadSampleProject("default.catroid", "default")).CurrentProject;
            ReferenceHelper.Project = project;

            var pointedSpriteReference = (project.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks[2] as PointToBrick).PointedSpriteReference;

            Assert.IsNotNull(pointedSpriteReference);

            var reference = ReferenceHelper.GetReferenceString(pointedSpriteReference);

            Assert.AreEqual("../../../../../../object[2]", reference);

            ReferenceHelper.Project = null;
        }

        [TestMethod]
        public void GetVariableReferenceStringTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetForeverBrickReferenceStringTest()
        {
            var project = (SampleLoader.LoadSampleProject("default.catroid", "default")).CurrentProject;
            ReferenceHelper.Project = project;

            var loopBeginBrickReference = (project.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks[4] as LoopEndBrick).LoopBeginBrickReference;

            Assert.IsNotNull(loopBeginBrickReference);

            var reference = ReferenceHelper.GetReferenceString(loopBeginBrickReference);

            Assert.AreEqual("../../brickList/foreverBrick[1]", reference);

            ReferenceHelper.Project = null;
        }

        [TestMethod]
        public void GetRepeatBrickReferenceStringTest()
        {
            var project = (SampleLoader.LoadSampleProject("default.catroid", "default")).CurrentProject;
            ReferenceHelper.Project = project;

            var loopBeginBrickReference = (project.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks[6] as LoopEndBrick).LoopBeginBrickReference;

            Assert.IsNotNull(loopBeginBrickReference);

            var reference = ReferenceHelper.GetReferenceString(loopBeginBrickReference);

            Assert.AreEqual("../../brickList/repeatBrick[1]", reference);

            ReferenceHelper.Project = null;
        }

        [TestMethod]
        public void GetLoopEndBrickReferenceStringTest()
        {
            var project = (SampleLoader.LoadSampleProject("default.catroid", "default")).CurrentProject;
            ReferenceHelper.Project = project;

            var loopEndBrickReference = (project.SpriteList.Sprites[1].Scripts.Scripts[0].Bricks.Bricks[3] as ForeverBrick).LoopEndBrickReference;

            Assert.IsNotNull(loopEndBrickReference);

            var reference = ReferenceHelper.GetReferenceString(loopEndBrickReference);

            Assert.AreEqual("../../brickList/loopEndBrick[1]", reference);

            ReferenceHelper.Project = null;
        }
    }
}
