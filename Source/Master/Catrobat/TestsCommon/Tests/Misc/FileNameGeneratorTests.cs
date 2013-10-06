using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Services.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.TestsCommon.Tests.Misc
{
  [TestClass]
  public class FileNameGeneratorTests
  {
    [ClassInitialize()]
    public static void TestClassInitialize(TestContext testContext)
    {
      
    }

    [TestMethod]
    public void GenerateValidFileName()
    {
      string fileName = FileNameGenerationHelper.Generate();

      Assert.IsTrue(fileName.EndsWith("_"));
      Assert.IsTrue(fileName.Length >= 33); //32 x Hexzahl + 1 x "_"
    }
  }
    
}
