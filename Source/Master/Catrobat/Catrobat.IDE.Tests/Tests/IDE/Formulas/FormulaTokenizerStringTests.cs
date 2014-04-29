using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.Formulas;
using Catrobat.IDE.Core.Models.Formulas.FormulaToken;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Tests.Extensions;
using Catrobat.IDE.Tests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Catrobat.IDE.Tests.Tests.IDE.Formulas
{
    /// <summary>Tests <see cref="FormulaTokenizer.Tokenize(string, out ParsingError)" />. </summary>
    [TestClass]
    public class FormulaTokenizerStringTests
    {
        private readonly UserVariable[] _localVariables = {new UserVariable {Name = "Variable1"}, new UserVariable {Name = "Variable2"}};
        private readonly UserVariable[] _globalVariables = {new UserVariable {Name = "Variable2"}, new UserVariable {Name = "Variable3"}};
        private readonly FormulaTokenizer _tokenizer;

        public FormulaTokenizerStringTests()
        {
            _tokenizer = new FormulaTokenizer(_localVariables, _globalVariables);
        }

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
            TestTokenizer((IList<IFormulaToken>) null, null);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestWhitespace()
        {
            foreach (var input in new[] { string.Empty, " ", "  " })
            {
                TestTokenizer(new List<IFormulaToken>(), input);
            }
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestCombination()
        {
            TestTokenizer(
                expectedTokens: new IFormulaToken[]
                {
                    FormulaTokenFactory.CreateDigitToken(4), 
                    FormulaTokenFactory.CreateDigitToken(2), 
                    FormulaTokenFactory.CreateLessEqualToken(), 
                    FormulaTokenFactory.CreateParenthesisToken(false), 
                    FormulaTokenFactory.CreateEqualsToken()
                }, input: "42≤)=");
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestParsingError()
        {
            TestTokenizer(
                expectedError: new ParsingError("Unknown token. ", 4, 0),
                input: "42<<blub5");
        }

        #region Constants

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestNumbers()
        {
            var culture = ServiceLocator.CultureService.GetCulture();
            foreach (var digit in Enumerable.Range(0, 10))
            {
                // access to foreach variable in closure
                var value = digit;
                TestTokenizer(() => FormulaTokenFactory.CreateDigitToken(value), value.ToString(culture));
            }
            TestTokenizer(FormulaTokenFactory.CreateDecimalSeparatorToken, culture.NumberFormat.NumberDecimalSeparator);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestConstants()
        {
            TestTokenizer(FormulaTokenFactory.CreatePiToken, new[] { "π" });
            TestTokenizer(FormulaTokenFactory.CreateTrueToken, new[] {AppResources.Formula_Constant_True});
            TestTokenizer(FormulaTokenFactory.CreateFalseToken, new[] {AppResources.Formula_Constant_False});
        }

        #endregion

        #region Operators

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestOperators()
        {
            TestTokenizer(FormulaTokenFactory.CreatePlusToken, "+");
            TestTokenizer(FormulaTokenFactory.CreateMinusToken, "-");
            TestTokenizer(FormulaTokenFactory.CreateMultiplyToken, "*");
            TestTokenizer(FormulaTokenFactory.CreateDivideToken, new[] {"/"});
            TestTokenizer(FormulaTokenFactory.CreateCaretToken, "^");
            TestTokenizer(FormulaTokenFactory.CreateEqualsToken, new[] { "=" });
            TestTokenizer(FormulaTokenFactory.CreateNotEqualsToken, new[] { "≠" });
            TestTokenizer(FormulaTokenFactory.CreateLessToken, "<");
            TestTokenizer(FormulaTokenFactory.CreateLessEqualToken, new[] { "≤" });
            TestTokenizer(FormulaTokenFactory.CreateGreaterToken, ">");
            TestTokenizer(FormulaTokenFactory.CreateGreaterEqualToken, new[] { "≥" });
            TestTokenizer(FormulaTokenFactory.CreateAndToken, AppResources.Formula_Operator_And);
            TestTokenizer(FormulaTokenFactory.CreateOrToken, AppResources.Formula_Operator_Or);
            TestTokenizer(FormulaTokenFactory.CreateNotToken, AppResources.Formula_Operator_Not);
            TestTokenizer(FormulaTokenFactory.CreateModToken, AppResources.Formula_Operator_Mod);
        }

        #endregion

        #region Functions

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]

        public void TestFunctions()
        {
            TestTokenizer(FormulaTokenFactory.CreateParameterSeparatorToken, ", ");
            TestTokenizer(FormulaTokenFactory.CreateExpToken, "exp");
            TestTokenizer(FormulaTokenFactory.CreateLogToken, "log");
            TestTokenizer(FormulaTokenFactory.CreateLnToken, "ln");
            TestTokenizer(FormulaTokenFactory.CreateMinToken, AppResources.Formula_Function_Min);
            TestTokenizer(FormulaTokenFactory.CreateMaxToken, AppResources.Formula_Function_Max);
            TestTokenizer(FormulaTokenFactory.CreateSinToken, "sin");
            TestTokenizer(FormulaTokenFactory.CreateCosToken, "cos");
            TestTokenizer(FormulaTokenFactory.CreateTanToken, "tan");
            TestTokenizer(FormulaTokenFactory.CreateArcsinToken, "arcsin");
            TestTokenizer(FormulaTokenFactory.CreateArccosToken, "arccos");
            TestTokenizer(FormulaTokenFactory.CreateArctanToken, "arctan");
            TestTokenizer(FormulaTokenFactory.CreateSqrtToken, AppResources.Formula_Function_Sqrt);
            TestTokenizer(FormulaTokenFactory.CreateAbsToken, AppResources.Formula_Function_Abs);
            TestTokenizer(FormulaTokenFactory.CreateRoundToken, AppResources.Formula_Function_Round);
            TestTokenizer(FormulaTokenFactory.CreateRandomToken, AppResources.Formula_Function_Random);
        }

        #endregion

        #region Sensors

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestSensors()
        {
            TestTokenizer(FormulaTokenFactory.CreateAccelerationXToken, new[] {AppResources.Formula_Sensor_AccelerationX});
            TestTokenizer(FormulaTokenFactory.CreateAccelerationYToken, new[] {AppResources.Formula_Sensor_AccelerationY});
            TestTokenizer(FormulaTokenFactory.CreateAccelerationZToken, new[] {AppResources.Formula_Sensor_AccelerationZ});
            TestTokenizer(FormulaTokenFactory.CreateCompassToken, new[] {AppResources.Formula_Sensor_Compass});
            TestTokenizer(FormulaTokenFactory.CreateInclinationXToken, new[] {AppResources.Formula_Sensor_InclinationX});
            TestTokenizer(FormulaTokenFactory.CreateInclinationYToken, new[] {AppResources.Formula_Sensor_InclinationY});
            TestTokenizer(FormulaTokenFactory.CreateLoudnessToken, new[] {AppResources.Formula_Sensor_Loudness});
        }

        #endregion

        #region Properties

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestProperties()
        {
            TestTokenizer(FormulaTokenFactory.CreateBrightnessToken, new[] {AppResources.Formula_Property_Brightness});
            TestTokenizer(FormulaTokenFactory.CreateLayerToken, new[] {AppResources.Formula_Property_Layer});
            TestTokenizer(FormulaTokenFactory.CreatePositionXToken, new[] {AppResources.Formula_Property_PositionX});
            TestTokenizer(FormulaTokenFactory.CreatePositionYToken, new[] {AppResources.Formula_Property_PositionY});
            TestTokenizer(FormulaTokenFactory.CreateRotationToken, new[] {AppResources.Formula_Property_Rotation});
            TestTokenizer(FormulaTokenFactory.CreateSizeToken, new[] {AppResources.Formula_Property_Size});
            TestTokenizer(FormulaTokenFactory.CreateTransparencyToken, new[] {AppResources.Formula_Property_Transparency});
        }

        #endregion

        #region Variables

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestVariables()
        {
            foreach (var localVariable in _localVariables)
            {
                // Access to foreach variable in closure
                var variable = localVariable;
                TestTokenizer(() => FormulaTokenFactory.CreateLocalVariableToken(variable), variable.Name);
            }

            // local variables before global variables
            var hiddenVariables = _globalVariables
                .Where(globalVariable => _localVariables
                    .Any(localVariable => localVariable.Name == globalVariable.Name));
            foreach (var globalVariable in _globalVariables.Except(hiddenVariables))
            {
                // Access to foreach variable in closure
                var variable = globalVariable;

                TestTokenizer(() => FormulaTokenFactory.CreateGlobalVariableToken(variable), variable.Name);
            }
        }

        #endregion

        #region Brackets

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestBrackets()
        {
            TestTokenizer(() => FormulaTokenFactory.CreateParenthesisToken(true), "(");
            TestTokenizer(() => FormulaTokenFactory.CreateParenthesisToken(false), ")");
        }

        #endregion

        #region Helpers

        private void TestTokenizer(ParsingError expectedError, string input)
        {
            var casedInputs = input == null
                ? new string[] { null }
                : new[] { input, input.ToLower(), input.ToUpper() };
            foreach (var casedInput in casedInputs)
            {
                ParsingError parsingError;
                Assert.IsNull(_tokenizer.Tokenize(casedInput, out parsingError));
                Assert.AreEqual(expectedError.Index, parsingError.Index);
            }
        }

        private void TestTokenizer(IList<IFormulaToken> expectedTokens, string input)
        {
            var casedInputs = input == null
                ? new string[] {null}
                : new[] {input, input.ToLower(), input.ToUpper()};
            foreach (var casedInput in casedInputs)
            {
                ParsingError parsingError;
                EnumerableAssert.AreEqual(expectedTokens, _tokenizer.Tokenize(casedInput, out parsingError));
                Assert.IsNull(parsingError);
            }
        }

        private void TestTokenizer(Func<IFormulaToken> tokenCreator, string input)
        {
            TestTokenizer(Enumerable.Repeat(tokenCreator.Invoke(), 1).ToList(), input);
        }

        private void TestTokenizer(Func<IFormulaToken> tokenCreator, IEnumerable<string> inputs)
        {
            foreach (var input in inputs)
            {
                TestTokenizer(tokenCreator, input);
            }
        }

        #endregion
    }
}
