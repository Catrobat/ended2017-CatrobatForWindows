using System;
using Catrobat.Core;
using Catrobat.Core.Objects;
using System.IO;
using Catrobat.Core.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.TestsCommon.Misc;
using Catrobat.TestsCommon.SampleData;

namespace Catrobat.TestsCommon.Tests.Data
{
  [TestClass]
  public class DataWritingTests
  {
    [ClassInitialize()]
    public static void TestClassInitialize(TestContext testContext)
    {
      TestHelper.InitializeTests();
    }

    [TestMethod]
    public void DataWriteSimpleTest()
    {
      string originalPath = "SampleData/SampleProjects/ultimateTest.xml";

      SampleLoader.LoadSampleXML("ultimateTest");
      CatrobatContext.Instance.CurrentProject.Save();

      Stream originalStream = ResourceLoader.GetResourceStream(Projects.TestCommon, originalPath);
      StreamReader originalReader = new StreamReader(originalStream);
      String originalXml = originalReader.ReadToEnd();

      String writtenXml = "";
      using (IStorage storage = StorageSystem.GetStorage())
      {
        writtenXml = storage.ReadTextFile(CatrobatContext.Instance.CurrentProject.BasePath + "/projectcode.xml");
        var project = new Project(writtenXml);
      }

      // delete all ' ' caracters
      originalXml = originalXml.Replace(" ", "");
      writtenXml = writtenXml.Replace(" ", "");

      // remove ".0" at the end of all doubles
      originalXml = originalXml.Replace(".0", "");
      writtenXml = writtenXml.Replace(".0", "");

      // replaye "encoding="utf-8"" with "encoding="UTF-8""
      originalXml = originalXml.Replace("encoding=\"utf-8\"", "encoding=\"UTF-8\"");
      writtenXml = writtenXml.Replace("encoding=\"utf-8\"", "encoding=\"UTF-8\"");


      Assert.AreEqual(originalXml, writtenXml);

      TestHelper.InitializeAndClearCatrobatContext();
    }

    [TestMethod]
    public void WriteReadDefaultTest()
    {
      TestHelper.InitializeAndClearCatrobatContext();
      SampleLoader.LoadSampleProject("test.catroid", "DefaultProject");

      CatrobatContext.Instance.CurrentProject.Save();

      String writtenXml = "";
      using (IStorage storage = StorageSystem.GetStorage())
      {
        writtenXml = storage.ReadTextFile(CatrobatContext.Instance.CurrentProject.BasePath + "/projectcode.xml");
        var project = new Project(writtenXml);
      }
    }

    [TestMethod]
    public void WriteReadUltimateTest()
    {
      TestHelper.InitializeAndClearCatrobatContext();
      SampleLoader.LoadSampleProject("UltimateTest.catroid", "UltimateTest");

      CatrobatContext.Instance.CurrentProject.Save();

      String writtenXml = "";
      using (IStorage storage = StorageSystem.GetStorage())
      {
        writtenXml = storage.ReadTextFile(CatrobatContext.Instance.CurrentProject.BasePath + "/projectcode.xml");
        var project = new Project(writtenXml);
      }
    }
  }
}
