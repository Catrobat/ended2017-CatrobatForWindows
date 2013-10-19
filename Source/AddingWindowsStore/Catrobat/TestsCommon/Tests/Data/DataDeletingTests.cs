using System.Collections.Generic;
using Catrobat.Core.Services.Storage;
using Catrobat.Core.Utilities.Helpers;
using Catrobat.Core.CatrobatObjects;
using Catrobat.Core.CatrobatObjects.Costumes;
using Catrobat.Core.CatrobatObjects.Sounds;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.TestsCommon.Misc;

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
            var project = ProjectGenerator.GenerateProject();

            var pathCostumes = project.BasePath + "/" + Project.ImagesPath + "/";
            var pathSounds = project.BasePath + "/" + Project.SoundsPath + "/";

            var costumes = new List<Costume>();
            var sounds = new List<Sound>();

            using (IStorage storage = StorageSystem.GetStorage())
            {
                foreach (var sprite in project.SpriteList.Sprites)
                {
                    foreach (var costume in sprite.Costumes.Costumes)
                    {
                        costumes.Add(costume);
                        Assert.IsTrue(storage.FileExists(pathCostumes + costume.FileName));
                    }
                    foreach (var sound in sprite.Sounds.Sounds)
                    {
                        sounds.Add(sound);
                        Assert.IsTrue(storage.FileExists(pathSounds + sound.FileName));
                    }

                    sprite.Delete();
                }

                foreach (var costume in costumes)
                    Assert.IsFalse(storage.FileExists(pathCostumes + costume.FileName));
                foreach (var sound in sounds)
                    Assert.IsFalse(storage.FileExists(pathSounds + sound.FileName));
            }
        }
    }
}
