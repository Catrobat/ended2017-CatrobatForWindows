using System;
using System.IO;
using System.Xml.Linq;

namespace TestsWindowsPhone.SampleData
{
  public class SampleLoader
  {
    private static string path = "SampleData/SampleProjects/";

    public static Project LoadSampleXML(string sampleName)
    {
      Stream stream = ResourceLoader.GetResourceStream(ResourceScope.TestsPhone, path + sampleName + ".xml");
      StreamReader reader = new StreamReader(stream);

      String xml = reader.ReadToEnd();
      return new Project(xml);
    }

    public static XDocument LoadSampleXDocument(string sampleName)
    {
      Stream stream = ResourceLoader.GetResourceStream(ResourceScope.TestsPhone, path + sampleName + ".xml");
      StreamReader reader = new StreamReader(stream);

      String xml = reader.ReadToEnd();
      return XDocument.Load(new StringReader(xml));
    }

    public static void LoadSampleProject(string sampleName, string sampleProjectName)
    {
      Stream stream = ResourceLoader.GetResourceStream(ResourceScope.TestsPhone, path + sampleName + ".xml");
      CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(stream, CatrobatContext.ProjectsPath + "/" + sampleProjectName);

      CatrobatContext.Instance.SetCurrentProject(sampleProjectName);
      CatrobatContext.Instance.CurrentProject.SetProjectName(sampleProjectName);
    }
  }
}
