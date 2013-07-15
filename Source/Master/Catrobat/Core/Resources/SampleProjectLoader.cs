using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Catrobat.Core.Objects;
using Catrobat.Core.Storage;
using Catrobat.Core.ZIP;

namespace Catrobat.Core.Resources
{
    public class SampleProjectLoader
    {
        private readonly Dictionary<string, string> _sampleProjectNames = new Dictionary<string, string>
        {
            //{"Piano.catrobat", "Piano"},
            //{"Pacman.catrobat", "Pacman"},
            //{"HAL9000.catrobat", "HAL 9000"},
            //{"Aquarium_v2.catrobat", "Aquarium v2"},
            //{"Fishy.catroid", "Fishy"},
            //{"Egg_timer.catrobat", "Egg Timer"}
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

                    try
                    {
                        var projectFolderPath = Path.Combine(CatrobatContext.ProjectsPath, projectName);

                        using (var storage = StorageSystem.GetStorage())
                        {
                            if (!storage.DirectoryExists(projectFolderPath))
                            {
                                CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(resourceStream, CatrobatContext.ProjectsPath + "/" + projectName);
                            }
                        }

                        using (var storage = StorageSystem.GetStorage())
                        {
                            var textFilePath = Path.Combine(CatrobatContext.ProjectsPath, projectName, Project.ProjectCodePath);
                            var xml = storage.ReadTextFile(textFilePath);

                            var project = new Project(xml);
                            project.SetSetProgramName(projectName);

                            project.Save();
                        }
                    }
                    catch (Exception) // TODO: Implement catch exception body
                    {}
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