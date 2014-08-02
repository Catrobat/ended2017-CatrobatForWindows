using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Xml;
using Catrobat.IDE.Core.Xml.Converter;
using Catrobat.IDE.Core.Xml.XmlObjects;

namespace Catrobat.IDE.Core.Services.Common
{
    public class ContextService : IContextService
    {
        public async Task<string> FindUniqueProgramName(string programName)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                var counter = 0;
                while (true)
                {
                    var programPath = Path.Combine(StorageConstants.ProgramsPath,
                        programName);

                    if (counter != 0)
                    {
                        programPath = Path.Combine(StorageConstants.ProgramsPath,
                            programName + counter);
                    }

                    if (!await storage.DirectoryExistsAsync(programPath))
                        break;

                    counter++;
                }

                var programNameUnique = programName;

                if (counter != 0)
                    programNameUnique = programName + counter;

                return programNameUnique;
            }
        }

        public async Task<XmlProjectRenamerResult> RenameProgram(
         string programCodeFilePath, string newProgramName)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                var projectCode = await storage.ReadTextFileAsync(programCodeFilePath);
                var result = XmlProgramHelper.RenameProgram(projectCode, newProgramName);

                await storage.WriteTextFileAsync(
                    programCodeFilePath, result.NewProjectCode);

                result.NewProjectName = newProgramName;

                return result;
            }
        }

        public async Task<Program> LoadProgramByName(string programName)
        {
            return new XmlProjectConverter().Convert(await LoadXmlProgramByName(programName));
        }

        public async Task<XmlProgram> LoadXmlProgramByName(string programName)
        {
            if (Debugger.IsAttached)
            {
                return await LoadNewProgramByNameWithoutTryCatch(programName);
            }
            else
            {
                try
                {
                    return await LoadNewProgramByNameWithoutTryCatch(programName);
                }
                catch
                {
                    if (Debugger.IsAttached)
                        Debugger.Break();

                    return null;
                }
            }
        }

        private static async Task<XmlProgram> LoadNewProgramByNameWithoutTryCatch(
            string programName)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                var tempPath = Path.Combine(StorageConstants.ProgramsPath, programName, StorageConstants.ProgramCodePath);
                var xml = await storage.ReadTextFileAsync(tempPath);

                var programVersion = XmlProgramHelper.GetProgramVersion(xml);
                if (programVersion != Constants.TargetIDEVersion)
                {
                    // this should happen when the app version was outdated when the program was added
                    // TODO: implement me
                }

                return new XmlProgram(xml);
            }
        }

        public async Task<Program> RestoreDefaultProgram(string programName)
        {
            IProgramGenerator projectGenerator = new ProgramGeneratorWhackAMole();

            return await projectGenerator.GenerateProject(AppResources.Main_DefaultProgramName, true);
        }

        public async Task<Program> CreateEmptyProgram(string newProgramName)
        {
            // TODO: move this code int a ProjectGenerator see ProjectGeneratorWhackAMole

            var newProject = new Program
            {
                Name = newProgramName,
                UploadHeader = new UploadHeader
                {
                    MediaLicense = "http://developer.catrobat.org/ccbysa_v3",
                    ProgramLicense = "http://developer.catrobat.org/agpl_v3",
                    Url = "http://pocketcode.org/details/871"
                }
            };

            using (var storage = StorageSystem.GetStorage())
            {
                var destinationPath = Path.Combine(StorageConstants.ProgramsPath, newProgramName);

                var counter = 1;
                while (await storage.DirectoryExistsAsync(destinationPath))
                {
                    newProgramName = newProgramName + counter;
                    destinationPath = Path.Combine(StorageConstants.ProgramsPath, newProgramName);
                    counter++;
                }
            }
            await newProject.Save();

            return newProject;
        }

        public async Task<Program> CopyProgram(string sourceProgramName, 
            string newProgramName)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                var sourcePath = Path.Combine(StorageConstants.ProgramsPath, sourceProgramName);
                var destinationPath = Path.Combine(StorageConstants.ProgramsPath, newProgramName);

                var counter = 1;
                while (await storage.DirectoryExistsAsync(destinationPath))
                {
                    newProgramName = newProgramName + counter;
                    destinationPath = Path.Combine(StorageConstants.ProgramsPath, newProgramName);
                    counter++;
                }

                await storage.CopyDirectoryAsync(sourcePath, destinationPath);

                var tempXmlPath = Path.Combine(destinationPath, StorageConstants.ProgramCodePath);
                var xml = await storage.ReadTextFileAsync(tempXmlPath);
                var newProject = new XmlProgram(xml);
                newProject.ProjectHeader.ProgramName = newProgramName;
                await newProject.Save();

                return new XmlProjectConverter().Convert(newProject);
            }
        }

        public async Task StoreLocalSettings(LocalSettings localSettings)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                await storage.WriteSerializableObjectAsync(
                    StorageConstants.LocalSettingsFilePath, localSettings);
            }
        }

        public async Task<LocalSettings> RestoreLocalSettings()
        {
            try
            {
                LocalSettings localSettings = null;

                using (var storage = StorageSystem.GetStorage())
                {
                    if (await storage.FileExistsAsync(StorageConstants.LocalSettingsFilePath))
                    {
                        localSettings = await storage.ReadSerializableObjectAsync(
                            StorageConstants.LocalSettingsFilePath, typeof(LocalSettings)) as LocalSettings;
                    }

                    return localSettings;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task CreateThumbnailsForNewProgram(string programName)
        {
            var programPath = Path.Combine(StorageConstants.ProgramsPath, programName);
            var pathToLooks = Path.Combine(programPath, StorageConstants.ProgramLooksPath);

            using (var storage = StorageSystem.GetStorage())
            {
                await storage.TryCreateThumbnailAsync(Path.Combine(programPath, 
                    StorageConstants.ProgramManualScreenshotPath));

                await storage.TryCreateThumbnailAsync(Path.Combine(programPath,
                    StorageConstants.ProgramAutomaticScreenshotPath));

                var fileNames = await storage.GetFileNamesAsync(pathToLooks);

                foreach (var fileName in fileNames)
                {
                    await storage.TryCreateThumbnailAsync(
                        Path.Combine(pathToLooks, fileName));
                }
            }
        }
    }
}
