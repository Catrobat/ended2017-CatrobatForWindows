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
    private static string path = BasePathHelper.GetSampleProjectsPath();

    public static Project LoadSampleXML(string sampleName)
    {
      Stream stream = ResourceLoader.GetResourceStream(ResourceScope.TestCommon, path + sampleName + ".xml");
      StreamReader reader = new StreamReader(stream);

      String xml = reader.ReadToEnd();
      stream.Dispose();
      return new Project(xml);
    }

    public static XDocument LoadSampleXDocument(string sampleName)
    {
      Stream stream = ResourceLoader.GetResourceStream(ResourceScope.TestCommon, path + sampleName + ".xml");
      StreamReader reader = new StreamReader(stream);

      String xml = reader.ReadToEnd();
      reader.Dispose();
      return XDocument.Load(new StringReader(xml));
    }

    public static void LoadSampleProject(string sampleName, string sampleProjectName)
    {
      Stream stream = ResourceLoader.GetResourceStream(ResourceScope.TestCommon, path + sampleName);
      CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(stream, CatrobatContext.ProjectsPath + "/" + sampleProjectName);
      stream.Dispose();
      CatrobatContext.Instance.SetCurrentProject(sampleProjectName);
      CatrobatContext.Instance.CurrentProject.SetProjectName(sampleProjectName);
    }
  }
}
