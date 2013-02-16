using System;
using System.Xml.Linq;
using System.Collections.Generic;
using Catrobat.Core.ConverterLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.TestsCommon.Misc;
using Catrobat.TestsCommon.SampleData;

namespace Catrobat.TestsCommon.Tests.Data
{
  [TestClass]
  public class XmlConverterTests
  {
    [ClassInitialize()]
    public static void TestClassInitialize(TestContext testContext)
    {
      TestHelper.InitializeTests();
    }

    [TestMethod]
    public void RemoveSpriteReferencesTest()
    {
      XDocument doc = SampleLoader.LoadSampleXDocument("Converter/UltimateTest");

      EditXML.RemoveSpriteReferences(doc);

      var referenceList = new List<XElement>(doc.Descendants("sprite"));
      Assert.AreEqual(referenceList.Count, 0);
    }

    [TestMethod]
    public void ProjectElementsTest()
    {
      XDocument doc = SampleLoader.LoadSampleXDocument("Converter/ProjectElements");

      EditXML.HandleProjectElements(doc);

      var project = doc.Element("Content.Project");

      Assert.AreEqual(project.Element("catroidVersionCode"), null);
      Assert.AreNotEqual(project.Element("applicationVersionCode"), null);
      Assert.AreEqual(Int32.Parse(project.Element("applicationVersionCode").Value), 10);
      Assert.AreEqual(project.FirstNode, project.Element("applicationVersionCode"));
      Assert.AreEqual(project.Element("applicationVersionCode").IsBefore(project.Element("applicationVersionName")), true);

      Assert.AreEqual(project.Element("catroidVersionName"), null);
      Assert.AreNotEqual(project.Element("applicationVersionName"), null);
      Assert.AreEqual(project.Element("applicationVersionName").Value, "0.6.0beta");
      Assert.AreEqual(project.Element("applicationVersionName").IsAfter(project.Element("applicationVersionCode")), true);
      Assert.AreEqual(project.Element("applicationVersionName").IsBefore(project.Element("applicationXmlVersion")), true);

      Assert.AreNotEqual(project.Element("applicationXmlVersion"), null);
      Assert.AreEqual(double.Parse(project.Element("applicationXmlVersion").Value), 1.0);
      Assert.AreEqual(project.Element("applicationXmlVersion").IsAfter(project.Element("applicationVersionName")), true);
      Assert.AreEqual(project.Element("applicationXmlVersion").IsBefore(project.Element("deviceName")), true);

      Assert.AreNotEqual(project.Element("deviceName"), null);
      Assert.AreEqual(project.Element("deviceName").Value, "HTC Desire");
      Assert.AreEqual(project.Element("deviceName").IsAfter(project.Element("applicationXmlVersion")), true);
      Assert.AreEqual(project.Element("deviceName").IsBefore(project.Element("platform")), true);

      Assert.AreNotEqual(project.Element("platform"), null);
      Assert.AreEqual(project.Element("platform").Value, "Android");
      Assert.AreEqual(project.Element("platform").IsAfter(project.Element("deviceName")), true);
      Assert.AreEqual(project.Element("platform").IsBefore(project.Element("platformVersion")), true);

      Assert.AreEqual(project.Element("androidVersion"), null);
      Assert.AreNotEqual(project.Element("platformVersion"), null);
      Assert.AreEqual(Int32.Parse(project.Element("platformVersion").Value), 10);
      Assert.AreEqual(project.Element("platformVersion").IsAfter(project.Element("platform")), true);
      Assert.AreEqual(project.Element("platformVersion").IsBefore(project.Element("projectName")), true);

      Assert.AreNotEqual(project.Element("projectName"), null);
      Assert.AreEqual(project.Element("projectName").Value, "Project Elements Test");
      Assert.AreEqual(project.Element("projectName").IsAfter(project.Element("platformVersion")), true);
      Assert.AreEqual(project.Element("projectName").IsBefore(project.Element("screenHeight")), true);

      Assert.AreNotEqual(project.Element("screenHeight"), null);
      Assert.AreEqual(Int32.Parse(project.Element("screenHeight").Value), 800);
      Assert.AreEqual(project.Element("screenHeight").IsAfter(project.Element("projectName")), true);
      Assert.AreEqual(project.Element("screenHeight").IsBefore(project.Element("screenWidth")), true);

      Assert.AreNotEqual(project.Element("screenWidth"), null);
      Assert.AreEqual(Int32.Parse(project.Element("screenWidth").Value), 480);
      Assert.AreEqual(project.Element("screenWidth").IsAfter(project.Element("screenHeight")), true);
      Assert.AreEqual(project.Element("screenWidth").IsBefore(project.Element("spriteList")), true);
    }

