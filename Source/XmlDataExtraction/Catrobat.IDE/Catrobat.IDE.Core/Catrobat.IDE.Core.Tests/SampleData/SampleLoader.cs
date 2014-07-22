using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Catrobat.Data.Xml.XmlObjects;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Tests.Misc.Storage;

namespace Catrobat.IDE.Core.Tests.SampleData
{
    public static class SampleLoader
    {
        private static readonly string Path = BasePathHelper.GetSampleProjectsPath();

        public static XDocument LoadSampleXDocument(string sampleName)
        {
            string xml = null;
            using (var resourceLoader = ServiceLocator.ResourceLoaderFactory.CreateResourceLoader())
            {
                var stream = resourceLoader.OpenResourceStream(ResourceScope.TestCommon, Path + sampleName + ".xml");
                var reader = new StreamReader(stream);

                xml = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();
                stream.Close();
                stream.Dispose();
            }
            return XDocument.Load(new StringReader(xml));
        }

        public static async Task<Project> LoadSampleProject(string sampleName, string sampleProjectName)
        {
            using (var resourceLoader = ServiceLocator.ResourceLoaderFactory.CreateResourceLoader())
            {
                var stream = resourceLoader.OpenResourceStream(ResourceScope.TestCommon, Path + sampleName);
                await CatrobatZipService.UnzipCatrobatPackageIntoIsolatedStorage(stream, CatrobatContextBase.ProjectsPath + "/" + sampleProjectName);
                stream.Close();
                stream.Dispose();
            }
            return await CatrobatContext.LoadProjectByNameStatic(sampleProjectName);
        }

        public static async Task<XmlProject> LoadSampleXmlProject(string sampleName, string sampleProjectName)
        {
            using (var resourceLoader = ServiceLocator.ResourceLoaderFactory.CreateResourceLoader())
            {
                var stream = resourceLoader.OpenResourceStream(ResourceScope.TestCommon, Path + sampleName);
                await CatrobatZipService.UnzipCatrobatPackageIntoIsolatedStorage(stream, CatrobatContextBase.ProjectsPath + "/" + sampleProjectName);
                stream.Close();
                stream.Dispose();
            }
            return await CatrobatContext.LoadXmlProjectByNameStatic(sampleProjectName);
        }
    }
}
