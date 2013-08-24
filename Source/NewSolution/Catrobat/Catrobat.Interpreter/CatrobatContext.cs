using System;
using System.Diagnostics;
using System.IO;
using Catrobat.Interpreter.Misc.Helpers;
using Catrobat.Interpreter.Objects;
using Catrobat.Interpreter.Storage;
using Catrobat.Interpreter.ZIP;

namespace Catrobat.Interpreter
{
    public sealed class CatrobatContext : CatrobatContextBase
    {
        #region Static methods

        public static Project LoadNewProjectByNameStatic(string projectName)
        {
            if (false) // Debugger.IsAttached)
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
                //project.Save();
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