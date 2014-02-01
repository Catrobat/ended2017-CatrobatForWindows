using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.FormulaEditor.Editor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Catrobat.IDE.Tests.Tests.IDE.Formula
{
    [TestClass]
    public class FormulaEvaluatorTests
    {
        private readonly FormulaEvaluator _evaluator = new FormulaEvaluator();
        private readonly Random _random = new Random();

        [TestMethod]
        public void TestNull()
        {
            Assert.AreEqual(null, _evaluator.Evaluate(null));
        }

        [TestMethod]
        public void TestNumber()
        {
            foreach (var value in new[] { 0, _random.NextDouble(), -_random.NextDouble() })
            {
                Assert.AreEqual(value, _evaluator.Evaluate(FormulaTreeFactory.CreateNumberNode(value)));
            }
        }

        [TestMethod]
        public void TestPi()
        {
            TestEvaluator(Math.PI, FormulaTreeFactory.CreatePiNode);
        }

        [TestMethod]
        public void TestArithmetric()
        {
            TestEvaluator((x, y) => x + y, FormulaTreeFactory.CreateAddNode);
            TestEvaluator((x, y) => x - y, FormulaTreeFactory.CreateSubtractNode);
            TestEvaluator((x, y) => x * y, FormulaTreeFactory.CreateMultiplyNode);
            TestEvaluator((x, y) => x / y, FormulaTreeFactory.CreateDivideNode);
        }

        [TestMethod]
        public void TestRelationalOperators()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TestLogic()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TestMinMax()
        {
            TestEvaluator(Math.Min, FormulaTreeFactory.CreateMinNode);
            TestEvaluator(Math.Max, FormulaTreeFactory.CreateMaxNode);
        }

        [TestMethod]
        public void TestExponentialFunctionsAndLogarithms()
        {
            TestEvaluator(Math.Exp, FormulaTreeFactory.CreateExpNode);
            TestEvaluator(Math.Log, FormulaTreeFactory.CreateLogNode);
            TestEvaluator(x => Math.Log(x, Math.E), FormulaTreeFactory.CreateLnNode);
        }

        [TestMethod]
        public void TestTrigonometricFunctions()
        {
            TestEvaluator(Math.Sin, FormulaTreeFactory.CreateSinNode);
            TestEvaluator(Math.Cos, FormulaTreeFactory.CreateCosNode);
            TestEvaluator(Math.Tan, FormulaTreeFactory.CreateTanNode);
            TestEvaluator(Math.Asin, FormulaTreeFactory.CreateArcsinNode);
            TestEvaluator(Math.Acos, FormulaTreeFactory.CreateArccosNode);
            TestEvaluator(Math.Atan, FormulaTreeFactory.CreateArctanNode);
        }

        [TestMethod]
        public void TestMiscellaneousFunctions()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TestSensors()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TestObjectVariables()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TestUserVariables()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TestBrackets()
        {
            Assert.Inconclusive();
        }

        private void TestEvaluator(double expectedValue, Func<ConstantFormulaTree> formulaCreator)
        {
            Assert.AreEqual(
                expected: expectedValue,
                actual: _evaluator.Evaluate(formulaCreator.Invoke()));
        }

        private void TestEvaluator(Func<double, double> expectedValue, Func<IFormulaTree, UnaryFormulaTree> formulaCreator)
        {
            var x = _random.NextDouble();
            Assert.AreEqual(
                expected: expectedValue(x),
                actual: _evaluator.Evaluate(formulaCreator.Invoke(FormulaTreeFactory.CreateNumberNode(x))));
        }

        private void TestEvaluator(Func<double, double, double> expectedValue, Func<IFormulaTree, IFormulaTree, BinaryFormulaTree> formulaCreator)
        {
            var x = _random.NextDouble();
            var y = _random.NextDouble();
            Assert.AreEqual(
                expected: expectedValue(x, y), 
                actual: _evaluator.Evaluate(formulaCreator.Invoke(FormulaTreeFactory.CreateNumberNode(x), FormulaTreeFactory.CreateNumberNode(y))));
        }
    }
}
