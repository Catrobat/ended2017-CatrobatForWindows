using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Scripts;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services.Storage;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Catrobat.IDE.Core.Services.Common
{
    public class ProjectGeneratorWhackAMole : IProjectGenerator
    {
        private const string ResourcePathToLookFiles = "Content/Programs/WhackAMole/looks/";
        private const string ResourcePathToSoundFiles = "Content/Programs/WhackAMole/sounds/";
        private const string ResourcePathToScreenshot = "Content/Programs/WhackAMole";
        private const string AutomatedScreenshotFilename = "automatic_screenshot.png";
        private const string ManualScreenshotFilename = "manual_screenshot.png";

        private const string LookFileNameBackground = "1D687270C999067F2DDFCB8CA59046E7_whack-a-mole-BG.png";
        private const string LookFileNameBackgroundFinished = "4137DB0956BAA9780A087C874900B5EB_whack-a-mole-BG.png";
        private const string LookFileNameMole1 = "4871A26C832F7C63CB7F11E84F41338D_mole.png";
        private const string LookFileNameMole2 = "A44CC0D6B30B16AF796243A6D7BA8AC5_mole.png";
        private const string LookFileNameMole3 = "D6E92825145EC9CA2BF9CF9ACC21D68A_mole.png";

        private const string SoundFileName1 = "060830B7BF0AE16166C15748387F9021_record.mp3";
        private const string SoundFileName2 = "0B12E691684C552746E9FC164BE4D592_record.mp3";
        private const string SoundFileName3 = "C006F161E41ACB98A9EB7B1E22405971_record.mp3";
        private const string SoundFileName4 = "C03622EC424461AB47259339AB71CF1C_record.mp3";

        public async Task<Project> GenerateProject(string twoLetterIsoLanguageCode, bool writeToDisk)
        {
            var project = new Project
            {
                Name = AppResources.Main_DefaultProjectName, 
                UploadHeader = new UploadHeader
                {
                    MediaLicense = "http://developer.catrobat.org/ccbysa_v3",
                    ProgramLicense = "http://developer.catrobat.org/agpl_v3",
                    Url = "http://pocketcode.org/details/871"
                }
            };

            if (writeToDisk)
            {
                await WriteScreenshotsToDisk(project.BasePath);
                await WriteLooksToDisk(Path.Combine(project.BasePath, Project.ImagesPath));
                await WriteSoundsToDisk(Path.Combine(project.BasePath, Project.SoundsPath));
            }

            FillSprites(project);

            if (writeToDisk)
                await project.Save();

            return project;
        }

        private static async Task WriteScreenshotsToDisk(string basePathToScreenshotFiles)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                using (var loader = ServiceLocator.ResourceLoaderFactory.CreateResourceLoader())
                {
                    var inputStream =
                        await loader.OpenResourceStreamAsync(ResourceScope.IdePhone,
                        Path.Combine(ResourcePathToScreenshot, AutomatedScreenshotFilename));

                    var outputStream = await storage.OpenFileAsync(
                        Path.Combine(basePathToScreenshotFiles, AutomatedScreenshotFilename),
                        StorageFileMode.Create, StorageFileAccess.Write);

                    inputStream.CopyTo(outputStream);
                    outputStream.Flush();
                    inputStream.Dispose();
                    outputStream.Dispose();

                    var inputStream2 =
                        await loader.OpenResourceStreamAsync(ResourceScope.IdePhone,
                        Path.Combine(ResourcePathToScreenshot, ManualScreenshotFilename));

                    var outputStream2 = await storage.OpenFileAsync(
                        Path.Combine(basePathToScreenshotFiles, ManualScreenshotFilename),
                        StorageFileMode.Create, StorageFileAccess.Write);

                    inputStream2.CopyTo(outputStream2);
                    outputStream2.Flush();
                    inputStream2.Dispose();
                    outputStream2.Dispose();
                }
            }
        }

        private static async Task WriteLooksToDisk(string basePathToLookFiles)
        {
            var lookFiles = new List<string>
            {
                LookFileNameBackground,
                LookFileNameBackgroundFinished,
                LookFileNameMole1,
                LookFileNameMole2,
                LookFileNameMole3
            };

            using (var storage = StorageSystem.GetStorage())
            {
                using (var loader = ServiceLocator.ResourceLoaderFactory.CreateResourceLoader())
                {
                    foreach (var lookFile in lookFiles)
                    {
                        var inputStream = await loader.OpenResourceStreamAsync(ResourceScope.IdePhone, // TODO: change resourceScope to suppot phone and store app  (after 8.1 upgrade)
                            Path.Combine(ResourcePathToLookFiles, lookFile));

                        var outputStream = await storage.OpenFileAsync(Path.Combine(basePathToLookFiles, lookFile),
                            StorageFileMode.Create, StorageFileAccess.Write);

                        inputStream.CopyTo(outputStream);
                        outputStream.Flush();
                        inputStream.Dispose();
                        outputStream.Dispose();
                    }
                }
            }
        }

        private static async Task WriteSoundsToDisk(string basePathToSoundFiles)
        {
            var soundFiles = new List<string>
            {
                SoundFileName1,
                SoundFileName2,
                SoundFileName3,
                SoundFileName4
            };

            using (var storage = StorageSystem.GetStorage())
            {
                using (var loader = ServiceLocator.ResourceLoaderFactory.CreateResourceLoader())
                {
                    foreach (var soundFile in soundFiles)
                    {
                        var inputStream = await loader.OpenResourceStreamAsync(ResourceScope.IdePhone, // TODO: change resourceScope to suppot phone and store app (after 8.1 upgrade)
                            Path.Combine(ResourcePathToSoundFiles, soundFile));

                        var outputStream = await storage.OpenFileAsync(Path.Combine(basePathToSoundFiles, soundFile),
                            StorageFileMode.Create, StorageFileAccess.Write);

                        inputStream.CopyTo(outputStream);
                        outputStream.Flush();
                        inputStream.Dispose();
                        outputStream.Dispose();
                    }
                }
            }
        }


        private static void FillSprites(Project project)
        {
            var objectBackground = new Sprite { Name = AppResources.DefaultProject_Background }; // TODO: use right localizations
            var objectMole1 = new Sprite { Name = AppResources.DefaultProject_Background };
            var objectMole2 = new Sprite { Name = AppResources.DefaultProject_Background };
            var objectMole3 = new Sprite { Name = AppResources.DefaultProject_Background };
            var objectMole4 = new Sprite { Name = AppResources.DefaultProject_Background };

            objectBackground.Costumes.Add(new Costume
            {
                Name = AppResources.DefaultProject_Background,
                FileName = LookFileNameBackground
            });

            objectBackground.Costumes.Add(new Costume
            {
                Name = AppResources.DefaultProject_Background,
                FileName = LookFileNameBackgroundFinished
            });

            objectBackground.Scripts.Add(new StartScript());


            // TODO: implement me




            project.Sprites.Add(objectBackground);
            project.Sprites.Add(objectMole1);
            project.Sprites.Add(objectMole2);
            project.Sprites.Add(objectMole3);
            project.Sprites.Add(objectMole4);
        }
    }
}
