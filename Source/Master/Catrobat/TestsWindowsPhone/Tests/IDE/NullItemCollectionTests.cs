using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using Catrobat.IDEWindowsPhone.Controls.ListPicker;
using Catrobat.IDEWindowsPhone.Converters;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Catrobat.TestsWindowsPhone.Tests.IDE
{
  [TestClass]
  public class NullItemCollectionTests
  {
    [TestMethod]
    public void CountTest()
    {
      var sourceCollection = new ObservableCollection<string> { "Item1", "Item2" };
      const string nullObject = "NullObject";

      var collection = new NullItemCollection { NullObject = nullObject, SourceCollection = sourceCollection };

      Assert.AreEqual(3, collection.Count);
    }

    [TestMethod]
    public void AddTest()
    {
      var sourceCollection = new ObservableCollection<string> { "Item1", "Item2" };
      const string nullObject = "NullObject";
      const string newObject = "NewObject";

      var collection = new NullItemCollection { NullObject = nullObject, SourceCollection = sourceCollection };

      Assert.AreEqual(3,collection.Add(newObject));
      Assert.AreEqual(newObject, sourceCollection[2]);
    }

    [TestMethod]
    public void ClearTest()
    {
      var sourceCollection = new ObservableCollection<string> { "Item1", "Item2" };
      const string nullObject = "NullObject";

      var collection = new NullItemCollection { NullObject = nullObject, SourceCollection = sourceCollection };

      collection.Clear();
      Assert.AreEqual(0, sourceCollection.Count);
    }

    [TestMethod]
    public void ContainsTest()
    {
      var sourceCollection = new ObservableCollection<string> { "Item1", "Item2" };
      const string nullObject = "NullObject";
      const string newObject = "NewObject";

      var collection = new NullItemCollection { NullObject = nullObject, SourceCollection = sourceCollection };

      Assert.IsFalse(collection.Contains(newObject));
      Assert.IsTrue(collection.Contains(sourceCollection[0]));
      Assert.IsTrue(collection.Contains(sourceCollection[1]));
    }

    [TestMethod]
    public void IndexOfTest()
    {
      var sourceCollection = new ObservableCollection<string> {"Item1", "Item2"};
      const string nullObject = "NullObject";

      var collection = new NullItemCollection {NullObject = nullObject, SourceCollection = sourceCollection};

      Assert.AreEqual(0, collection.IndexOf(nullObject));
      Assert.AreEqual(1, collection.IndexOf(sourceCollection[0]));
      Assert.AreEqual(2, collection.IndexOf(sourceCollection[1]));
    }

    [TestMethod]
    public void InsertTest()
    {
      var sourceCollection = new ObservableCollection<string> { "Item1", "Item2" };
      const string nullObject = "NullObject";
      const string newObject1 = "NewObject1";
      const string newObject2 = "NewObject2";

      var collection = new NullItemCollection { NullObject = nullObject, SourceCollection = sourceCollection };
      collection.Insert(1, newObject1);
      collection.Insert(4, newObject2);


      Assert.AreEqual(0, sourceCollection.IndexOf(newObject1));
      Assert.AreEqual(3, sourceCollection.IndexOf(newObject2));
    }

    [TestMethod]
    public void RemoveTest()
    {
      var sourceCollection = new ObservableCollection<string> { "Item1", "Item2", "Item3" };
      const string item4 = "Item4";
      const string item5 = "Item5";
      sourceCollection.Add(item4);
      sourceCollection.Add(item5);

      const string nullObject = "NullObject";

      var collection = new NullItemCollection { NullObject = nullObject, SourceCollection = sourceCollection };
      collection.Remove(item4);

      Assert.IsFalse(sourceCollection.Contains(item4));
    }

    [TestMethod]
    public void RemoveAtTest()
    {
      var sourceCollection = new ObservableCollection<string> { "Item1", "Item2", "Item3" };
      const string item4 = "Item4";
      const string item5 = "Item5";
      sourceCollection.Add(item4);
      sourceCollection.Add(item5);

      const string nullObject = "NullObject";

      var collection = new NullItemCollection { NullObject = nullObject, SourceCollection = sourceCollection };
      collection.RemoveAt(4);

      Assert.IsFalse(sourceCollection.Contains(item4));
    }

    [TestMethod]
    public void IndexOperatorTest()
    {
      var sourceCollection = new ObservableCollection<string> { "Item1", "Item2", "Item3" };
      const string item4 = "Item4";
      const string item5 = "Item5";
      sourceCollection.Add(item4);
      sourceCollection.Add(item5);

      const string nullObject = "NullObject";

      var collection = new NullItemCollection { NullObject = nullObject, SourceCollection = sourceCollection };
      collection.RemoveAt(4);

      Assert.AreEqual(sourceCollection[3], collection[4]);
      collection[1] = sourceCollection[3];
      Assert.AreEqual(sourceCollection[3], collection[1]);
    }
  }
}
