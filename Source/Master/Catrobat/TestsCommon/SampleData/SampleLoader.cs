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
      String xml = null;
      using (var resourceLoader = ResourceLoader.CreateResourceLoader())
      {
        Stream stream = resourceLoader.OpenResourceStream(ResourceScope.TestCommon, path + sampleName + ".xml");
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
        Stream stream = resourceLoader.OpenResourceStream(ResourceScope.TestCommon, path + sampleName + ".xml");
        StreamReader reader = new StreamReader(stream);

        xml = reader.ReadToEnd();
        reader.Close();
        reader.Dispose();
        stream.Close();
        stream.Dispose();
      }
      return XDocument.Load(new StringReader(xml));
    }

    public static CatrobatContext LoadSampleProject(string sampleName, string sampleProjectName)
    {
      var catrobatContext = new CatrobatContext();
      using (var resourceLoader = ResourceLoader.CreateResourceLoader())
      {
        Stream stream = resourceLoader.OpenResourceStream(ResourceScope.TestCommon, path + sampleName);
        CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(stream, CatrobatContextBase.ProjectsPath + "/" + sampleProjectName);
        stream.Close();
        stream.Dispose();
      }
      catrobatContext.SetCurrentProject(sampleProjectName);
      catrobatContext.CurrentProject.SetProgramName(sampleProjectName);

      return catrobatContext;
    }
  }
}
