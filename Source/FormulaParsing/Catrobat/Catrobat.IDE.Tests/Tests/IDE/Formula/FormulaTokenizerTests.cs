using System;
using System.Globalization;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree;
using Catrobat.IDE.Core.FormulaEditor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Tests.IDE.Formula
{
    [TestClass]
    public class FormulaTokenizerTests
    {
        private readonly FormulaSerializer _serializer = new FormulaSerializer();

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestNull()
        {
            Assert.AreEqual(string.Empty, _serializer.Serialize(null));
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TODO()
        {
            Assert.Inconclusive("Implement some tests like in FormulaSerializerTests");
        }
        


    }
}
