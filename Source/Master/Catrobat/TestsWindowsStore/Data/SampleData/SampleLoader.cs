using System;
using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.Core.ZIP;
using System.IO;
using System.Xml.Linq;
using Windows.UI.Xaml;
using Catrobat.Core.Storage;

namespace TestsWindowsStore.Data.SampleData
{
  public class SampleLoader
  {
    private static string path = "/TestsWindowsStore/Data/SampleData/SampleProjects/";

    public static Project LoadSampleXML(string sampleName)
    {
      Stream stream = ResourceLoader.GetResourceStream(Projects.Core, path + sampleName + ".xml");
      StreamReader reader = new StreamReader(stream);

      String xml = reader.ReadToEnd();
      return new Project(xml);
    }

    public static XDocument LoadSampleXDocument(string sampleName)
    {
      Stream stream = ResourceLoader.GetResourceStream(Projects.Core, path + sampleName + ".xml");
      StreamReader reader = new StreamReader(stream);

      String xml = reader.ReadToEnd();
      return XDocument.Load(new StringReader(xml));
    }

    public static void LoadSampleProject(string sampleName, string sampleProjectName)
    {
      Stream stream = ResourceLoader.GetResourceStream(Projects.Core, path + sampleName + ".xml");
      CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(stream, CatrobatContext.ProjectsPath + "/" + sampleProjectName);

      CatrobatContext.Instance.SetCurrentProject(sampleProjectName);
      CatrobatContext.Instance.CurrentProject.SetProjectName(sampleProjectName);
    }
  }
}
