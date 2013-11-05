using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.FormulaEditor.Editor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Catrobat.IDE.Tests.Tests.IDE.Formula
{
    [TestClass]
    public class FormulaEvaluationTests
    {
        private readonly FormulaEvaluator _evaluator = new FormulaEvaluator();
        private readonly Random _random = new Random();

        [TestMethod]
        public void FormulaEvaluationTests_Null()
        {
            Assert.AreEqual(null, _evaluator.Evaluate(null));
        }

        #region numbers

        [TestMethod]
        public void FormulaEvaluationTests_Number()
        {
            foreach (var value in new[] { 0, _random.NextDouble(), -_random.NextDouble() })
            {
                Assert.AreEqual(value, _evaluator.Evaluate(FormulaTreeFactory.CreateNumberNode(value)));
            }
        }

        [TestMethod]
        public void FormulaEvaluationTests_Pi()
        {
            Assert.AreEqual(Math.PI, _evaluator.Evaluate(FormulaTreeFactory.CreatePiNode()));
        }

        #endregion

        #region arithmetic

        [TestMethod]
        public void FormulaEvaluationTests_Add()
        {
            Assert.Inconclusive();
        }

        #endregion

    }
}
