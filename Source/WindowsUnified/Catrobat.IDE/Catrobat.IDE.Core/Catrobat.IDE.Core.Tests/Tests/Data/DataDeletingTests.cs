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

        [TestMethod, TestCategory("GatedTests")]
        public async Task DeleteSprite()
        {
            const string programName = "DataDeletingTests.DeleteSprite";

            using (IStorage storage = StorageSystem.GetStorage())
            {
                storage.DeleteDirectory(Path.Combine(StorageConstants.ProjectsPath, programName));
            }

            ITestProjectGenerator projectgenerator = new ProjectGeneratorReflection();
            var project = projectgenerator.GenerateProject();
            await project.SetProgramNameAndRenameDirectory(programName);
            // TODO: write dummy costume files to disk

            using (IStorage storage = StorageSystem.GetStorage())
            {
                foreach (var sprite in project.Sprites)
                {
                    foreach (var costume in sprite.Costumes)
                    {
                        //Projects/DataDeletingTests.DeleteSprite/images/
                        var stream = storage.OpenFile(Path.Combine(project.BasePath, StorageConstants.ProgramImagesPath , costume.FileName), 
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

            var pathCostumes = project.BasePath + "/" + StorageConstants.ProgramImagesPath + "/";
            var pathSounds = project.BasePath + "/" + StorageConstants.ProgramSoundsPath + "/";

            var costumes = new List<Costume>();
            var sounds = new List<Sound>();

            using (IStorage storage = StorageSystem.GetStorage())
            {
                foreach (var sprite in project.Sprites)
                {
                    foreach (var costume in sprite.Costumes)
                    {
                        costumes.Add(costume);
                        Assert.IsTrue(storage.FileExists(pathCostumes + costume.FileName));
                    }
                    foreach (var sound in sprite.Sounds)
                    {
                        sounds.Add(sound);
                        Assert.IsTrue(storage.FileExists(pathSounds + sound.FileName));
                    }

                    await sprite.Delete(project);
                }

                foreach (var costume in costumes)
                    Assert.IsFalse(storage.FileExists(pathCostumes + costume.FileName));
                foreach (var sound in sounds)
                    Assert.IsFalse(storage.FileExists(pathSounds + sound.FileName));
            }
        }
    }
}
