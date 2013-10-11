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

        [TestMethod]
        public void EqualsProjectTest()
        {
            var project1 = ProjectGenerator.GenerateProject();
            var project2 = ProjectGenerator.GenerateProject();

            Assert.IsTrue(project1.Equals(project2));
        }
    }
}
