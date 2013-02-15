using System;
using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Costumes;
using Catrobat.Core.Objects.Sounds;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using TestsWindowsStore.Data.SampleData;
using Catrobat.Core.Storage;

namespace TestsWindowsStore.Data
{
    [TestClass]
    public class DataDeletingTests
    {
        [TestMethod]
        public void DeleteSprite()
        {
            SampleLoader.LoadSampleProject("UltimateTest.catroid", "UltimateTest");

            var project = CatrobatContext.Instance.CurrentProject;

            var sprite1 = project.SpriteList.Sprites[0];
            var sprite2 = project.SpriteList.Sprites[1];

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
        }
    }
}
