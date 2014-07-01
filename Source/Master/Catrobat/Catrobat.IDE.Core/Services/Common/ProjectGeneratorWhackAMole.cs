using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Scripts;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services.Storage;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;

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

        public string GetProjectName(string twoLetterIsoLanguageCode)
        {
            return AppResources.Main_DefaultProjectName;
        }

        public async Task<Project> GenerateProject(bool writeToDisk)
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
            var objectBackground = new Sprite { Name = "Background" }; //AppResources.DefaultProject_Background }; // TODO: use right localizations
            var objectMole1 = new Sprite { Name = "Mole1" }; //AppResources.DefaultProject_Background };
            var objectMole2 = new Sprite { Name = "Mole2" }; //AppResources.DefaultProject_Background };
            var objectMole3 = new Sprite { Name = "Mole3" }; //AppResources.DefaultProject_Background };
            var objectMole4 = new Sprite { Name = "Mole4" }; //AppResources.DefaultProject_Background };

            Costume back = new Costume
            {
                Name = AppResources.DefaultProject_Background,
                FileName = LookFileNameBackground
            };
            objectBackground.Costumes.Add(back);

            objectBackground.Costumes.Add(new Costume
            {
                Name = AppResources.DefaultProject_Background,
                FileName = LookFileNameBackgroundFinished
            });

            objectBackground.Scripts.Add(new StartScript());

            #region mole1

            objectMole1.Costumes.Add(new Costume { Name = "moving mole", FileName = LookFileNameMole1 });
            objectMole1.Costumes.Add(new Costume { Name = "dizzy mole", FileName = LookFileNameMole2 });
            objectMole1.Costumes.Add(new Costume { Name = "mole", FileName = LookFileNameMole3 });

            StartScript startScriptMole1 = new StartScript();
            {
                SetSizeBrick sizeBrick = new SetSizeBrick();
                sizeBrick.Percentage = FormulaTreeFactory.CreateNumberNode(30);
                startScriptMole1.Bricks.Add(sizeBrick);
            }
            startScriptMole1.Bricks.Add(new ForeverBrick());
            {
                SetPositionBrick pos = new SetPositionBrick();
                pos.ValueX = FormulaTreeFactory.CreateNumberNode(-135);
                pos.ValueY = FormulaTreeFactory.CreateNumberNode(-100);
                startScriptMole1.Bricks.Add(pos);
            }
            {
                DelayBrick wait = new DelayBrick();
                wait.Duration = FormulaTreeFactory.CreateRandomNode(
                    firstChild: FormulaTreeFactory.CreateNumberNode(1), 
                    secondChild: FormulaTreeFactory.CreateNumberNode(2));
                startScriptMole1.Bricks.Add(wait);
            }
            startScriptMole1.Bricks.Add(new ShowBrick());
            {
                SetCostumeBrick costume = new SetCostumeBrick();
                costume.Value = objectMole1.Costumes[0];
                startScriptMole1.Bricks.Add(costume);
            }
            {
                AnimatePositionBrick slide = new AnimatePositionBrick();
                slide.Duration = FormulaTreeFactory.CreateNumberNode(0.1);
                slide.ToX = FormulaTreeFactory.CreateNumberNode(0);
                slide.ToY = FormulaTreeFactory.CreateNumberNode(10);
                startScriptMole1.Bricks.Add(slide);
            }
            {
                SetCostumeBrick costume = new SetCostumeBrick();
                costume.Value = objectMole1.Costumes[2];
                startScriptMole1.Bricks.Add(costume);
            }
            {
                DelayBrick wait = new DelayBrick();
                wait.Duration = FormulaTreeFactory.CreateRandomNode(
                    firstChild: FormulaTreeFactory.CreateNumberNode(1),
                    secondChild: FormulaTreeFactory.CreateNumberNode(5));  
                startScriptMole1.Bricks.Add(wait);
            }
            startScriptMole1.Bricks.Add(new HideBrick());
            {
                DelayBrick wait = new DelayBrick();
                wait.Duration = FormulaTreeFactory.CreateRandomNode(
                    firstChild: FormulaTreeFactory.CreateNumberNode(1),
                    secondChild: FormulaTreeFactory.CreateNumberNode(5));  
                startScriptMole1.Bricks.Add(wait);
            }
            startScriptMole1.Bricks.Add(new EndForeverBrick());
            objectMole1.Scripts.Add(startScriptMole1);

            TappedScript tappedScriptMole1 = new TappedScript();
            {
                SetCostumeBrick costume = new SetCostumeBrick();
                costume.Value = objectMole1.Costumes[1];
                tappedScriptMole1.Bricks.Add(costume);
            }
            {
                DelayBrick wait = new DelayBrick();
                wait.Duration = FormulaTreeFactory.CreateNumberNode(1.5);
                tappedScriptMole1.Bricks.Add(wait);     
            }
            tappedScriptMole1.Bricks.Add(new HideBrick());
            objectMole1.Scripts.Add(tappedScriptMole1);

            #endregion

            #region mole2

            objectMole2.Costumes.Add(new Costume { Name = "moving mole", FileName = LookFileNameMole1 });
            objectMole2.Costumes.Add(new Costume { Name = "dizzy mole", FileName = LookFileNameMole2 });
            objectMole2.Costumes.Add(new Costume { Name = "mole", FileName = LookFileNameMole3 });

            StartScript startScriptMole2 = new StartScript();
            {
                SetSizeBrick sizeBrick = new SetSizeBrick();
                sizeBrick.Percentage = FormulaTreeFactory.CreateNumberNode(30);
                startScriptMole2.Bricks.Add(sizeBrick);
            }
            startScriptMole2.Bricks.Add(new ForeverBrick());
            {
                SetPositionBrick pos = new SetPositionBrick();
                pos.ValueX = FormulaTreeFactory.CreateNumberNode(135);
                pos.ValueY = FormulaTreeFactory.CreateNumberNode(-100);
                startScriptMole2.Bricks.Add(pos);
            }
            {
                DelayBrick wait = new DelayBrick();
                wait.Duration = FormulaTreeFactory.CreateRandomNode(
                    firstChild: FormulaTreeFactory.CreateNumberNode(1),
                    secondChild: FormulaTreeFactory.CreateNumberNode(2));
                startScriptMole2.Bricks.Add(wait);
            }
            startScriptMole2.Bricks.Add(new ShowBrick());
            {
                SetCostumeBrick costume = new SetCostumeBrick();
                costume.Value = objectMole2.Costumes[0];
                startScriptMole2.Bricks.Add(costume);
            }
            {
                AnimatePositionBrick slide = new AnimatePositionBrick();
                slide.Duration = FormulaTreeFactory.CreateNumberNode(0.1);
                slide.ToX = FormulaTreeFactory.CreateNumberNode(0);
                slide.ToY = FormulaTreeFactory.CreateNumberNode(10);
                startScriptMole2.Bricks.Add(slide);
            }
            {
                SetCostumeBrick costume = new SetCostumeBrick();
                costume.Value = objectMole2.Costumes[2];
                startScriptMole2.Bricks.Add(costume);
            }
            {
                DelayBrick wait = new DelayBrick();
                wait.Duration = FormulaTreeFactory.CreateRandomNode(
                    firstChild: FormulaTreeFactory.CreateNumberNode(1),
                    secondChild: FormulaTreeFactory.CreateNumberNode(5));
                startScriptMole2.Bricks.Add(wait);
            }
            startScriptMole2.Bricks.Add(new HideBrick());
            {
                DelayBrick wait = new DelayBrick();
                wait.Duration = FormulaTreeFactory.CreateRandomNode(
                    firstChild: FormulaTreeFactory.CreateNumberNode(1),
                    secondChild: FormulaTreeFactory.CreateNumberNode(5));
                startScriptMole2.Bricks.Add(wait);
            }
            startScriptMole2.Bricks.Add(new EndForeverBrick());
            objectMole2.Scripts.Add(startScriptMole2);

            TappedScript tappedScriptMole2 = new TappedScript();
            {
                SetCostumeBrick costume = new SetCostumeBrick();
                costume.Value = objectMole2.Costumes[1];
                tappedScriptMole2.Bricks.Add(costume);
            }
            {
                DelayBrick wait = new DelayBrick();
                wait.Duration = FormulaTreeFactory.CreateNumberNode(1.5);
                tappedScriptMole2.Bricks.Add(wait);
            }
            tappedScriptMole2.Bricks.Add(new HideBrick());
            objectMole2.Scripts.Add(tappedScriptMole2);

            #endregion

            #region mole3

            objectMole3.Costumes.Add(new Costume { Name = "moving mole", FileName = LookFileNameMole1 });
            objectMole3.Costumes.Add(new Costume { Name = "dizzy mole", FileName = LookFileNameMole2 });
            objectMole3.Costumes.Add(new Costume { Name = "mole", FileName = LookFileNameMole3 });

            StartScript startScriptMole3 = new StartScript();
            {
                SetSizeBrick sizeBrick = new SetSizeBrick();
                sizeBrick.Percentage = FormulaTreeFactory.CreateNumberNode(30);
                startScriptMole3.Bricks.Add(sizeBrick);
            }
            startScriptMole3.Bricks.Add(new ForeverBrick());
            {
                SetPositionBrick pos = new SetPositionBrick();
                pos.ValueX = FormulaTreeFactory.CreateNumberNode(-135);
                pos.ValueY = FormulaTreeFactory.CreateNumberNode(-255);
                startScriptMole3.Bricks.Add(pos);
            }
            {
                DelayBrick wait = new DelayBrick();
                wait.Duration = FormulaTreeFactory.CreateRandomNode(
                    firstChild: FormulaTreeFactory.CreateNumberNode(1),
                    secondChild: FormulaTreeFactory.CreateNumberNode(2));
                startScriptMole3.Bricks.Add(wait);
            }
            startScriptMole3.Bricks.Add(new ShowBrick());
            {
                SetCostumeBrick costume = new SetCostumeBrick();
                costume.Value = objectMole3.Costumes[0];
                startScriptMole3.Bricks.Add(costume);
            }
            {
                AnimatePositionBrick slide = new AnimatePositionBrick();
                slide.Duration = FormulaTreeFactory.CreateNumberNode(0.1);
                slide.ToX = FormulaTreeFactory.CreateNumberNode(0);
                slide.ToY = FormulaTreeFactory.CreateNumberNode(10);
                startScriptMole3.Bricks.Add(slide);
            }
            {
                SetCostumeBrick costume = new SetCostumeBrick();
                costume.Value = objectMole3.Costumes[2];
                startScriptMole3.Bricks.Add(costume);
            }
            {
                DelayBrick wait = new DelayBrick();
                wait.Duration = FormulaTreeFactory.CreateRandomNode(
                    firstChild: FormulaTreeFactory.CreateNumberNode(1),
                    secondChild: FormulaTreeFactory.CreateNumberNode(5));
                startScriptMole3.Bricks.Add(wait);
            }
            startScriptMole3.Bricks.Add(new HideBrick());
            {
                DelayBrick wait = new DelayBrick();
                wait.Duration = FormulaTreeFactory.CreateRandomNode(
                    firstChild: FormulaTreeFactory.CreateNumberNode(1),
                    secondChild: FormulaTreeFactory.CreateNumberNode(5));
                startScriptMole3.Bricks.Add(wait);
            }
            startScriptMole3.Bricks.Add(new EndForeverBrick());
            objectMole3.Scripts.Add(startScriptMole3);

            TappedScript tappedScriptMole3 = new TappedScript();
            {
                SetCostumeBrick costume = new SetCostumeBrick();
                costume.Value = objectMole3.Costumes[1];
                tappedScriptMole3.Bricks.Add(costume);
            }
            {
                DelayBrick wait = new DelayBrick();
                wait.Duration = FormulaTreeFactory.CreateNumberNode(1.5);
                tappedScriptMole3.Bricks.Add(wait);
            }
            tappedScriptMole3.Bricks.Add(new HideBrick());
            objectMole3.Scripts.Add(tappedScriptMole3);

            #endregion

            #region mole4

            objectMole4.Costumes.Add(new Costume { Name = "moving mole", FileName = LookFileNameMole1 });
            objectMole4.Costumes.Add(new Costume { Name = "dizzy mole", FileName = LookFileNameMole2 });
            objectMole4.Costumes.Add(new Costume { Name = "mole", FileName = LookFileNameMole3 });

            StartScript startScriptMole4 = new StartScript();
            {
                SetSizeBrick sizeBrick = new SetSizeBrick();
                sizeBrick.Percentage = FormulaTreeFactory.CreateNumberNode(30);
                startScriptMole4.Bricks.Add(sizeBrick);
            }
            startScriptMole4.Bricks.Add(new ForeverBrick());
            {
                SetPositionBrick pos = new SetPositionBrick();
                pos.ValueX = FormulaTreeFactory.CreateNumberNode(135);
                pos.ValueY = FormulaTreeFactory.CreateNumberNode(-255);
                startScriptMole4.Bricks.Add(pos);
            }
            {
                DelayBrick wait = new DelayBrick();
                wait.Duration = FormulaTreeFactory.CreateRandomNode(
                    firstChild: FormulaTreeFactory.CreateNumberNode(1),
                    secondChild: FormulaTreeFactory.CreateNumberNode(2));
                startScriptMole4.Bricks.Add(wait);
            }
            startScriptMole4.Bricks.Add(new ShowBrick());
            {
                SetCostumeBrick costume = new SetCostumeBrick();
                costume.Value = objectMole4.Costumes[0];
                startScriptMole4.Bricks.Add(costume);
            }
            {
                AnimatePositionBrick slide = new AnimatePositionBrick();
                slide.Duration = FormulaTreeFactory.CreateNumberNode(0.1);
                slide.ToX = FormulaTreeFactory.CreateNumberNode(0);
                slide.ToY = FormulaTreeFactory.CreateNumberNode(10);
                startScriptMole4.Bricks.Add(slide);
            }
            {
                SetCostumeBrick costume = new SetCostumeBrick();
                costume.Value = objectMole4.Costumes[2];
                startScriptMole4.Bricks.Add(costume);
            }
            {
                DelayBrick wait = new DelayBrick();
                wait.Duration = FormulaTreeFactory.CreateRandomNode(
                    firstChild: FormulaTreeFactory.CreateNumberNode(1),
                    secondChild: FormulaTreeFactory.CreateNumberNode(5));
                startScriptMole4.Bricks.Add(wait);
            }
            startScriptMole4.Bricks.Add(new HideBrick());
            {
                DelayBrick wait = new DelayBrick();
                wait.Duration = FormulaTreeFactory.CreateRandomNode(
                    firstChild: FormulaTreeFactory.CreateNumberNode(1),
                    secondChild: FormulaTreeFactory.CreateNumberNode(5));
                startScriptMole4.Bricks.Add(wait);
            }
            startScriptMole4.Bricks.Add(new EndForeverBrick());
            objectMole4.Scripts.Add(startScriptMole4);

            TappedScript tappedScriptMole4 = new TappedScript();
            {
                SetCostumeBrick costume = new SetCostumeBrick();
                costume.Value = objectMole4.Costumes[1];
                tappedScriptMole4.Bricks.Add(costume);
            }
            {
                DelayBrick wait = new DelayBrick();
                wait.Duration = FormulaTreeFactory.CreateNumberNode(1.5);
                tappedScriptMole4.Bricks.Add(wait);
            }
            tappedScriptMole4.Bricks.Add(new HideBrick());
            objectMole4.Scripts.Add(tappedScriptMole4);

            #endregion

            project.Sprites.Add(objectBackground);
            project.Sprites.Add(objectMole1);
            project.Sprites.Add(objectMole2);
            project.Sprites.Add(objectMole3);
            project.Sprites.Add(objectMole4);
        }


        public string GetProjectName()
        {
            return AppResources.Main_DefaultProjectName;
        }

        public int GetOrderId()
        {
            return 1;
        }
    }
}