    [TestMethod]
    public void PointToBrickTest()
    {
      XDocument doc = SampleLoader.LoadSampleXDocument("Converter/UltimateTest");

      EditXML.HandlePointToBrick(doc);

      var spriteList = new List<XElement>(doc.Element("Content.Project").Element("spriteList").Elements());

      var sprite1 = spriteList[0];
      var sprite2 = spriteList[1];
      var sprite3 = spriteList[2];

      var sprite1PointToBrick = sprite1.Element("scriptList").Element("Content.StartScript").Element("brickList").Element("Bricks.PointToBrick");
      Assert.AreEqual(sprite1PointToBrick.Element("pointedSprite").Attribute("reference").Value, "../../../../../../sprite[2]");

      {
        Assert.AreEqual(sprite2.Attribute("reference"), null);

        var costumeList = new List<XElement>(sprite2.Element("costumeDataList").Elements());
        Assert.AreEqual(costumeList.Count, 2);

        var brickList1 = new List<XElement>(sprite2.Element("scriptList").Element("Content.StartScript").Element("brickList").Elements());
        Assert.AreEqual(brickList1.Count, 3);

        var brickList2 = new List<XElement>(sprite2.Element("scriptList").Element("Content.WhenScript").Element("brickList").Elements());
        Assert.AreEqual(brickList2.Count, 7);
        Assert.AreEqual(brickList2[1].Element("pointedSprite").Attribute("reference").Value, "../../../../../../sprite");

        var brickList3 = new List<XElement>(sprite2.Element("scriptList").Element("Content.BroadcastScript").Element("brickList").Elements());
        Assert.AreEqual(brickList3.Count, 6);

        var soundList = new List<XElement>(sprite2.Element("soundList").Elements());
        Assert.AreEqual(soundList.Count, 2);
      }

      var sprite3PointToBrick = sprite3.Element("scriptList").Element("Content.StartScript").Element("brickList").Element("Bricks.PointToBrick");
      Assert.AreEqual(sprite3PointToBrick.Element("pointedSprite").Attribute("reference").Value, "../../../../../../sprite[2]");
    }

    [TestMethod]
    public void SoundTest()
    {
      XDocument doc = SampleLoader.LoadSampleXDocument("Converter/UltimateTest");

      EditXML.HandleSounds(doc);

      var spriteList = new List<XElement>(doc.Descendants("pointedSprite"));
      var sprite2 = spriteList[0];

      var brickList = new List<XElement>(sprite2.Element("scriptList").Element("Content.StartScript").Element("brickList").Elements());

      Assert.AreEqual(brickList[0].Element("soundInfo").Attribute("reference").Value, "../../../../../soundList/soundInfo");
      Assert.AreEqual(brickList[1].Element("soundInfo").Attribute("reference").Value, "../../../../../soundList/soundInfo");
      Assert.AreEqual(brickList[2].Element("soundInfo").Attribute("reference").Value, "../../../../../soundList/soundInfo[2]");

      var soundList = new List<XElement>(sprite2.Element("soundList").Elements());
      Assert.AreEqual(soundList[0].Element("fileName").Value, "68223C25ABEFABA96FD2BEC8C44D5A12_Aufnahme.mp3");
      Assert.AreEqual(soundList[0].Element("name").Value, "Aufnahme");
      Assert.AreEqual(soundList[1].Element("fileName").Value, "84904B77F5B78BBDCC04634285D1B8DE_Aufnahme.mp3");
      Assert.AreEqual(soundList[1].Element("name").Value, "Aufnahme1");
    }

