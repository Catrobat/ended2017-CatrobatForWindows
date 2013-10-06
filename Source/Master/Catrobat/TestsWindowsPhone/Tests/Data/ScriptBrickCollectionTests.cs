using System.Collections;
using Catrobat.Core.CatroatObjects.Scripts;
using Catrobat.Core.CatrobatObjects.Scripts;
using Catrobat.IDEWindowsPhone.Views.Editor.Scripts;
using Catrobat.TestsWindowsPhone.SampleData;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Catrobat.Core.CatrobatObjects.Bricks;
using Catrobat.Core.CatrobatObjects;
using Catrobat.IDEWindowsPhone.Misc.Storage;
using Catrobat.TestsWindowsPhone.Misc;

namespace Catrobat.TestsWindowsPhone.Tests.Data
{
  [TestClass]
  public class ScriptBrickCollectionTests
  {
    [TestInitialize]
    public void Initialize()
    {
      TestHelper.InitializeTests();
    }

    [TestMethod]
    public void ScriptBrickEnumeratorTest()
    {
      var project = SampleLoader.LoadSampleXML("simple");

      {
        Sprite sprite = project.SpriteList.Sprites[0];
        ScriptBrickCollection collection = new ScriptBrickCollection();
        collection.Update(sprite);
        IEnumerator enumerator = collection.GetEnumerator();

        enumerator.MoveNext();
        Assert.IsTrue(enumerator.Current is StartScript);
        enumerator.MoveNext();
        Assert.IsTrue(enumerator.Current is SetCostumeBrick);
      }

      {
        Sprite sprite = project.SpriteList.Sprites[1];
        ScriptBrickCollection collection = new ScriptBrickCollection();
        collection.Update(sprite);
        IEnumerator enumerator = collection.GetEnumerator();

        enumerator.MoveNext();
        Assert.IsTrue(enumerator.Current is StartScript);
        enumerator.MoveNext();
        Assert.IsTrue(enumerator.Current is SetCostumeBrick);

        enumerator.MoveNext();
        Assert.IsTrue(enumerator.Current is WhenScript);
        enumerator.MoveNext();
        Assert.IsTrue(enumerator.Current is SetCostumeBrick);
        enumerator.MoveNext();
        Assert.IsTrue(enumerator.Current is WaitBrick);
        enumerator.MoveNext();
        Assert.IsTrue(enumerator.Current is SetCostumeBrick);
        enumerator.MoveNext();
        Assert.IsTrue(enumerator.Current is WaitBrick);
        enumerator.MoveNext();
        Assert.IsTrue(enumerator.Current is SetCostumeBrick);
      }
    }

