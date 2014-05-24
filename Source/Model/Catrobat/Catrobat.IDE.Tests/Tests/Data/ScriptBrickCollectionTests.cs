using System.Collections;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Scripts;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Tests.Misc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Tests.Data
{
    [TestClass]
    public class ScriptBrickCollectionTests
    {
        [TestInitialize]
        public void Initialize()
        {
            TestHelper.InitializeTests();
        }

        [TestMethod, TestCategory("GatedTests")]
        public void ScriptBrickEnumeratorTest()
        {
            ITestProjectGenerator projectGenerator = new ProjectGeneratorForScriptBrickCollectionTests();
            var project = projectGenerator.GenerateProject();


            {
                var sprite = project.Sprites[0];
                var collection = new ScriptBrickCollection();
                collection.Update(sprite);
                IEnumerator enumerator = collection.GetEnumerator();

                enumerator.MoveNext();
                Assert.IsTrue(enumerator.Current is StartScript);
                enumerator.MoveNext();
                Assert.IsTrue(enumerator.Current is SetCostumeBrick);
            }

            {
                var sprite = project.Sprites[1];
                var collection = new ScriptBrickCollection();
                collection.Update(sprite);
                IEnumerator enumerator = collection.GetEnumerator();

                enumerator.MoveNext();
                Assert.IsTrue(enumerator.Current is StartScript);
                enumerator.MoveNext();
                Assert.IsTrue(enumerator.Current is SetCostumeBrick);

                enumerator.MoveNext();
                Assert.IsTrue(enumerator.Current is TappedScript);
                enumerator.MoveNext();
                Assert.IsTrue(enumerator.Current is SetCostumeBrick);
                enumerator.MoveNext();
                Assert.IsTrue(enumerator.Current is DelayBrick);
                enumerator.MoveNext();
                Assert.IsTrue(enumerator.Current is SetCostumeBrick);
                enumerator.MoveNext();
                Assert.IsTrue(enumerator.Current is DelayBrick);
                enumerator.MoveNext();
                Assert.IsTrue(enumerator.Current is SetCostumeBrick);

                enumerator.MoveNext();
                Assert.IsTrue(enumerator.Current is EmptyDummyBrick);

                Assert.IsFalse(enumerator.MoveNext());
            }
        }

        [TestMethod, TestCategory("GatedTests")]
        public void ScriptBrickCollectionRemoveTest()
        {
            ITestProjectGenerator projectGenerator = new ProjectGeneratorForScriptBrickCollectionTests();
            var project = projectGenerator.GenerateProject();

            var sprite = project.Sprites[1];
            var collection = new ScriptBrickCollection();
            collection.Update(sprite);
            IEnumerator enumerator = collection.GetEnumerator();

            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is StartScript);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);

            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is TappedScript);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is DelayBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is DelayBrick);
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
            Assert.IsTrue(enumerator.Current is TappedScript);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is DelayBrick);

            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is EmptyDummyBrick);

            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod, TestCategory("GatedTests")]
        public void ScriptBrickCollectionInsertTest()
        {
            ITestProjectGenerator projectGenerator = new ProjectGeneratorForScriptBrickCollectionTests();
            var project = projectGenerator.GenerateProject();

            var sprite = project.Sprites[1];
            var collection = new ScriptBrickCollection();
            collection.Update(sprite);
            IEnumerator enumerator = collection.GetEnumerator();

            Brick newBrick1 = new ChangeTransparencyBrick();
            collection.Insert(4, newBrick1);
            Brick newBrick2 = new ChangePositionXBrick();
            collection.Insert(4, newBrick2);

            //Script newScript1 = new BroadcastScript(); // TODO: test adding scripts
            //collection.Insert(5, newScript1);

            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is StartScript);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);

            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is TappedScript);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is ChangePositionXBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is ChangeTransparencyBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is DelayBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is DelayBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);

            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is EmptyDummyBrick);

            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod, TestCategory("GatedTests")]
        public void ScriptBrickCollectionAddTest()
        {
            ITestProjectGenerator projectGenerator = new ProjectGeneratorForScriptBrickCollectionTests();
            var project = projectGenerator.GenerateProject();

            var sprite = project.Sprites[1];
            var collection = new ScriptBrickCollection();
            collection.Update(sprite);

            Script newScript1 = new BroadcastReceivedScript();
            collection.Add(newScript1);

            Brick newBrick1 = new ChangeTransparencyBrick();
            collection.Add(newBrick1);
            Brick newBrick2 = new ChangePositionXBrick();
            collection.Add(newBrick2);

            Script newScript2 = new TappedScript();
            collection.Add(newScript2);

            IEnumerator enumerator = collection.GetEnumerator();

            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is StartScript);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);

            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is TappedScript);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is DelayBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is DelayBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is BroadcastReceivedScript);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is ChangeTransparencyBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is ChangePositionXBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is TappedScript);

            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is EmptyDummyBrick);

            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod, TestCategory("GatedTests")]
        public void ScriptBrickCollectionContainsTest()
        {
            ITestProjectGenerator projectGenerator = new ProjectGeneratorForScriptBrickCollectionTests();
            var project = projectGenerator.GenerateProject();

            var sprite = project.Sprites[1];
            var collection = new ScriptBrickCollection();
            collection.Update(sprite);
            IEnumerator enumerator = collection.GetEnumerator();

            while (enumerator.MoveNext())
                if (!(enumerator.Current is EmptyDummyBrick))
                    Assert.IsTrue(collection.Contains(enumerator.Current));

            Brick notContainedBrick = new BroadcastSendBrick();
            Assert.IsFalse(collection.Contains(notContainedBrick));

            Script notContainedScript = new BroadcastReceivedScript();
            Assert.IsFalse(collection.Contains(notContainedScript));
        }

        [TestMethod, TestCategory("GatedTests")]
        public void ScriptBrickCollectionIndexIfTest()
        {
            ITestProjectGenerator projectGenerator = new ProjectGeneratorForScriptBrickCollectionTests();
            var project = projectGenerator.GenerateProject();

            var sprite = project.Sprites[1];
            var collection = new ScriptBrickCollection();
            collection.Update(sprite);
            IEnumerator enumerator = collection.GetEnumerator();

            var referenceIndex = 0;
            while (enumerator.MoveNext())
            {
                if (!(enumerator.Current is EmptyDummyBrick))
                    Assert.AreEqual(referenceIndex, collection.IndexOf(enumerator.Current));

                referenceIndex++;
            }

            Brick notContainedBrick = new BroadcastSendBrick();
            Assert.AreEqual(-1, collection.IndexOf(notContainedBrick));

            Script notContainedScript = new BroadcastReceivedScript();
            Assert.AreEqual(-1, collection.IndexOf(notContainedScript));
        }

        [TestMethod, TestCategory("GatedTests")]
        public void ScriptBrickCollectionAddBrickTest()
        {
            ITestProjectGenerator projectGenerator = new ProjectGeneratorForScriptBrickCollectionTests();
            var project = projectGenerator.GenerateProject();

            var sprite = project.Sprites[1];
            var collection = new ScriptBrickCollection();
            collection.Update(sprite);


            Brick insertedBrick1 = new ChangeBrightnessBrick();
            Brick insertedBrick2 = new MoveBrick();
            collection.AddScriptBrick(insertedBrick1, 4, 8);
            collection.AddScriptBrick(insertedBrick2, 1, 6);


            IEnumerator enumerator = collection.GetEnumerator();

            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is StartScript);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);
            //enumerator.MoveNext();
            //Assert.IsTrue(enumerator.Current is MoveNStepsBrick);

            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is TappedScript);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is DelayBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is DelayBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is ChangeBrightnessBrick);

            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is MoveBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is EmptyDummyBrick);

            Assert.IsFalse(enumerator.MoveNext());
        }
    }
}
