using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Xml;
using Catrobat.IDE.Core.Xml.XmlObjects;
using System.Text.RegularExpressions;
using Catrobat.IDE.Core.XmlModelConvertion.Converters;
using Catrobat.Core.Resources.Localization;

namespace Catrobat.IDE.Core.Services.Common
{
    public class ContextService : IContextService
    {
        public async Task<string> ConvertToValidFileName(string fileName)
        {
            fileName = Regex.Replace(fileName, @"(\s)\s+", "$1").Trim();
            string invalidFileNameChars = new string(Path.GetInvalidFileNameChars());
            fileName = Regex.Replace(fileName, @"[" + Regex.Escape(invalidFileNameChars) + "]", "").Trim();
            fileName = Regex.Replace(fileName, @"( *(\.)+)*$", "").Trim();
            //fileName = Regex.Replace(fileName, @"[^A-Za-z0-9_-]", "");

            if(fileName.Length == 0)
            {
                fileName = "DEFAULT";
            }
            return fileName;
        }

        public async Task<string> FindUniqueName(string requestedName, List<string> nameList)
        {
            int counter = 0;
            string uniqueName = "";
            string currentName = requestedName;
            while (counter <= nameList.Count)
            {
                if (counter != 0)
                {
                    currentName = requestedName + counter;
                }

                var match = nameList.FirstOrDefault(stringToCheck => stringToCheck.Equals(currentName));
                if (match == null)
                    break;

                counter++;
            }
            uniqueName = requestedName;

            if (counter != 0)
                uniqueName = requestedName + counter;

            return uniqueName.Trim();
        }

        public async Task<string> FindUniqueProgramName(string programName)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                int counter = 0;
                while (true)
                {
                    string programPath = Path.Combine(StorageConstants.ProgramsPath,
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
                string programNameUnique = programName;

                if (counter != 0)
                    programNameUnique = programName + counter;

                if(programName != programNameUnique)
                {
                    ServiceLocator.NotifictionService.ShowToastNotification("",
                    AppResources.Main_ProgramNameDuplicate, ToastDisplayDuration.Short, ToastTag.Default);
                }

                return programNameUnique.Trim();
            }
        }

        public async Task<XmlProgramRenamerResult> RenameProgram(
         string programCodeFilePath, string newProgramName)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                var projectCode = await storage.ReadTextFileAsync(programCodeFilePath);
                var result = XmlProgramHelper.RenameProgram(projectCode, newProgramName);

                await storage.WriteTextFileAsync(
                    programCodeFilePath, result.NewProgramCode);

                result.NewProgramName = newProgramName;

