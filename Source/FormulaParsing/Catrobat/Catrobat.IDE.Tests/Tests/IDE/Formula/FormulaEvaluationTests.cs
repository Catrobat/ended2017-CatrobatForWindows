using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.FormulaEditor;
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
        public void TestNull()
        {
            Assert.AreEqual(null, _evaluator.Evaluate(null));
        }

        [TestMethod]
        public void TestNumber()
        {
            foreach (var value in new[] { 0, _random.NextSignedDouble(), -_random.NextSignedDouble() })
            {
                Assert.AreEqual(value, FormulaTreeFactory.CreateNumberNode(value).EvaluateNumber());
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
            TestEvaluator((x, y) => Math.Abs(x - y) <= double.Epsilon, FormulaTreeFactory.CreateEqualsNode);
            TestEvaluator((bool x, bool y) => x == y, FormulaTreeFactory.CreateEqualsNode);
            TestEvaluator((x, y) => Math.Abs(x - y) > double.Epsilon, FormulaTreeFactory.CreateNotEqualsNode);
            TestEvaluator((bool x, bool y) => x != y, FormulaTreeFactory.CreateNotEqualsNode);

            TestEvaluator((x, y) => x > y, FormulaTreeFactory.CreateGreaterNode);
            TestEvaluator((x, y) => x >= y, FormulaTreeFactory.CreateGreaterEqualNode);
            TestEvaluator((x, y) => x < y, FormulaTreeFactory.CreateLessNode);
            TestEvaluator((x, y) => x <= y, FormulaTreeFactory.CreateLessEqualNode);
        }

        [TestMethod]
        public void TestLogic()
        {
            TestEvaluator(true, FormulaTreeFactory.CreateTrueNode);
            TestEvaluator(false, FormulaTreeFactory.CreateFalseNode);

            TestEvaluator((x, y) => x & y, FormulaTreeFactory.CreateAndNode);
            TestEvaluator(false, FormulaTreeFactory.CreateAndNode(FormulaTreeFactory.CreateFalseNode(), null));
            TestEvaluator((x, y) => ((long)x) & ((long)y), FormulaTreeFactory.CreateAndNode);
            TestEvaluator((x, y) => x | y, FormulaTreeFactory.CreateOrNode);
            TestEvaluator(true, FormulaTreeFactory.CreateOrNode(FormulaTreeFactory.CreateTrueNode(), null));
            TestEvaluator((x, y) => ((long)x) | ((long)y), FormulaTreeFactory.CreateOrNode);

            TestEvaluator(x => !x, FormulaTreeFactory.CreateNotNode);
            TestEvaluator(x => ~((long)x), FormulaTreeFactory.CreateNotNode);
        }

        [TestMethod]
        public void TestMinMax()
        {
            // without explicit invocation compiler cannot infer types :/
            TestEvaluator((x, y) => Math.Min(x, y), FormulaTreeFactory.CreateMinNode);
            TestEvaluator((x, y) => Math.Max(x, y), FormulaTreeFactory.CreateMaxNode);
        }

        [TestMethod]
        public void TestExponentialFunctionsAndLogarithms()
        {
            TestEvaluator(Math.Exp, FormulaTreeFactory.CreateExpNode);
            TestEvaluator(Math.Log10, FormulaTreeFactory.CreateLogNode);
            TestEvaluator(Math.Log, FormulaTreeFactory.CreateLnNode);
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
            TestEvaluator(Math.Sqrt, FormulaTreeFactory.CreateSqrtNode);
            TestEvaluator(Math.Abs, FormulaTreeFactory.CreateAbsNode);
            TestEvaluator((x, y) => x % y, FormulaTreeFactory.CreateModNode);
            TestEvaluator((x, y, result) => (x <= y && x <= result && result < y) || (x >= y && x >= result && result > y), FormulaTreeFactory.CreateRandomNode);
            TestEvaluator(Math.Round, FormulaTreeFactory.CreateRoundNode);
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

        #region Helpers

        private void TestEvaluator(double expectedValue, IFormulaTree formula)
        {
            Assert.AreEqual(
                expected: expectedValue,
                actual: formula.EvaluateNumber());
        }

        private void TestEvaluator(bool expectedValue, IFormulaTree formula)
        {
            Assert.AreEqual(
                expected: expectedValue,
                actual: formula.EvaluateLogic());
        }

        private void TestEvaluator(Func<double, bool> condition, IFormulaTree formula)
        {
            Assert.IsTrue(condition(formula.EvaluateNumber()));
        }

        private void TestEvaluator(double expectedValue, Func<ConstantFormulaTree> formulaCreator)
        {
            TestEvaluator(expectedValue, formulaCreator.Invoke());
        }

        private void TestEvaluator(bool expectedValue, Func<ConstantFormulaTree> formulaCreator)
        {
            TestEvaluator(expectedValue, formulaCreator.Invoke());
        }

        private void TestEvaluator(Func<double, double> expectedValue, Func<IFormulaTree, UnaryFormulaTree> formulaCreator)
        {
            foreach (var x in new[] {_random.NextDouble(), _random.NextSignedDouble()})
            {
                TestEvaluator(
                expectedValue: expectedValue(x),
                formula: formulaCreator.Invoke(FormulaTreeFactory.CreateNumberNode(x)));
            }
        }

        private void TestEvaluator(Func<bool, bool> expectedValue, Func<IFormulaTree, UnaryFormulaTree> formulaCreator)
        {
            var x = _random.NextBool();
            TestEvaluator(
                expectedValue: expectedValue(x),
                formula: formulaCreator.Invoke(FormulaTreeFactory.CreateTruthValueNode(x)));
        }

        private void TestEvaluator(Func<double, double, double> expectedValue, Func<IFormulaTree, IFormulaTree, BinaryFormulaTree> formulaCreator)
        {
            foreach (var x in new[] {_random.NextDouble(), _random.NextSignedDouble()})
            {
                foreach (var y in new[] {_random.NextDouble(), _random.NextSignedDouble()})
                {
                    TestEvaluator(
                        expectedValue: expectedValue(x, y),
                        formula:
                            formulaCreator.Invoke(FormulaTreeFactory.CreateNumberNode(x),
                                FormulaTreeFactory.CreateNumberNode(y)));
                }
            }
        }

        private void TestEvaluator(Func<double, double, bool> expectedValue, Func<IFormulaTree, IFormulaTree, BinaryFormulaTree> formulaCreator)
        {
            foreach (var x in new[] {_random.NextDouble(), _random.NextSignedDouble()})
            {
                foreach (var y in new[] {_random.NextDouble(), _random.NextSignedDouble()})
                {
                    TestEvaluator(
                        expectedValue: expectedValue(x, y),
                        formula:
                            formulaCreator.Invoke(FormulaTreeFactory.CreateNumberNode(x),
                                FormulaTreeFactory.CreateNumberNode(y)));
                }
            }
        }

        private void TestEvaluator(Func<bool, bool, bool> expectedValue, Func<IFormulaTree, IFormulaTree, BinaryFormulaTree> formulaCreator)
        {
            var x = _random.NextBool();
            var y = _random.NextBool();
            TestEvaluator(
                expectedValue: expectedValue(x, y),
                formula: formulaCreator.Invoke(FormulaTreeFactory.CreateTruthValueNode(x), FormulaTreeFactory.CreateTruthValueNode(y)));
        }

        private void TestEvaluator(Func<double, double, double, bool> condition, Func<IFormulaTree, IFormulaTree, BinaryFormulaTree> formulaCreator)
        {
            foreach (var x in new[] {_random.NextDouble(), _random.NextSignedDouble()})
            {
                foreach (var y in new[] {_random.NextDouble(), _random.NextSignedDouble()})
                {
                    TestEvaluator(
                        condition: result => condition(x, y, result),
                        formula:
                            formulaCreator.Invoke(FormulaTreeFactory.CreateNumberNode(x),
                                FormulaTreeFactory.CreateNumberNode(y)));
                }
            }
        }

        #endregion
    }
}
