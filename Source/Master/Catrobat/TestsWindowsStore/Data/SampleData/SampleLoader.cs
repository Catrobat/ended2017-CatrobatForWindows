using System;
using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.Core.ZIP;
using System.IO;
using System.Xml.Linq;
using Windows.UI.Xaml;

namespace TestsWindowsStore.Data.SampleData
{
  public class SampleLoader
  {
    public static Project LoadSampleXML(string sampleName)
    {
      throw new NotImplementedException("Implement for WindowsStore");
      //string path = "Tests/Data/SampleData/SampleProjects/";

      //path += sampleName + ".xml";

      //Stream stream = Application.GetResourceStream(new Uri("/MetroCatUT;component/" + path, UriKind.Relative)).Stream;
      //StreamReader reader = new StreamReader(stream);

      //String xml = reader.ReadToEnd();
      //return new Project(xml);
    }

    public static XDocument LoadSampleXDocument(string sampleName)
    {
      throw new NotImplementedException("Implement for WindowsStore");
      //string path = "Tests/Data/SampleData/SampleProjects/";

      //path += sampleName + ".xml";

      //Stream stream = Application.GetResourceStream(new Uri("/MetroCatUT;component/" + path, UriKind.Relative)).Stream;
      //StreamReader reader = new StreamReader(stream);

      //String xml = reader.ReadToEnd();
      //return XDocument.Load(new StringReader(xml));
    }

    public static void LoadSampleProject(string sampleName, string sampleProjectName)
    {
      throw new NotImplementedException("Implement for WindowsStore");
      //string path = "Tests/Data/SampleData/SampleProjects/";
      //path += sampleName;

      //Uri uri = new Uri("/MetroCatUT;component/" + path, UriKind.Relative);
      //var resourceStreamInfo = Application.GetResourceStream(uri);
      //CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(resourceStreamInfo.Stream, CatrobatContext.ProjectsPath + "/" + sampleProjectName);

      //CatrobatContext.Instance.SetCurrentProject(sampleProjectName);
      //CatrobatContext.Instance.CurrentProject.SetProjectName(sampleProjectName);
    }
  }
}
