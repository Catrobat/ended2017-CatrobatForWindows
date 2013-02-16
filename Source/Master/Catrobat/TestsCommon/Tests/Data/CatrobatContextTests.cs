using System;
using Catrobat.Core;
using Catrobat.TestsCommon.Misc;
using Catrobat.TestsCommon.SampleData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.TestsCommon.Tests.Data
{
  [TestClass]
  public class CatrobatContextTests
  {
    [ClassInitialize()]
    public static void TestClassInitialize(TestContext testContext)
    {
      TestHelper.InitializeTests();
    }

    [TestMethod]
    public void InitializeCatrobatContextTest()
    {
      TestHelper.InitializeAndClearCatrobatContext();
      SampleLoader.LoadSampleProject("test.catroid", "DefaultProject");
      // TODO: load sample priject

      // check if project has sucessfully loaded default project
      Assert.AreEqual(CatrobatContext.Instance.LocalSettings.CurrentProjectName, "DefaultProject");
    }

    [TestMethod]
    public void StoreLocalSettingsTest()
    {
      throw new NotImplementedException("Implement for WindowsStore");
      //CatrobatContext.Instance.StoreLocalSettings();

      //using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
      //{
      //  Assert.AreEqual(isolatedStorage.FileExists(CatrobatContext.LocalSettingsFilePath), true);
      //}
    }
  }
}