    [TestMethod]
    public void LoopTest()
    {
      XDocument doc = SampleLoader.LoadSampleXDocument("Converter/UltimateTest");

      EditXML.HandleLoops(doc);

      var spriteList = new List<XElement>(doc.Descendants("pointedSprite"));
      var sprite2 = spriteList[0];

      var brickList = new List<XElement>(sprite2.Element("scriptList").Element("Content.BroadcastScript").Element("brickList").Elements());

      var foreverLoopEndBrickRef = brickList[0].Element("loopEndBrick");
      Assert.AreEqual(foreverLoopEndBrickRef.Attribute("class"), null);
      Assert.AreEqual(foreverLoopEndBrickRef.Attribute("reference").Value, "../../loopEndBrick");

      var repeatLoopEndBrickRef = brickList[1].Element("loopEndBrick");
      Assert.AreEqual(repeatLoopEndBrickRef.Attribute("class"), null);
      Assert.AreEqual(repeatLoopEndBrickRef.Attribute("reference").Value, "../../loopEndBrick[2]");

      var foreverLoopBeginBrickRef = brickList[3].Element("loopBeginBrick");
      Assert.AreEqual(foreverLoopBeginBrickRef.Attribute("class").Value, "Bricks.ForeverBrick");
      Assert.AreEqual(foreverLoopBeginBrickRef.Attribute("reference").Value, "../../Bricks.ForeverBrick");

      var repeatLoopBeginBrickRef = brickList[5].Element("loopBeginBrick");
      Assert.AreEqual(repeatLoopBeginBrickRef.Attribute("class").Value, "Bricks.RepeatBrick");
      Assert.AreEqual(repeatLoopBeginBrickRef.Attribute("reference").Value, "../../Bricks.RepeatBrick");
    }

    [TestMethod]
    public void RemoveNameSpacesTest()
    {
      XDocument doc = SampleLoader.LoadSampleXDocument("Converter/UltimateTest");

      EditXML.HandlePointToBrick(doc);
      EditXML.HandleSounds(doc);
      EditXML.HandleLoops(doc);
      EditXML.RemoveNameSpaces(doc);

      var project = doc.Element("project");
      Assert.AreEqual(doc.Element("Content.Project"), null);
      Assert.AreNotEqual(project, null);

      var spriteList = new List<XElement>(project.Element("spriteList").Elements());
      foreach (XElement sprite in spriteList)
      {
        Assert.AreNotEqual(sprite.Name, "Content.Sprite");
        Assert.AreEqual(sprite.Name, "sprite");

        if (sprite.Attribute("reference") != null)
          continue;

        var costumeList = new List<XElement>(sprite.Element("costumeDataList").Elements());
        foreach (XElement costume in costumeList)
        {
          Assert.AreNotEqual(costume.Name, "Common.CostumeData");
          Assert.AreEqual(costume.Name, "costumeData");
        }

        var scriptList = new List<XElement>(sprite.Elements("scriptList").Elements());
        foreach (XElement script in scriptList)
        {
          Assert.AreNotEqual(sprite.Name.ToString().StartsWith("Content."), true);

          var brickList = new List<XElement>(script.Element("brickList").Elements());
          foreach (XElement brick in brickList)
            Assert.AreNotEqual(brick.Name.ToString().StartsWith("Bricks."), true);
        }

        var soundList = new List<XElement>(sprite.Element("soundList").Elements());
        foreach (XElement sound in soundList)
        {
          Assert.AreNotEqual(sound.Name, "Common.SoundInfo");
          Assert.AreEqual(sound.Name, "soundInfo");
        }
      }
    }

    [TestMethod]
    public void ChangeReferencesTest()
    {
      XDocument doc = SampleLoader.LoadSampleXDocument("Converter/UltimateTest");

      EditXML.RemoveSpriteReferences(doc);
      EditXML.HandlePointToBrick(doc);
      EditXML.HandleSounds(doc);
      EditXML.HandleLoops(doc);
      EditXML.RemoveNameSpaces(doc);
      EditXML.ChangeReferences(doc);

      foreach (XElement element in doc.Descendants())
        if (element.Attribute("reference") != null)
        {
          string reference = element.Attribute("reference").Value;
          Assert.AreNotEqual(reference.Contains("Content."), true);
          Assert.AreNotEqual(reference.Contains("Common."), true);
          Assert.AreNotEqual(reference.Contains("Bricks."), true);
        }
    }
  }
}
