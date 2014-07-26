using System;
using System.Collections.Generic;
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
        public async Task<XmlProjectRenamerResult> RenameProjectFromFile(
         string projectCodeFilePath, string newProjectName)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                var counter = 0;
                while (true)
                {
                    var projectPath = Path.Combine(StorageConstants.ProjectsPath,
                        newProjectName);

                    if (counter != 0)
                    {
                        projectPath = Path.Combine(StorageConstants.ProjectsPath,
                            newProjectName + counter);
                    }

                    if (!await storage.DirectoryExistsAsync(projectPath))
                        break;

                    counter++;
                }

                var newProjectNameUnique = newProjectName;

                if (counter != 0)
                    newProjectNameUnique = newProjectName + counter;

                var projectCode = await storage.ReadTextFileAsync(projectCodeFilePath);
                var result = XmlProgramRenamer.RenameProjectFromCode(projectCode, newProjectNameUnique);

                await storage.WriteTextFileAsync(
                    projectCodeFilePath, result.NewProjectCode);

                result.NewProjectName = newProjectNameUnique;

                return result;
            }
        }

        public async Task<Project> LoadProjectByNameStatic(string projectName)
        {
            return new XmlProjectConverter().Convert(await LoadXmlProjectByNameStatic(projectName));
        }

        public async Task<XmlProject> LoadXmlProjectByNameStatic(string projectName)
        {
            //if (Debugger.IsAttached)
            //{
            //    return await LoadNewProjectByNameStaticWithoutTryCatch(projectName);
            //}
            //else
            //{
            try
            {
                return await LoadNewProjectByNameStaticWithoutTryCatch(projectName);
            }
            catch
            {
                return null;
            }
            //}
        }

        public async Task<XmlProject> LoadNewProjectByNameStaticWithoutTryCatch(string projectName)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                var tempPath = Path.Combine(StorageConstants.ProjectsPath, projectName, Project.ProjectCodePath);
                var xml = await storage.ReadTextFileAsync(tempPath);
                return new XmlProject(xml)
                {
                    ProjectHeader =
                    {
                        ProgramName = projectName
                    }
                };
            }
        }

        public async Task<Project> RestoreDefaultProjectStatic(string projectName)
        {
            IProgramGenerator projectGenerator = new ProjectGeneratorWhackAMole();

            return await projectGenerator.GenerateProject(AppResources.Main_DefaultProjectName, true);
        }

        public async Task<Project> CreateEmptyProject(string newProjectName)
        {
            var newProject = new Project
            {
                Name = newProjectName,
                UploadHeader = new UploadHeader
                {
                    MediaLicense = "http://developer.catrobat.org/ccbysa_v3",
                    ProgramLicense = "http://developer.catrobat.org/agpl_v3",
                    Url = "http://pocketcode.org/details/871"
                }
            };

            using (var storage = StorageSystem.GetStorage())
            {
                var destinationPath = Path.Combine(StorageConstants.ProjectsPath, newProjectName);

                var counter = 1;
                while (await storage.DirectoryExistsAsync(destinationPath))
                {
                    newProjectName = newProjectName + counter;
                    destinationPath = Path.Combine(StorageConstants.ProjectsPath, newProjectName);
                    counter++;
                }
            }
            await newProject.Save();

            return newProject;
        }

        public async Task<Project> CopyProject(string sourceProjectName, string newProjectName)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                var sourcePath = Path.Combine(StorageConstants.ProjectsPath, sourceProjectName);
                var destinationPath = Path.Combine(StorageConstants.ProjectsPath, newProjectName);

                var counter = 1;
                while (await storage.DirectoryExistsAsync(destinationPath))
                {
                    newProjectName = newProjectName + counter;
                    destinationPath = Path.Combine(StorageConstants.ProjectsPath, newProjectName);
                    counter++;
                }

                await storage.CopyDirectoryAsync(sourcePath, destinationPath);

                var tempXmlPath = Path.Combine(destinationPath, Project.ProjectCodePath);
                var xml = await storage.ReadTextFileAsync(tempXmlPath);
                var newProject = new XmlProject(xml);
                newProject.ProjectHeader.ProgramName = newProjectName;
                await newProject.Save();

                return new XmlProjectConverter().Convert(newProject);
            }
        }

        public async Task StoreLocalSettingsStatic(LocalSettings localSettings)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                await storage.WriteSerializableObjectAsync(StorageConstants.LocalSettingsFilePath, localSettings);
            }
        }

        public async Task<LocalSettings> RestoreLocalSettingsStatic()
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

    }
}
