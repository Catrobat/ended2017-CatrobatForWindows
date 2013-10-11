using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Utilities;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;

namespace Catrobat.IDE.Core.Resources
{
    public class SampleProjectLoader
    {
        private readonly Dictionary<string, string> _sampleProjectNames = new Dictionary<string, string>
        {
            {"Whack_A_Mole.catrobat", "Whack A Mole"},
            {"stoeckchen.catrobat", "stoeckchen"}
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
                    var resourceLoader = ServiceLocator.ResourceLoaderFactory.CreateResourceLoader();
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