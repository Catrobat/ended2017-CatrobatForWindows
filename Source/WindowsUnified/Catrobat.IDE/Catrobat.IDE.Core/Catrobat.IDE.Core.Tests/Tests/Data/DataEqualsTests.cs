using System;
using Catrobat.IDE.Core.Tests.Extensions;
using Catrobat.IDE.Core.Tests.Misc;
using Catrobat.IDE.Core.Tests.SampleData.ProgramGenerators;
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
        [TestMethod] // 
        public void EqualsProjectTest()
        {
            ITestProgramGenerator projectgenerator = new ProgramGeneratorReflection(42, DateTime.Now);
            var project1 = projectgenerator.GenerateProgram();
            var project2 = projectgenerator.GenerateProgram();

            ModelAssert.AreTestEqual(project1, project2);
        }
    }
}
