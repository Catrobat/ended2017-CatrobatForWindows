using System;
using Catrobat.Core;
using Catrobat.Core.Objects;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.IO;
using Windows.UI.Xaml;

namespace TestsWindowsStore.Data
{
  [TestClass]
  public class DataWritingTests
  {
    [TestMethod]
    public void DataWriteSimpleTest()
    {
      throw new NotImplementedException("Implement for WindowsStore");
      //string originalPath = "Tests/Data/SampleData/SampleProjects/ultimateTest.xml";

      //SampleLoader.LoadSampleXML("ultimateTest");
      //CatrobatContext.Instance.CurrentProject.Save();

      //Stream originalStream = Application.GetResourceStream(new Uri("/MetroCatUT;component/" + originalPath, UriKind.Relative)).Stream;
      //StreamReader originalReader = new StreamReader(originalStream);
      //String originalXml = originalReader.ReadToEnd();

      //String writtenXml = "";
      //using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
      //{
      //  StreamReader reader = new StreamReader(new IsolatedStorageFileStream(CatrobatContext.Instance.CurrentProject.BasePath + 
      //    "/projectcode.xml", FileMode.Open, isolatedStorage));

      //  writtenXml = reader.ReadToEnd();
      //  reader.Close();
      //}

      //// delete all ' ' caracters
      //originalXml = originalXml.Replace(" ", "");
      //writtenXml = writtenXml.Replace(" ", "");

      //// remove ".0" at the end of all doubles
      //originalXml = originalXml.Replace(".0", "");
      //writtenXml = writtenXml.Replace(".0", "");

      //// replaye "encoding="utf-8"" with "encoding="UTF-8""
      //originalXml = originalXml.Replace("encoding=\"utf-8\"", "encoding=\"UTF-8\"");
      //writtenXml = writtenXml.Replace("encoding=\"utf-8\"", "encoding=\"UTF-8\"");


      //Assert.AreEqual(originalXml, writtenXml);

      //TestHelper.InitializeAndClearCatrobatContext();
    }

    [TestMethod]
    public void WriteReadDefaultTest()
    {
      throw new NotImplementedException("Implement for WindowsStore");
      //TestHelper.InitializeAndClearCatrobatContext();
      //SampleLoader.LoadSampleProject("test.catroid", "DefaultProject");

      //CatrobatContext.Instance.CurrentProject.Save();

      //String writtenXml = "";
      //using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
      //{
      //  StreamReader reader = new StreamReader(new IsolatedStorageFileStream(CatrobatContext.Instance.CurrentProject.BasePath +
      //    "/projectcode.xml", FileMode.Open, isolatedStorage));

      //  writtenXml = reader.ReadToEnd();
      //  var project = new Project(writtenXml);

      //  reader.Close();
      //}
    }

    [TestMethod]
    public void WriteReadUltimateTest()
    {
      throw new NotImplementedException("Implement for WindowsStore");
      //TestHelper.InitializeAndClearCatrobatContext();
      //SampleLoader.LoadSampleProject("UltimateTest.catroid", "UltimateTest");

      //CatrobatContext.Instance.CurrentProject.Save();

      //String writtenXml = "";
      //using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
      //{
      //  StreamReader reader = new StreamReader(new IsolatedStorageFileStream(CatrobatContext.Instance.CurrentProject.BasePath +
      //    "/projectcode.xml", FileMode.Open, isolatedStorage));

      //  writtenXml = reader.ReadToEnd();
      //  var project = new Project(writtenXml);

      //  reader.Close();
      //}
    }
  }
}
