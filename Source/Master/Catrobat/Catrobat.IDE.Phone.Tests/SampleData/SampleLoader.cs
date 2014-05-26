using System.Threading.Tasks;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using System;
using System.IO;
using System.Xml.Linq;
using Catrobat.IDE.Core.Xml;
using Catrobat.IDE.Core.Xml.XmlObjects;

namespace Catrobat.IDE.Phone.Tests.SampleData
{
  public class SampleLoader
  {
    private static string path = "SampleData/SampleProjects/";

    public static XmlProject LoadSampleXML(string sampleName)
    {
      String xml = null;
      using (var resourceLoader = ServiceLocator.ResourceLoaderFactory.CreateResourceLoader())
      {
        Stream stream = resourceLoader.OpenResourceStream(ResourceScope.TestsPhone, path + sampleName + ".xml");
        var reader = new StreamReader(stream);

        xml = reader.ReadToEnd();
        reader.Close();
        reader.Dispose();
        stream.Close();
        stream.Dispose();
      }
      return new XmlProject(xml);
    }

    public static XDocument LoadSampleXDocument(string sampleName)
    {
      String xml = null;
      using (var resourceLoader = ServiceLocator.ResourceLoaderFactory.CreateResourceLoader())
      {
        Stream stream = resourceLoader.OpenResourceStream(ResourceScope.TestsPhone, path + sampleName + ".xml");
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
            var stream = resourceLoader.OpenResourceStream(ResourceScope.TestsPhone, path + sampleName);
            await CatrobatZipService.UnzipCatrobatPackageIntoIsolatedStorage(stream, CatrobatContextBase.ProjectsPath + "/" + sampleProjectName);
            stream.Close();
            stream.Dispose();
        }
        return await CatrobatContext.LoadNewProjectByNameStatic(sampleProjectName);
    }
  }
}
