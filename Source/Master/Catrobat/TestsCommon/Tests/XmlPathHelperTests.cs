using Catrobat.Core.ConverterLib;
using System.Xml.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.TestsCommon.Misc;
using Catrobat.TestsCommon.SampleData;

namespace Catrobat.TestsCommon.Tests
{
  [TestClass]
  public class XmlPathHelperTests
  {
    [ClassInitialize()]
    public static void TestClassInitialize(TestContext testContext)
    {
      TestHelper.InitializeTests();
    }

    [TestMethod]
    public void GetElementTest()
    {
      XDocument doc = SampleLoader.LoadSampleXDocument("Converter/UltimateTest");

      var references = new List<XElement>();
      foreach (XElement element in doc.Descendants())
        if (element.Attribute("reference") != null && !element.Name.ToString().StartsWith("sprite"))
          references.Add(element);

      var soundInfo1 = (new List<XElement>(doc.Descendants("soundInfo")))[0];
      Assert.AreEqual(PathHelper.GetElement(references[0]), soundInfo1);

      var sprite1 = (new List<XElement>(doc.Descendants("Content.Sprite")))[0];
      Assert.AreEqual(PathHelper.GetElement(references[1]), sprite1);

      var foreverbrick = (new List<XElement>(doc.Descendants("Bricks.ForeverBrick")))[0];
      Assert.AreEqual(PathHelper.GetElement(references[2]), foreverbrick);

      var repeatBrick = (new List<XElement>(doc.Descendants("Bricks.RepeatBrick")))[0];
      Assert.AreEqual(PathHelper.GetElement(references[3]), repeatBrick);

      var foreverLoopEndBrick = foreverbrick.Element("loopEndBrick");
      Assert.AreEqual(PathHelper.GetElement(references[4]), foreverLoopEndBrick);

      var repeatLoopEndBrick = repeatBrick.Element("loopEndBrick");
      Assert.AreEqual(PathHelper.GetElement(references[5]), repeatLoopEndBrick);

      var soundInfo2 = (new List<XElement>(doc.Descendants("soundInfo")))[2];
      Assert.AreEqual(PathHelper.GetElement(references[6]), soundInfo1);
      Assert.AreEqual(PathHelper.GetElement(references[7]), soundInfo2);

      var costume = (new List<XElement>(doc.Descendants("costumeDataList").Elements()))[0];
      Assert.AreEqual(PathHelper.GetElement(references[8]), costume);

      var pointedSprite = (new List<XElement>(doc.Descendants("pointedSprite")))[0];
      Assert.AreEqual(PathHelper.GetElement(references[9]), pointedSprite);
      Assert.AreEqual(PathHelper.GetElement(references[10]), pointedSprite);
    }
  }
}
