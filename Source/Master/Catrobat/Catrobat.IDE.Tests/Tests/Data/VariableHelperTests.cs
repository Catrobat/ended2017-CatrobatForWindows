using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Tests.Misc;

namespace Catrobat.IDE.Tests.Tests.Data
{
    [TestClass]
    public class VariableHelperTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }

        [TestMethod]
        public void GetGlobalVariableListTest()
        {
            IProjectGenerator projectgenerator = new ProjectGeneratorReflection();
            var project1 = projectgenerator.GenerateProject();
            var globalVariableList = VariableHelper.GetGlobalVariableList(project1);

            Assert.AreEqual(3, globalVariableList.Count);

            for (int i = 0; i < globalVariableList.Count; i++)
                Assert.AreEqual("GlobalTestVariable" + i, globalVariableList[i].Name);
        }

        [TestMethod]
        public void GetLocalVariableListTest()
        {
            IProjectGenerator projectgenerator = new ProjectGeneratorReflection();
            var project1 = projectgenerator.GenerateProject();

            foreach (var sprite in project1.SpriteList.Sprites)
            {
                var localVariableList = VariableHelper.GetLocalVariableList(project1, sprite);
                Assert.AreEqual(1, localVariableList.Count);
                Assert.AreEqual("LocalTestVariable", localVariableList[0].Name);
            }   
        }

        [TestMethod]
        public void DeleteGlobalVariableTest()
        {
            IProjectGenerator projectgenerator = new ProjectGeneratorReflection();
            var project1 = projectgenerator.GenerateProject();
            var globalVariableList = VariableHelper.GetGlobalVariableList(project1);

            VariableHelper.DeleteGlobalVariable(project1, globalVariableList[2]);
            
            globalVariableList = VariableHelper.GetGlobalVariableList(project1);

            Assert.AreEqual(2, globalVariableList.Count);

            for (int i = 0; i < globalVariableList.Count; i++)
                Assert.AreEqual("GlobalTestVariable" + i, globalVariableList[i].Name);
        }

        [TestMethod]
        public void DeleteLocalVariableTest()
        {
            IProjectGenerator projectgenerator = new ProjectGeneratorReflection();
            var project1 = projectgenerator.GenerateProject();

            foreach (var sprite in project1.SpriteList.Sprites)
            {
                var localVariableList = VariableHelper.GetLocalVariableList(project1, sprite);

                VariableHelper.DeleteLocalVariable(project1, sprite, localVariableList[0]);

                localVariableList = VariableHelper.GetLocalVariableList(project1, sprite);

                Assert.AreEqual(0, localVariableList.Count);
            }
        }

        [TestMethod]
        public void AddGlobalVariableTest()
        {
            IProjectGenerator projectgenerator = new ProjectGeneratorReflection();
            var project1 = projectgenerator.GenerateProject();

            var newUserVariable = new UserVariable
                {
                    Name = "NewUserVariable"
                };

            VariableHelper.AddGlobalVariable(project1, newUserVariable);

            Assert.IsTrue(VariableHelper.GetGlobalVariableList(project1).Contains(newUserVariable));
        }

        [TestMethod]
        public void AddLocalVariableTest()
        {
            IProjectGenerator projectgenerator = new ProjectGeneratorReflection();
            var project1 = projectgenerator.GenerateProject();

            foreach (var sprite in project1.SpriteList.Sprites)
            {
                var newUserVariable = new UserVariable
                    {
                        Name = "NewUserVariable"
                    };

                VariableHelper.AddLocalVariable(project1, sprite, newUserVariable);

                Assert.IsTrue(VariableHelper.GetLocalVariableList(project1,sprite).Contains(newUserVariable));
            }
        }

        [TestMethod]
        public void IsVariableLocalTest()
        {
            IProjectGenerator projectgenerator = new ProjectGeneratorReflection();
            var project1 = projectgenerator.GenerateProject();
            var localVariable = project1.VariableList.ObjectVariableList.ObjectVariableEntries[0].VariableList.UserVariables[0];

            Assert.IsNotNull(localVariable);
            Assert.IsTrue(VariableHelper.IsVariableLocal(project1, localVariable));
        }

        [TestMethod]
        public void CreateUniqueGlobalVariableTest()
        {
            IProjectGenerator projectgenerator = new ProjectGeneratorReflection();
            var project1 = projectgenerator.GenerateProject();

            for (int i = 0; i < 20; i++)
            {
                var globalVariable = VariableHelper.CreateUniqueGlobalVariable();
                var globalVariables = VariableHelper.GetGlobalVariableList(project1);

                Assert.IsFalse(globalVariables.Contains(globalVariable));

                globalVariables.Add(globalVariable);
            }
        }

        [TestMethod]
        public void CreateUniqueLocalVariableTest()
        {
            IProjectGenerator projectgenerator = new ProjectGeneratorReflection();
            var project1 = projectgenerator.GenerateProject();
            var sprite1 = project1.SpriteList.Sprites[0];

            for (int i = 0; i < 20; i++)
            {
                var localVariable = VariableHelper.CreateUniqueLocalVariable(sprite1);
                var localVariables = VariableHelper.GetLocalVariableList(project1, sprite1);

                Assert.IsFalse(localVariables.Contains(localVariable));

                localVariables.Add(localVariable);
            }
        }

        [TestMethod]
        public void VariableNameExistsTest()
        {
            IProjectGenerator projectgenerator = new ProjectGeneratorReflection();
            var project1 = projectgenerator.GenerateProject();
            var existingGlobalVariableName = "GlobalTestVariable1";
            var notExistingGlobalVariableName = "test2";

            var sprite1 = project1.SpriteList.Sprites[0];
            Assert.IsTrue(VariableHelper.VariableNameExists(project1, sprite1, existingGlobalVariableName));
            Assert.IsFalse(VariableHelper.VariableNameExists(project1, sprite1, notExistingGlobalVariableName));

            var notExistingLocalVariableName = "test1";
            foreach (var sprite in project1.SpriteList.Sprites)
            {
                var existingLocalVariableName = "LocalTestVariable";

                Assert.IsTrue(VariableHelper.VariableNameExists(project1, sprite, existingLocalVariableName));
                Assert.IsFalse(VariableHelper.VariableNameExists(project1, sprite, notExistingLocalVariableName));
            }
        }

        [TestMethod]
        public void VariableNameExistsCheckSelfTest()
        {
            IProjectGenerator projectgenerator = new ProjectGeneratorReflection();
            var project1 = projectgenerator.GenerateProject();
            var globalVariable = VariableHelper.GetGlobalVariableList(project1)[0];
            var sameGlobalVariableName = "GlobalTestVariable0";
            var existingGlobalVariableName = "GlobalTestVariable1";
            var notExistingGlobalVariableName = "test2";

            var sprite1 = project1.SpriteList.Sprites[0];
            Assert.IsTrue(VariableHelper.VariableNameExistsCheckSelf(project1, sprite1, globalVariable, existingGlobalVariableName));
            Assert.IsFalse(VariableHelper.VariableNameExistsCheckSelf(project1, sprite1, globalVariable, notExistingGlobalVariableName));
            Assert.IsFalse(VariableHelper.VariableNameExistsCheckSelf(project1, sprite1, globalVariable, sameGlobalVariableName));

            var notExistingLocalVariableName = "test1";
            foreach (var sprite in project1.SpriteList.Sprites)
            {
                foreach (var localVariable in VariableHelper.GetLocalVariableList(project1, sprite))
                {
                    var existingLocalVariableName = "LocalTestVariable";

                    if(localVariable.Name != existingLocalVariableName)
                        Assert.IsTrue(VariableHelper.VariableNameExistsCheckSelf(project1, sprite, localVariable, existingLocalVariableName));
                    else
                        Assert.IsFalse(VariableHelper.VariableNameExistsCheckSelf(project1, sprite, localVariable, existingLocalVariableName));
 
                    Assert.IsFalse(VariableHelper.VariableNameExistsCheckSelf(project1, sprite, localVariable, notExistingLocalVariableName));
                }
            }

        }
    }
}
