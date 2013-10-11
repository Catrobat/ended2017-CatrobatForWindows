using System.Collections.Generic;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Costumes;
using Catrobat.IDE.Core.CatrobatObjects.Sounds;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Tests.Misc;

namespace Catrobat.IDE.Tests.Tests.Data
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
