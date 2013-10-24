using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Utilities;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;

namespace Catrobat.IDE.Core
{
    public sealed class CatrobatContext : CatrobatContextBase
    {
        #region Static methods

        public static Project LoadNewProjectByNameStatic(string projectName)
        {
            if (Debugger.IsAttached)
            {
                return LoadNewProjectByNameStaticWithoutTryCatch(projectName);
            }
            else
            {
                try
                {
                    return LoadNewProjectByNameStaticWithoutTryCatch(projectName);
                }
                catch
                {
                    return null;
                }
            }
        }

        private static Project LoadNewProjectByNameStaticWithoutTryCatch(string projectName)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                var tempPath = Path.Combine(ProjectsPath, projectName, Project.ProjectCodePath);
                var xml = storage.ReadTextFile(tempPath);
                var newProject = new Project(xml);
                newProject.SetProgramName(projectName);
                return newProject;
            }
        }

        public static async Task<Project> RestoreDefaultProjectStatic(string projectName)
        {
            IProjectGenerator projectGenerator = new ProjectGeneratorDefault();

            return await projectGenerator.GenerateProject(ServiceLocator.CulureService.GetCulture().TwoLetterISOLanguageName, true);
        }

        public static async Task<Project> CreateEmptyProject(string newProjectName)
        {
            var newProject = new Project();

            using (var storage = StorageSystem.GetStorage())
            {
                var destinationPath = Path.Combine(CatrobatContextBase.ProjectsPath, newProjectName);

                var counter = 1;
                while (storage.DirectoryExists(destinationPath))
                {
                    newProjectName = newProjectName + counter;
                    destinationPath = Path.Combine(CatrobatContextBase.ProjectsPath, newProjectName);
                    counter++;
                }
            }

            newProject.ProjectHeader = new ProjectHeader(true);
            newProject.SpriteList = new SpriteList();
            newProject.VariableList.ObjectVariableList = new ObjectVariableList();
            newProject.VariableList.ProgramVariableList = new ProgramVariableList();
            newProject.SetProgramName(newProjectName);
            await newProject.Save();

            return newProject;
        }

        public static async Task<Project> CopyProject(string sourceProjectName, string newProjectName)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                var sourcePath = Path.Combine(CatrobatContextBase.ProjectsPath, sourceProjectName);
                var destinationPath = Path.Combine(CatrobatContextBase.ProjectsPath, newProjectName);

                var counter = 1;
                while (storage.DirectoryExists(destinationPath))
                {
                    newProjectName = newProjectName + counter;
                    destinationPath = Path.Combine(CatrobatContextBase.ProjectsPath, newProjectName);
                    counter++;
                }

                storage.CopyDirectory(sourcePath, destinationPath);

                var tempXmlPath = Path.Combine(destinationPath, Project.ProjectCodePath);
                var xml = storage.ReadTextFile(tempXmlPath);
                var newProject = new Project(xml);
                newProject.SetProgramName(newProjectName);
                await newProject.Save();

                return newProject;
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
                        localSettings = await storage.ReadSerializableObjectAsync(LocalSettingsFilePath, typeof(LocalSettings)) as LocalSettings;
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