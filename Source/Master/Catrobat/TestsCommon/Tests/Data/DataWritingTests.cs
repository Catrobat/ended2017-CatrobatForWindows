using System;
using Catrobat.Core;
using Catrobat.Core.Objects;
using System.IO;
using Catrobat.Core.Storage;
using Catrobat.TestsCommon.Misc.Storage;
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
    public void WriteReadSimpleTest()
    {
      var project = SampleLoader.LoadSampleXML("simple");
      project.Save();
      String originalXml = null;

      using (var resourceLoader = ResourceLoader.CreateResourceLoader())
      {
        Stream originalStream = resourceLoader.OpenResourceStream(ResourceScope.TestCommon,
                                                                  BasePathHelper.GetSampleProjectsPath() + "simple.xml");
        StreamReader originalReader = new StreamReader(originalStream);
        originalXml = originalReader.ReadToEnd();
        originalReader.Close();
        originalReader.Dispose();
        originalStream.Close();
        originalStream.Dispose();
      }

      String writtenXml = "";
      using (IStorage storage = StorageSystem.GetStorage())
      {
        writtenXml = storage.ReadTextFile(project.BasePath + "/projectcode.xml");
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

      originalXml = originalXml.Replace("\n\r", "\n");
      writtenXml = writtenXml.Replace("\n\r", "\n");

      originalXml = originalXml.Replace("\r\n", "\n");
      writtenXml = writtenXml.Replace("\r\n", "\n");

      originalXml = originalXml.Replace("\r", "");
      writtenXml = writtenXml.Replace("\r", "");

      Assert.AreEqual(originalXml, writtenXml);
    }

    [TestMethod]
    public void WriteReadUltimateTest()
    {
      var project = SampleLoader.LoadSampleXML("ultimateTest");
      project.Save();
      String originalXml = null;
      using (var resourceLoader = ResourceLoader.CreateResourceLoader())
      {
        Stream originalStream = resourceLoader.OpenResourceStream(ResourceScope.TestCommon,
                                                                  BasePathHelper.GetSampleProjectsPath() +
                                                                  "ultimateTest.xml");
        StreamReader originalReader = new StreamReader(originalStream);
        originalXml = originalReader.ReadToEnd();
        originalReader.Close();
        originalReader.Dispose();
        originalStream.Close();
        originalStream.Dispose();
      }

      String writtenXml = "";
      using (IStorage storage = StorageSystem.GetStorage())
      {
        writtenXml = storage.ReadTextFile(project.BasePath + "/projectcode.xml");
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

      originalXml = originalXml.Replace("\n\r", "\n");
      writtenXml = writtenXml.Replace("\n\r", "\n");

      originalXml = originalXml.Replace("\r\n", "\n");
      writtenXml = writtenXml.Replace("\r\n", "\n");

      originalXml = originalXml.Replace("\r", "");
      writtenXml = writtenXml.Replace("\r", "");


      Assert.AreEqual(originalXml, writtenXml);
    }
  }
}
