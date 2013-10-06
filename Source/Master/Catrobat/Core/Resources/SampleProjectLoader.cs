using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Catrobat.Core.Misc;
using Catrobat.Core.Objects;
using Catrobat.Core.Services.Common;
using Catrobat.Core.Storage;

namespace Catrobat.Core.Resources
{
    public class SampleProjectLoader
    {
        private readonly Dictionary<string, string> _sampleProjectNames = new Dictionary<string, string>
        {
            {"Whack_A_Mole.catrobat", "Whack A Mole"}
        };

        public void LoadSampleProjects()
        {
            using (var storage = StorageSystem.GetStorage())
            {
                storage.DeleteDirectory("");
            }

            foreach (KeyValuePair<string, string> pair in _sampleProjectNames)
            {
                var projectFileName = pair.Key;
                var projectName = pair.Value;
                var path = string.Format("SampleProjects/{0}", projectFileName);

                Stream resourceStream = null;

                try
                {
                    var resourceLoader = ResourceLoader.CreateResourceLoader();
                    resourceStream = resourceLoader.OpenResourceStream(ResourceScope.Resources, path);

                    if (resourceStream != null)
                    {
                        var projectFolderPath = Path.Combine(CatrobatContextBase.ProjectsPath, projectName);

                        using (var storage = StorageSystem.GetStorage())
                        {
                            if (!storage.DirectoryExists(projectFolderPath))
                            {
                                CatrobatZipService.UnzipCatrobatPackageIntoIsolatedStorage(resourceStream, CatrobatContextBase.ProjectsPath + "/" + projectName);
                            }
                        }

                        using (var storage = StorageSystem.GetStorage())
                        {
                            var textFilePath = Path.Combine(CatrobatContextBase.ProjectsPath, projectName, Project.ProjectCodePath);
                            var xml = storage.ReadTextFile(textFilePath);

                            var project = new Project(xml);
                            project.SetProgramName(projectName);

                            project.Save();
                        }
                    }
                }
                catch (Exception)
                {
                    Debugger.Break(); // sample project does not exist: please remove from _sampleProjectNames or add to Core/Resources/SampleProjects
                }
                finally
                {
                    if (resourceStream != null)
                    {
                        resourceStream.Dispose();
                    }
                }
            }
        }
    }
}