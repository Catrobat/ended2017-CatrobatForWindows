using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Tests.Misc.Storage;
using Catrobat.IDE.Core.Xml.XmlObjects;

namespace Catrobat.IDE.Core.Tests.SampleData
{
    public static class SampleLoader
    {
        private static readonly string path = BasePathHelper.GetSampleProjectsPath();

        public static XDocument LoadSampleXDocument(string sampleName)
        {
            string xml = null;
            using (var resourceLoader = ServiceLocator.ResourceLoaderFactory.CreateResourceLoader())
            {
                var stream = resourceLoader.OpenResourceStream(ResourceScope.TestCommon, path + sampleName + ".xml");
                var reader = new StreamReader(stream);

                xml = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();
                stream.Dispose();
            }
            return XDocument.Load(new StringReader(xml));
        }

        public static async Task<Project> LoadSampleProject(string sampleName, string sampleProjectName)
        {
            var zipService = new ZipService();

            using (var resourceLoader = ServiceLocator.ResourceLoaderFactory.CreateResourceLoader())
            {
                var stream = resourceLoader.OpenResourceStream(ResourceScope.TestCommon, path + sampleName);
                var projectPath = Path.Combine(CatrobatContextBase.ProjectsPath, sampleProjectName);
                using (var storage = StorageSystem.GetStorage())
                {
                    await storage.DeleteDirectoryAsync(projectPath);
                }

                await zipService.UnzipCatrobatPackageIntoIsolatedStorage(stream, projectPath);
                stream.Dispose();
            }
            return await CatrobatContext.LoadProjectByNameStatic(sampleProjectName);
        }

        public static async Task<XmlProject> LoadSampleXmlProject(string sampleName, string sampleProjectName)
        {
            var zipService = new ZipService();

            using (var resourceLoader = ServiceLocator.ResourceLoaderFactory.CreateResourceLoader())
            {
                var stream = resourceLoader.OpenResourceStream(ResourceScope.TestCommon, path + sampleName);
                await zipService.UnzipCatrobatPackageIntoIsolatedStorage(stream, CatrobatContextBase.ProjectsPath + "/" + sampleProjectName);
                stream.Dispose();
            }
            return await CatrobatContext.LoadXmlProjectByNameStatic(sampleProjectName);
        }
    }
}
