using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.TestsCommon.Misc;
using Catrobat.TestsCommon.SampleData;

namespace Catrobat.TestsCommon.Tests
{
    [TestClass]
    public class XPathHelperTests
    {
      [ClassInitialize()]
      public static void TestClassInitialize(TestContext testContext)
      {
        TestHelper.InitializeTests();
      }

        [TestMethod]
        public void GetCostumeTest()
        {
            var project = SampleLoader.LoadSampleXML("ultimateTest");

            var sprite1 = project.SpriteList.Sprites[0] as Sprite;

            var setCostumeBrick1 = sprite1.Scripts.Scripts[0].Bricks.Bricks[12] as SetCostumeBrick;
            Assert.AreEqual(XPathHelper.getElement(setCostumeBrick1.CostumeReference.Reference, sprite1), sprite1.Costumes.Costumes[0]);
        }

        [TestMethod]
        public void GetSoundInfoTest()
        {
            var project = SampleLoader.LoadSampleXML("ultimateTest");

            var sprite2 = project.SpriteList.Sprites[1] as Sprite;

            var playSoundBrick1 = sprite2.Scripts.Scripts[0].Bricks.Bricks[0] as PlaySoundBrick;
            Assert.AreEqual(XPathHelper.getElement(playSoundBrick1.SoundReference.Reference, sprite2), sprite2.Sounds.Sounds[0]);

            var playSoundBrick2 = sprite2.Scripts.Scripts[0].Bricks.Bricks[1] as PlaySoundBrick;
            Assert.AreEqual(XPathHelper.getElement(playSoundBrick2.SoundReference.Reference, sprite2), sprite2.Sounds.Sounds[0]);

            var playSoundBrick3 = sprite2.Scripts.Scripts[0].Bricks.Bricks[2] as PlaySoundBrick;
            Assert.AreEqual(XPathHelper.getElement(playSoundBrick3.SoundReference.Reference, sprite2), sprite2.Sounds.Sounds[1]);
        }

        [TestMethod]
        public void GetSpriteTest()
        {
            var project = SampleLoader.LoadSampleXML("ultimateTest");

            var sprite1 = project.SpriteList.Sprites[0] as Sprite;
            var sprite2 = project.SpriteList.Sprites[1] as Sprite;
            var sprite3 = project.SpriteList.Sprites[2] as Sprite;

            var pointToBrick1 = sprite1.Scripts.Scripts[0].Bricks.Bricks[10] as PointToBrick;
            Assert.AreEqual(XPathHelper.getElement(pointToBrick1.PointedSpriteReference.Reference, sprite1), sprite2);

            var pointToBrick2 = sprite2.Scripts.Scripts[1].Bricks.Bricks[1] as PointToBrick;
            Assert.AreEqual(XPathHelper.getElement(pointToBrick2.PointedSpriteReference.Reference, sprite2), sprite1);

            var pointToBrick3 = sprite3.Scripts.Scripts[0].Bricks.Bricks[1] as PointToBrick;
            Assert.AreEqual(XPathHelper.getElement(pointToBrick3.PointedSpriteReference.Reference, sprite3), sprite2);
        }

        [TestMethod]
        public void GetLoopBeginTest()
        {
            var project = SampleLoader.LoadSampleXML("ultimateTest");

            var sprite2 = project.SpriteList.Sprites[1] as Sprite;
            ReadHelper.currentBrickList = sprite2.Scripts.Scripts[2].Bricks;

            var foreverBrick = sprite2.Scripts.Scripts[2].Bricks.Bricks[0] as ForeverBrick;
            Assert.AreEqual(XPathHelper.getElement(foreverBrick.LoopEndBrickReference.Reference, sprite2), sprite2.Scripts.Scripts[2].Bricks.Bricks[3]);

            var repeatBrick = sprite2.Scripts.Scripts[2].Bricks.Bricks[1] as RepeatBrick;
            Assert.AreEqual(XPathHelper.getElement(repeatBrick.LoopEndBrickReference.Reference, sprite2), sprite2.Scripts.Scripts[2].Bricks.Bricks[5]);
        }

