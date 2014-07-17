using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Xml.Converter;
using Catrobat.IDE.Core.Xml.XmlObjects;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Catrobat.IDE.Core
{
    public sealed class CatrobatContext : CatrobatContextBase
    {
        #region Static methods

        public static async Task<Project> LoadProjectByNameStatic(string projectName)
        {
            return new XmlProjectConverter().Convert(await LoadXmlProjectByNameStatic(projectName));
        }

        public static async Task<XmlProject> LoadXmlProjectByNameStatic(string projectName)
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

        private static async Task<XmlProject> LoadNewProjectByNameStaticWithoutTryCatch(string projectName)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                var tempPath = Path.Combine(ProjectsPath, projectName, Project.ProjectCodePath);
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

        public static async Task<Project> RestoreDefaultProjectStatic(string projectName)
        {
            IProjectGenerator projectGenerator = new ProjectGeneratorWhackAMole(); 

            return await projectGenerator.GenerateProject(AppResources.Main_DefaultProjectName ,true);
        }

        public static async Task<Project> CreateEmptyProject(string newProjectName)
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
                var destinationPath = Path.Combine(ProjectsPath, newProjectName);

                var counter = 1;
                while (await storage.DirectoryExistsAsync(destinationPath))
                {
                    newProjectName = newProjectName + counter;
                    destinationPath = Path.Combine(ProjectsPath, newProjectName);
                    counter++;
                }
            }
            await newProject.Save();

            return newProject;
        }

        public static async Task<Project> CopyProject(string sourceProjectName, string newProjectName)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                var sourcePath = Path.Combine(ProjectsPath, sourceProjectName);
                var destinationPath = Path.Combine(ProjectsPath, newProjectName);

                var counter = 1;
                while (await storage.DirectoryExistsAsync(destinationPath))
                {
                    newProjectName = newProjectName + counter;
                    destinationPath = Path.Combine(ProjectsPath, newProjectName);
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

        public static async Task StoreLocalSettingsStatic(LocalSettings localSettings)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                await storage.WriteSerializableObjectAsync(LocalSettingsFilePath, localSettings);
            }
        }

        public static async Task<LocalSettings> RestoreLocalSettingsStatic()
        {
            try
            {
                LocalSettings localSettings = null;

                using (var storage = StorageSystem.GetStorage())
                {
                    if (await storage.FileExistsAsync(LocalSettingsFilePath))
                    {
                        localSettings = await storage.ReadSerializableObjectAsync(
                            LocalSettingsFilePath, typeof(LocalSettings)) as LocalSettings;
                    }

                    return localSettings;
                }
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}