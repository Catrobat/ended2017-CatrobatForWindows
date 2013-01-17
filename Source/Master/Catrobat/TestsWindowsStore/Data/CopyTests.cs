using System;
using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace TestsWindowsStore.Data
{
  [TestClass]
  public class CopyTests
  {
    [TestMethod]
    public void CopySprite()
    {
      throw new NotImplementedException("Fix changed names");
      //SampleLoader.LoadSampleProject("UltimateTest.catroid", "UltimateTest");

      //var project = CatrobatContext.Instance.CurrentProject;

      //var sprite1 = project.SpriteList.Sprites[0];
      //var sprite2 = project.SpriteList.Sprites[1];
      //var sprite3 = project.SpriteList.Sprites[2];

      //var newSprite1 = sprite1.Copy() as Sprite;
      //var newSprite2 = sprite2.Copy() as Sprite;
      //var newSprite3 = sprite3.Copy() as Sprite;

      //var pathCostumes = project.BasePath + "/" + Project.ImagesPath + "/";
      //var pathSounds = project.BasePath + "/" + Project.SoundsPath + "/";

      //{
      //  Assert.AreEqual(sprite1.CostumeList.Costumes.Count, newSprite1.CostumeList.Costumes.Count);
      //  Assert.AreEqual(sprite1.Name, newSprite1.Name);
      //  Assert.AreEqual(sprite1.ScriptList.Scripts.Count, newSprite1.ScriptList.Scripts.Count);

      //  Assert.AreEqual(sprite1.CostumeList.Costumes[0].Name, newSprite1.CostumeList.Costumes[0].Name);
      //  Assert.AreNotEqual(sprite1.CostumeList.Costumes[0].FileName, newSprite1.CostumeList.Costumes[0].FileName);//should be different
      //  using (var isoStore = IsolatedStorageFile.GetUserStoreForApplication())
      //  {
      //    Assert.AreEqual(isoStore.FileExists(pathCostumes + sprite1.CostumeList.Costumes[0].FileName), true);
      //    Assert.AreEqual(isoStore.FileExists(pathCostumes + newSprite1.CostumeList.Costumes[0].FileName), true);
      //  }

      //  var startScript = sprite1.ScriptList.Scripts[0] as StartScript;
      //  var newStartScript = newSprite1.ScriptList.Scripts[0] as StartScript;

      //  Assert.AreEqual(startScript.BrickList.Bricks.Count, newStartScript.BrickList.Bricks.Count);

      //  var brick1 = startScript.BrickList.Bricks[0] as PlaceAtBrick;
      //  var newBrick1 = newStartScript.BrickList.Bricks[0] as PlaceAtBrick;
      //  Assert.AreEqual(brick1.XPosition, newBrick1.XPosition);
      //  Assert.AreEqual(brick1.YPosition, newBrick1.YPosition);

      //  var brick2 = startScript.BrickList.Bricks[1] as SetXBrick;
      //  var newBrick2 = newStartScript.BrickList.Bricks[1] as SetXBrick;
      //  Assert.AreEqual(brick2.XPosition, newBrick2.XPosition);

      //  var brick3 = startScript.BrickList.Bricks[2] as SetYBrick;
      //  var newBrick3 = newStartScript.BrickList.Bricks[2] as SetYBrick;
      //  Assert.AreEqual(brick3.YPosition, newBrick3.YPosition);

      //  var brick4 = startScript.BrickList.Bricks[3] as ChangeXByBrick;
      //  var newBrick4 = newStartScript.BrickList.Bricks[3] as ChangeXByBrick;
      //  Assert.AreEqual(brick4.XMovement, newBrick4.XMovement);

      //  var brick5 = startScript.BrickList.Bricks[4] as ChangeYByBrick;
      //  var newBrick5 = newStartScript.BrickList.Bricks[4] as ChangeYByBrick;
      //  Assert.AreEqual(brick5.YMovement, newBrick5.YMovement);

      //  var brick6 = startScript.BrickList.Bricks[5] as IfOnEdgeBounceBrick;
      //  var newBrick6 = newStartScript.BrickList.Bricks[5] as IfOnEdgeBounceBrick;

      //  var brick7 = startScript.BrickList.Bricks[6] as MoveNStepsBrick;
      //  var newBrick7 = newStartScript.BrickList.Bricks[6] as MoveNStepsBrick;
      //  Assert.AreEqual(brick7.Steps, newBrick7.Steps);

      //  var brick8 = startScript.BrickList.Bricks[7] as TurnLeftBrick;
      //  var newBrick8 = newStartScript.BrickList.Bricks[7] as TurnLeftBrick;
      //  Assert.AreEqual(brick8.Degrees, newBrick8.Degrees);

      //  var brick9 = startScript.BrickList.Bricks[8] as TurnRightBrick;
      //  var newBrick9 = newStartScript.BrickList.Bricks[8] as TurnRightBrick;
      //  Assert.AreEqual(brick9.Degrees, newBrick9.Degrees);

      //  var brick10 = startScript.BrickList.Bricks[9] as PointInDirectionBrick;
      //  var newBrick10 = newStartScript.BrickList.Bricks[9] as PointInDirectionBrick;
      //  Assert.AreEqual(brick10.Degrees, newBrick10.Degrees);

      //  var brick11 = startScript.BrickList.Bricks[10] as PointToBrick;
      //  var newBrick11 = newStartScript.BrickList.Bricks[10] as PointToBrick;
      //  Assert.AreEqual(brick11.PointedSpriteReference.Reference, newBrick11.PointedSpriteReference.Reference);
      //  Assert.AreEqual(newBrick11.PointedSprite, sprite2);
      //  Assert.AreEqual(brick11.PointedSprite, newBrick11.PointedSprite); //should be the same

      //  var brick12 = startScript.BrickList.Bricks[11] as GlideToBrick;
      //  var newBrick12 = newStartScript.BrickList.Bricks[11] as GlideToBrick;
      //  Assert.AreEqual(brick12.DurationInMilliSeconds, newBrick12.DurationInMilliSeconds);
      //  Assert.AreEqual(brick12.XDestination, newBrick12.XDestination);
      //  Assert.AreEqual(brick12.YDestination, newBrick12.YDestination);

      //  var brick13 = startScript.BrickList.Bricks[12] as SetCostumeBrick;
      //  var newBrick13 = newStartScript.BrickList.Bricks[12] as SetCostumeBrick;
      //  Assert.AreEqual(brick13.CostumeReference.Reference, newBrick13.CostumeReference.Reference);
      //  Assert.AreEqual(newBrick13.Costume, newSprite1.CostumeList.Costumes[0]);
      //  Assert.AreNotEqual(brick13.Costume, newBrick13.Costume);

      //  var brick14 = startScript.BrickList.Bricks[13] as NextCostumeBrick;
      //  var newBrick14 = newStartScript.BrickList.Bricks[13] as NextCostumeBrick;

      //  var brick15 = startScript.BrickList.Bricks[14] as SetSizeToBrick;
      //  var newBrick15 = newStartScript.BrickList.Bricks[14] as SetSizeToBrick;
      //  Assert.AreEqual(brick15.Size, newBrick15.Size);

      //  var brick16 = startScript.BrickList.Bricks[15] as ChangeSizeByNBrick;
      //  var newBrick16 = newStartScript.BrickList.Bricks[15] as ChangeSizeByNBrick;
      //  Assert.AreEqual(brick16.Size, newBrick16.Size);

      //  var brick17 = startScript.BrickList.Bricks[16] as HideBrick;
      //  var newBrick17 = newStartScript.BrickList.Bricks[16] as HideBrick;

      //  var brick18 = startScript.BrickList.Bricks[17] as ShowBrick;
      //  var newBrick18 = newStartScript.BrickList.Bricks[17] as ShowBrick;

      //  var brick19 = startScript.BrickList.Bricks[18] as SetGhostEffectBrick;
      //  var newBrick19 = newStartScript.BrickList.Bricks[18] as SetGhostEffectBrick;
      //  Assert.AreEqual(brick19.Transparency, newBrick19.Transparency);

      //  var brick20 = startScript.BrickList.Bricks[19] as ChangeGhostEffectBrick;
      //  var newBrick20 = newStartScript.BrickList.Bricks[19] as ChangeGhostEffectBrick;
      //  Assert.AreEqual(brick20.ChangeGhostEffect, newBrick20.ChangeGhostEffect);

      //  var brick21 = startScript.BrickList.Bricks[20] as SetBrightnessBrick;
      //  var newBrick21 = newStartScript.BrickList.Bricks[20] as SetBrightnessBrick;
      //  Assert.AreEqual(brick21.Brightness, newBrick21.Brightness);

      //  var brick22 = startScript.BrickList.Bricks[21] as ChangeBrightnessBrick;
      //  var newBrick22 = newStartScript.BrickList.Bricks[21] as ChangeBrightnessBrick;
      //  Assert.AreEqual(brick22.ChangeBrightness, newBrick22.ChangeBrightness);

      //  var brick23 = startScript.BrickList.Bricks[22] as ClearGraphicEffectBrick;
      //  var newBrick23 = newStartScript.BrickList.Bricks[22] as ClearGraphicEffectBrick;

      //  Assert.AreEqual(sprite1.SoundList.Sounds.Count, newSprite1.SoundList.Sounds.Count);
      //}

      //{
      //  Assert.AreEqual(sprite2.CostumeList.Costumes.Count, newSprite2.CostumeList.Costumes.Count);

      //  Assert.AreEqual(sprite2.CostumeList.Costumes[0].Name, newSprite2.CostumeList.Costumes[0].Name);
      //  Assert.AreNotEqual(sprite2.CostumeList.Costumes[0].FileName, newSprite2.CostumeList.Costumes[0].FileName);
      //  Assert.AreEqual(sprite2.CostumeList.Costumes[1].Name, newSprite2.CostumeList.Costumes[1].Name);
      //  Assert.AreNotEqual(sprite2.CostumeList.Costumes[1].FileName, newSprite2.CostumeList.Costumes[1].FileName);
      //  using (var isoStore = IsolatedStorageFile.GetUserStoreForApplication())
      //  {
      //    Assert.AreEqual(isoStore.FileExists(pathCostumes + sprite2.CostumeList.Costumes[0].FileName), true);
      //    Assert.AreEqual(isoStore.FileExists(pathCostumes + newSprite2.CostumeList.Costumes[0].FileName), true);
      //    Assert.AreEqual(isoStore.FileExists(pathCostumes + sprite2.CostumeList.Costumes[1].FileName), true);
      //    Assert.AreEqual(isoStore.FileExists(pathCostumes + newSprite2.CostumeList.Costumes[1].FileName), true);
      //  }

      //  Assert.AreEqual(sprite2.Name, newSprite2.Name);
      //  Assert.AreEqual(sprite2.ScriptList.Scripts.Count, newSprite2.ScriptList.Scripts.Count);

      //  var startScript = sprite2.ScriptList.Scripts[0] as StartScript;
      //  var newStartScript = newSprite2.ScriptList.Scripts[0] as StartScript;

      //  Assert.AreEqual(startScript.BrickList.Bricks.Count, newStartScript.BrickList.Bricks.Count);

      //  var brick1 = startScript.BrickList.Bricks[0] as PlaySoundBrick;
      //  var newBrick1 = newStartScript.BrickList.Bricks[0] as PlaySoundBrick;
      //  Assert.AreEqual(brick1.SoundReference.Reference, newBrick1.SoundReference.Reference);
      //  Assert.AreEqual(newBrick1.Sound, newSprite2.SoundList.Sounds[0]);
      //  Assert.AreNotEqual(brick1.Sound, newBrick1.Sound);

      //  var brick2 = startScript.BrickList.Bricks[1] as PlaySoundBrick;
      //  var newBrick2 = newStartScript.BrickList.Bricks[1] as PlaySoundBrick;
      //  Assert.AreEqual(brick2.SoundReference.Reference, newBrick2.SoundReference.Reference);
      //  Assert.AreEqual(newBrick2.Sound, newSprite2.SoundList.Sounds[0]);
      //  Assert.AreNotEqual(brick2.Sound, newBrick2.Sound);

      //  var brick3 = startScript.BrickList.Bricks[2] as PlaySoundBrick;
      //  var newBrick3 = newStartScript.BrickList.Bricks[2] as PlaySoundBrick;
      //  Assert.AreEqual(brick3.SoundReference.Reference, newBrick3.SoundReference.Reference);
      //  Assert.AreEqual(newBrick3.Sound, newSprite2.SoundList.Sounds[1]);
      //  Assert.AreNotEqual(brick3.Sound, newBrick3.Sound);

      //  var whenScript = sprite2.ScriptList.Scripts[1] as WhenScript;
      //  var newWhenScript = newSprite2.ScriptList.Scripts[1] as WhenScript;

      //  Assert.AreEqual(whenScript.BrickList.Bricks.Count, newWhenScript.BrickList.Bricks.Count);

      //  var brick4 = whenScript.BrickList.Bricks[0] as BroadcastBrick;
      //  var newBrick4 = newWhenScript.BrickList.Bricks[0] as BroadcastBrick;
      //  Assert.AreEqual(brick4.BroadcastMessage, newBrick4.BroadcastMessage);

      //  var brick5 = whenScript.BrickList.Bricks[1] as PointToBrick;
      //  var newBrick5 = newWhenScript.BrickList.Bricks[1] as PointToBrick;
      //  Assert.AreEqual(brick5.PointedSpriteReference.Reference, newBrick5.PointedSpriteReference.Reference);
      //  Assert.AreEqual(newBrick5.PointedSprite, sprite1);
      //  Assert.AreEqual(brick5.PointedSprite, newBrick5.PointedSprite);

      //  var brick6 = whenScript.BrickList.Bricks[2] as BroadcastWaitBrick;
      //  var newBrick6 = newWhenScript.BrickList.Bricks[2] as BroadcastWaitBrick;
      //  Assert.AreEqual(brick6.BroadcastMessage, newBrick6.BroadcastMessage);

      //  var brick7 = whenScript.BrickList.Bricks[3] as StopAllSoundsBrick;
      //  var newBrick7 = newWhenScript.BrickList.Bricks[3] as StopAllSoundsBrick;

      //  var brick8 = whenScript.BrickList.Bricks[4] as WaitBrick;
      //  var newBrick8 = newWhenScript.BrickList.Bricks[4] as WaitBrick;
      //  Assert.AreEqual(brick8.TimeToWaitInMilliSeconds, newBrick8.TimeToWaitInMilliSeconds);

      //  var brick9 = whenScript.BrickList.Bricks[5] as NoteBrick;
      //  var newBrick9 = newWhenScript.BrickList.Bricks[5] as NoteBrick;
      //  Assert.AreEqual(brick9.Note, newBrick9.Note);

      //  var brick10 = whenScript.BrickList.Bricks[6] as SetVolumeToBrick;
      //  var newBrick10 = newWhenScript.BrickList.Bricks[6] as SetVolumeToBrick;
      //  Assert.AreEqual(brick10.Volume, newBrick10.Volume);

      //  Assert.AreEqual(whenScript.Action, newWhenScript.Action);

      //  var broadcastScript1 = sprite2.ScriptList.Scripts[2] as BroadcastScript;
      //  var newBroadcastScript1 = newSprite2.ScriptList.Scripts[2] as BroadcastScript;

      //  Assert.AreEqual(broadcastScript1.BrickList.Bricks.Count, newBroadcastScript1.BrickList.Bricks.Count);

      //  var brick11 = broadcastScript1.BrickList.Bricks[0] as ForeverBrick;
      //  var newBrick11 = newBroadcastScript1.BrickList.Bricks[0] as ForeverBrick;
      //  Assert.AreEqual(brick11.LoopEndBrickReference.Reference, newBrick11.LoopEndBrickReference.Reference);
      //  Assert.AreEqual(newBrick11.LoopEndBrick, newBroadcastScript1.BrickList.Bricks[3]);
      //  Assert.AreNotEqual(brick11.LoopEndBrick, newBrick11.LoopEndBrick);

      //  var brick12 = broadcastScript1.BrickList.Bricks[1] as RepeatBrick;
      //  var newBrick12 = newBroadcastScript1.BrickList.Bricks[1] as RepeatBrick;
      //  Assert.AreEqual(brick12.LoopEndBrickReference.Reference, newBrick12.LoopEndBrickReference.Reference);
      //  Assert.AreEqual(newBrick12.LoopEndBrick, newBroadcastScript1.BrickList.Bricks[5]);
      //  Assert.AreNotEqual(brick12.LoopEndBrick, newBrick12.LoopEndBrick);
      //  Assert.AreEqual(brick12.TimesToRepeat, newBrick12.TimesToRepeat);

      //  var brick13 = broadcastScript1.BrickList.Bricks[2] as ChangeVolumeByBrick;
      //  var newBrick13 = newBroadcastScript1.BrickList.Bricks[2] as ChangeVolumeByBrick;
      //  Assert.AreEqual(brick13.Volume, newBrick13.Volume);

      //  var brick14 = broadcastScript1.BrickList.Bricks[3] as LoopEndBrick;
      //  var newBrick14 = newBroadcastScript1.BrickList.Bricks[3] as LoopEndBrick;
      //  Assert.AreEqual(brick14.LoopBeginBrickReference.Class, newBrick14.LoopBeginBrickReference.Class);
      //  Assert.AreEqual(brick14.LoopBeginBrickReference.Reference, newBrick14.LoopBeginBrickReference.Reference);
      //  Assert.AreEqual(newBrick14.LoopBeginBrick, newBroadcastScript1.BrickList.Bricks[0]);
      //  Assert.AreNotEqual(brick14.LoopBeginBrick, newBrick14.LoopBeginBrick);

      //  var brick15 = broadcastScript1.BrickList.Bricks[4] as SpeakBrick;
      //  var newBrick15 = newBroadcastScript1.BrickList.Bricks[4] as SpeakBrick;
      //  Assert.AreEqual(brick15.Text, newBrick15.Text);

      //  var brick16 = broadcastScript1.BrickList.Bricks[5] as LoopEndBrick;
      //  var newBrick16 = newBroadcastScript1.BrickList.Bricks[5] as LoopEndBrick;
      //  Assert.AreEqual(brick16.LoopBeginBrickReference.Class, newBrick16.LoopBeginBrickReference.Class);
      //  Assert.AreEqual(brick16.LoopBeginBrickReference.Reference, newBrick16.LoopBeginBrickReference.Reference);
      //  Assert.AreEqual(newBrick16.LoopBeginBrick, newBroadcastScript1.BrickList.Bricks[1]);
      //  Assert.AreNotEqual(brick16.LoopBeginBrick, newBrick16.LoopBeginBrick);

      //  Assert.AreEqual(broadcastScript1.ReceivedMessage, newBroadcastScript1.ReceivedMessage);

      //  var broadcastScript2 = sprite2.ScriptList.Scripts[3] as BroadcastScript;
      //  var newBroadcastScript2 = newSprite2.ScriptList.Scripts[3] as BroadcastScript;
      //  Assert.AreEqual(broadcastScript2.BrickList.Bricks.Count, newBroadcastScript2.BrickList.Bricks.Count);
      //  Assert.AreEqual(broadcastScript2.ReceivedMessage, newBroadcastScript2.ReceivedMessage);

      //  var soundList = sprite2.SoundList.Sounds;
      //  var newSoundList = newSprite2.SoundList.Sounds;
      //  Assert.AreEqual(soundList.Count, newSoundList.Count);

      //  var soundInfo1 = sprite2.SoundList.Sounds[0] as Sound;
      //  var newSoundInfo1 = newSprite2.SoundList.Sounds[0] as Sound;
      //  Assert.AreEqual(soundInfo1.Name, newSoundInfo1.Name);
      //  Assert.AreNotEqual(soundInfo1.FileName, newSoundInfo1.FileName);

      //  var soundInfo2 = sprite2.SoundList.Sounds[1] as Sound;
      //  var newSoundInfo2 = newSprite2.SoundList.Sounds[1] as Sound;
      //  Assert.AreEqual(soundInfo2.Name, newSoundInfo2.Name);
      //  Assert.AreNotEqual(soundInfo2.FileName, newSoundInfo2.FileName);

      //  using (var isoStore = IsolatedStorageFile.GetUserStoreForApplication())
      //  {
      //    Assert.AreEqual(isoStore.FileExists(pathSounds + soundInfo1.FileName), true);
      //    Assert.AreEqual(isoStore.FileExists(pathSounds + newSoundInfo1.FileName), true);
      //    Assert.AreEqual(isoStore.FileExists(pathSounds + soundInfo2.FileName), true);
      //    Assert.AreEqual(isoStore.FileExists(pathSounds + newSoundInfo2.FileName), true);
      //  }
      //}
      //{
      //  Assert.AreEqual(sprite3.CostumeList.Costumes.Count, newSprite3.CostumeList.Costumes.Count);
      //  Assert.AreEqual(sprite3.Name, newSprite3.Name);
      //  Assert.AreEqual(sprite3.ScriptList.Scripts.Count, newSprite3.ScriptList.Scripts.Count);

      //  var startScript = sprite3.ScriptList.Scripts[0] as StartScript;
      //  var newStartScript = newSprite3.ScriptList.Scripts[0] as StartScript;

      //  Assert.AreEqual(startScript.BrickList.Bricks.Count, newStartScript.BrickList.Bricks.Count);

      //  var brick1 = startScript.BrickList.Bricks[0] as NxtMotorTurnAngleBrick;
      //  var newBrick1 = newStartScript.BrickList.Bricks[0] as NxtMotorTurnAngleBrick;
      //  Assert.AreEqual(brick1.Degrees, newBrick1.Degrees);
      //  Assert.AreEqual(brick1.Motor, newBrick1.Motor);

      //  var brick2 = startScript.BrickList.Bricks[1] as PointToBrick;
      //  var newBrick2 = newStartScript.BrickList.Bricks[1] as PointToBrick;
      //  Assert.AreEqual(brick2.PointedSpriteReference.Reference, newBrick2.PointedSpriteReference.Reference);
      //  Assert.AreEqual(newBrick2.PointedSprite, sprite2);
      //  Assert.AreEqual(brick2.PointedSprite, newBrick2.PointedSprite);

      //  var brick3 = startScript.BrickList.Bricks[2] as NxtMotorStopBrick;
      //  var newBrick3 = newStartScript.BrickList.Bricks[2] as NxtMotorStopBrick;
      //  Assert.AreEqual(brick3.Motor, newBrick3.Motor);

      //  var brick4 = startScript.BrickList.Bricks[3] as NxtMotorActionBrick;
      //  var newBrick4 = newStartScript.BrickList.Bricks[3] as NxtMotorActionBrick;
      //  Assert.AreEqual(brick4.Speed, newBrick4.Speed);
      //  Assert.AreEqual(brick4.Motor, newBrick4.Motor);

      //  var brick5 = startScript.BrickList.Bricks[4] as NxtPlayToneBrick;
      //  var newBrick5 = newStartScript.BrickList.Bricks[4] as NxtPlayToneBrick;
      //  Assert.AreEqual(brick5.DurationInMs, newBrick5.DurationInMs);
      //  Assert.AreEqual(brick5.Hertz, newBrick5.Hertz);

      //  Assert.AreEqual(sprite3.SoundList.Sounds.Count, newSprite3.SoundList.Sounds.Count);
      //}
    }
  }
}
