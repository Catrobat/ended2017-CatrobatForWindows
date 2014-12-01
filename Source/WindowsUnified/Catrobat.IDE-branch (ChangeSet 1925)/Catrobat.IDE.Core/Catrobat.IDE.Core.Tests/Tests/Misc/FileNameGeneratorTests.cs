using Catrobat.IDE.Core.Utilities.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.Misc
{
  [TestClass]
  public class FileNameGeneratorTests
  {
    [ClassInitialize()]
    public static void TestClassInitialize(TestContext testContext)
    {
      
    }

    [TestMethod, TestCategory("Data")]
    public void GenerateValidFileName()
    {
      string fileName1 = FileNameGenerationHelper.Generate();
      string fileName2 = FileNameGenerationHelper.Generate();

      Assert.AreNotEqual(fileName1, fileName2);
    }
  }
    
}
