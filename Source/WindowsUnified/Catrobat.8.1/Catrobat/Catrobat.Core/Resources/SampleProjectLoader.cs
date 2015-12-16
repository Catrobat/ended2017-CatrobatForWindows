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
using Catrobat.IDE.Core.XmlModelConvertion.Converters;

namespace Catrobat.IDE.Core.Resources
{
    public class SampleProjectLoader
    {
        private readonly Dictionary<string, string> _sampleProjectNames = new Dictionary<string, string>
        {
            //{"stick.catrobat", "stick"}
        };

        public async Task LoadSampleProgram()
        {
            using (var storage = StorageSystem.GetStorage())
            {
                await storage.DeleteDirectoryAsync("");
            }

            foreach (KeyValuePair<string, string> pair in _sampleProjectNames)
            {
                var projectFileName = pair.Key;
                var projectName = pair.Value;
                var resourcePath = string.Format("SamplePrograms/{0}", projectFileName);

                Stream resourceStream = null;

                try
                {
                    var resourceLoader = ServiceLocator.ResourceLoaderFactory.CreateResourceLoader();
                    resourceStream = await resourceLoader.OpenResourceStreamAsync(ResourceScope.Resources, resourcePath);
                    

                    if (resourceStream != null)
                    {
                        var projectFolderPath = Path.Combine(StorageConstants.ProgramsPath, projectName);

                        using (var storage = StorageSystem.GetStorage())
                        {
                            if (!await storage.DirectoryExistsAsync(projectFolderPath))
                            {
                                await ServiceLocator.ZipService.UnzipCatrobatPackageIntoIsolatedStorage(resourceStream, StorageConstants.ProgramsPath + "/" + projectName);
                            }
                        }

                        using (var storage = StorageSystem.GetStorage())
                        {
                            var textFilePath = Path.Combine(StorageConstants.ProgramsPath, projectName, StorageConstants.ProgramCodePath);
                            var xml = await storage.ReadTextFileAsync(textFilePath);

                            var xmlProgram = new XmlProgram(xml)
                            {
                                ProgramHeader = {ProgramName = projectName}
                            };


                            var path = Path.Combine(StorageConstants.ProgramsPath,
                                projectFileName, StorageConstants.ProgramCodePath);
                            var xmlString = xmlProgram.ToXmlString();
                            await storage.WriteTextFileAsync(path, xmlString);
                        }
                    }
                }
                catch (Exception)
                {
                    Debugger.Break(); // sample project does not exist: please remove from _sampleProjectNames or add to Core/Resources/SamplePrograms
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