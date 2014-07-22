using System;
using Catrobat.IDE.Core.Tests.Extensions;
using Catrobat.IDE.Core.Tests.Misc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.Data
{
    [TestClass]
    public class DataEqualsTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }


        // TODO: test has timing problems that are probably related to image loading
        [TestMethod] // , TestCategory("GatedTests")
        public void EqualsProjectTest()
        {
            ITestProjectGenerator projectgenerator = new ProjectGeneratorReflection(42, DateTime.Now);
            var project1 = projectgenerator.GenerateProject();
            var project2 = projectgenerator.GenerateProject();

            ModelAssert.AreTestEqual(project1, project2);
        }
    }
}
