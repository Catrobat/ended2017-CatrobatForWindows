using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Tests.Misc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.Data
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
        public async Task DeleteSprite()
        {
            const string programName = "DataDeletingTests.DeleteSprite";

            using (IStorage storage = StorageSystem.GetStorage())
            {
                storage.DeleteDirectory(Path.Combine(
                    StorageConstants.ProgramsPath, programName));
            }

            ITestProgramGenerator projectgenerator = new ProjectGeneratorReflection();
            var project = projectgenerator.GenerateProgram();
            await project.SetProgramNameAndRenameDirectory(programName);
            // TODO: write dummy look files to disk

            using (IStorage storage = StorageSystem.GetStorage())
            {
                foreach (var sprite in project.Sprites)
                {
                    foreach (var look in sprite.Looks)
                    {
                        //Projects/DataDeletingTests.DeleteSprite/images/
                        var stream = storage.OpenFile(Path.Combine(project.BasePath, StorageConstants.ProgramLooksPath , look.FileName), 
                            StorageFileMode.Create, StorageFileAccess.Write);
                        stream.Close();
                    }

                    foreach (var sound in sprite.Sounds)
                    {
                        var stream = storage.OpenFile(Path.Combine(project.BasePath, StorageConstants.ProgramSoundsPath, sound.FileName),
                            StorageFileMode.Create, StorageFileAccess.Write);
                        stream.Close();
                    }
                }
            }


            await project.Save();

            var pathLooks = project.BasePath + "/" + StorageConstants.ProgramLooksPath + "/";
            var pathSounds = project.BasePath + "/" + StorageConstants.ProgramSoundsPath + "/";

            var looks = new List<Look>();
            var sounds = new List<Sound>();

            using (IStorage storage = StorageSystem.GetStorage())
            {
                foreach (var sprite in project.Sprites)
                {
                    foreach (var look in sprite.Looks)
                    {
                        looks.Add(look);
                        Assert.IsTrue(storage.FileExists(pathLooks + look.FileName));
                    }
                    foreach (var sound in sprite.Sounds)
                    {
                        sounds.Add(sound);
                        Assert.IsTrue(storage.FileExists(pathSounds + sound.FileName));
                    }

                    await sprite.Delete(project);
                }

                foreach (var look in looks)
                    Assert.IsFalse(storage.FileExists(pathLooks + look.FileName));
                foreach (var sound in sounds)
                    Assert.IsFalse(storage.FileExists(pathSounds + sound.FileName));
            }
        }
    }
}
