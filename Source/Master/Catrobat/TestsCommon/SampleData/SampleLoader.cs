using System;
using Catrobat.Core;
using Catrobat.Core.Utilities;
using Catrobat.Core.Utilities.Storage;
using Catrobat.Core.CatrobatObjects;
using System.IO;
using System.Xml.Linq;
using Catrobat.Core.Services;
using Catrobat.Core.Services.Common;
using Catrobat.TestsCommon.Misc.Storage;

namespace Catrobat.TestsCommon.SampleData
{
    public class SampleLoader
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

        public static Project LoadSampleProject(string sampleName, string sampleProjectName)
        {
            using (var resourceLoader = ServiceLocator.ResourceLoaderFactory.CreateResourceLoader())
            {
                var stream = resourceLoader.OpenResourceStream(ResourceScope.TestCommon, Path + sampleName);
                CatrobatZipService.UnzipCatrobatPackageIntoIsolatedStorage(stream, CatrobatContextBase.ProjectsPath + "/" + sampleProjectName);
                stream.Close();
                stream.Dispose();
            }
            return CatrobatContext.LoadNewProjectByNameStatic(sampleProjectName);
        }
    }
}
