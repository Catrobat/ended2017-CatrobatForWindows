using System;
using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.Core.ZIP;
using System.IO;
using System.Xml.Linq;
using Catrobat.Core.Storage;

namespace Catrobat.TestsWindowsPhone.SampleData
{
  public class SampleLoader
  {
    private static string path = "SampleData/SampleProjects/";

    public static Project LoadSampleXML(string sampleName)
    {
      String xml = null;
      using (var resourceLoader = ResourceLoader.CreateResourceLoader())
      {
        Stream stream = resourceLoader.OpenResourceStream(ResourceScope.TestsPhone, path + sampleName + ".xml");
        StreamReader reader = new StreamReader(stream);

        xml = reader.ReadToEnd();
        reader.Close();
        reader.Dispose();
        stream.Close();
        stream.Dispose();
      }
      return new Project(xml);
    }

    public static XDocument LoadSampleXDocument(string sampleName)
    {
      String xml = null;
      using (var resourceLoader = ResourceLoader.CreateResourceLoader())
      {
        Stream stream = resourceLoader.OpenResourceStream(ResourceScope.TestsPhone, path + sampleName + ".xml");
        StreamReader reader = new StreamReader(stream);

        xml = reader.ReadToEnd();
        reader.Close();
        reader.Dispose();
        stream.Close();
        stream.Dispose();
      }
      return XDocument.Load(new StringReader(xml));
    }

    public static void LoadSampleProject(string sampleName, string sampleProjectName)
    {
      using (var resourceLoader = ResourceLoader.CreateResourceLoader())
      {
        Stream stream = resourceLoader.OpenResourceStream(ResourceScope.TestsPhone, path + sampleName + ".xml");
        CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(stream, CatrobatContext.ProjectsPath + "/" + sampleProjectName);
        stream.Close();
        stream.Dispose();
      }
      CatrobatContext.Instance.SetCurrentProject(sampleProjectName);
      CatrobatContext.Instance.CurrentProject.SetProjectName(sampleProjectName);
    }
  }
}
