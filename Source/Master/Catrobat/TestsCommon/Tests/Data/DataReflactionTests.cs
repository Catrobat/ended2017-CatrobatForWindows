using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Catrobat.TestsCommon.Misc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.TestsCommon.Tests.Data
{
    [TestClass]
    public class DataReflactionTests
    {
        [ClassInitialize()]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }

        [TestMethod]
        public void WriteReadTest1()
        {
            var bricks = ReflectionHelper.GetInstances<Brick>();

            // TODO: finish this
        }
    }
}
