using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Tests.Misc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Tests.Data
{
    [TestClass]
    public class DataEqualsTests
    {
        [ClassInitialize()]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }

        [TestMethod, TestCategory("GuardedTests")]
        public void EqualsProjectTest()
        {
            IProjectGenerator projectgenerator = new ProjectGeneratorReflection();
            var project1 = projectgenerator.GenerateProject();
            var project2 = projectgenerator.GenerateProject();

            Assert.IsTrue(project1.Equals(project2));
        }
    }
}
