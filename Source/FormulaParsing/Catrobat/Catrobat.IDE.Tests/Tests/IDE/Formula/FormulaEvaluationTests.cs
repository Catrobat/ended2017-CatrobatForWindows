using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.FormulaEditor;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Tests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Catrobat.IDE.Tests.Tests.IDE.Formula
{
    [TestClass]
    public class FormulaEvaluationTests
    {
        private readonly FormulaEvaluator _evaluator = new FormulaEvaluator();
        private readonly Random _random = new Random();

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestNull()
        {
            Assert.AreEqual(null, _evaluator.Evaluate(null));
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestConstants()
        {
            TestEvaluator(x => x, FormulaTreeFactory.CreateNumberNode);

            TestEvaluator(Math.PI, FormulaTreeFactory.CreatePiNode);

            TestEvaluator(true, FormulaTreeFactory.CreateTrueNode);
            TestEvaluator(false, FormulaTreeFactory.CreateFalseNode);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestOperators()
        {
            TestEvaluator((x, y) => x + y, FormulaTreeFactory.CreateAddNode);
            TestEvaluator((x, y) => x - y, FormulaTreeFactory.CreateSubtractNode);
            // without explicit invocation compiler cannot infer types :/
            TestEvaluator(x => -x, FormulaTreeFactory.CreateNegativeSignNode);
            TestEvaluator((x, y) => x * y, FormulaTreeFactory.CreateMultiplyNode);
            TestEvaluator((x, y) => x / y, FormulaTreeFactory.CreateDivideNode);
            TestEvaluator((x, y) => Math.Pow(x, y), FormulaTreeFactory.CreatePowerNode);

            TestEvaluator((x, y) => Math.Abs(x - y) <= double.Epsilon, FormulaTreeFactory.CreateEqualsNode);
            TestEvaluator((bool x, bool y) => x == y, FormulaTreeFactory.CreateEqualsNode);
            TestEvaluator((x, y) => Math.Abs(x - y) > double.Epsilon, FormulaTreeFactory.CreateNotEqualsNode);
            TestEvaluator((bool x, bool y) => x != y, FormulaTreeFactory.CreateNotEqualsNode);

            TestEvaluator((x, y) => x < y, FormulaTreeFactory.CreateLessNode);
            TestEvaluator((x, y) => x <= y, FormulaTreeFactory.CreateLessEqualNode);
            TestEvaluator((x, y) => x > y, FormulaTreeFactory.CreateGreaterNode);
            TestEvaluator((x, y) => x >= y, FormulaTreeFactory.CreateGreaterEqualNode);

            TestEvaluator((x, y) => x & y, FormulaTreeFactory.CreateAndNode);
            TestEvaluator(false, FormulaTreeFactory.CreateAndNode(FormulaTreeFactory.CreateFalseNode(), null));
            TestEvaluator((x, y) => ((long)x) & ((long)y), FormulaTreeFactory.CreateAndNode);
            TestEvaluator((x, y) => x | y, FormulaTreeFactory.CreateOrNode);
            TestEvaluator(true, FormulaTreeFactory.CreateOrNode(FormulaTreeFactory.CreateTrueNode(), null));
            TestEvaluator((x, y) => ((long)x) | ((long)y), FormulaTreeFactory.CreateOrNode);

            TestEvaluator(x => !x, FormulaTreeFactory.CreateNotNode);
            TestEvaluator(x => ~((long)x), FormulaTreeFactory.CreateNotNode);

            TestEvaluator((x, y) => x % y, FormulaTreeFactory.CreateModuloNode);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestFunctions()
        {
            TestEvaluator(Math.Exp, FormulaTreeFactory.CreateExpNode);
            TestEvaluator(Math.Log10, FormulaTreeFactory.CreateLogNode);
            TestEvaluator(Math.Log, FormulaTreeFactory.CreateLnNode);

            // without explicit invocation compiler cannot infer types :/
            TestEvaluator((x, y) => Math.Min(x, y), FormulaTreeFactory.CreateMinNode);
            TestEvaluator((x, y) => Math.Max(x, y), FormulaTreeFactory.CreateMaxNode);

            TestEvaluator(Math.Sin, FormulaTreeFactory.CreateSinNode);
            TestEvaluator(Math.Cos, FormulaTreeFactory.CreateCosNode);
            TestEvaluator(Math.Tan, FormulaTreeFactory.CreateTanNode);
            TestEvaluator(Math.Asin, FormulaTreeFactory.CreateArcsinNode);
            TestEvaluator(Math.Acos, FormulaTreeFactory.CreateArccosNode);
            TestEvaluator(Math.Atan, FormulaTreeFactory.CreateArctanNode);

            TestEvaluator(Math.Sqrt, FormulaTreeFactory.CreateSqrtNode);

            TestEvaluator(Math.Abs, FormulaTreeFactory.CreateAbsNode);

            TestEvaluator(Math.Round, FormulaTreeFactory.CreateRoundNode);

            TestEvaluator(
                condition: (x, y, result) => 
                    (x < y && x <= result && result < y) || 
                    (x > y && x >= result && result > y) ||
                    // ReSharper disable CompareOfFloatsByEqualityOperator
                    (x == y && x == result && result == y), 
                    // ReSharper restore CompareOfFloatsByEqualityOperator
                formulaCreator: FormulaTreeFactory.CreateRandomNode);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestSensors()
        {
            ServiceLocator.Register<SensorServiceTest>(TypeCreationMode.Lazy);

            TestEvaluator(0, FormulaTreeFactory.CreateAccelerationXNode);
            TestEvaluator(0, FormulaTreeFactory.CreateAccelerationYNode);
            TestEvaluator(0, FormulaTreeFactory.CreateAccelerationZNode);
            TestEvaluator(0, FormulaTreeFactory.CreateCompassNode);
            TestEvaluator(0, FormulaTreeFactory.CreateInclinationXNode);
            TestEvaluator(0, FormulaTreeFactory.CreateInclinationYNode);
            TestEvaluator(0, FormulaTreeFactory.CreateLoudnessNode);

            ServiceLocator.SensorService.Start();
            // ReSharper disable CompareOfFloatsByEqualityOperator
            TestEvaluator(
                condition: result => result != 0 && -1 <= result && result <= 1, 
                formulaCreator: FormulaTreeFactory.CreateAccelerationXNode);
            TestEvaluator(
                condition: result => result != 0 && -1 <= result && result <= 1,
                formulaCreator: FormulaTreeFactory.CreateAccelerationYNode);
            TestEvaluator(
                condition: result => result != 0 && -1 <= result && result <= 1,
                formulaCreator: FormulaTreeFactory.CreateAccelerationZNode);
            TestEvaluator(
                condition: result => result != 0 && 0 <= result && result < 360,
                formulaCreator: FormulaTreeFactory.CreateCompassNode);
            Assert.Inconclusive("InclinationX. ");
            Assert.Inconclusive("InclinationY. ");
            Assert.Inconclusive("Loudness. ");
            // ReSharper restore CompareOfFloatsByEqualityOperator
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestProperites()
        {
            TestEvaluator(100, FormulaTreeFactory.CreateBrightnessNode);
            TestEvaluator(-1, FormulaTreeFactory.CreateLayerNode);
            TestEvaluator(0, FormulaTreeFactory.CreateTransparencyNode);
            TestEvaluator(0, FormulaTreeFactory.CreatePositionXNode);
            TestEvaluator(0, FormulaTreeFactory.CreatePositionYNode);
            TestEvaluator(90, FormulaTreeFactory.CreateRotationNode);
            TestEvaluator(100, FormulaTreeFactory.CreateSizeNode);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestVariables()
        {
            var testVariable = new UserVariable
            {
                Name = "TestVariable"
            };
            TestEvaluator(0, FormulaTreeFactory.CreateLocalVariableNode(testVariable));
            TestEvaluator(0, FormulaTreeFactory.CreateGlobalVariableNode(testVariable));
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestBrackets()
        {
            TestEvaluator((double x) => x, FormulaTreeFactory.CreateParenthesesNode);
            TestEvaluator((bool x) => x, FormulaTreeFactory.CreateParenthesesNode);
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

        private void TestEvaluator(Func<double, bool> condition, Func<ConstantFormulaTree> formulaCreator)
        {
            TestEvaluator(condition, formulaCreator.Invoke());
        }

        private void TestEvaluator(Func<double, double, double, bool> condition, Func<IFormulaTree, IFormulaTree, BinaryFormulaTree> formulaCreator)
        {
            foreach (var x in new[] { _random.NextDouble(), 0, _random.NextDouble(-1, 0) })
            {
                foreach (var y in new[] { _random.NextDouble(), 0, _random.NextDouble(-1, 0) })
                {
                    TestEvaluator(
                        condition: result => condition(x, y, result),
                        formula: formulaCreator.Invoke(FormulaTreeFactory.CreateNumberNode(x), FormulaTreeFactory.CreateNumberNode(y)));
                }
            }
        }

        private void TestEvaluator(double expectedValue, Func<ConstantFormulaTree> formulaCreator)
        {
            TestEvaluator(expectedValue, formulaCreator.Invoke());
        }

        private void TestEvaluator(bool expectedValue, Func<ConstantFormulaTree> formulaCreator)
        {
            TestEvaluator(expectedValue, formulaCreator.Invoke());
        }

        private void TestEvaluator(Func<double, double> expectedValue, Func<double, IFormulaTree> formulaCreator)
        {
            foreach (var x in new[] { _random.NextDouble(), 0, _random.NextDouble(-1, 0) })
            {
                TestEvaluator(
                expectedValue: expectedValue(x),
                formula: formulaCreator.Invoke(x));
            }
        }

        private void TestEvaluator(Func<double, double> expectedValue, Func<IFormulaTree, UnaryFormulaTree> formulaCreator)
        {
            foreach (var x in new[] {_random.NextDouble(), 0, _random.NextDouble(-1, 0)})
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
            foreach (var x in new[] {_random.NextDouble(), 0, _random.NextDouble(-1, 0)})
            {
                foreach (var y in new[] {_random.NextDouble(), 0, _random.NextDouble(-1, 0)})
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
            foreach (var x in new[] {_random.NextDouble(), 0, _random.NextDouble(-1, 0)})
            {
                foreach (var y in new[] {_random.NextDouble(), 0, _random.NextDouble(-1, 0)})
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

        #endregion
    }
}
