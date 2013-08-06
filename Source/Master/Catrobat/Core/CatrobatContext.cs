using System;
using System.IO;
using Catrobat.Core.Objects;
using Catrobat.Core.Storage;
using Catrobat.Core.ZIP;

namespace Catrobat.Core
{
    public sealed class CatrobatContext : CatrobatContextBase
    {
        #region Static methods

        public static Project CreateNewProjectByNameStatic(string projectName)
        {
            var projectCodeFile = Path.Combine(ProjectsPath, projectName);

            try
            {
                using (var storage = StorageSystem.GetStorage())
                {
                    var tempPath = Path.Combine(projectCodeFile, Project.ProjectCodePath);
                    var xml = storage.ReadTextFile(tempPath);
                    var newProject = new Project(xml);
                    return newProject;
                }
            }
            catch
            {
                throw new Exception("Project does not exist");
            }
        }

        public static Project RestoreDefaultProjectStatic(string projectName)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                var projectCodeFile = Path.Combine(ProjectsPath, projectName);

                if (!storage.FileExists(projectCodeFile))
                {
                    using (var resourceLoader = ResourceLoader.CreateResourceLoader())
                    {
                        var stream = resourceLoader.OpenResourceStream(ResourceScope.Resources, DefaultProjectPath);
                        CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(stream, projectCodeFile);
                        stream.Dispose();
                    }
                }

                var tempXmlPath = Path.Combine(projectCodeFile, Project.ProjectCodePath);
                var xml = storage.ReadTextFile(tempXmlPath);

                var project = new Project(xml);
                project.SetProgramName(projectName);
                return project;
            }
        }

        public static void StoreLocalSettingsStatic(LocalSettings localSettings)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                storage.WriteSerializableObject(LocalSettingsFilePath, localSettings);
            }
        }

        public static LocalSettings RestoreLocalSettingsStatic()
        {
            try
            {
                LocalSettings localSettings = null;

                using (var storage = StorageSystem.GetStorage())
                {
                    if (storage.FileExists(LocalSettingsFilePath))
                    {
                        localSettings = storage.ReadSerializableObject(LocalSettingsFilePath, typeof(LocalSettings)) as LocalSettings;
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