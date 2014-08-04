using System;
using System.Globalization;
using System.Linq;
using Catrobat.IDE.Core.Formulas;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Formulas.Tokens;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.Formulas
{
    /// <summary>Tests <see cref="FormulaSerializer.Serialize(IFormulaToken)" />. </summary>
    [TestClass]
    public class FormulaSerializerTokenTests
    {
        [TestInitialize]
        public void TestClassInitialize()
        {
            ServiceLocator.Register<CultureServiceTest>(TypeCreationMode.Lazy);

            // use culture different to CultureInfo.CurrentCulture (1.2 vs. 1,2)
            ServiceLocator.CultureService.SetCulture(new CultureInfo("de"));
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
            var culture = ServiceLocator.CultureService.GetCulture();
            TestSerializer(x => x.ToString(culture), FormulaTokenFactory.CreateDigitToken);
            TestSerializer(culture.NumberFormat.NumberDecimalSeparator, FormulaTokenFactory.CreateDecimalSeparatorToken);
            TestSerializer("π", FormulaTokenFactory.CreatePiToken);
            TestSerializer(AppResources.Formula_Constant_True, FormulaTokenFactory.CreateTrueToken);
            TestSerializer(AppResources.Formula_Constant_False, FormulaTokenFactory.CreateFalseToken);
        }

        private void TestSerializer(Func<int, string> expectedValue, Func<int, ConstantFormulaTree> tokenCreator)
        {
            foreach (var digit in Enumerable.Range(0, 10))
            {
                TestSerializer(expectedValue.Invoke(digit), tokenCreator.Invoke(digit));
            }
        }

        #endregion

        #region Operators

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestOperators()
        {
            TestSerializer("+", FormulaTokenFactory.CreatePlusToken);
            TestSerializer("-", FormulaTokenFactory.CreateMinusToken);
            TestSerializer("*", FormulaTokenFactory.CreateMultiplyToken);
            TestSerializer("/", FormulaTokenFactory.CreateDivideToken);
            TestSerializer("^", FormulaTokenFactory.CreateCaretToken);
            TestSerializer("=", FormulaTokenFactory.CreateEqualsToken);
            TestSerializer("≠", FormulaTokenFactory.CreateNotEqualsToken);
            TestSerializer("<", FormulaTokenFactory.CreateLessToken);
            TestSerializer("≤", FormulaTokenFactory.CreateLessEqualToken);
            TestSerializer(">", FormulaTokenFactory.CreateGreaterToken);
            TestSerializer("≥", FormulaTokenFactory.CreateGreaterEqualToken);
            TestSerializer(AppResources.Formula_Operator_And, FormulaTokenFactory.CreateAndToken);
            TestSerializer(AppResources.Formula_Operator_Or, FormulaTokenFactory.CreateOrToken);
            TestSerializer(AppResources.Formula_Operator_Not, FormulaTokenFactory.CreateNotToken);
            TestSerializer(AppResources.Formula_Operator_Mod, FormulaTokenFactory.CreateModToken);
        }

        #endregion

        #region Functions

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestFunctions()
        {
            TestSerializer(",", FormulaTokenFactory.CreateParameterSeparatorToken);
            TestSerializer("exp", FormulaTokenFactory.CreateExpToken);
            TestSerializer("log", FormulaTokenFactory.CreateLogToken);
            TestSerializer("ln", FormulaTokenFactory.CreateLnToken);
            TestSerializer(AppResources.Formula_Function_Min, FormulaTokenFactory.CreateMinToken);
            TestSerializer(AppResources.Formula_Function_Max, FormulaTokenFactory.CreateMaxToken);
            TestSerializer("sin", FormulaTokenFactory.CreateSinToken);
            TestSerializer("cos", FormulaTokenFactory.CreateCosToken);
            TestSerializer("tan", FormulaTokenFactory.CreateTanToken);
            TestSerializer("arcsin", FormulaTokenFactory.CreateArcsinToken);
            TestSerializer("arccos", FormulaTokenFactory.CreateArccosToken);
            TestSerializer("arctan", FormulaTokenFactory.CreateArctanToken);
            TestSerializer(AppResources.Formula_Function_Sqrt, FormulaTokenFactory.CreateSqrtToken);
            TestSerializer(AppResources.Formula_Function_Abs, FormulaTokenFactory.CreateAbsToken);
            TestSerializer(AppResources.Formula_Function_Round, FormulaTokenFactory.CreateRoundToken);
            TestSerializer(AppResources.Formula_Function_Random, FormulaTokenFactory.CreateRandomToken);
        }

        #endregion

        #region Sensors

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestSensors()
        {
            TestSerializer(AppResources.Formula_Sensor_AccelerationX, FormulaTokenFactory.CreateAccelerationXToken);
            TestSerializer(AppResources.Formula_Sensor_AccelerationY, FormulaTokenFactory.CreateAccelerationYToken);
            TestSerializer(AppResources.Formula_Sensor_AccelerationZ, FormulaTokenFactory.CreateAccelerationZToken);
            TestSerializer(AppResources.Formula_Sensor_Compass, FormulaTokenFactory.CreateCompassToken);
            TestSerializer(AppResources.Formula_Sensor_InclinationX, FormulaTokenFactory.CreateInclinationXToken);
            TestSerializer(AppResources.Formula_Sensor_InclinationY, FormulaTokenFactory.CreateInclinationYToken);
            TestSerializer(AppResources.Formula_Sensor_Loudness, FormulaTokenFactory.CreateLoudnessToken);
        }

        #endregion

        #region Properties

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestProperties()
        {
            TestSerializer(AppResources.Formula_Property_Brightness, FormulaTokenFactory.CreateBrightnessToken);
            TestSerializer(AppResources.Formula_Property_Layer, FormulaTokenFactory.CreateLayerToken);
            TestSerializer(AppResources.Formula_Property_Transparency, FormulaTokenFactory.CreateTransparencyToken);
            TestSerializer(AppResources.Formula_Property_PositionX, FormulaTokenFactory.CreatePositionXToken);
            TestSerializer(AppResources.Formula_Property_PositionY, FormulaTokenFactory.CreatePositionYToken);
            TestSerializer(AppResources.Formula_Property_Rotation, FormulaTokenFactory.CreateRotationToken);
            TestSerializer(AppResources.Formula_Property_Size, FormulaTokenFactory.CreateSizeToken);
            TestSerializer(AppResources.Formula_Property_Transparency, FormulaTokenFactory.CreateTransparencyToken);
        }

        #endregion

        #region Variables

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestVariables()
        {
            TestSerializer<LocalVariable>(FormulaTokenFactory.CreateLocalVariableToken);
            TestSerializer<GlobalVariable>(FormulaTokenFactory.CreateGlobalVariableToken);
        }

        private void TestSerializer<TVariable>(Func<TVariable, ConstantFormulaTree> formulaCreator) where TVariable : Variable, new()
        {
            var x = new TVariable
            {
                Name = "TestVariable"
            };
            var y = new TVariable();
            TestSerializer(x.Name, formulaCreator.Invoke(x));
            TestSerializer(" ", formulaCreator.Invoke(y));
        }

        #endregion

        #region Brackets

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestBrackets()
        {
            TestSerializer("(", FormulaTokenFactory.CreateParenthesisToken(true));
            TestSerializer(")", FormulaTokenFactory.CreateParenthesisToken(false));
        }

        #endregion

        #region Helpers

        private void TestSerializer(string expectedValue, IFormulaToken token)
        {
            Assert.AreEqual(expectedValue, FormulaSerializer.Serialize(token));
        }

        private void TestSerializer(string expectedValue, Func<IFormulaToken> tokenCreator)
        {
            TestSerializer(expectedValue, tokenCreator.Invoke());
        }

        #endregion
    }
}
