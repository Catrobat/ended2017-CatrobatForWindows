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

            //Assert.AreEqual(project.ApplicationVersionCode, 820);
            //Assert.AreEqual(project.ApplicationVersionName, "0.6.0beta-820-debug");
            //Assert.AreEqual(project.ApplicationXmlVersion, 1.0);
            //Assert.AreEqual(project.DeviceName, "LG-P350");
            //Assert.AreEqual(project.Platform, "Android");
            //Assert.AreEqual(project.PlatformVersion, "8");
            //Assert.AreEqual(project.ProjectName, "test");
            //Assert.AreEqual(project.ScreenHeight, 320);
            //Assert.AreEqual(project.ScreenWidth, 240);

            Assert.AreEqual(project.SpriteList.Sprites.Count, 2);
            {
                var sprite1 = project.SpriteList.Sprites[0];
                Assert.AreEqual(sprite1.Costumes.Costumes.Count, 1);
                Assert.AreEqual(sprite1.Name, "Background");

                Assert.AreEqual(sprite1.Costumes.Costumes[0].Name, "background");
                Assert.AreEqual(sprite1.Costumes.Costumes[0].FileName, "3F3C722FCCBBD45ACF1211E3155FD5C6_background");

                var startScript = sprite1.Scripts.Scripts[0] as StartScript;

                Assert.AreEqual(startScript.Bricks.Bricks.Count, 1);

                var brick = startScript.Bricks.Bricks[0] as SetCostumeBrick;
                Assert.AreEqual(brick.CostumeReference.Reference, "../../../../../costumeDataList/costumeData");
                Assert.AreEqual(brick.Costume, sprite1.Costumes.Costumes[0]);

                Assert.AreEqual(sprite1.Sounds.Sounds.Count, 0);
            }

            {
                var sprite2 = project.SpriteList.Sprites[1];
                var costumeList = sprite2.Costumes;
                Assert.AreEqual(costumeList.Costumes.Count, 3);
                var costume1 = costumeList.Costumes[0];
                Assert.AreEqual(costume1.FileName, "C3F37BB1E4B17CCC6D3FA0578DDBC164_normalCat");
                Assert.AreEqual(costume1.Name, "normalCat");
                var costume2 = costumeList.Costumes[1];
                Assert.AreEqual(costume2.FileName, "A5E10E13DDC4ED4B188DA2A5D0B61CF9_banzaiCat");
                Assert.AreEqual(costume2.Name, "banzaiCat");
                var costume3 = costumeList.Costumes[2];
                Assert.AreEqual(costume3.FileName, "E64E017A63AFB9EC687B76E02376B1D9_cheshireCat");
                Assert.AreEqual(costume3.Name, "cheshireCat");

                Assert.AreEqual(sprite2.Name, "Catroid");

                var startScript = sprite2.Scripts.Scripts[0] as StartScript;
                Assert.AreEqual(startScript.Bricks.Bricks.Count, 1);

                var brick1 = startScript.Bricks.Bricks[0] as SetCostumeBrick;
                Assert.AreEqual(brick1.CostumeReference.Reference, "../../../../../costumeDataList/costumeData");
                Assert.AreEqual(brick1.Costume, costume1);

                var whenScript = sprite2.Scripts.Scripts[1] as WhenScript;
                Assert.AreEqual(whenScript.Bricks.Bricks.Count, 5);
                Assert.AreEqual(whenScript.Action, WhenScript.WhenScriptAction.Tapped);

                var brick2 = whenScript.Bricks.Bricks[0] as SetCostumeBrick;
                Assert.AreEqual(brick2.CostumeReference.Reference, "../../../../../costumeDataList/costumeData[2]");
                Assert.AreEqual(brick2.Costume, costume2);

                var brick3 = whenScript.Bricks.Bricks[1] as WaitBrick;
                Assert.AreEqual(brick3.TimeToWaitInSeconds, 500);

                var brick4 = whenScript.Bricks.Bricks[2] as SetCostumeBrick;
                Assert.AreEqual(brick4.CostumeReference.Reference, "../../../../../costumeDataList/costumeData[3]");
                Assert.AreEqual(brick4.Costume, costume3);

                var brick5 = whenScript.Bricks.Bricks[3] as WaitBrick;
                Assert.AreEqual(brick5.TimeToWaitInSeconds, 500);

                var brick6 = whenScript.Bricks.Bricks[4] as SetCostumeBrick;
                Assert.AreEqual(brick6.CostumeReference.Reference, "../../../../../costumeDataList/costumeData");
                Assert.AreEqual(brick6.Costume, costume1);

                Assert.AreEqual(sprite2.Sounds.Sounds.Count, 0);
            }
        }

        [TestMethod]
        public void ReadUltimateTest()
        {
            var project = SampleLoader.LoadSampleXML("ultimateTest");

            //Assert.AreEqual(project.ApplicationVersionCode, 10);
            //Assert.AreEqual(project.ApplicationVersionName, "0.6.0beta");
            //Assert.AreEqual(project.ApplicationXmlVersion, 1.0);
            //Assert.AreEqual(project.DeviceName, "HTC Desire");
            //Assert.AreEqual(project.Platform, "Android");
            //Assert.AreEqual(project.PlatformVersion, "10");
            //Assert.AreEqual(project.ProjectName, "UltimateTest");
            //Assert.AreEqual(project.ScreenHeight, 800);
            //Assert.AreEqual(project.ScreenWidth, 480);

            Assert.AreEqual(project.SpriteList.Sprites.Count, 3);

            var sprite1 = project.SpriteList.Sprites[0];
            var sprite2 = project.SpriteList.Sprites[1];
            var sprite3 = project.SpriteList.Sprites[2];

            {
                Assert.AreEqual(sprite1.Costumes.Costumes.Count, 1);
                Assert.AreEqual(sprite1.Costumes.Costumes[0].Name, "Hintergrund");
                Assert.AreEqual(sprite1.Costumes.Costumes[0].FileName, "B978398F6E8D16B857AA81618F3EF879_Hintergrund");

                Assert.AreEqual(sprite1.Name, "Hintergrund");

                Assert.AreEqual(sprite1.Scripts.Scripts.Count, 1);
                var startScript = sprite1.Scripts.Scripts[0] as StartScript;

                Assert.AreEqual(startScript.Bricks.Bricks.Count, 23);

                var brick1 = startScript.Bricks.Bricks[0] as PlaceAtBrick;
                Assert.AreEqual(brick1.XPosition, 0);
                Assert.AreEqual(brick1.YPosition, 0);

                var brick2 = startScript.Bricks.Bricks[1] as SetXBrick;
                Assert.AreEqual(brick2.XPosition, 0);

                var brick3 = startScript.Bricks.Bricks[2] as SetYBrick;
                Assert.AreEqual(brick3.YPosition, 0);

                var brick4 = startScript.Bricks.Bricks[3] as ChangeXByBrick;
                Assert.AreEqual(brick4.XMovement, 100);

                var brick5 = startScript.Bricks.Bricks[4] as ChangeYByBrick;
                Assert.AreEqual(brick5.YMovement, 100);

                var brick6 = startScript.Bricks.Bricks[5] as IfOnEdgeBounceBrick;

                var brick7 = startScript.Bricks.Bricks[6] as MoveNStepsBrick;
                Assert.AreEqual(brick7.Steps, 10.0);

                var brick8 = startScript.Bricks.Bricks[7] as TurnLeftBrick;
                Assert.AreEqual(brick8.Degrees, 15.0);

                var brick9 = startScript.Bricks.Bricks[8] as TurnRightBrick;
                Assert.AreEqual(brick9.Degrees, 15.0);

                var brick10 = startScript.Bricks.Bricks[9] as PointInDirectionBrick;
                Assert.AreEqual(brick10.Degrees, 90.0);

                var brick11 = startScript.Bricks.Bricks[10] as PointToBrick;
                Assert.AreEqual(brick11.PointedSpriteReference.Reference, "../../../../../../sprite[2]");
                Assert.AreEqual(brick11.PointedSprite, sprite2);

                var brick12 = startScript.Bricks.Bricks[11] as GlideToBrick;
                Assert.AreEqual(brick12.DurationInSeconds, 1000);
                Assert.AreEqual(brick12.XDestination, 800);
                Assert.AreEqual(brick12.YDestination, 0);

                var brick13 = startScript.Bricks.Bricks[12] as SetCostumeBrick;
                Assert.AreEqual(brick13.CostumeReference.Reference, "../../../../../costumeDataList/costumeData");
                Assert.AreEqual(brick13.Costume, sprite1.Costumes.Costumes[0]);

                var brick14 = startScript.Bricks.Bricks[13] as NextCostumeBrick;

                var brick15 = startScript.Bricks.Bricks[14] as SetSizeToBrick;
                Assert.AreEqual(brick15.Size, 100.0);

                var brick16 = startScript.Bricks.Bricks[15] as ChangeSizeByNBrick;
                Assert.AreEqual(brick16.Size, 20.0);

                var brick17 = startScript.Bricks.Bricks[16] as HideBrick;

                var brick18 = startScript.Bricks.Bricks[17] as ShowBrick;

                var brick19 = startScript.Bricks.Bricks[18] as SetGhostEffectBrick;
                Assert.AreEqual(brick19.Transparency, 0.0);

                var brick20 = startScript.Bricks.Bricks[19] as ChangeGhostEffectBrick;
                Assert.AreEqual(brick20.ChangeGhostEffect, 25.0);

                var brick21 = startScript.Bricks.Bricks[20] as SetBrightnessBrick;
                Assert.AreEqual(brick21.Brightness, 0.0);

                var brick22 = startScript.Bricks.Bricks[21] as ChangeBrightnessBrick;
                Assert.AreEqual(brick22.ChangeBrightness, 25.0);

                var brick23 = startScript.Bricks.Bricks[22] as ClearGraphicEffectBrick;

                Assert.AreEqual(sprite1.Sounds.Sounds.Count, 0);
            }

            {
                Assert.AreEqual(sprite2.Costumes.Costumes.Count, 2);
                Assert.AreEqual(sprite2.Costumes.Costumes[0].Name, "IMG_20120727_130526");
                Assert.AreEqual(sprite2.Costumes.Costumes[0].FileName, "9CC1F04E1A241687B199E4D189E13843_IMG_20120727_130526.jpg");
                Assert.AreEqual(sprite2.Costumes.Costumes[1].Name, "IMG_20120727_130526_Kopie");
                Assert.AreEqual(sprite2.Costumes.Costumes[1].FileName, "9CC1F04E1A241687B199E4D189E13843_IMG_20120727_130526.jpg");

                Assert.AreEqual(sprite2.Name, "Sound");
                Assert.AreEqual(sprite2.Scripts.Scripts.Count, 4);

                var startScript = sprite2.Scripts.Scripts[0] as StartScript;

                Assert.AreEqual(startScript.Bricks.Bricks.Count, 3);

                var brick1 = startScript.Bricks.Bricks[0] as PlaySoundBrick;
                Assert.AreEqual(brick1.SoundReference.Reference, "../../../../../soundList/soundInfo");
                Assert.AreEqual(brick1.Sound, sprite2.Sounds.Sounds[0]);

                var brick2 = startScript.Bricks.Bricks[1] as PlaySoundBrick;
                Assert.AreEqual(brick2.SoundReference.Reference, "../../../../../soundList/soundInfo");
                Assert.AreEqual(brick2.Sound, sprite2.Sounds.Sounds[0]);

                var brick3 = startScript.Bricks.Bricks[2] as PlaySoundBrick;
                Assert.AreEqual(brick3.SoundReference.Reference, "../../../../../soundList/soundInfo[2]");
                Assert.AreEqual(brick3.Sound, sprite2.Sounds.Sounds[1]);

                var whenScript = sprite2.Scripts.Scripts[1] as WhenScript;

                Assert.AreEqual(whenScript.Bricks.Bricks.Count, 7);

                var brick4 = whenScript.Bricks.Bricks[0] as BroadcastBrick;
                Assert.AreEqual(brick4.BroadcastMessage, "sprechen");

                var brick5 = whenScript.Bricks.Bricks[1] as PointToBrick;
                Assert.AreEqual(brick5.PointedSpriteReference.Reference, "../../../../../../sprite");
                Assert.AreEqual(brick5.PointedSprite, sprite1);

                var brick6 = whenScript.Bricks.Bricks[2] as BroadcastWaitBrick;
                Assert.AreEqual(brick6.BroadcastMessage, "sprechen");

                var brick7 = whenScript.Bricks.Bricks[3] as StopAllSoundsBrick;

                var brick8 = whenScript.Bricks.Bricks[4] as WaitBrick;
                Assert.AreEqual(brick8.TimeToWaitInSeconds, 1000);

                var brick9 = whenScript.Bricks.Bricks[5] as NoteBrick;
                Assert.AreEqual(brick9.Note, "Notiz");

                var brick10 = whenScript.Bricks.Bricks[6] as SetVolumeToBrick;
                Assert.AreEqual(brick10.Volume, 100.0);

                Assert.AreEqual(whenScript.Action, WhenScript.WhenScriptAction.Tapped);

                var broadcastScript1 = sprite2.Scripts.Scripts[2] as BroadcastScript;

                Assert.AreEqual(broadcastScript1.Bricks.Bricks.Count, 6);

                var brick11 = broadcastScript1.Bricks.Bricks[0] as ForeverBrick;
                Assert.AreEqual(brick11.LoopEndBrickReference.Reference, "../../loopEndBrick");
                Assert.AreEqual(brick11.LoopEndBrick, broadcastScript1.Bricks.Bricks[3]);

                var brick12 = broadcastScript1.Bricks.Bricks[1] as RepeatBrick;
                Assert.AreEqual(brick12.LoopEndBrickReference.Reference, "../../loopEndBrick[2]");
                Assert.AreEqual(brick12.LoopEndBrick, broadcastScript1.Bricks.Bricks[5]);
                Assert.AreEqual(brick12.TimesToRepeat, 2);

                var brick13 = broadcastScript1.Bricks.Bricks[2] as ChangeVolumeByBrick;
                Assert.AreEqual(brick13.Volume, 25.0);

                var brick14 = broadcastScript1.Bricks.Bricks[3] as LoopEndBrick;
                Assert.AreEqual(brick14.LoopBeginBrickReference.Class, "foreverBrick");
                Assert.AreEqual(brick14.LoopBeginBrickReference.Reference, "../../foreverBrick");
                Assert.AreEqual(brick14.LoopBeginBrick, broadcastScript1.Bricks.Bricks[0]);

                var brick15 = broadcastScript1.Bricks.Bricks[4] as SpeakBrick;
                Assert.AreEqual(brick15.Text, "aaaa");

                var brick16 = broadcastScript1.Bricks.Bricks[5] as LoopEndBrick;
                Assert.AreEqual(brick16.LoopBeginBrickReference.Class, "repeatBrick");
                Assert.AreEqual(brick16.LoopBeginBrickReference.Reference, "../../repeatBrick");
                Assert.AreEqual(brick16.LoopBeginBrick, broadcastScript1.Bricks.Bricks[1]);

                Assert.AreEqual(broadcastScript1.ReceivedMessage, "sprechen");

                var broadcastScript2 = sprite2.Scripts.Scripts[3] as BroadcastScript;
                Assert.AreEqual(broadcastScript2.Bricks.Bricks.Count, 0);
                Assert.AreEqual(broadcastScript2.ReceivedMessage, "");

                var soundList = sprite2.Sounds.Sounds;
                Assert.AreEqual(soundList.Count, 2);

                var soundInfo1 = sprite2.Sounds.Sounds[0];
                Assert.AreEqual(soundInfo1.FileName, "68223C25ABEFABA96FD2BEC8C44D5A12_Aufnahme.mp3");
                Assert.AreEqual(soundInfo1.Name, "Aufnahme");

                var soundInfo2 = sprite2.Sounds.Sounds[1];
                Assert.AreEqual(soundInfo2.FileName, "84904B77F5B78BBDCC04634285D1B8DE_Aufnahme.mp3");
                Assert.AreEqual(soundInfo2.Name, "Aufnahme1");
            }
            {
                Assert.AreEqual(sprite3.Costumes.Costumes.Count, 0);
                Assert.AreEqual(sprite3.Name, "Robot");
                Assert.AreEqual(sprite3.Scripts.Scripts.Count, 1);

                var startScript = sprite3.Scripts.Scripts[0] as StartScript;

                Assert.AreEqual(startScript.Bricks.Bricks.Count, 5);

                var brick1 = startScript.Bricks.Bricks[0] as NxtMotorTurnAngleBrick;
                Assert.AreEqual(brick1.Degrees, 180);
                Assert.AreEqual(brick1.Motor, "MOTOR_A");

                var brick2 = startScript.Bricks.Bricks[1] as PointToBrick;
                Assert.AreEqual(brick2.PointedSpriteReference.Reference, "../../../../../../sprite[2]");
                Assert.AreEqual(brick2.PointedSprite, sprite2);

                var brick3 = startScript.Bricks.Bricks[2] as NxtMotorStopBrick;
                Assert.AreEqual(brick3.Motor, "MOTOR_A");

                var brick4 = startScript.Bricks.Bricks[3] as NxtMotorActionBrick;
                Assert.AreEqual(brick4.Motor, "MOTOR_A");
                Assert.AreEqual(brick4.Speed, 3);

                var brick5 = startScript.Bricks.Bricks[4] as NxtPlayToneBrick;
                Assert.AreEqual(brick5.DurationInSeconds, 1000);
                Assert.AreEqual(brick5.Frequency, 2800);

                Assert.AreEqual(sprite3.Sounds.Sounds.Count, 0);
            }
        }
    }
}
