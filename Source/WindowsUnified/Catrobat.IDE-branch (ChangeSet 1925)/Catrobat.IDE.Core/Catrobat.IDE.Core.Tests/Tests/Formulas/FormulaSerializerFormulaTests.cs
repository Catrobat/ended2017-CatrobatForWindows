using System;
using System.Globalization;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Formulas;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.Formulas
{
    /// <summary>Tests <see cref="FormulaSerializer.Serialize(FormulaTree)" />. </summary>
    [TestClass]
    public class FormulaSerializerFormulaTests
    {
        private readonly Random _random = new Random();

        [TestInitialize]
        public void TestClassInitialize()
        {
            ServiceLocator.Register<CultureServiceTest>(TypeCreationMode.Lazy);

            // use culture different to CultureInfo.CurrentCulture (1.2 vs. 1,2)
            ServiceLocator.CultureService.SetCulture(new CultureInfo("de"));
        }

        [TestMethod, TestCategory("Formulas")]
        public void TestNull()
        {
            Assert.AreEqual(string.Empty, FormulaSerializer.Serialize(null));
        }

        #region Constants

        [TestMethod, TestCategory("Formulas")]
        public void TestConstants()
        {

            var culture = ServiceLocator.CultureService.GetCulture();
            TestSerializer(x => x.ToString("R", culture), FormulaTreeFactory.CreateNumberNode);
            TestSerializer("π", FormulaTreeFactory.CreatePiNode);
            TestSerializer(AppResources.Formula_Constant_True, FormulaTreeFactory.CreateTrueNode);
            TestSerializer(AppResources.Formula_Constant_False, FormulaTreeFactory.CreateFalseNode);
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

        [TestMethod, TestCategory("Formulas")]
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
            TestSerializerNL("{0} " + AppResources.Formula_Operator_And + " {1}", FormulaTreeFactory.CreateAndNode);
            TestSerializerNL("{0} " + AppResources.Formula_Operator_Or + " {1}", FormulaTreeFactory.CreateOrNode);
            TestSerializerNL(AppResources.Formula_Operator_Not + " {0}", FormulaTreeFactory.CreateNotNode);
            TestSerializerN("{0} " + AppResources.Formula_Operator_Mod + " {1}", FormulaTreeFactory.CreateModuloNode);
        }

        #endregion

        #region Functions

        [TestMethod, TestCategory("Formulas")]
        public void TestFunctions()
        {
            TestSerializerN("exp({0})", FormulaTreeFactory.CreateExpNode);
            TestSerializerN("log({0})", FormulaTreeFactory.CreateLogNode);
            TestSerializerN("ln({0})", FormulaTreeFactory.CreateLnNode);
            TestSerializerN(AppResources.Formula_Function_Min + "({0}, {1})", FormulaTreeFactory.CreateMinNode);
            TestSerializerN(AppResources.Formula_Function_Max + "({0}, {1})", FormulaTreeFactory.CreateMaxNode);
            TestSerializerN("sin({0})", FormulaTreeFactory.CreateSinNode);
            TestSerializerN("cos({0})", FormulaTreeFactory.CreateCosNode);
            TestSerializerN("tan({0})", FormulaTreeFactory.CreateTanNode);
            TestSerializerN("arcsin({0})", FormulaTreeFactory.CreateArcsinNode);
            TestSerializerN("arccos({0})", FormulaTreeFactory.CreateArccosNode);
            TestSerializerN("arctan({0})", FormulaTreeFactory.CreateArctanNode);
            TestSerializerN(AppResources.Formula_Function_Sqrt + "({0})", FormulaTreeFactory.CreateSqrtNode);
            TestSerializerN(AppResources.Formula_Function_Abs + "({0})", FormulaTreeFactory.CreateAbsNode);
            TestSerializerN(AppResources.Formula_Function_Round + "({0})", FormulaTreeFactory.CreateRoundNode);
            TestSerializerNL(AppResources.Formula_Function_Random + "({0}, {1})", FormulaTreeFactory.CreateRandomNode);
        }

        #endregion

        #region Sensors

        [TestMethod, TestCategory("Formulas")]
        public void TestSensors()
        {
            TestSerializer(AppResources.Formula_Sensor_AccelerationX, FormulaTreeFactory.CreateAccelerationXNode);
            TestSerializer(AppResources.Formula_Sensor_AccelerationY, FormulaTreeFactory.CreateAccelerationYNode);
            TestSerializer(AppResources.Formula_Sensor_AccelerationZ, FormulaTreeFactory.CreateAccelerationZNode);
            TestSerializer(AppResources.Formula_Sensor_Compass, FormulaTreeFactory.CreateCompassNode);
            TestSerializer(AppResources.Formula_Sensor_InclinationX, FormulaTreeFactory.CreateInclinationXNode);
            TestSerializer(AppResources.Formula_Sensor_InclinationY, FormulaTreeFactory.CreateInclinationYNode);
            TestSerializer(AppResources.Formula_Sensor_Loudness, FormulaTreeFactory.CreateLoudnessNode);
        }

        #endregion

        #region Properties

        [TestMethod, TestCategory("Formulas")]
        public void TestProperties()
        {
            TestSerializer(AppResources.Formula_Property_Brightness, FormulaTreeFactory.CreateBrightnessNode);
            TestSerializer(AppResources.Formula_Property_Layer, FormulaTreeFactory.CreateLayerNode);
            TestSerializer(AppResources.Formula_Property_Transparency, FormulaTreeFactory.CreateTransparencyNode);
            TestSerializer(AppResources.Formula_Property_PositionX, FormulaTreeFactory.CreatePositionXNode);
            TestSerializer(AppResources.Formula_Property_PositionY, FormulaTreeFactory.CreatePositionYNode);
            TestSerializer(AppResources.Formula_Property_Rotation, FormulaTreeFactory.CreateRotationNode);
            TestSerializer(AppResources.Formula_Property_Size, FormulaTreeFactory.CreateSizeNode);
            TestSerializer(AppResources.Formula_Property_Transparency, FormulaTreeFactory.CreateTransparencyNode);
        }

        #endregion

        #region Variables

        [TestMethod, TestCategory("Formulas")]
        public void TestVariables()
        {
            TestSerializer<LocalVariable>("{0}", FormulaTreeFactory.CreateLocalVariableNode);
            TestSerializer<GlobalVariable>("{0}", FormulaTreeFactory.CreateGlobalVariableNode);
        }

        private void TestSerializer<TVariable>(string format, Func<TVariable, ConstantFormulaTree> formulaCreator) where TVariable : Variable, new()
        {
            var x = new TVariable
            {
                Name = "TestVariable"
            };
            var y = new TVariable();
            TestSerializer(string.Format(format, x.Name), formulaCreator.Invoke(x));
            TestSerializer(string.Format(format, " "), formulaCreator.Invoke(y));
            TestSerializer(string.Format(format, " "), formulaCreator.Invoke(null));
        }

        #endregion

        #region Brackets

        [TestMethod, TestCategory("Formulas")]
        public void TestBrackets()
        {
            TestSerializerNL("({0})", FormulaTreeFactory.CreateParenthesesNode);
        }

        #endregion

        #region Helpers

        private void TestSerializer(string expectedValue, FormulaTree formula)
        {
            Assert.AreEqual(expectedValue, FormulaSerializer.Serialize(formula));
        }

        private void TestSerializer(string expectedValue, Func<FormulaTree> formulaCreator)
        {
            TestSerializer(expectedValue, formulaCreator.Invoke());
        }

        private void TestSerializerN(string format, Func<FormulaTree, UnaryFormulaTree> formulaCreator)
        {
            var x = _random.Next();
            TestSerializer(string.Format(format, x), formulaCreator.Invoke(FormulaTreeFactory.CreateNumberNode(x)));
            TestSerializer(string.Format(format, " "), formulaCreator.Invoke(null));
        }

        private void TestSerializerL(string format, Func<FormulaTree, UnaryFormulaTree> formulaCreator)
        {
            var x = _random.NextBool();
            var xString = x ? AppResources.Formula_Constant_True : AppResources.Formula_Constant_False;
            TestSerializer(string.Format(format, xString), formulaCreator.Invoke(FormulaTreeFactory.CreateTruthValueNode(x)));
            TestSerializer(string.Format(format, " "), formulaCreator.Invoke(null));
        }

        // ReSharper disable once InconsistentNaming
        private void TestSerializerNL(string format, Func<FormulaTree, UnaryFormulaTree> formulaCreator)
        {
            TestSerializerL(format, formulaCreator);
            TestSerializerN(format, formulaCreator);
        }

        private void TestSerializerN(string format, Func<FormulaTree, FormulaTree, BinaryFormulaTree> formulaCreator)
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

        private void TestSerializerL(string format, Func<FormulaTree, FormulaTree, BinaryFormulaTree> formulaCreator)
        {
            var x = _random.NextBool();
            var y = _random.NextBool();
            var xString = x ? AppResources.Formula_Constant_True : AppResources.Formula_Constant_False;
            var yString = y ? AppResources.Formula_Constant_True : AppResources.Formula_Constant_False;
            TestSerializer(
                expectedValue: string.Format(format, xString, yString), 
                formula: formulaCreator.Invoke(FormulaTreeFactory.CreateTruthValueNode(x), FormulaTreeFactory.CreateTruthValueNode(y)));
            TestSerializer(
                expectedValue: string.Format(format, " ", " "),
                formula: formulaCreator.Invoke(null, null));
        }

        // ReSharper disable once InconsistentNaming
        private void TestSerializerNL(string format, Func<FormulaTree, FormulaTree, BinaryFormulaTree> formulaCreator)
        {
            TestSerializerN(format, formulaCreator);
            TestSerializerL(format, formulaCreator);
        }

        #endregion
    }
}
