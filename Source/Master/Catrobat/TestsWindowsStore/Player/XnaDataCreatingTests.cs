using System;
using Catrobat.Core;
using Catrobat.Core.ZIP;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Collections.Generic;
using Windows.UI.Xaml;

namespace TestsWindowsStore.Player
{
  [TestClass]
  public class XnaDataCreationTest
  {

    [TestMethod]
    [Ignore]
    public void CreateXnaStructure()
    {
      throw new NotImplementedException("Implement for WindowsStore");
      //SampleLoader.LoadSampleXML("projectcode");
      //string path = "Tests/Data/SampleData/SampleProjects/default.catroid";
      //Uri uri = new Uri("/MetroCatUT;component/" + path, UriKind.Relative);
      //var resourceStreamInfo = Application.GetResourceStream(uri);
      //CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(resourceStreamInfo.Stream, "Projects/DefaultProject");

      //var project = CatrobatContext.Instance.CurrentProject;
      //PlayerData.InitializeXNA();
      //XnaSpriteList xnaSpriteList = PlayerData.CreateXNAStructure();
      //List<XnaSprite> xnaSprites = xnaSpriteList.Sprites;

      //Assert.AreEqual(xnaSprites.Count, 2);
      //Assert.AreEqual(xnaSprites[0].XnaCostumeData.Count, 1);
      //Assert.AreEqual(xnaSprites[0].XnaScripts.Count, 1);
      //Assert.AreEqual(xnaSprites[0].XnaSounds.Count, 0);
      //Assert.AreEqual(xnaSprites[1].XnaCostumeData.Count, 3);
      //Assert.AreEqual(xnaSprites[1].XnaScripts.Count, 2);
      //Assert.AreEqual(xnaSprites[1].XnaSounds.Count, 0);

      //Assert.AreEqual(xnaSprites[0].XnaCostumeData[0].FileName, "3F3C722FCCBBD45ACF1211E3155FD5C6_background");
      //Assert.AreEqual(xnaSprites[0].XnaScripts[0].ScriptAction, MetroCatPlayerLib.XnaObjects.Action.Start);
      //Assert.AreEqual(xnaSprites[0].XnaScripts[0].Bricks.XnaBricks.Count, 1);
      //Assert.IsInstanceOfType(xnaSprites[0].XnaScripts[0].Bricks.XnaBricks[0], typeof(XnaSetCostumeBrick));

      //Assert.AreEqual(xnaSprites[1].XnaCostumeData[0].FileName, "C3F37BB1E4B17CCC6D3FA0578DDBC164_normalCat");
      //Assert.AreEqual(xnaSprites[1].XnaCostumeData[1].FileName, "A5E10E13DDC4ED4B188DA2A5D0B61CF9_banzaiCat");
      //Assert.AreEqual(xnaSprites[1].XnaCostumeData[2].FileName, "E64E017A63AFB9EC687B76E02376B1D9_cheshireCat");
      //Assert.AreEqual(xnaSprites[1].XnaScripts[0].ScriptAction, MetroCatPlayerLib.XnaObjects.Action.Start);
      //Assert.AreNotEqual(xnaSprites[1].XnaScripts[1].ScriptAction, MetroCatPlayerLib.XnaObjects.Action.Start);
      //Assert.AreNotEqual(xnaSprites[1].XnaScripts[1].ScriptAction, MetroCatPlayerLib.XnaObjects.Action.Broadcast);
      //Assert.AreEqual(xnaSprites[1].XnaScripts[0].Bricks.XnaBricks.Count, 1);
      //Assert.IsInstanceOfType(xnaSprites[1].XnaScripts[0].Bricks.XnaBricks[0], typeof(XnaSetCostumeBrick));
      //Assert.AreEqual(xnaSprites[1].XnaScripts[1].Bricks.XnaBricks.Count, 5);
      //Assert.IsInstanceOfType(xnaSprites[1].XnaScripts[1].Bricks.XnaBricks[0], typeof(XnaSetCostumeBrick));
      //Assert.IsInstanceOfType(xnaSprites[1].XnaScripts[1].Bricks.XnaBricks[1], typeof(XnaWaitBrick));
      //Assert.IsInstanceOfType(xnaSprites[1].XnaScripts[1].Bricks.XnaBricks[2], typeof(XnaSetCostumeBrick));
      //Assert.IsInstanceOfType(xnaSprites[1].XnaScripts[1].Bricks.XnaBricks[3], typeof(XnaWaitBrick));
      //Assert.IsInstanceOfType(xnaSprites[1].XnaScripts[1].Bricks.XnaBricks[4], typeof(XnaSetCostumeBrick));


    }
  }
}
