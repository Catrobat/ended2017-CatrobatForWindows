using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using Catrobat.Core.Annotations;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Catrobat.Core.Objects.Costumes;
using Catrobat.Core.Objects.Scripts;
using Catrobat.Core.Objects.Sounds;
using Catrobat.Core.Objects.Variables;
using Catrobat.Core.Resources;
using Catrobat.Core.Storage;
using Catrobat.Core.ZIP;

namespace Catrobat.Core
{
    public delegate void ContextSaving();

    public sealed class CatrobatContext : CatrobatContextBase
    {
        public  CatrobatContext()
        {

        }

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
    }
}