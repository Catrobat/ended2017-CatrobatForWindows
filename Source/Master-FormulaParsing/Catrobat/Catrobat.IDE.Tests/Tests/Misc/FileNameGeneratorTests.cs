using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.Services.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Tests.Misc
{
  [TestClass]
  public class FileNameGeneratorTests
  {
    [ClassInitialize()]
    public static void TestClassInitialize(TestContext testContext)
    {
      
    }

    [TestMethod,TestCategory("GatedTests")]
    public void GenerateValidFileName()
    {
      string fileName1 = FileNameGenerationHelper.Generate();
      string fileName2 = FileNameGenerationHelper.Generate();

      Assert.AreNotEqual(fileName1, fileName2);
    }
  }
    
}
