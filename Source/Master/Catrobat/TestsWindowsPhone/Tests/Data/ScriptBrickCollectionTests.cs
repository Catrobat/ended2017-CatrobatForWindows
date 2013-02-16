using System.Collections;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Catrobat.IDEWindowsPhone.Views.Editor.Scripts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestsWindowsPhone.SampleData;

namespace Catrobat.TestsWindowsPhone.Tests.Data
{
  [TestClass]
  public class ScriptBrickCollectionTests
  {
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

      Brick newBrick1 = new ChangeGhostEffectBrick(sprite);
      collection.Insert(4, newBrick1);
      Brick newBrick2 = new ChangeXByBrick(sprite);
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

      Script newScript1 = new BroadcastScript(sprite);
      collection.Add(newScript1);

      Brick newBrick1 = new ChangeGhostEffectBrick(sprite);
      collection.Add(newBrick1);
      Brick newBrick2 = new ChangeXByBrick(sprite);
      collection.Add(newBrick2);

      Script newScript2 = new WhenScript(sprite);
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

      Brick notContainedBrick = new BroadcastBrick(sprite);
      Assert.IsFalse(collection.Contains(notContainedBrick));

      Script notContainedScript = new BroadcastScript(sprite);
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
        Assert.AreEqual(collection.IndexOf(enumerator.Current), referenceIndex);
        referenceIndex++;
      }

      Brick notContainedBrick = new BroadcastBrick(sprite);
      Assert.AreEqual(collection.IndexOf(notContainedBrick), -1);

      Script notContainedScript = new BroadcastScript(sprite);
      Assert.AreEqual(collection.IndexOf(notContainedScript), -1);
    }

    [TestMethod]
    public void ScriptBrickCollectionAddBrickTest()
    {
      var project = SampleLoader.LoadSampleXML("simple");

      Sprite sprite = project.SpriteList.Sprites[1];
      ScriptBrickCollection collection = new ScriptBrickCollection();
      collection.Update(sprite);


      Brick insertedBrick1 = new ChangeBrightnessBrick(sprite);
      Brick insertedBrick2 = new MoveNStepsBrick(sprite);
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