        [TestMethod]
        public void GetLoopEndTest()
        {
            var project = SampleLoader.LoadSampleXML("ultimateTest");

            var sprite2 = project.SpriteList.Sprites[1] as Sprite;
            ReadHelper.currentBrickList = sprite2.Scripts.Scripts[2].Bricks;

            var loopEndBrick1 = sprite2.Scripts.Scripts[2].Bricks.Bricks[3] as LoopEndBrick;
            Assert.AreEqual(XPathHelper.getElement(loopEndBrick1.LoopBeginBrickReference.Reference, sprite2), sprite2.Scripts.Scripts[2].Bricks.Bricks[0]);

            var loopEndBrick2 = sprite2.Scripts.Scripts[2].Bricks.Bricks[5] as LoopEndBrick;
            Assert.AreEqual(XPathHelper.getElement(loopEndBrick2.LoopBeginBrickReference.Reference, sprite2), sprite2.Scripts.Scripts[2].Bricks.Bricks[1]);
        }

        [TestMethod]
        public void GetReferenceTest()
        {
            var project = SampleLoader.LoadSampleXML("ultimateTest");

            var sprite1 = project.SpriteList.Sprites[0] as Sprite;
            var sprite2 = project.SpriteList.Sprites[1] as Sprite;
            var sprite3 = project.SpriteList.Sprites[2] as Sprite;

            var costume = (sprite1.Scripts.Scripts[0].Bricks.Bricks[12] as SetCostumeBrick).Costume;
            Assert.AreEqual(XPathHelper.getReference(costume, sprite1), "../../../../../costumeDataList/costumeData");

            var soundInfo1 = (sprite2.Scripts.Scripts[0].Bricks.Bricks[0] as PlaySoundBrick).Sound;
            Assert.AreEqual(XPathHelper.getReference(soundInfo1, sprite2), "../../../../../soundList/soundInfo");

            var soundInfo2 = (sprite2.Scripts.Scripts[0].Bricks.Bricks[2] as PlaySoundBrick).Sound;
            Assert.AreEqual(XPathHelper.getReference(soundInfo2, sprite2), "../../../../../soundList/soundInfo[2]");


            Assert.AreEqual(XPathHelper.getReference(sprite2, sprite1), "../../../../../../sprite[2]");

            Assert.AreEqual(XPathHelper.getReference(sprite1, sprite2), "../../../../../../sprite");

            Assert.AreEqual(XPathHelper.getReference(sprite2, sprite3), "../../../../../../sprite[2]");

            var loopEndBrick1 = (sprite2.Scripts.Scripts[2].Bricks.Bricks[0] as ForeverBrick).LoopEndBrick as LoopEndBrick;
            Assert.AreEqual(XPathHelper.getReference(loopEndBrick1, sprite2), "../../loopEndBrick");

            var loopEndBrick2 = (sprite2.Scripts.Scripts[2].Bricks.Bricks[1] as RepeatBrick).LoopEndBrick as LoopEndBrick;
            Assert.AreEqual(XPathHelper.getReference(loopEndBrick2, sprite2), "../../loopEndBrick[2]");

            var loopBeginBrick1 = (sprite2.Scripts.Scripts[2].Bricks.Bricks[3] as LoopEndBrick).LoopBeginBrick as LoopBeginBrick;
            Assert.AreEqual(XPathHelper.getReference(loopBeginBrick1, sprite2), "../../foreverBrick");

            var loopBeginBrick2 = (sprite2.Scripts.Scripts[2].Bricks.Bricks[5] as LoopEndBrick).LoopBeginBrick as LoopBeginBrick;
            Assert.AreEqual(XPathHelper.getReference(loopBeginBrick2, sprite2), "../../repeatBrick");
        }
    }
}