    [TestMethod]
    public void ScriptBrickCollectionRemoveTest()
    {
      var project = SampleLoader.LoadSampleXML("simple");

      Sprite sprite = project.SpriteList.Sprites[1];
      ScriptBrickCollection collection = new ScriptBrickCollection();
      collection.Update(sprite);
      IEnumerator enumerator = collection.GetEnumerator();

      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is StartScript);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is SetCostumeBrick);

      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is WhenScript);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is SetCostumeBrick);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is WaitBrick);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is SetCostumeBrick);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is WaitBrick);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is SetCostumeBrick);


      collection.RemoveAt(4);
      collection.Remove(enumerator.Current);

      enumerator.Reset();
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is StartScript);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is SetCostumeBrick);

      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is WhenScript);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is SetCostumeBrick);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is SetCostumeBrick);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is WaitBrick);

      Assert.IsFalse(enumerator.MoveNext());
    }

    [TestMethod]
    public void ScriptBrickCollectionInsertTest()
    {
      var project = SampleLoader.LoadSampleXML("simple");

      Sprite sprite = project.SpriteList.Sprites[1];
      ScriptBrickCollection collection = new ScriptBrickCollection();
      collection.Update(sprite);
      IEnumerator enumerator = collection.GetEnumerator();

      Brick newBrick1 = new ChangeGhostEffectBrick();
      collection.Insert(4, newBrick1);
      Brick newBrick2 = new ChangeXByBrick();
      collection.Insert(4, newBrick2);

      //Script newScript1 = new BroadcastScript(); // TODO: test adding scripts
      //collection.Insert(5, newScript1);

      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is StartScript);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is SetCostumeBrick);

      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is WhenScript);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is SetCostumeBrick);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is ChangeXByBrick);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is ChangeGhostEffectBrick);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is WaitBrick);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is SetCostumeBrick);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is WaitBrick);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is SetCostumeBrick);

      Assert.IsFalse(enumerator.MoveNext());
    }

    [TestMethod]
    public void ScriptBrickCollectionAddTest()
    {
      var project = SampleLoader.LoadSampleXML("simple");

      Sprite sprite = project.SpriteList.Sprites[1];
      ScriptBrickCollection collection = new ScriptBrickCollection();
      collection.Update(sprite);

      Script newScript1 = new BroadcastScript();
      collection.Add(newScript1);

      Brick newBrick1 = new ChangeGhostEffectBrick();
      collection.Add(newBrick1);
      Brick newBrick2 = new ChangeXByBrick();
      collection.Add(newBrick2);

      Script newScript2 = new WhenScript();
      collection.Add(newScript2);

      IEnumerator enumerator = collection.GetEnumerator();

      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is StartScript);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is SetCostumeBrick);

      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is WhenScript);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is SetCostumeBrick);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is WaitBrick);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is SetCostumeBrick);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is WaitBrick);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is SetCostumeBrick);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is BroadcastScript);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is ChangeGhostEffectBrick);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is ChangeXByBrick);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is WhenScript);

      Assert.IsFalse(enumerator.MoveNext());
    }


    [TestMethod]
    public void ScriptBrickCollectionContainsTest()
    {
      var project = SampleLoader.LoadSampleXML("simple");

      Sprite sprite = project.SpriteList.Sprites[1];
      ScriptBrickCollection collection = new ScriptBrickCollection();
      collection.Update(sprite);
      IEnumerator enumerator = collection.GetEnumerator();

      while (enumerator.MoveNext())
        Assert.IsTrue(collection.Contains(enumerator.Current));

      Brick notContainedBrick = new BroadcastBrick();
      Assert.IsFalse(collection.Contains(notContainedBrick));

      Script notContainedScript = new BroadcastScript();
      Assert.IsFalse(collection.Contains(notContainedScript));
    }

    [TestMethod]
    public void ScriptBrickCollectionIndexIfTest()
    {
      var project = SampleLoader.LoadSampleXML("simple");

      Sprite sprite = project.SpriteList.Sprites[1];
      ScriptBrickCollection collection = new ScriptBrickCollection();
      collection.Update(sprite);
      IEnumerator enumerator = collection.GetEnumerator();

      int referenceIndex = 0;
      while (enumerator.MoveNext())
      {
          Assert.AreEqual(referenceIndex, collection.IndexOf(enumerator.Current));
        referenceIndex++;
      }

      Brick notContainedBrick = new BroadcastBrick();
      Assert.AreEqual(-1, collection.IndexOf(notContainedBrick));

      Script notContainedScript = new BroadcastScript();
      Assert.AreEqual(-1, collection.IndexOf(notContainedScript));
    }

    [TestMethod]
    public void ScriptBrickCollectionAddBrickTest()
    {
      var project = SampleLoader.LoadSampleXML("simple");

      Sprite sprite = project.SpriteList.Sprites[1];
      ScriptBrickCollection collection = new ScriptBrickCollection();
      collection.Update(sprite);


      Brick insertedBrick1 = new ChangeBrightnessBrick();
      Brick insertedBrick2 = new MoveNStepsBrick();
      collection.AddScriptBrick(insertedBrick1, 4, 8);
      collection.AddScriptBrick(insertedBrick2, 1, 6);


      IEnumerator enumerator = collection.GetEnumerator();

      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is StartScript);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is SetCostumeBrick);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is MoveNStepsBrick);

      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is WhenScript);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is SetCostumeBrick);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is WaitBrick);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is SetCostumeBrick);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is WaitBrick);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is SetCostumeBrick);
      enumerator.MoveNext();
      Assert.IsTrue(enumerator.Current is ChangeBrightnessBrick);
      Assert.IsFalse(enumerator.MoveNext());
    }
  }
}
