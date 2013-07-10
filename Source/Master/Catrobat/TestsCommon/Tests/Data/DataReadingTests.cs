using System.Runtime;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Catrobat.Core.Objects.Scripts;
using Catrobat.TestsCommon.Misc;
using Catrobat.TestsCommon.SampleData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.TestsCommon.Tests.Data
{
    [TestClass]
    public class DataReadingTests
    {
        [ClassInitialize()]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }

        [TestMethod]
        public void ReadSimpleTest()
        {
            var project = SampleLoader.LoadSampleXML("simple");

            //Assert.AreEqual(820, project.ApplicationVersionCode);
            //Assert.AreEqual("0.6.0beta-820-debug", project.ApplicationVersionName);
            //Assert.AreEqual(1.0, project.ApplicationXmlVersion);
            //Assert.AreEqual("LG-P350", project.DeviceName);
            //Assert.AreEqual("Android", project.Platform);
            //Assert.AreEqual("8", project.PlatformVersion);
            //Assert.AreEqual("test", project.ProjectName);
            //Assert.AreEqual(320, project.ScreenHeight);
            //Assert.AreEqual(240, project.ScreenWidth);

            Assert.AreEqual(2, project.SpriteList.Sprites.Count);
            {
                var sprite1 = project.SpriteList.Sprites[0];
                Assert.AreEqual(1, sprite1.Costumes.Costumes.Count);
                Assert.AreEqual("Background", sprite1.Name);

                Assert.AreEqual("background", sprite1.Costumes.Costumes[0].Name);
                Assert.AreEqual("3F3C722FCCBBD45ACF1211E3155FD5C6_background", sprite1.Costumes.Costumes[0].FileName);

                var startScript = sprite1.Scripts.Scripts[0] as StartScript;

                Assert.IsNotNull(startScript);
                Assert.AreEqual(1, startScript.Bricks.Bricks.Count);

                var brick = startScript.Bricks.Bricks[0] as SetCostumeBrick;
                Assert.IsNotNull(brick);
                Assert.AreEqual("../../../../../costumeDataList/costumeData", brick.CostumeReference.Reference);
                Assert.AreEqual(sprite1.Costumes.Costumes[0], brick.Costume);

                Assert.AreEqual(0, sprite1.Sounds.Sounds.Count);
            }

            {
                var sprite2 = project.SpriteList.Sprites[1];
                var costumeList = sprite2.Costumes;
                Assert.AreEqual(3, costumeList.Costumes.Count);
                var costume1 = costumeList.Costumes[0];
                Assert.AreEqual("C3F37BB1E4B17CCC6D3FA0578DDBC164_normalCat", costume1.FileName);
                Assert.AreEqual("normalCat", costume1.Name);
                var costume2 = costumeList.Costumes[1];
                Assert.AreEqual("A5E10E13DDC4ED4B188DA2A5D0B61CF9_banzaiCat", costume2.FileName);
                Assert.AreEqual("banzaiCat", costume2.Name);
                var costume3 = costumeList.Costumes[2];
                Assert.AreEqual("E64E017A63AFB9EC687B76E02376B1D9_cheshireCat", costume3.FileName);
                Assert.AreEqual("cheshireCat", costume3.Name);

                Assert.AreEqual("Catroid", sprite2.Name);

                var startScript = sprite2.Scripts.Scripts[0] as StartScript;
                Assert.IsNotNull(startScript);
                Assert.AreEqual(1, startScript.Bricks.Bricks.Count);

                var brick1 = startScript.Bricks.Bricks[0] as SetCostumeBrick;
                Assert.IsNotNull(brick1);
                Assert.AreEqual("../../../../../costumeDataList/costumeData", brick1.CostumeReference.Reference);
                Assert.AreEqual(brick1.Costume, costume1);

                var whenScript = sprite2.Scripts.Scripts[1] as WhenScript;
                Assert.IsNotNull(whenScript);
                Assert.AreEqual(5, whenScript.Bricks.Bricks.Count);
                Assert.AreEqual(WhenScript.WhenScriptAction.Tapped, whenScript.Action);

                var brick2 = whenScript.Bricks.Bricks[0] as SetCostumeBrick;
                Assert.IsNotNull(brick2);
                Assert.AreEqual("../../../../../costumeDataList/costumeData[2]", brick2.CostumeReference.Reference);
                Assert.AreEqual(costume2, brick2.Costume);

                var brick3 = whenScript.Bricks.Bricks[1] as WaitBrick;
                Assert.IsNotNull(brick3);
                Assert.AreEqual(500, brick3.TimeToWaitInSeconds);

                var brick4 = whenScript.Bricks.Bricks[2] as SetCostumeBrick;
                Assert.IsNotNull(brick4);
                Assert.AreEqual("../../../../../costumeDataList/costumeData[3]", brick4.CostumeReference.Reference);
                Assert.AreEqual(costume3, brick4.Costume);

                var brick5 = whenScript.Bricks.Bricks[3] as WaitBrick;
                Assert.IsNotNull(brick5);
                Assert.AreEqual(500, brick5.TimeToWaitInSeconds);

                var brick6 = whenScript.Bricks.Bricks[4] as SetCostumeBrick;
                Assert.IsNotNull(brick6);
                Assert.AreEqual("../../../../../costumeDataList/costumeData", brick6.CostumeReference.Reference);
                Assert.AreEqual(costume1, brick6.Costume);

                Assert.AreEqual(0, sprite2.Sounds.Sounds.Count);
            }
        }

        [TestMethod]
        public void ReadUltimateTest()
        {
            var project = SampleLoader.LoadSampleXML("ultimateTest");

            //Assert.AreEqual(10, project.ApplicationVersionCode);
            //Assert.AreEqual("0.6.0beta", project.ApplicationVersionName);
            //Assert.AreEqual(1.0, project.ApplicationXmlVersion);
            //Assert.AreEqual("HTC Desire", project.DeviceName);
            //Assert.AreEqual("Android", project.Platform);
            //Assert.AreEqual("10", project.PlatformVersion);
            //Assert.AreEqual("UltimateTest", project.ProjectName);
            //Assert.AreEqual(800, project.ScreenHeight);
            //Assert.AreEqual(480, project.ScreenWidth);

            Assert.AreEqual(3, project.SpriteList.Sprites.Count);

            var sprite1 = project.SpriteList.Sprites[0];
            var sprite2 = project.SpriteList.Sprites[1];
            var sprite3 = project.SpriteList.Sprites[2];

            {
                Assert.AreEqual(1, sprite1.Costumes.Costumes.Count);
                Assert.AreEqual("Hintergrund", sprite1.Costumes.Costumes[0].Name);
                Assert.AreEqual("B978398F6E8D16B857AA81618F3EF879_Hintergrund", sprite1.Costumes.Costumes[0].FileName);

                Assert.AreEqual("Hintergrund", sprite1.Name);

                Assert.AreEqual(1, sprite1.Scripts.Scripts.Count);
                var startScript = sprite1.Scripts.Scripts[0] as StartScript;

                Assert.IsNotNull(startScript);
                Assert.AreEqual(23, startScript.Bricks.Bricks.Count);

                var brick1 = startScript.Bricks.Bricks[0] as PlaceAtBrick;
                Assert.IsNotNull(brick1);
                Assert.AreEqual(0, brick1.XPosition);
                Assert.AreEqual(0, brick1.YPosition);

                var brick2 = startScript.Bricks.Bricks[1] as SetXBrick;
                Assert.IsNotNull(brick2);
                Assert.AreEqual(0, brick2.XPosition);

                var brick3 = startScript.Bricks.Bricks[2] as SetYBrick;
                Assert.IsNotNull(brick3);
                Assert.AreEqual(0, brick3.YPosition);

                var brick4 = startScript.Bricks.Bricks[3] as ChangeXByBrick;
                Assert.IsNotNull(brick4);
                Assert.AreEqual(100, brick4.XMovement);

                var brick5 = startScript.Bricks.Bricks[4] as ChangeYByBrick;
                Assert.IsNotNull(brick5);
                Assert.AreEqual(100, brick5.YMovement);

                var brick6 = startScript.Bricks.Bricks[5] as IfOnEdgeBounceBrick;

                var brick7 = startScript.Bricks.Bricks[6] as MoveNStepsBrick;
                Assert.IsNotNull(brick7);
                Assert.AreEqual(10.0, brick7.Steps);

                var brick8 = startScript.Bricks.Bricks[7] as TurnLeftBrick;
                Assert.IsNotNull(brick8);
                Assert.AreEqual(15.0, brick8.Degrees);

                var brick9 = startScript.Bricks.Bricks[8] as TurnRightBrick;
                Assert.IsNotNull(brick9);
                Assert.AreEqual(15.0, brick9.Degrees);

                var brick10 = startScript.Bricks.Bricks[9] as PointInDirectionBrick;
                Assert.IsNotNull(brick10);
                Assert.AreEqual(90.0, brick10.Degrees);

                var brick11 = startScript.Bricks.Bricks[10] as PointToBrick;
                Assert.IsNotNull(brick11);
                Assert.AreEqual("../../../../../../sprite[2]", brick11.PointedSpriteReference.Reference);
                Assert.AreEqual(brick11.PointedSprite, sprite2);

                var brick12 = startScript.Bricks.Bricks[11] as GlideToBrick;
                Assert.IsNotNull(brick12);
                Assert.AreEqual(1000, brick12.DurationInSeconds);
                Assert.AreEqual(800, brick12.XDestination);
                Assert.AreEqual(0, brick12.YDestination);

                var brick13 = startScript.Bricks.Bricks[12] as SetCostumeBrick;
                Assert.IsNotNull(brick13);
                Assert.AreEqual("../../../../../costumeDataList/costumeData", brick13.CostumeReference.Reference);
                Assert.AreEqual(sprite1.Costumes.Costumes[0], brick13.Costume);

                var brick14 = startScript.Bricks.Bricks[13] as NextCostumeBrick;

                var brick15 = startScript.Bricks.Bricks[14] as SetSizeToBrick;
                Assert.IsNotNull(brick15);
                Assert.AreEqual(100.0, brick15.Size);

                var brick16 = startScript.Bricks.Bricks[15] as ChangeSizeByNBrick;
                Assert.IsNotNull(brick16);
                Assert.AreEqual(20.0, brick16.Size);

                var brick17 = startScript.Bricks.Bricks[16] as HideBrick;

                var brick18 = startScript.Bricks.Bricks[17] as ShowBrick;

                var brick19 = startScript.Bricks.Bricks[18] as SetGhostEffectBrick;
                Assert.IsNotNull(brick19);
                Assert.AreEqual(0.0, brick19.Transparency);

                var brick20 = startScript.Bricks.Bricks[19] as ChangeGhostEffectBrick;
                Assert.IsNotNull(brick20);
                Assert.AreEqual(25.0, brick20.ChangeGhostEffect);

                var brick21 = startScript.Bricks.Bricks[20] as SetBrightnessBrick;
                Assert.IsNotNull(brick21);
                Assert.AreEqual(0.0, brick21.Brightness);

                var brick22 = startScript.Bricks.Bricks[21] as ChangeBrightnessBrick;
                Assert.IsNotNull(brick22);
                Assert.AreEqual(25.0, brick22.ChangeBrightness);

                var brick23 = startScript.Bricks.Bricks[22] as ClearGraphicEffectBrick;

                Assert.AreEqual(0, sprite1.Sounds.Sounds.Count);
            }

            {
                Assert.AreEqual(2, sprite2.Costumes.Costumes.Count);
                Assert.AreEqual("IMG_20120727_130526", sprite2.Costumes.Costumes[0].Name);
                Assert.AreEqual("9CC1F04E1A241687B199E4D189E13843_IMG_20120727_130526.jpg", sprite2.Costumes.Costumes[0].FileName);
                Assert.AreEqual("IMG_20120727_130526_Kopie", sprite2.Costumes.Costumes[1].Name);
                Assert.AreEqual("9CC1F04E1A241687B199E4D189E13843_IMG_20120727_130526.jpg", sprite2.Costumes.Costumes[1].FileName);

                Assert.AreEqual("Sound", sprite2.Name);
                Assert.AreEqual(4, sprite2.Scripts.Scripts.Count);

                var startScript = sprite2.Scripts.Scripts[0] as StartScript;

                Assert.IsNotNull(startScript);
                Assert.AreEqual(3, startScript.Bricks.Bricks.Count);

                var brick1 = startScript.Bricks.Bricks[0] as PlaySoundBrick;
                Assert.IsNotNull(brick1);
                Assert.AreEqual("../../../../../soundList/soundInfo", brick1.SoundReference.Reference);
                Assert.AreEqual(sprite2.Sounds.Sounds[0], brick1.Sound);

                var brick2 = startScript.Bricks.Bricks[1] as PlaySoundBrick;
                Assert.IsNotNull(brick2);
                Assert.AreEqual("../../../../../soundList/soundInfo", brick2.SoundReference.Reference);
                Assert.AreEqual(sprite2.Sounds.Sounds[0], brick2.Sound);

                var brick3 = startScript.Bricks.Bricks[2] as PlaySoundBrick;
                Assert.IsNotNull(brick3);
                Assert.AreEqual("../../../../../soundList/soundInfo[2]", brick3.SoundReference.Reference);
                Assert.AreEqual(sprite2.Sounds.Sounds[1], brick3.Sound);

                var whenScript = sprite2.Scripts.Scripts[1] as WhenScript;

                Assert.IsNotNull(whenScript);
                Assert.AreEqual(7, whenScript.Bricks.Bricks.Count);

                var brick4 = whenScript.Bricks.Bricks[0] as BroadcastBrick;
                Assert.IsNotNull(brick4);
                Assert.AreEqual("sprechen", brick4.BroadcastMessage);

                var brick5 = whenScript.Bricks.Bricks[1] as PointToBrick;
                Assert.IsNotNull(brick5);
                Assert.AreEqual("../../../../../../sprite", brick5.PointedSpriteReference.Reference);
                Assert.AreEqual(brick5.PointedSprite, sprite1);

                var brick6 = whenScript.Bricks.Bricks[2] as BroadcastWaitBrick;
                Assert.IsNotNull(brick6);
                Assert.AreEqual("sprechen", brick6.BroadcastMessage);

                var brick7 = whenScript.Bricks.Bricks[3] as StopAllSoundsBrick;

                var brick8 = whenScript.Bricks.Bricks[4] as WaitBrick;
                Assert.IsNotNull(brick8);
                Assert.AreEqual(1000, brick8.TimeToWaitInSeconds);

                var brick9 = whenScript.Bricks.Bricks[5] as NoteBrick;
                Assert.IsNotNull(brick9);
                Assert.AreEqual("Notiz", brick9.Note);

                var brick10 = whenScript.Bricks.Bricks[6] as SetVolumeToBrick;
                Assert.IsNotNull(brick10);
                Assert.AreEqual(100.0, brick10.Volume);

                Assert.AreEqual(WhenScript.WhenScriptAction.Tapped, whenScript.Action);

                var broadcastScript1 = sprite2.Scripts.Scripts[2] as BroadcastScript;

                Assert.IsNotNull(broadcastScript1);
                Assert.AreEqual(6, broadcastScript1.Bricks.Bricks.Count);

                var brick11 = broadcastScript1.Bricks.Bricks[0] as ForeverBrick;
                Assert.IsNotNull(brick11);
                Assert.AreEqual("../../loopEndBrick", brick11.LoopEndBrickReference.Reference);
                Assert.AreEqual(broadcastScript1.Bricks.Bricks[3], brick11.LoopEndBrick);

                var brick12 = broadcastScript1.Bricks.Bricks[1] as RepeatBrick;
                Assert.IsNotNull(brick12);
                Assert.AreEqual("../../loopEndBrick[2]", brick12.LoopEndBrickReference.Reference);
                Assert.AreEqual(broadcastScript1.Bricks.Bricks[5], brick12.LoopEndBrick);
                Assert.AreEqual(2, brick12.TimesToRepeat);

                var brick13 = broadcastScript1.Bricks.Bricks[2] as ChangeVolumeByBrick;
                Assert.IsNotNull(brick13);
                Assert.AreEqual(25.0, brick13.Volume);

                var brick14 = broadcastScript1.Bricks.Bricks[3] as LoopEndBrick;
                Assert.IsNotNull(brick14);
                Assert.AreEqual("foreverBrick", brick14.LoopBeginBrickReference.Class);
                Assert.AreEqual("../../foreverBrick", brick14.LoopBeginBrickReference.Reference);
                Assert.AreEqual(broadcastScript1.Bricks.Bricks[0], brick14.LoopBeginBrick);

                var brick15 = broadcastScript1.Bricks.Bricks[4] as SpeakBrick;
                Assert.IsNotNull(brick15);
                Assert.AreEqual("aaaa", brick15.Text);

                var brick16 = broadcastScript1.Bricks.Bricks[5] as LoopEndBrick;
                Assert.IsNotNull(brick16);
                Assert.AreEqual("repeatBrick", brick16.LoopBeginBrickReference.Class);
                Assert.AreEqual("../../repeatBrick", brick16.LoopBeginBrickReference.Reference);
                Assert.AreEqual(broadcastScript1.Bricks.Bricks[1], brick16.LoopBeginBrick);

                Assert.AreEqual("sprechen", broadcastScript1.ReceivedMessage);

                var broadcastScript2 = sprite2.Scripts.Scripts[3] as BroadcastScript;
                Assert.IsNotNull(broadcastScript2);
                Assert.AreEqual(0, broadcastScript2.Bricks.Bricks.Count);
                Assert.AreEqual("", broadcastScript2.ReceivedMessage);

                var soundList = sprite2.Sounds.Sounds;
                Assert.AreEqual(2, soundList.Count);

                var soundInfo1 = sprite2.Sounds.Sounds[0];
                Assert.AreEqual("68223C25ABEFABA96FD2BEC8C44D5A12_Aufnahme.mp3", soundInfo1.FileName);
                Assert.AreEqual("Aufnahme", soundInfo1.Name);

                var soundInfo2 = sprite2.Sounds.Sounds[1];
                Assert.AreEqual("84904B77F5B78BBDCC04634285D1B8DE_Aufnahme.mp3", soundInfo2.FileName);
                Assert.AreEqual("Aufnahme1", soundInfo2.Name);
            }
            {
                Assert.AreEqual(0, sprite3.Costumes.Costumes.Count);
                Assert.AreEqual("Robot", sprite3.Name);
                Assert.AreEqual(1, sprite3.Scripts.Scripts.Count);

                var startScript = sprite3.Scripts.Scripts[0] as StartScript;
                Assert.IsNotNull(startScript);
                Assert.AreEqual(5, startScript.Bricks.Bricks.Count);

                var brick1 = startScript.Bricks.Bricks[0] as NxtMotorTurnAngleBrick;
                Assert.IsNotNull(brick1);
                Assert.AreEqual(180, brick1.Degrees);
                Assert.AreEqual("MOTOR_A", brick1.Motor);

                var brick2 = startScript.Bricks.Bricks[1] as PointToBrick;
                Assert.IsNotNull(brick2);
                Assert.AreEqual("../../../../../../sprite[2]", brick2.PointedSpriteReference.Reference);
                Assert.AreEqual(sprite2, brick2.PointedSprite);

                var brick3 = startScript.Bricks.Bricks[2] as NxtMotorStopBrick;
                Assert.IsNotNull(brick3);
                Assert.AreEqual("MOTOR_A", brick3.Motor);

                var brick4 = startScript.Bricks.Bricks[3] as NxtMotorActionBrick;
                Assert.IsNotNull(brick4);
                Assert.AreEqual("MOTOR_A", brick4.Motor);
                Assert.AreEqual(3, brick4.Speed);

                var brick5 = startScript.Bricks.Bricks[4] as NxtPlayToneBrick;
                Assert.IsNotNull(brick5);
                Assert.AreEqual(1000, brick5.DurationInSeconds);
                Assert.AreEqual(2800, brick5.Frequency);

                Assert.AreEqual(0, sprite3.Sounds.Sounds.Count);
            }
        }
    }
}
