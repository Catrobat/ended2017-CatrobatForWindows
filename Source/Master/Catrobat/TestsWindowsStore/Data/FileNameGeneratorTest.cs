using Catrobat.Core.Misc.Helpers;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace TestsWindowsStore.Data
{
  [TestClass]
  public class FileNameGeneratorTest
  {
    [TestMethod]
    public void generateValidFileName()
    {
      string fileName = FileNameGenerator.generate();

      Assert.IsTrue(fileName.EndsWith("_"));
      Assert.IsTrue(fileName.Length >= 33); //32 x Hexzahl + 1 x "_"
    }
  }
    
}
