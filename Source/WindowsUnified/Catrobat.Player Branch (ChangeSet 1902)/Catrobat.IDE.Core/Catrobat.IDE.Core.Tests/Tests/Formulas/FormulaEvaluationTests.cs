using System;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Formulas;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.Formulas
{
    /// <summary>Tests <see cref="FormulaEvaluator" />. </summary>
    [TestClass]
    public class FormulaEvaluationTests
    {
        private readonly Random _random = new Random();

        [TestInitialize]
        public void TestClassInitialize()
        {
            ServiceLocator.Register<SensorServiceTest>(TypeCreationMode.Lazy);
        }

        [TestMethod, TestCategory("Formulas")]
        public void TestNull()
        {
            Assert.AreEqual(null, FormulaEvaluator.Evaluate(null));
        }

        [TestMethod, TestCategory("Formulas")]
        public void TestConstants()
        {
            TestEvaluator(x => x, FormulaTreeFactory.CreateNumberNode);

            TestEvaluator(Math.PI, FormulaTreeFactory.CreatePiNode);

            TestEvaluator(true, FormulaTreeFactory.CreateTrueNode);
            TestEvaluator(false, FormulaTreeFactory.CreateFalseNode);
        }

        [TestMethod, TestCategory("Formulas")]
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

        [TestMethod, TestCategory("Formulas")]
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
            TestEvaluator(
                condition: (bool x, bool y, bool result) => result == x || result == y, 
                formulaCreator: FormulaTreeFactory.CreateRandomNode);
        }

        [TestMethod, TestCategory("Formulas"), TestCategory("ExcludeGated")]
        public void TestSensors()
        {
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

        [TestMethod, TestCategory("Formulas")]
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

        [TestMethod, TestCategory("Formulas")]
        public void TestVariables()
        {
            TestEvaluator<LocalVariable>(0, FormulaTreeFactory.CreateLocalVariableNode);
            TestEvaluator<GlobalVariable>(0, FormulaTreeFactory.CreateGlobalVariableNode);
        }

        [TestMethod, TestCategory("Formulas")]
        public void TestBrackets()
        {
            TestEvaluator((double x) => x, FormulaTreeFactory.CreateParenthesesNode);
            TestEvaluator((bool x) => x, FormulaTreeFactory.CreateParenthesesNode);
        }

        #region Helpers

        private void TestEvaluator(double expectedValue, FormulaTree formula)
        {
            Assert.AreEqual(
                expected: expectedValue,
                actual: FormulaEvaluator.Evaluate(formula));
        }

        private void TestEvaluator(bool expectedValue, FormulaTree formula)
        {
            Assert.AreEqual(
                expected: expectedValue,
                actual: FormulaEvaluator.EvaluateLogic(formula));
        }

        private void TestEvaluator(Func<double, bool> condition, FormulaTree formula)
        {
            Assert.IsTrue(condition(FormulaEvaluator.EvaluateNumber(formula)));
        }

        private void TestEvaluator(Func<bool, bool> condition, FormulaTree formula)
        {
            Assert.IsTrue(condition(FormulaEvaluator.EvaluateLogic(formula)));
        }

        private void TestEvaluator(Func<double, bool> condition, Func<ConstantFormulaTree> formulaCreator)
        {
            TestEvaluator(condition, formulaCreator.Invoke());
        }

        private void TestEvaluator<TVariable>(double expectedValue, Func<TVariable, ConstantFormulaTree> formulaCreator) where TVariable : Variable, new()
        {
            var x = new TVariable
            {
                Name = "TestVariable"
            };
            TestEvaluator(expectedValue, formulaCreator.Invoke(x));
        }

        private void TestEvaluator(Func<double, double, double, bool> condition, Func<FormulaTree, FormulaTree, BinaryFormulaTree> formulaCreator)
        {
            foreach (var x in new[] { _random.NextDouble(), 0, _random.NextDouble(-1, 0) })
            {
                foreach (var y in new[] { _random.NextDouble(), 0, _random.NextDouble(-1, 0) })
                {
                    TestEvaluator(
                        condition: result => condition(x, y, result),
                        formula: formulaCreator.Invoke(
                            arg1: FormulaTreeFactory.CreateNumberNode(x),
                            arg2: FormulaTreeFactory.CreateNumberNode(y)));
                }
            }
        }

        private void TestEvaluator(Func<bool, bool, bool, bool> condition, Func<FormulaTree, FormulaTree, BinaryFormulaTree> formulaCreator)
        {
            foreach (var x in new[] { true, false })
            {
                foreach (var y in new[] { true, false })
                {
                    TestEvaluator(
                        condition: result => condition(x, y, result),
                        formula: formulaCreator.Invoke(
                            arg1: FormulaTreeFactory.CreateTruthValueNode(x),
                            arg2: FormulaTreeFactory.CreateTruthValueNode(y)));
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

        private void TestEvaluator(Func<double, double> expectedValue, Func<double, FormulaTree> formulaCreator)
        {
            foreach (var x in new[] { _random.NextDouble(), 0, _random.NextDouble(-1, 0) })
            {
                TestEvaluator(
                    expectedValue: expectedValue(x),
                    formula: formulaCreator.Invoke(x));
            }
        }

        private void TestEvaluator(Func<double, double> expectedValue, Func<FormulaTree, UnaryFormulaTree> formulaCreator)
        {
            foreach (var x in new[] {_random.NextDouble(), 0, _random.NextDouble(-1, 0)})
            {
                TestEvaluator(
                expectedValue: expectedValue(x),
                formula: formulaCreator.Invoke(FormulaTreeFactory.CreateNumberNode(x)));
            }
        }

        private void TestEvaluator(Func<bool, bool> expectedValue, Func<FormulaTree, UnaryFormulaTree> formulaCreator)
        {
            var x = _random.NextBool();
            TestEvaluator(
                expectedValue: expectedValue(x),
                formula: formulaCreator.Invoke(FormulaTreeFactory.CreateTruthValueNode(x)));
        }

        private void TestEvaluator(Func<double, double, double> expectedValue, Func<FormulaTree, FormulaTree, BinaryFormulaTree> formulaCreator)
        {
            foreach (var x in new[] {_random.NextDouble(), 0, _random.NextDouble(-1, 0)})
            {
                foreach (var y in new[] {_random.NextDouble(), 0, _random.NextDouble(-1, 0)})
                {
                    TestEvaluator(
                        expectedValue: expectedValue(x, y),
                        formula: formulaCreator.Invoke(
                            arg1: FormulaTreeFactory.CreateNumberNode(x), 
                            arg2: FormulaTreeFactory.CreateNumberNode(y)));
                }
            }
        }

        private void TestEvaluator(Func<double, double, bool> expectedValue, Func<FormulaTree, FormulaTree, BinaryFormulaTree> formulaCreator)
        {
            foreach (var x in new[] {_random.NextDouble(), 0, _random.NextDouble(-1, 0)})
            {
                foreach (var y in new[] {_random.NextDouble(), 0, _random.NextDouble(-1, 0)})
                {
                    TestEvaluator(
                        expectedValue: expectedValue(x, y),
                        formula: formulaCreator.Invoke(FormulaTreeFactory.CreateNumberNode(x), FormulaTreeFactory.CreateNumberNode(y)));
                }
            }
        }

        private void TestEvaluator(Func<bool, bool, bool> expectedValue, Func<FormulaTree, FormulaTree, BinaryFormulaTree> formulaCreator)
        {
            var x = _random.NextBool();
            var y = _random.NextBool();
            TestEvaluator(
                expectedValue: expectedValue(x, y),
                formula: formulaCreator.Invoke(
                    arg1: FormulaTreeFactory.CreateTruthValueNode(x),
                    arg2: FormulaTreeFactory.CreateTruthValueNode(y)));
        }

        #endregion
    }
}
