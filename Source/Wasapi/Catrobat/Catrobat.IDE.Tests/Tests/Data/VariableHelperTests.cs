using System.Linq;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Utilities.Helpers;
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

        [TestMethod,TestCategory("GatedTests")]
        public void GetGlobalVariableListTest()
        {
            ITestProjectGenerator projectgenerator = new ProjectGeneratorReflection();
            var project1 = projectgenerator.GenerateProject();
            var globalVariableList = VariableHelper.GetGlobalVariableList(project1);

            Assert.AreEqual(3, globalVariableList.Count);

            for (int i = 0; i < globalVariableList.Count; i++)
                Assert.AreEqual("GlobalTestVariable" + i, globalVariableList[i].Name);
        }

        [TestMethod,TestCategory("GatedTests")]
        public void GetLocalVariableListTest()
        {
            ITestProjectGenerator projectgenerator = new ProjectGeneratorReflection();
            var project1 = projectgenerator.GenerateProject();

            foreach (var sprite in project1.Sprites)
            {
                var localVariableList = VariableHelper.GetLocalVariableList(project1, sprite);
                Assert.AreEqual(1, localVariableList.Count);
                Assert.AreEqual("LocalTestVariable", localVariableList[0].Name);
            }   
        }

        [TestMethod,TestCategory("GatedTests")]
        public void DeleteGlobalVariableTest()
        {
            ITestProjectGenerator projectgenerator = new ProjectGeneratorReflection();
            var project1 = projectgenerator.GenerateProject();
            var globalVariableList = VariableHelper.GetGlobalVariableList(project1);

            VariableHelper.DeleteGlobalVariable(project1, globalVariableList[2]);
            
            globalVariableList = VariableHelper.GetGlobalVariableList(project1);

            Assert.AreEqual(2, globalVariableList.Count);

            for (int i = 0; i < globalVariableList.Count; i++)
                Assert.AreEqual("GlobalTestVariable" + i, globalVariableList[i].Name);
        }

        [TestMethod,TestCategory("GatedTests")]
        public void DeleteLocalVariableTest()
        {
            ITestProjectGenerator projectgenerator = new ProjectGeneratorReflection();
            var project1 = projectgenerator.GenerateProject();

            foreach (var sprite in project1.Sprites)
            {
                var localVariableList = VariableHelper.GetLocalVariableList(project1, sprite);

                VariableHelper.DeleteLocalVariable(project1, sprite, localVariableList[0]);

                localVariableList = VariableHelper.GetLocalVariableList(project1, sprite);

                Assert.AreEqual(0, localVariableList.Count);
            }
        }

        [TestMethod,TestCategory("GatedTests")]
        public void AddGlobalVariableTest()
        {
            ITestProjectGenerator projectgenerator = new ProjectGeneratorReflection();
            var project1 = projectgenerator.GenerateProject();

            var newUserVariable = new GlobalVariable
                {
                    Name = "NewUserVariable"
                };

            VariableHelper.AddGlobalVariable(project1, newUserVariable);

            Assert.IsTrue(VariableHelper.GetGlobalVariableList(project1).Contains(newUserVariable));
        }

        [TestMethod,TestCategory("GatedTests")]
        public void AddLocalVariableTest()
        {
            ITestProjectGenerator projectgenerator = new ProjectGeneratorReflection();
            var project1 = projectgenerator.GenerateProject();

            foreach (var sprite in project1.Sprites)
            {
                var newUserVariable = new LocalVariable
                    {
                        Name = "NewUserVariable"
                    };

                VariableHelper.AddLocalVariable(project1, sprite, newUserVariable);

                Assert.IsTrue(VariableHelper.GetLocalVariableList(project1,sprite).Contains(newUserVariable));
            }
        }

        [TestMethod,TestCategory("GatedTests")]
        public void IsVariableLocalTest()
        {
            ITestProjectGenerator projectgenerator = new ProjectGeneratorReflection();
            var project1 = projectgenerator.GenerateProject();
            var localVariable = project1.Sprites.SelectMany(sprite => sprite.LocalVariables).First();

            Assert.IsNotNull(localVariable);
            Assert.IsTrue(VariableHelper.IsVariableLocal(project1, localVariable));
        }

        [TestMethod,TestCategory("GatedTests")]
        public void CreateUniqueGlobalVariableTest()
        {
            ITestProjectGenerator projectgenerator = new ProjectGeneratorReflection();
            var project1 = projectgenerator.GenerateProject();

            for (int i = 0; i < 20; i++)
            {
                var globalVariable = VariableHelper.CreateUniqueGlobalVariable();
                var globalVariables = VariableHelper.GetGlobalVariableList(project1);

                Assert.IsFalse(globalVariables.Contains(globalVariable));

                globalVariables.Add(globalVariable);
            }
        }

        [TestMethod,TestCategory("GatedTests")]
        public void CreateUniqueLocalVariableTest()
        {
            ITestProjectGenerator projectgenerator = new ProjectGeneratorReflection();
            var project1 = projectgenerator.GenerateProject();
            var sprite1 = project1.Sprites[0];

            for (int i = 0; i < 20; i++)
            {
                var localVariable = VariableHelper.CreateUniqueLocalVariable(sprite1);
                var localVariables = VariableHelper.GetLocalVariableList(project1, sprite1);

                Assert.IsFalse(localVariables.Contains(localVariable));

                localVariables.Add(localVariable);
            }
        }

        [TestMethod,TestCategory("GatedTests")]
        public void VariableNameExistsTest()
        {
            ITestProjectGenerator projectgenerator = new ProjectGeneratorReflection();
            var project1 = projectgenerator.GenerateProject();
            var existingGlobalVariableName = "GlobalTestVariable1";
            var notExistingGlobalVariableName = "test2";

            var sprite1 = project1.Sprites[0];
            Assert.IsTrue(VariableHelper.VariableNameExists(project1, sprite1, existingGlobalVariableName));
            Assert.IsFalse(VariableHelper.VariableNameExists(project1, sprite1, notExistingGlobalVariableName));

            var notExistingLocalVariableName = "test1";
            foreach (var sprite in project1.Sprites)
            {
                var existingLocalVariableName = "LocalTestVariable";

                Assert.IsTrue(VariableHelper.VariableNameExists(project1, sprite, existingLocalVariableName));
                Assert.IsFalse(VariableHelper.VariableNameExists(project1, sprite, notExistingLocalVariableName));
            }
        }

        [TestMethod,TestCategory("GatedTests")]
        public void VariableNameExistsCheckSelfTest()
        {
            ITestProjectGenerator projectgenerator = new ProjectGeneratorReflection();
            var project1 = projectgenerator.GenerateProject();
            var globalVariable = VariableHelper.GetGlobalVariableList(project1)[0];
            var sameGlobalVariableName = "GlobalTestVariable0";
            var existingGlobalVariableName = "GlobalTestVariable1";
            var notExistingGlobalVariableName = "test2";

            var sprite1 = project1.Sprites[0];
            Assert.IsTrue(VariableHelper.VariableNameExistsCheckSelf(project1, sprite1, globalVariable, existingGlobalVariableName));
            Assert.IsFalse(VariableHelper.VariableNameExistsCheckSelf(project1, sprite1, globalVariable, notExistingGlobalVariableName));
            Assert.IsFalse(VariableHelper.VariableNameExistsCheckSelf(project1, sprite1, globalVariable, sameGlobalVariableName));

            var notExistingLocalVariableName = "test1";
            foreach (var sprite in project1.Sprites)
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
