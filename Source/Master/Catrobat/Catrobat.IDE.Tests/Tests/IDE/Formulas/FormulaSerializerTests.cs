using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Formulas;
using Catrobat.IDE.Core.Models.Formulas.FormulaTree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Tests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace Catrobat.IDE.Tests.Tests.IDE.Formulas
{
    /// <summary>Tests <see cref="FormulaSerializer.Serialize" />. </summary>
    [TestClass]
    public class FormulaSerializerTests
    {
        private readonly Random _random = new Random();

        [TestInitialize]
        public void TestClassInitialize()
        {
            ServiceLocator.Register<CultureServiceTest>(TypeCreationMode.Lazy);

            // use culture different to CultureInfo.CurrentCulture (in France the decimal separator is the comma)
            ServiceLocator.CultureService.SetCulture(new CultureInfo("fr-FR"));
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestNull()
        {
            Assert.AreEqual(string.Empty, FormulaSerializer.Serialize(null));
        }

        #region Constants

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestConstants()
        {
            TestSerializer(
                expectedValue: x => x.ToString("R", ServiceLocator.CultureService.GetCulture()), 
                formulaCreator: FormulaTreeFactory.CreateNumberNode);
            TestSerializer("pi", FormulaTreeFactory.CreatePiNode);
            TestSerializer("True", FormulaTreeFactory.CreateTrueNode);
            TestSerializer("False", FormulaTreeFactory.CreateFalseNode);
            Assert.Inconclusive("Translations");
        }

        private void TestSerializer(Func<double, string> expectedValue, Func<double, ConstantFormulaTree> formulaCreator)
        {
            foreach (var x in new[] { _random.Next(), _random.NextDouble(), 0, _random.NextDouble(-1, 0) })
            {
                TestSerializer(expectedValue.Invoke(x), formulaCreator.Invoke(x));
            }
        }

        #endregion

        #region Operators

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestOperators()
        {
            TestSerializerN("{0}+{1}", FormulaTreeFactory.CreateAddNode);
            TestSerializerN("{0}-{1}", FormulaTreeFactory.CreateSubtractNode);
            TestSerializerN("-{0}", FormulaTreeFactory.CreateNegativeSignNode);
            TestSerializerN("{0}*{1}", FormulaTreeFactory.CreateMultiplyNode);
            TestSerializerN("{0}/{1}", FormulaTreeFactory.CreateDivideNode);
            TestSerializerN("{0}^{1}", FormulaTreeFactory.CreatePowerNode);
            TestSerializerNL("{0}={1}", FormulaTreeFactory.CreateEqualsNode);
            TestSerializerNL("{0}≠{1}", FormulaTreeFactory.CreateNotEqualsNode);
            TestSerializerN("{0}<{1}", FormulaTreeFactory.CreateLessNode);
            TestSerializerN("{0}≤{1}", FormulaTreeFactory.CreateLessEqualNode);
            TestSerializerN("{0}>{1}", FormulaTreeFactory.CreateGreaterNode);
            TestSerializerN("{0}≥{1}", FormulaTreeFactory.CreateGreaterEqualNode);
            TestSerializerNL("{0} And {1}", FormulaTreeFactory.CreateAndNode);
            TestSerializerNL("{0} Or {1}", FormulaTreeFactory.CreateOrNode);
            TestSerializerNL("Not {0}", FormulaTreeFactory.CreateNotNode);
            TestSerializerN("{0} mod {1}", FormulaTreeFactory.CreateModuloNode);

            Assert.Inconclusive("Translations");
        }

        #endregion

        #region Functions

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestFunctions()
        {
            TestSerializerN("exp({0})", FormulaTreeFactory.CreateExpNode);
            TestSerializerN("log({0})", FormulaTreeFactory.CreateLogNode);
            TestSerializerN("ln({0})", FormulaTreeFactory.CreateLnNode);
            TestSerializerN("min({0}, {1})", FormulaTreeFactory.CreateMinNode);
            TestSerializerN("max({0}, {1})", FormulaTreeFactory.CreateMaxNode);
            TestSerializerN("sin({0})", FormulaTreeFactory.CreateSinNode);
            TestSerializerN("cos({0})", FormulaTreeFactory.CreateCosNode);
            TestSerializerN("tan({0})", FormulaTreeFactory.CreateTanNode);
            TestSerializerN("arcsin({0})", FormulaTreeFactory.CreateArcsinNode);
            TestSerializerN("arccos({0})", FormulaTreeFactory.CreateArccosNode);
            TestSerializerN("arctan({0})", FormulaTreeFactory.CreateArctanNode);
            TestSerializerN("sqrt({0})", FormulaTreeFactory.CreateSqrtNode);
            TestSerializerN("abs({0})", FormulaTreeFactory.CreateAbsNode);
            TestSerializerN("round({0})", FormulaTreeFactory.CreateRoundNode);
            TestSerializerNL("random({0}, {1})", FormulaTreeFactory.CreateRandomNode);

            Assert.Inconclusive("Translations");
        }

        #endregion

        #region Sensors

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestSensors()
        {
            TestSerializer("AccelerationX", FormulaTreeFactory.CreateAccelerationXNode);
            TestSerializer("AccelerationY", FormulaTreeFactory.CreateAccelerationYNode);
            TestSerializer("AccelerationZ", FormulaTreeFactory.CreateAccelerationZNode);
            TestSerializer("Compass", FormulaTreeFactory.CreateCompassNode);
            TestSerializer("InclinationX", FormulaTreeFactory.CreateInclinationXNode);
            TestSerializer("InclinationY", FormulaTreeFactory.CreateInclinationYNode);
            TestSerializer("Loudness", FormulaTreeFactory.CreateLoudnessNode);

            Assert.Inconclusive("Translations");
        }

        #endregion

        #region Properties

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestProperties()
        {
            TestSerializer("Brightness", FormulaTreeFactory.CreateBrightnessNode);
            TestSerializer("Layer", FormulaTreeFactory.CreateLayerNode);
            TestSerializer("Transparency", FormulaTreeFactory.CreateTransparencyNode);
            TestSerializer("PositionX", FormulaTreeFactory.CreatePositionXNode);
            TestSerializer("PositionY", FormulaTreeFactory.CreatePositionYNode);
            TestSerializer("Rotation", FormulaTreeFactory.CreateRotationNode);
            TestSerializer("Size", FormulaTreeFactory.CreateSizeNode);
            TestSerializer("Transparency", FormulaTreeFactory.CreateTransparencyNode);

            Assert.Inconclusive("Translations");
        }

        #endregion

        #region Variables

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestVariables()
        {
            TestSerializer("{0}", FormulaTreeFactory.CreateLocalVariableNode);
            TestSerializer("{0}", FormulaTreeFactory.CreateGlobalVariableNode);
        }

        private void TestSerializer(string format, Func<UserVariable, ConstantFormulaTree> formulaCreator)
        {
            var x = new UserVariable
            {
                Name = "TestVariable"
            };
            var y = new UserVariable();
            TestSerializer(string.Format(format, x.Name), formulaCreator.Invoke(x));
            TestSerializer(string.Format(format, y.Name), formulaCreator.Invoke(y));
            TestSerializer(string.Format(format, " "), formulaCreator.Invoke(null));
        }

        #endregion

        #region Brackets

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestParentheses()
        {
            TestSerializerNL("({0})", FormulaTreeFactory.CreateParenthesesNode);
        }

        #endregion

        #region Helpers

        private void TestSerializer(string expectedValue, IFormulaTree formula)
        {
            Assert.AreEqual(expectedValue, FormulaSerializer.Serialize(formula));
        }

        private void TestSerializer(string expectedValue, Func<IFormulaTree> formulaCreator)
        {
            TestSerializer(expectedValue, formulaCreator.Invoke());
        }

        private void TestSerializerN(string format, Func<IFormulaTree, UnaryFormulaTree> formulaCreator)
        {
            var x = _random.Next();
            TestSerializer(string.Format(format, x), formulaCreator.Invoke(FormulaTreeFactory.CreateNumberNode(x)));
            TestSerializer(string.Format(format, " "), formulaCreator.Invoke(null));
        }

        private void TestSerializerL(string format, Func<IFormulaTree, UnaryFormulaTree> formulaCreator)
        {
            var x = _random.NextBool();
            TestSerializer(string.Format(format, x), formulaCreator.Invoke(FormulaTreeFactory.CreateTruthValueNode(x)));
            TestSerializer(string.Format(format, " "), formulaCreator.Invoke(null));
        }

        // ReSharper disable once InconsistentNaming
        private void TestSerializerNL(string format, Func<IFormulaTree, UnaryFormulaTree> formulaCreator)
        {
            TestSerializerL(format, formulaCreator);
            TestSerializerN(format, formulaCreator);
        }

        private void TestSerializerN(string format, Func<IFormulaTree, IFormulaTree, BinaryFormulaTree> formulaCreator)
        {
            var x = _random.Next();
            var y = _random.Next();
            TestSerializer(
                expectedValue: string.Format(format, x, y), 
                formula: formulaCreator.Invoke(FormulaTreeFactory.CreateNumberNode(x), FormulaTreeFactory.CreateNumberNode(y)));
            TestSerializer(
                expectedValue: string.Format(format, " ", " "),
                formula: formulaCreator.Invoke(null, null));
        }

        private void TestSerializerL(string format, Func<IFormulaTree, IFormulaTree, BinaryFormulaTree> formulaCreator)
        {
            var x = _random.NextBool();
            var y = _random.NextBool();
            TestSerializer(
                expectedValue: string.Format(format, x, y), 
                formula: formulaCreator.Invoke(FormulaTreeFactory.CreateTruthValueNode(x), FormulaTreeFactory.CreateTruthValueNode(y)));
            TestSerializer(
                expectedValue: string.Format(format, " ", " "),
                formula: formulaCreator.Invoke(null, null));
        }

        // ReSharper disable once InconsistentNaming
        private void TestSerializerNL(string format, Func<IFormulaTree, IFormulaTree, BinaryFormulaTree> formulaCreator)
        {
            TestSerializerN(format, formulaCreator);
            TestSerializerL(format, formulaCreator);
        }

        #endregion
    }
}