                return result;
            }
        }

        public async Task<Program> LoadProgramByName(string programName)
        {
            Program program = null;
            try
            {
                ProgramConverter programConverter = new ProgramConverter();
                program = programConverter.Convert(await LoadXmlProgramByName(programName));
            }
            catch (Exception)
            {
                if (Debugger.IsAttached)
                    Debugger.Break();
            }
            return program;
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
                if (programVersion != XmlConstants.TargetIDEVersion)
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

            return await projectGenerator.GenerateProgram(AppResources.Main_DefaultProgramName, true);
        }

        public async Task<Program> CreateEmptyProgram(string newProgramName)
        {
            // TODO: move this code into a ProjectGenerator see ProjectGeneratorWhackAMole

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

        public async Task<string> CopyProgramPart1(string sourceProgramName)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                var newProgramName = sourceProgramName;
                var sourcePath = Path.Combine(StorageConstants.ProgramsPath, sourceProgramName);
                var destinationPath = Path.Combine(StorageConstants.ProgramsPath, newProgramName);

                var counter = 1;
                while (await storage.DirectoryExistsAsync(destinationPath))
                {
                    newProgramName = newProgramName + counter;
                    destinationPath = Path.Combine(StorageConstants.ProgramsPath, newProgramName);
                    counter++;
                }

                var sourceScreenshotPathManual = Path.Combine(sourcePath, StorageConstants.ProgramManualScreenshotPath);
                var destinationScreenshotPathManual = Path.Combine(destinationPath, StorageConstants.ProgramManualScreenshotPath);
                await storage.CopyFileAsync(sourceScreenshotPathManual, destinationScreenshotPathManual);

                var sourceScreenshotPathAutomated = Path.Combine(sourcePath, StorageConstants.ProgramAutomaticScreenshotPath);
                var destinationScreenshotPathAutomated = Path.Combine(destinationPath, StorageConstants.ProgramAutomaticScreenshotPath);
                await storage.CopyFileAsync(sourceScreenshotPathAutomated, destinationScreenshotPathAutomated);

                return newProgramName;
            }
        }

        public async Task<Program> CopyProgramPart2(string sourceProgramName,
            string newProgramName)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                var sourcePath = Path.Combine(StorageConstants.ProgramsPath, sourceProgramName);
                var destinationPath = Path.Combine(StorageConstants.ProgramsPath, newProgramName);

                await storage.CopyDirectoryAsync(sourcePath, destinationPath);

                var tempXmlPath = Path.Combine(destinationPath, StorageConstants.ProgramCodePath);
                var xml = await storage.ReadTextFileAsync(tempXmlPath);
                var xmlProgram = new XmlProgram(xml)
                {
                    ProgramHeader = { ProgramName = newProgramName }
                };

                var path = Path.Combine(StorageConstants.ProgramsPath,
                    newProgramName, StorageConstants.ProgramCodePath);
                var programConverter = new ProgramConverter();
                var program = programConverter.Convert(xmlProgram);

                var xmlString = xmlProgram.ToXmlString();
                await storage.WriteTextFileAsync(path, xmlString);

                return program;
            }
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
                var xmlProgram = new XmlProgram(xml)
                {
                    ProgramHeader = {ProgramName = newProgramName}
                };

                var path = Path.Combine(StorageConstants.ProgramsPath, 
                    newProgramName, StorageConstants.ProgramCodePath);
                var programConverter = new ProgramConverter();
                var program = programConverter.Convert(xmlProgram);

                var xmlString = xmlProgram.ToXmlString();
                await storage.WriteTextFileAsync(path, xmlString);
                
                return program;
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

            //var programPath = Path.Combine(StorageConstants.ProgramsPath, programName);
            //var pathToLooks = Path.Combine(programPath, StorageConstants.ProgramLooksPath);

            //try
            //{

            //    using (var storage = StorageSystem.GetStorage())
            //    {
            //        await storage.TryCreateThumbnailAsync(Path.Combine(programPath,
            //            StorageConstants.ProgramManualScreenshotPath));

            //        await storage.TryCreateThumbnailAsync(Path.Combine(programPath,
            //            StorageConstants.ProgramAutomaticScreenshotPath));

            //        if (await storage.DirectoryExistsAsync(pathToLooks))
            //        {
            //            var fileNames = await storage.GetFileNamesAsync(pathToLooks);

            //            foreach (var fileName in fileNames)
            //            {
            //                await storage.TryCreateThumbnailAsync(
            //                    Path.Combine(pathToLooks, fileName));
            //            }  
            //        }
            //    }
            //}
            //catch
            //{
            //    if(Debugger.IsAttached)
            //        Debugger.Break();
            //}
        }

        public void UpdateProgramHeader(XmlProgram program)
        {
            program.ProgramHeader.ApplicationBuildName = ServiceLocator.
                SystemInformationService.CurrentApplicationBuildName;

            program.ProgramHeader.ApplicationBuildNumber = ServiceLocator.
                SystemInformationService.CurrentApplicationBulidNumber;

            program.ProgramHeader.ApplicationName = XmlConstants.ApplicationName;

            program.ProgramHeader.ApplicationVersion = ServiceLocator.
                SystemInformationService.CurrentApplicationVersion;

            program.ProgramHeader.CatrobatLanguageVersion = XmlConstants.TargetIDEVersion;

            program.ProgramHeader.DeviceName = ServiceLocator.
                SystemInformationService.DeviceName;

            program.ProgramHeader.Platform = ServiceLocator.
                SystemInformationService.PlatformName;

            program.ProgramHeader.PlatformVersion = ServiceLocator.
                SystemInformationService.PlatformVersion; ;
            
            // TODO: check if and how the following properties should be set
            //program.ProjectHeader.DateTimeUpload = "";
            //program.ProjectHeader.ProgramLicense = "";
            //program.ProjectHeader.MediaLicense = "";
            //program.ProjectHeader.ScreenHeight = "";
            //program.ProjectHeader.ScreenWidth = "";
            //program.ProjectHeader.Tags = "";
        }
    }
}
