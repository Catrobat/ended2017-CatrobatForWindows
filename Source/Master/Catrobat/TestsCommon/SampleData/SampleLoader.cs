using System;
using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.Core.ZIP;
using System.IO;
using System.Xml.Linq;
using Catrobat.Core.Storage;
using Catrobat.TestsCommon.Misc.Storage;

namespace Catrobat.TestsCommon.SampleData
{
    public class SampleLoader
    {
        private static readonly string Path = BasePathHelper.GetSampleProjectsPath();

        public static XDocument LoadSampleXDocument(string sampleName)
        {
            string xml = null;
            using (var resourceLoader = ResourceLoader.CreateResourceLoader())
            {
                Stream stream = resourceLoader.OpenResourceStream(ResourceScope.TestCommon, Path + sampleName + ".xml");
                StreamReader reader = new StreamReader(stream);

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
            using (var resourceLoader = ResourceLoader.CreateResourceLoader())
            {
                Stream stream = resourceLoader.OpenResourceStream(ResourceScope.TestCommon, Path + sampleName);
                CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(stream, CatrobatContextBase.ProjectsPath + "/" + sampleProjectName);
                stream.Close();
                stream.Dispose();
            }
            return CatrobatContext.CreateNewProjectByNameStatic(sampleProjectName);
        }
    }
}
