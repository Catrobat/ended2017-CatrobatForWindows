using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Models.Scripts;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services.Storage;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Catrobat.IDE.Core.Services.Common
{
    public class ProgramGeneratorWhackAMole : IProgramGenerator
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

        public async Task<Program> GenerateProgram(string programName, bool writeToDisk)
        {
            var project = new Program
            {
                Name = programName, 
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
                await WriteLooksToDisk(Path.Combine(project.BasePath, StorageConstants.ProgramLooksPath));
                await WriteSoundsToDisk(Path.Combine(project.BasePath, StorageConstants.ProgramSoundsPath));
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
                    var inputStream = await loader.OpenResourceStreamAsync(ResourceScope.Ide,
                        Path.Combine(ResourcePathToScreenshot, AutomatedScreenshotFilename));

                    var outputStream = await storage.OpenFileAsync(
                        Path.Combine(basePathToScreenshotFiles, AutomatedScreenshotFilename),
                        StorageFileMode.Create, StorageFileAccess.Write);

                    inputStream.CopyTo(outputStream);
                    outputStream.Flush();
                    inputStream.Dispose();
                    outputStream.Dispose();

                    var inputStream2 =
                        await loader.OpenResourceStreamAsync(ResourceScope.Ide,
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
                        var inputStream = await loader.OpenResourceStreamAsync(ResourceScope.Ide, // TODO: change resourceScope to suppot phone and store app  (after 8.1 upgrade)
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
                        var inputStream = await loader.OpenResourceStreamAsync(ResourceScope.Ide, // TODO: change resourceScope to suppot phone and store app (after 8.1 upgrade)
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


        private static void FillSprites(Program project)
        {
            var objectBackground = new Sprite { Name = AppResources.DefaultProgram_Background }; 
            var objectMole1 = new Sprite { Name = AppResources.WhackAMole_Mole + " 1" };
            var objectMole2 = new Sprite { Name = AppResources.WhackAMole_Mole + " 2" };
            var objectMole3 = new Sprite { Name = AppResources.WhackAMole_Mole + " 3" };
            var objectMole4 = new Sprite { Name = AppResources.WhackAMole_Mole + " 4" };

            #region Background

            objectBackground.Looks.Add(new Look { Name = AppResources.DefaultProgram_Background, FileName = LookFileNameBackground });
            objectBackground.Looks.Add(new Look { Name = AppResources.DefaultProgram_Background + "End", FileName = LookFileNameBackgroundFinished });

            StartScript startScriptBackground = new StartScript();
            {
                SetLookBrick look = new SetLookBrick();
                look.Value = objectBackground.Looks[0];
                startScriptBackground.Bricks.Add(look);
            }
            {
                SetSizeBrick sizeBack = new SetSizeBrick();
                sizeBack.Percentage = FormulaTreeFactory.CreateNumberNode(75);
                startScriptBackground.Bricks.Add(sizeBack);
            }
            objectBackground.Scripts.Add(startScriptBackground);

            #endregion

            #region mole1

            objectMole1.Looks.Add(new Look { Name = AppResources.WhackAMole_MovingMole, FileName = LookFileNameMole1 });
            objectMole1.Looks.Add(new Look { Name = AppResources.WhackAMole_DizzyMole, FileName = LookFileNameMole2 });
            objectMole1.Looks.Add(new Look { Name = AppResources.WhackAMole_Mole, FileName = LookFileNameMole3 });

            StartScript startScriptMole1 = new StartScript();
            {
                SetSizeBrick sizeBrick = new SetSizeBrick();
                sizeBrick.Percentage = FormulaTreeFactory.CreateNumberNode(20);
                startScriptMole1.Bricks.Add(sizeBrick);
            }
            ForeverBrick startForeverBrick1 = new ForeverBrick();
            EndForeverBrick endForeverBrick1 = new EndForeverBrick();
            startForeverBrick1.End = endForeverBrick1;
            endForeverBrick1.Begin = startForeverBrick1;

            startScriptMole1.Bricks.Add(startForeverBrick1);  
            {
                SetPositionBrick pos = new SetPositionBrick();
                pos.ValueX = FormulaTreeFactory.CreateNumberNode(-115);
                pos.ValueY = FormulaTreeFactory.CreateNumberNode(-80);
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
                SetLookBrick look = new SetLookBrick();
                look.Value = objectMole1.Looks[0];
                startScriptMole1.Bricks.Add(look);
            }
            {
                AnimatePositionBrick slide = new AnimatePositionBrick();
                slide.Duration = FormulaTreeFactory.CreateNumberNode(0.1);
                slide.ToX = FormulaTreeFactory.CreateNumberNode(0);
                slide.ToY = FormulaTreeFactory.CreateNumberNode(10);
                startScriptMole1.Bricks.Add(slide);
            }
            {
                SetLookBrick look = new SetLookBrick();
                look.Value = objectMole1.Looks[2];
                startScriptMole1.Bricks.Add(look);
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
            startScriptMole1.Bricks.Add(endForeverBrick1); 
            objectMole1.Scripts.Add(startScriptMole1);

            TappedScript tappedScriptMole1 = new TappedScript();
            {
                SetLookBrick look = new SetLookBrick();
                look.Value = objectMole1.Looks[1];
                tappedScriptMole1.Bricks.Add(look);
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

            objectMole2.Looks.Add(new Look { Name = AppResources.WhackAMole_MovingMole, FileName = LookFileNameMole1 });
            objectMole2.Looks.Add(new Look { Name = AppResources.WhackAMole_DizzyMole, FileName = LookFileNameMole2 });
            objectMole2.Looks.Add(new Look { Name = AppResources.WhackAMole_Mole, FileName = LookFileNameMole3 });

            StartScript startScriptMole2 = new StartScript();
            {
                SetSizeBrick sizeBrick = new SetSizeBrick();
                sizeBrick.Percentage = FormulaTreeFactory.CreateNumberNode(20);
                startScriptMole2.Bricks.Add(sizeBrick);
            }
            ForeverBrick startForeverBrick2 = new ForeverBrick();
            EndForeverBrick endForeverBrick2 = new EndForeverBrick();
            startForeverBrick2.End = endForeverBrick2;
            endForeverBrick2.Begin = startForeverBrick2;

            startScriptMole2.Bricks.Add(startForeverBrick2);
            {
                SetPositionBrick pos = new SetPositionBrick();
                pos.ValueX = FormulaTreeFactory.CreateNumberNode(115);
                pos.ValueY = FormulaTreeFactory.CreateNumberNode(-80);
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
                SetLookBrick look = new SetLookBrick();
                look.Value = objectMole2.Looks[0];
                startScriptMole2.Bricks.Add(look);
            }
            {
                AnimatePositionBrick slide = new AnimatePositionBrick();
                slide.Duration = FormulaTreeFactory.CreateNumberNode(0.1);
                slide.ToX = FormulaTreeFactory.CreateNumberNode(0);
                slide.ToY = FormulaTreeFactory.CreateNumberNode(10);
                startScriptMole2.Bricks.Add(slide);
            }
            {
                SetLookBrick look = new SetLookBrick();
                look.Value = objectMole2.Looks[2];
                startScriptMole2.Bricks.Add(look);
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
            startScriptMole2.Bricks.Add(endForeverBrick2);
            objectMole2.Scripts.Add(startScriptMole2);

            TappedScript tappedScriptMole2 = new TappedScript();
            {
                SetLookBrick look = new SetLookBrick();
                look.Value = objectMole2.Looks[1];
                tappedScriptMole2.Bricks.Add(look);
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

            objectMole3.Looks.Add(new Look { Name = AppResources.WhackAMole_MovingMole, FileName = LookFileNameMole1 });
            objectMole3.Looks.Add(new Look { Name = AppResources.WhackAMole_DizzyMole, FileName = LookFileNameMole2 });
            objectMole3.Looks.Add(new Look { Name = AppResources.WhackAMole_Mole, FileName = LookFileNameMole3 });

            StartScript startScriptMole3 = new StartScript();
            {
                SetSizeBrick sizeBrick = new SetSizeBrick();
                sizeBrick.Percentage = FormulaTreeFactory.CreateNumberNode(20);
                startScriptMole3.Bricks.Add(sizeBrick);
            }
            ForeverBrick startForeverBrick3 = new ForeverBrick();
            EndForeverBrick endForeverBrick3 = new EndForeverBrick();
            startForeverBrick3.End = endForeverBrick3;
            endForeverBrick3.Begin = startForeverBrick3;

            startScriptMole3.Bricks.Add(startForeverBrick3);
            {
                SetPositionBrick pos = new SetPositionBrick();
                pos.ValueX = FormulaTreeFactory.CreateNumberNode(-115);
                pos.ValueY = FormulaTreeFactory.CreateNumberNode(-215);
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
                SetLookBrick look = new SetLookBrick();
                look.Value = objectMole3.Looks[0];
                startScriptMole3.Bricks.Add(look);
            }
            {
                AnimatePositionBrick slide = new AnimatePositionBrick();
                slide.Duration = FormulaTreeFactory.CreateNumberNode(0.1);
                slide.ToX = FormulaTreeFactory.CreateNumberNode(0);
                slide.ToY = FormulaTreeFactory.CreateNumberNode(10);
                startScriptMole3.Bricks.Add(slide);
            }
            {
                SetLookBrick look = new SetLookBrick();
                look.Value = objectMole3.Looks[2];
                startScriptMole3.Bricks.Add(look);
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
            startScriptMole3.Bricks.Add(endForeverBrick3);
            objectMole3.Scripts.Add(startScriptMole3);

            TappedScript tappedScriptMole3 = new TappedScript();
            {
                SetLookBrick look = new SetLookBrick();
                look.Value = objectMole3.Looks[1];
                tappedScriptMole3.Bricks.Add(look);
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

            objectMole4.Looks.Add(new Look { Name = AppResources.WhackAMole_MovingMole, FileName = LookFileNameMole1 });
            objectMole4.Looks.Add(new Look { Name = AppResources.WhackAMole_DizzyMole, FileName = LookFileNameMole2 });
            objectMole4.Looks.Add(new Look { Name = AppResources.WhackAMole_Mole, FileName = LookFileNameMole3 });

            StartScript startScriptMole4 = new StartScript();
            {
                SetSizeBrick sizeBrick = new SetSizeBrick();
                sizeBrick.Percentage = FormulaTreeFactory.CreateNumberNode(20);
                startScriptMole4.Bricks.Add(sizeBrick);
            }
            ForeverBrick startForeverBrick4 = new ForeverBrick();
            EndForeverBrick endForeverBrick4 = new EndForeverBrick();
            startForeverBrick4.End = endForeverBrick4;
            endForeverBrick4.Begin = startForeverBrick4;

            startScriptMole4.Bricks.Add(startForeverBrick4);
            {
                SetPositionBrick pos = new SetPositionBrick();
                pos.ValueX = FormulaTreeFactory.CreateNumberNode(115);
                pos.ValueY = FormulaTreeFactory.CreateNumberNode(-215);
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
                SetLookBrick look = new SetLookBrick();
                look.Value = objectMole4.Looks[0];
                startScriptMole4.Bricks.Add(look);
            }
            {
                AnimatePositionBrick slide = new AnimatePositionBrick();
                slide.Duration = FormulaTreeFactory.CreateNumberNode(0.1);
                slide.ToX = FormulaTreeFactory.CreateNumberNode(0);
                slide.ToY = FormulaTreeFactory.CreateNumberNode(10);
                startScriptMole4.Bricks.Add(slide);
            }
            {
                SetLookBrick look = new SetLookBrick();
                look.Value = objectMole4.Looks[2];
                startScriptMole4.Bricks.Add(look);
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
            startScriptMole4.Bricks.Add(startForeverBrick4);
            objectMole4.Scripts.Add(startScriptMole4);

            TappedScript tappedScriptMole4 = new TappedScript();
            {
                SetLookBrick look = new SetLookBrick();
                look.Value = objectMole4.Looks[1];
                tappedScriptMole4.Bricks.Add(look);
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


        public string GetProgramDefaultName()
        {
            return AppResources.Main_WhackAMoleName;
        }

        public int GetOrderId()
        {
            return 1;
        }
    }
}
