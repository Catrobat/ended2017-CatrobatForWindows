using Catrobat.Core.Misc.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.TestsCommon.Tests
{
  [TestClass]
  public class FileNameGeneratorTest
  {
    [ClassInitialize()]
    public static void TestClassInitialize(TestContext testContext)
    {
      
    }

    [TestMethod]
    public void GenerateValidFileName()
    {
      string fileName = FileNameGenerator.generate();

      Assert.IsTrue(fileName.EndsWith("_"));
      Assert.IsTrue(fileName.Length >= 33); //32 x Hexzahl + 1 x "_"
    }
  }
    
}
