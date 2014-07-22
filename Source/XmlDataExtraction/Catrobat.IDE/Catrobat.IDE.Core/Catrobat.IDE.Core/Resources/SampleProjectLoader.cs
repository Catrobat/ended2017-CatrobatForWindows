using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.Xml.XmlObjects;

namespace Catrobat.IDE.Core.Resources
{
    public class SampleProjectLoader
    {
        private readonly Dictionary<string, string> _sampleProjectNames = new Dictionary<string, string>
        {
            //{"stick.catrobat", "stick"}
        };

        public async Task LoadSampleProjects()
        {
            using (var storage = StorageSystem.GetStorage())
            {
                await storage.DeleteDirectoryAsync("");
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
                    resourceStream = await resourceLoader.OpenResourceStreamAsync(ResourceScope.Resources, path);
                    

                    if (resourceStream != null)
                    {
                        var projectFolderPath = Path.Combine(CatrobatContextBase.ProjectsPath, projectName);

                        using (var storage = StorageSystem.GetStorage())
                        {
                            if (!await storage.DirectoryExistsAsync(projectFolderPath))
                            {
                                await CatrobatZipService.UnzipCatrobatPackageIntoIsolatedStorage(resourceStream, CatrobatContextBase.ProjectsPath + "/" + projectName);
                            }
                        }

                        using (var storage = StorageSystem.GetStorage())
                        {
                            var textFilePath = Path.Combine(CatrobatContextBase.ProjectsPath, projectName, Project.ProjectCodePath);
                            var xml = await storage.ReadTextFileAsync(textFilePath);

                            var project = new XmlProject(xml);
                            project.ProjectHeader.ProgramName = projectName;

                            await project.Save();
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