using Catrobat.Core.Misc.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.TestsCommon.Misc;

namespace Catrobat.TestsCommon.Tests.Data
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
            var project1 = ProjectGenerator.GenerateProject();
            var globalVariableList = VariableHelper.GetGlobalVariableList(project1);

            Assert.AreEqual(3, globalVariableList.Count);

            for(int i = 0; i < 3; i++)
                Assert.AreEqual("GlobalTestVariable" + i, globalVariableList[i].Name);
        }

    }
}
