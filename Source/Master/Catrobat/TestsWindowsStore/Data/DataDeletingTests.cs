using System;
using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Costumes;
using Catrobat.Core.Objects.Sounds;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace TestsWindowsStore.Data
{
  [TestClass]
  public class DataDeletingTests
  {
    [TestMethod]
    public void DeleteSprite()
    {
      throw new NotImplementedException("Fix changed names");
      //SampleLoader.LoadSampleProject("UltimateTest.catroid", "UltimateTest");

      //var project = CatrobatContext.Instance.CurrentProject;

      //var sprite1 = project.SpriteList.Sprites[0];
      //var sprite2 = project.SpriteList.Sprites[1];

      //var costume = sprite1.CostumeList.Costumes[0] as Costume;
      //var soundInfo = sprite2.SoundList.Sounds[0] as Sound;

      //sprite1.Delete();
      //sprite2.Delete();

      //var pathCostumes = project.BasePath + "/" + Project.ImagesPath + "/";
      //var pathSounds = project.BasePath + "/" + Project.SoundsPath + "/";

      //using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
      //{
      //  Assert.AreEqual(myIsolatedStorage.FileExists(pathCostumes + costume.FileName), false);
      //  Assert.AreEqual(myIsolatedStorage.FileExists(pathSounds + soundInfo.FileName), false);
      //}
    }
  }
}
