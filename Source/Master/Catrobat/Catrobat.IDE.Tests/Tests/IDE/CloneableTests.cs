using System.Threading.Tasks;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Tests.Misc;
using Catrobat.IDE.Tests.SampleData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Tests.IDE
{
    [TestClass]
    public class CloneableTests
    {
        public static ITestProjectGenerator ProjectGenerator = new ProjectGeneratorForReferenceHelperTests();

        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task CloneSprite_UpdateCostumeTest()
        {
            var project = await SampleLoader.LoadSampleProject("default.catroid", "default");
            var oldSprite = project.Sprites[0];
            var newSprite = await oldSprite.CloneAsync(project);

            var oldCostume = oldSprite.Costumes[0];
            var newCostume = newSprite.Costumes[0];

            var oldCostumeBrick = oldSprite.Scripts[0].Bricks[0] as SetCostumeBrick;
            var newCostumeBrick = newSprite.Scripts[0].Bricks[0] as SetCostumeBrick;
            Assert.IsNotNull(oldCostumeBrick);
            Assert.IsNotNull(newCostumeBrick);
            Assert.AreNotEqual(oldCostumeBrick.Value, newCostumeBrick.Value);
            Assert.AreEqual(oldCostume, oldCostumeBrick.Value);
            Assert.AreEqual(newCostume, newCostumeBrick.Value);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task CloneSprite_UpdateSoundTest()
        {
            var project = await SampleLoader.LoadSampleProject("default.catroid", "default");
            var oldSprite = project.Sprites[1];
            var newSprite = await oldSprite.CloneAsync(project);

            var oldSound = oldSprite.Sounds[0];
            var newSound = newSprite.Sounds[0];

            var oldPlaySoundBrick = oldSprite.Scripts[0].Bricks[1] as PlaySoundBrick;
            var newPlaySoundBrick = newSprite.Scripts[0].Bricks[1] as PlaySoundBrick;
            Assert.IsNotNull(oldPlaySoundBrick);
            Assert.IsNotNull(newPlaySoundBrick);
            Assert.AreNotEqual(oldPlaySoundBrick.Value, newPlaySoundBrick.Value);
            Assert.AreEqual(oldSound, oldPlaySoundBrick.Value);
            Assert.AreEqual(newSound, newPlaySoundBrick.Value);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task CloneSprite_CopyVariableOnSpriteCopyTest()
        {
            var project = await SampleLoader.LoadSampleProject("default.catroid", "default");
            var oldSprite = project.Sprites[0];
            var newSprite = await oldSprite.CloneAsync(project);

            var oldLocalVariable = oldSprite.LocalVariables[0];
            var newLocalVariable = newSprite.LocalVariables[0];

            var oldBrick1 = oldSprite.Scripts[0].Bricks[1] as SetVariableBrick;
            var newBrick1 = newSprite.Scripts[0].Bricks[1] as SetVariableBrick;
            Assert.IsNotNull(oldBrick1);
            Assert.IsNotNull(newBrick1);
            Assert.AreNotSame(oldBrick1.Variable, newBrick1.Variable);
            Assert.AreSame(oldLocalVariable, oldBrick1.Variable);
            Assert.AreSame(newLocalVariable, newBrick1.Variable);


            oldSprite = project.Sprites[1];
            newSprite = await oldSprite.CloneAsync(project);
            Assert.IsNotNull(newSprite);

            var globalVariable = project.GlobalVariables[0];

            var oldBrick2 = oldSprite.Scripts[1].Bricks[5] as ChangeVariableBrick;
            var newBrick2 = newSprite.Scripts[1].Bricks[5] as ChangeVariableBrick;
            Assert.IsNotNull(oldBrick2);
            Assert.IsNotNull(newBrick2);
            Assert.AreSame(oldBrick2.Variable, newBrick2.Variable);
            Assert.AreSame(globalVariable, oldBrick2.Variable);
            Assert.AreSame(globalVariable, newBrick2.Variable);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task CloneSprite_UpdateLoopBeginBrickTest()
        {
            var project = await SampleLoader.LoadSampleProject("default.catroid", "default");
            var oldSprite = project.Sprites[1];
            var newSprite = await oldSprite.CloneAsync(project);

            var oldBlockBeginBrick = oldSprite.Scripts[0].Bricks[3] as BlockBeginBrick;
            var newBlockBeginBrick = newSprite.Scripts[0].Bricks[3] as BlockBeginBrick;

            var oldBrick1 = oldSprite.Scripts[0].Bricks[4] as BlockEndBrick;
            var newBrick1 = newSprite.Scripts[0].Bricks[4] as BlockEndBrick;
            Assert.IsNotNull(oldBrick1);
            Assert.IsNotNull(newBrick1);
            Assert.AreNotEqual(oldBrick1.Begin, newBrick1.Begin);
            Assert.AreEqual(oldBlockBeginBrick, oldBrick1.Begin);
            Assert.AreEqual(newBlockBeginBrick, newBrick1.Begin);


            oldBlockBeginBrick = oldSprite.Scripts[0].Bricks[5] as BlockBeginBrick;
            newBlockBeginBrick = newSprite.Scripts[0].Bricks[5] as BlockBeginBrick;

            var oldBrick2 = oldSprite.Scripts[0].Bricks[6] as BlockEndBrick;
            var newBrick2 = newSprite.Scripts[0].Bricks[6] as BlockEndBrick;
            Assert.IsNotNull(oldBrick2);
            Assert.IsNotNull(newBrick2);
            Assert.AreNotEqual(oldBrick2.Begin, newBrick2.Begin);
            Assert.AreEqual(oldBlockBeginBrick, oldBrick2.Begin);
            Assert.AreEqual(newBlockBeginBrick, newBrick2.Begin);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task CloneSprite_UpdateBlockEndBrickTest()
        {
            var project = await SampleLoader.LoadSampleProject("default.catroid", "default");
            var oldSprite = project.Sprites[1];
            var newSprite = await oldSprite.CloneAsync(project);

            var oldForeverBrick = oldSprite.Scripts[0].Bricks[3] as ForeverBrick;
            var newForeverBrick = newSprite.Scripts[0].Bricks[3] as ForeverBrick;
            var oldEndBlockBrick = oldSprite.Scripts[0].Bricks[4] as BlockEndBrick;
            var newEndBlockBrick = newSprite.Scripts[0].Bricks[4] as BlockEndBrick;
            Assert.IsNotNull(oldForeverBrick);
            Assert.IsNotNull(newForeverBrick);
            
            Assert.AreNotSame(oldForeverBrick.End, newForeverBrick.End);
            Assert.AreSame(oldEndBlockBrick, oldForeverBrick.End);
            Assert.AreSame(newEndBlockBrick, newForeverBrick.End);


            var oldBrick2 = oldSprite.Scripts[0].Bricks[5] as RepeatBrick;
            var newBrick2 = newSprite.Scripts[0].Bricks[5] as RepeatBrick;
            oldEndBlockBrick = oldSprite.Scripts[0].Bricks[6] as BlockEndBrick;
            newEndBlockBrick = newSprite.Scripts[0].Bricks[6] as BlockEndBrick;
            Assert.IsNotNull(oldBrick2);
            Assert.IsNotNull(newBrick2);
            
            Assert.AreNotEqual(oldBrick2.End, newBrick2.End);
            Assert.AreEqual(oldEndBlockBrick, oldBrick2.End);
            Assert.AreEqual(newEndBlockBrick, newBrick2.End);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task CloneSprite_UpdateIfBrickTest()
        {
            var project = await SampleLoader.LoadSampleProject("default.catroid", "default");
            var oldSprite = project.Sprites[0];
            var newSprite = await oldSprite.CloneAsync(project);

            var oldIfBrick1 = oldSprite.Scripts[0].Bricks[3] as IfBrick;
            var newIfBrick1 = newSprite.Scripts[0].Bricks[3] as IfBrick;

            var oldElseBrick1 = oldSprite.Scripts[0].Bricks[4] as ElseBrick;
            var newElseBrick1 = newSprite.Scripts[0].Bricks[4] as ElseBrick;
            Assert.IsNotNull(oldElseBrick1);
            Assert.IsNotNull(newElseBrick1);
            Assert.AreNotEqual(oldElseBrick1.Begin, newElseBrick1.Begin);
            Assert.AreEqual(oldIfBrick1, oldElseBrick1.Begin);
            Assert.AreEqual(newIfBrick1, newElseBrick1.Begin);

            var oldEndIfBrick1 = oldSprite.Scripts[0].Bricks[5] as EndIfBrick;
            var newEndIfBrick1 = newSprite.Scripts[0].Bricks[5] as EndIfBrick;
            Assert.IsNotNull(oldEndIfBrick1);
            Assert.IsNotNull(newEndIfBrick1);
            Assert.AreNotEqual(oldEndIfBrick1.Begin, newEndIfBrick1.Begin);
            Assert.AreEqual(oldIfBrick1, oldEndIfBrick1.Begin);
            Assert.AreEqual(newIfBrick1, newEndIfBrick1.Begin);



            oldIfBrick1 = oldSprite.Scripts[0].Bricks[2] as IfBrick;
            newIfBrick1 = newSprite.Scripts[0].Bricks[2] as IfBrick;

            oldElseBrick1 = oldSprite.Scripts[0].Bricks[6] as ElseBrick;
            newElseBrick1 = newSprite.Scripts[0].Bricks[6] as ElseBrick;
            Assert.IsNotNull(oldElseBrick1);
            Assert.IsNotNull(newElseBrick1);
            Assert.AreNotEqual(oldElseBrick1.Begin, newElseBrick1.Begin);
            Assert.AreEqual(oldIfBrick1, oldElseBrick1.Begin);
            Assert.AreEqual(newIfBrick1, newElseBrick1.Begin);

            oldEndIfBrick1 = oldSprite.Scripts[0].Bricks[7] as EndIfBrick;
            newEndIfBrick1 = newSprite.Scripts[0].Bricks[7] as EndIfBrick;
            Assert.IsNotNull(oldEndIfBrick1);
            Assert.IsNotNull(newEndIfBrick1);
            Assert.AreNotEqual(oldEndIfBrick1.Begin, newEndIfBrick1.Begin);
            Assert.AreEqual(oldIfBrick1, oldEndIfBrick1.Begin);
            Assert.AreEqual(newIfBrick1, newEndIfBrick1.Begin);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task CloneSprite_UpdateElseBrickTest()
        {
            var project = await SampleLoader.LoadSampleProject("default.catroid", "default");
            var oldSprite = project.Sprites[0];
            var newSprite = await oldSprite.CloneAsync(project);

            var oldElseBrick1 = oldSprite.Scripts[0].Bricks[6] as ElseBrick;
            var newElseBrick1 = newSprite.Scripts[0].Bricks[6] as ElseBrick;

            var oldIfBrick1 = oldSprite.Scripts[0].Bricks[2] as IfBrick;
            var newIfBrick1 = newSprite.Scripts[0].Bricks[2] as IfBrick;
            Assert.IsNotNull(oldIfBrick1);
            Assert.IsNotNull(newIfBrick1);
            Assert.AreNotEqual(oldIfBrick1.Else, newIfBrick1.Else);
            Assert.AreEqual(oldElseBrick1, oldIfBrick1.Else);
            Assert.AreEqual(newElseBrick1, newIfBrick1.Else);

            var oldEndIfBrick1 = oldSprite.Scripts[0].Bricks[7] as EndIfBrick;
            var newEndIfBrick1 = newSprite.Scripts[0].Bricks[7] as EndIfBrick;
            Assert.IsNotNull(oldEndIfBrick1);
            Assert.IsNotNull(newEndIfBrick1);
            Assert.AreNotEqual(oldEndIfBrick1.Else, newEndIfBrick1.Else);
            Assert.AreEqual(oldElseBrick1, oldEndIfBrick1.Else);
            Assert.AreEqual(newElseBrick1, newEndIfBrick1.Else);



            oldElseBrick1 = oldSprite.Scripts[0].Bricks[4] as ElseBrick;
            newElseBrick1 = newSprite.Scripts[0].Bricks[4] as ElseBrick;

            oldIfBrick1 = oldSprite.Scripts[0].Bricks[3] as IfBrick;
            newIfBrick1 = newSprite.Scripts[0].Bricks[3] as IfBrick;
            Assert.IsNotNull(oldIfBrick1);
            Assert.IsNotNull(newIfBrick1);
            Assert.AreNotEqual(oldIfBrick1.Else, newIfBrick1.Else);
            Assert.AreEqual(oldElseBrick1, oldIfBrick1.Else);
            Assert.AreEqual(newElseBrick1, newIfBrick1.Else);

            oldEndIfBrick1 = oldSprite.Scripts[0].Bricks[5] as EndIfBrick;
            newEndIfBrick1 = newSprite.Scripts[0].Bricks[5] as EndIfBrick;
            Assert.IsNotNull(oldEndIfBrick1);
            Assert.IsNotNull(newEndIfBrick1);
            Assert.AreNotEqual(oldEndIfBrick1.Else, newEndIfBrick1.Else);
            Assert.AreEqual(oldElseBrick1, oldEndIfBrick1.Else);
            Assert.AreEqual(newElseBrick1, newEndIfBrick1.Else);
        }

        [TestMethod, TestCategory("GatedTests.Obsolete")]
        public async Task CloneSprite_UpdateEndIfBrickTest()
        {
            var project = await SampleLoader.LoadSampleProject("default.catroid", "default");
            var oldSprite = project.Sprites[0];
            var newSprite = await oldSprite.CloneAsync(project);

            var oldEndIfBrick1 = oldSprite.Scripts[0].Bricks[5] as EndIfBrick;
            var newEndIfBrick1 = newSprite.Scripts[0].Bricks[5] as EndIfBrick;

            var oldIfBrick1 = oldSprite.Scripts[0].Bricks[3] as IfBrick;
            var newIfBrick1 = newSprite.Scripts[0].Bricks[3] as IfBrick;
            Assert.IsNotNull(oldIfBrick1);
            Assert.IsNotNull(newIfBrick1);
            Assert.AreNotEqual(oldIfBrick1.End, newIfBrick1.End);
            Assert.AreEqual(oldEndIfBrick1, oldIfBrick1.End);
            Assert.AreEqual(newEndIfBrick1, newIfBrick1.End);

            var oldElseBrick1 = oldSprite.Scripts[0].Bricks[4] as ElseBrick;
            var newElseBrick1 = newSprite.Scripts[0].Bricks[4] as ElseBrick;
            Assert.IsNotNull(oldElseBrick1);
            Assert.IsNotNull(newElseBrick1);
            Assert.AreNotEqual(oldElseBrick1.End, newElseBrick1.End);
            Assert.AreEqual(oldEndIfBrick1, oldElseBrick1.End);
            Assert.AreEqual(newEndIfBrick1, newElseBrick1.End);



            oldEndIfBrick1 = oldSprite.Scripts[0].Bricks[7] as EndIfBrick;
            newEndIfBrick1 = newSprite.Scripts[0].Bricks[7] as EndIfBrick;

            oldIfBrick1 = oldSprite.Scripts[0].Bricks[2] as IfBrick;
            newIfBrick1 = newSprite.Scripts[0].Bricks[2] as IfBrick;
            Assert.IsNotNull(oldIfBrick1);
            Assert.IsNotNull(newIfBrick1);
            Assert.AreNotEqual(oldIfBrick1.End, newIfBrick1.End);
            Assert.AreEqual(oldEndIfBrick1, oldIfBrick1.End);
            Assert.AreEqual(newEndIfBrick1, newIfBrick1.End);

            oldElseBrick1 = oldSprite.Scripts[0].Bricks[6] as ElseBrick;
            newElseBrick1 = newSprite.Scripts[0].Bricks[6] as ElseBrick;
            Assert.IsNotNull(oldElseBrick1);
            Assert.IsNotNull(newElseBrick1);
            Assert.AreNotEqual(oldElseBrick1.End, newElseBrick1.End);
            Assert.AreEqual(oldEndIfBrick1, oldElseBrick1.End);
            Assert.AreEqual(newEndIfBrick1, newElseBrick1.End);
        }
    }
}
