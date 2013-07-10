using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.Core.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.TestsCommon.Misc;
using Catrobat.TestsCommon.SampleData;

namespace Catrobat.TestsCommon.Tests.Data
{
  [TestClass]
  public class DataDeletingTests
  {
    [ClassInitialize()]
    public static void TestClassInitialize(TestContext testContext)
    {
      TestHelper.InitializeTests();
    }

    [TestMethod]
    public void DeleteSprite()
    {
      var catrobatContext = SampleLoader.LoadSampleProject("UltimateTest.catroid", "UltimateTest");
      CatrobatContext.SetContextHolder(new ContextHolderTests(catrobatContext));

      var project = catrobatContext.CurrentProject;

      var sprite1 = project.ObjectList.Sprites[0];
      var sprite2 = project.ObjectList.Sprites[1];

      var costume = sprite1.Costumes.Costumes[0];
      var soundInfo = sprite2.Sounds.Sounds[0];

      sprite1.Delete();
      sprite2.Delete();

      var pathCostumes = project.BasePath + "/" + Project.ImagesPath + "/";
      var pathSounds = project.BasePath + "/" + Project.SoundsPath + "/";

      using (IStorage storage = StorageSystem.GetStorage())
      {
        Assert.AreEqual(storage.FileExists(pathCostumes + costume.FileName), false);
        Assert.AreEqual(storage.FileExists(pathSounds + soundInfo.FileName), false);
      }

      CatrobatContext.SetContextHolder(null);
    }
  }
}
