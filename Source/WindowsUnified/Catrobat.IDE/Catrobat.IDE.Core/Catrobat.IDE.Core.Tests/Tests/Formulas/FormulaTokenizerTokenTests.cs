using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Formulas;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Formulas.Tokens;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Extensions;
using Catrobat.IDE.Core.Tests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.Formulas
{
    /// <summary>Tests <see cref="FormulaTokenizer.Tokenize(FormulaTree)" />. </summary>
    [TestClass]
    public class FormulaTokenizerTokenTests
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
            Assert.IsNull(FormulaTokenizer.Tokenize(null));
        }

        #region Constants

        [TestMethod, TestCategory("Formulas")]
        public void TestNumbers()
        {
            var culture = ServiceLocator.CultureService.GetCulture();
            foreach (var value in new[] {0, _random.Next(), -_random.Next(), _random.NextDouble()})
            {
                var tokens = FormulaTokenizer.Tokenize(FormulaTreeFactory.CreateNumberNode(value));
                var valueString = tokens.Select(token =>
                {
                    var digitToken = token as FormulaNodeNumber;
                    if (digitToken != null)
                    {
                        var digitString = digitToken.Value.ToString(culture);
                        Assert.AreEqual(1, digitString.Length);
                        return digitString;
                    }

                    var minusToken = token as FormulaNodeSubtract;
                    if (minusToken != null)
                    {
                        return culture.NumberFormat.NegativeSign;
                    }

                    var decimalToken = token as FormulaTokenDecimalSeparator;
                    if (decimalToken != null)
                    {
                        return culture.NumberFormat.NumberDecimalSeparator;
                    }

                    Assert.Fail();
                    return null;
                }).Aggregate(string.Empty, (acc, x) => acc + x);
                double actualValue;
                Assert.IsTrue(double.TryParse(valueString, NumberStyles.Number, culture, out actualValue));
                Assert.AreEqual(value, actualValue);
            }
        }

        [TestMethod, TestCategory("Formulas")]
        public void TestConstants()
        {
            TestTokenizer(FormulaTokenFactory.CreatePiToken, FormulaTreeFactory.CreatePiNode);

            TestTokenizer(FormulaTokenFactory.CreateTrueToken, FormulaTreeFactory.CreateTrueNode);
            TestTokenizer(FormulaTokenFactory.CreateFalseToken, FormulaTreeFactory.CreateFalseNode);
        }

        #endregion

        #region Operators

        [TestMethod, TestCategory("Formulas")]
        public void TestOperators()
        {
            TestInfixOperatorN(FormulaTokenFactory.CreatePlusToken, FormulaTreeFactory.CreateAddNode);
            TestInfixOperatorN(FormulaTokenFactory.CreateMinusToken, FormulaTreeFactory.CreateSubtractNode);
            TestPrefixOperatorN(FormulaTokenFactory.CreateMinusToken, FormulaTreeFactory.CreateNegativeSignNode);
            TestInfixOperatorN(FormulaTokenFactory.CreateMultiplyToken, FormulaTreeFactory.CreateMultiplyNode);
            TestInfixOperatorN(FormulaTokenFactory.CreateDivideToken, FormulaTreeFactory.CreateDivideNode);
            TestInfixOperatorN(FormulaTokenFactory.CreateCaretToken, FormulaTreeFactory.CreatePowerNode);

            TestInfixOperatorNL(FormulaTokenFactory.CreateEqualsToken, FormulaTreeFactory.CreateEqualsNode);
            TestInfixOperatorNL(FormulaTokenFactory.CreateNotEqualsToken, FormulaTreeFactory.CreateNotEqualsNode);

            TestInfixOperatorNL(FormulaTokenFactory.CreateLessToken, FormulaTreeFactory.CreateLessNode);
            TestInfixOperatorNL(FormulaTokenFactory.CreateLessEqualToken, FormulaTreeFactory.CreateLessEqualNode);
            TestInfixOperatorNL(FormulaTokenFactory.CreateGreaterToken, FormulaTreeFactory.CreateGreaterNode);
            TestInfixOperatorNL(FormulaTokenFactory.CreateGreaterEqualToken, FormulaTreeFactory.CreateGreaterEqualNode);

            TestInfixOperatorNL(FormulaTokenFactory.CreateAndToken, FormulaTreeFactory.CreateAndNode);
            TestInfixOperatorNL(FormulaTokenFactory.CreateOrToken, FormulaTreeFactory.CreateOrNode);

            TestPrefixOperatorNL(FormulaTokenFactory.CreateNotToken, FormulaTreeFactory.CreateNotNode);

            TestInfixOperatorN(FormulaTokenFactory.CreateModToken, FormulaTreeFactory.CreateModuloNode);
        }

        // ReSharper disable once InconsistentNaming
        private void TestPrefixOperatorNL(Func<FormulaNodePrefixOperator> tokenCreator, Func<FormulaTree, UnaryFormulaTree> formulaCreator)
        {
            TestTokenizerNL(
                expectedTokens: x => Enumerable.Repeat(tokenCreator.Invoke(), 1).Concat(x),
                formulaCreator: formulaCreator);
        }

        private void TestPrefixOperatorN(Func<FormulaNodeInfixOperator> tokenCreator, Func<FormulaTree, UnaryFormulaTree> formulaCreator)
        {
            TestTokenizerN(
                expectedTokens: x => Enumerable.Repeat(tokenCreator.Invoke(), 1).Concat(x),
                formulaCreator: formulaCreator);
        }

        private void TestInfixOperatorN(Func<FormulaNodeInfixOperator> tokenCreator, Func<FormulaTree, FormulaTree, FormulaNodeInfixOperator> formulaCreator)
        {
            TestTokenizerN(
                expectedTokens: (x, y) => x.Concat(Enumerable.Repeat(tokenCreator.Invoke(), 1)).Concat(y),
                formulaCreator: formulaCreator);
        }

        // ReSharper disable once InconsistentNaming
        private void TestInfixOperatorNL(Func<FormulaNodeInfixOperator> tokenCreator, Func<FormulaTree, FormulaTree, FormulaNodeInfixOperator> formulaCreator)
        {
            TestTokenizerNL(
                expectedTokens: (x, y) => x.Concat(Enumerable.Repeat(tokenCreator.Invoke(), 1)).Concat(y),
                formulaCreator: formulaCreator);
        }

        #endregion

        #region Functions

        [TestMethod, TestCategory("Formulas")]
        public void TestFunctions()
        {
            TestUnaryFunctionN(FormulaTokenFactory.CreateExpToken, FormulaTreeFactory.CreateExpNode);
            TestUnaryFunctionN(FormulaTokenFactory.CreateLogToken, FormulaTreeFactory.CreateLogNode);
            TestUnaryFunctionN(FormulaTokenFactory.CreateLnToken, FormulaTreeFactory.CreateLnNode);

            TestBinaryFunctionN(FormulaTokenFactory.CreateMinToken, FormulaTreeFactory.CreateMinNode);
            TestBinaryFunctionN(FormulaTokenFactory.CreateMaxToken, FormulaTreeFactory.CreateMaxNode);

            TestUnaryFunctionN(FormulaTokenFactory.CreateSinToken, FormulaTreeFactory.CreateSinNode);
            TestUnaryFunctionN(FormulaTokenFactory.CreateCosToken, FormulaTreeFactory.CreateCosNode);
            TestUnaryFunctionN(FormulaTokenFactory.CreateTanToken, FormulaTreeFactory.CreateTanNode);
            TestUnaryFunctionN(FormulaTokenFactory.CreateArcsinToken, FormulaTreeFactory.CreateArcsinNode);
            TestUnaryFunctionN(FormulaTokenFactory.CreateArccosToken, FormulaTreeFactory.CreateArccosNode);
            TestUnaryFunctionN(FormulaTokenFactory.CreateArctanToken, FormulaTreeFactory.CreateArctanNode);

            TestUnaryFunctionN(FormulaTokenFactory.CreateSqrtToken, FormulaTreeFactory.CreateSqrtNode);

            TestUnaryFunctionN(FormulaTokenFactory.CreateAbsToken, FormulaTreeFactory.CreateAbsNode);
            TestUnaryFunctionN(FormulaTokenFactory.CreateRoundToken, FormulaTreeFactory.CreateRoundNode);
            TestBinaryFunctionN(FormulaTokenFactory.CreateRandomToken, FormulaTreeFactory.CreateRandomNode);
        }

        private void TestUnaryFunctionN(Func<FormulaNodeUnaryFunction> tokenCreator, Func<FormulaTree, UnaryFormulaTree> formulaCreator)
        {
            TestTokenizerN(
                expectedTokens: x => Enumerable.Repeat<IFormulaToken>(tokenCreator.Invoke(), 1)
                    .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateParenthesisToken(true), 1))
                    .Concat(x)
                    .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateParenthesisToken(false), 1)),
                formulaCreator: formulaCreator);
        }

        private void TestBinaryFunctionN(Func<FormulaNodeBinaryFunction> tokenCreator, Func<FormulaTree, FormulaTree, FormulaNodeBinaryFunction> formulaCreator)
        {
            TestTokenizerN(
                expectedTokens: (x, y) => Enumerable.Repeat<IFormulaToken>(tokenCreator.Invoke(), 1)
                    .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateParenthesisToken(true), 1))
                    .Concat(x)
                    .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateParameterSeparatorToken(), 1))
                    .Concat(y)
                    .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateParenthesisToken(false), 1)),
                formulaCreator: formulaCreator);
        }

        #endregion

        #region Sensors

        [TestMethod, TestCategory("Formulas")]
        public void TestSensors()
        {
            TestTokenizer(FormulaTokenFactory.CreateAccelerationXToken, FormulaTreeFactory.CreateAccelerationXNode);
            TestTokenizer(FormulaTokenFactory.CreateAccelerationYToken, FormulaTreeFactory.CreateAccelerationYNode);
            TestTokenizer(FormulaTokenFactory.CreateAccelerationZToken, FormulaTreeFactory.CreateAccelerationZNode);
            TestTokenizer(FormulaTokenFactory.CreateCompassToken, FormulaTreeFactory.CreateCompassNode);
            TestTokenizer(FormulaTokenFactory.CreateInclinationXToken, FormulaTreeFactory.CreateInclinationXNode);
            TestTokenizer(FormulaTokenFactory.CreateInclinationYToken, FormulaTreeFactory.CreateInclinationYNode);
            TestTokenizer(FormulaTokenFactory.CreateLoudnessToken, FormulaTreeFactory.CreateLoudnessNode);
        }

        #endregion

        #region Properties

        [TestMethod, TestCategory("Formulas")]
        public void TestProperties()
        {
            TestTokenizer(FormulaTokenFactory.CreateBrightnessToken, FormulaTreeFactory.CreateBrightnessNode);
            TestTokenizer(FormulaTokenFactory.CreateLayerToken, FormulaTreeFactory.CreateLayerNode);
            TestTokenizer(FormulaTokenFactory.CreateTransparencyToken, FormulaTreeFactory.CreateTransparencyNode);
            TestTokenizer(FormulaTokenFactory.CreatePositionXToken, FormulaTreeFactory.CreatePositionXNode);
            TestTokenizer(FormulaTokenFactory.CreatePositionYToken, FormulaTreeFactory.CreatePositionYNode);
            TestTokenizer(FormulaTokenFactory.CreateRotationToken, FormulaTreeFactory.CreateRotationNode);
            TestTokenizer(FormulaTokenFactory.CreateSizeToken, FormulaTreeFactory.CreateSizeNode);
        }

        #endregion

        #region Variables

        [TestMethod, TestCategory("Formulas")]
        public void TestVariables()
        {
            TestVariable<LocalVariable>(FormulaTokenFactory.CreateLocalVariableToken, FormulaTreeFactory.CreateLocalVariableNode);
            TestVariable<GlobalVariable>(FormulaTokenFactory.CreateGlobalVariableToken, FormulaTreeFactory.CreateGlobalVariableNode);
        }

        private void TestVariable<TVariable>(Func<TVariable, IFormulaToken> expectedToken, Func<TVariable, ConstantFormulaTree> formulaCreator) where TVariable : Variable, new()
        {
            var x = new TVariable
            {
                Name = "TestVariable"
            };
            var y = new TVariable();
            TestTokenizer(Enumerable.Repeat(expectedToken.Invoke(x), 1), formulaCreator.Invoke(x));
            TestTokenizer(Enumerable.Repeat(expectedToken.Invoke(y), 1), formulaCreator.Invoke(y));
            TestTokenizer(Enumerable.Repeat(expectedToken.Invoke(null), 1), formulaCreator.Invoke(null));
        }

        #endregion

        #region Brackets

        [TestMethod, TestCategory("Formulas")]
        public void TestBrackets()
        {
            TestBracketNL(FormulaTokenFactory.CreateParenthesisToken, FormulaTreeFactory.CreateParenthesesNode);
        }

        // ReSharper disable once InconsistentNaming
        private void TestBracketNL(Func<bool, FormulaTokenBracket> expectedToken, Func<FormulaTree, FormulaNodeBrackets> formulaCreator)
        {
            TestTokenizerNL(
                expectedTokens: x => Enumerable.Repeat(expectedToken.Invoke(true), 1)
                    .Concat(x)
                    .Concat(Enumerable.Repeat(expectedToken.Invoke(false), 1)),
                formulaCreator: formulaCreator);

        }

        #endregion

        #region Helpers

        private void TestTokenizer(IEnumerable<IFormulaToken> expectedTokens, FormulaTree formula)
        {
            EnumerableAssert.AreTestEqual(expectedTokens, FormulaTokenizer.Tokenize(formula));
        }

        private void TestTokenizer(IFormulaToken expectedToken, FormulaTree formula)
        {
            TestTokenizer(Enumerable.Repeat(expectedToken, 1), formula);
        }

        private void TestTokenizer(Func<IFormulaToken> tokenCreator, Func<FormulaTree> formulaCreator)
        {
            TestTokenizer(tokenCreator.Invoke(), formulaCreator.Invoke());
        }

        private void TestTokenizerN(Func<IEnumerable<IFormulaToken>, IEnumerable<IFormulaToken>> expectedTokens, Func<FormulaTree, UnaryFormulaTree> formulaCreator)
        {
            var x = _random.Next();
            TestTokenizer(
                expectedTokens: expectedTokens.Invoke(FormulaTreeFactory.CreateNumberNode(x).Tokenize()), 
                formula: formulaCreator.Invoke(FormulaTreeFactory.CreateNumberNode(x)));

            // test default value
            TestTokenizer(
                expectedTokens: expectedTokens.Invoke(Enumerable.Empty<IFormulaToken>()), 
                formula: formulaCreator.Invoke(null));
        }

        private void TestTokenizerL(Func<IEnumerable<IFormulaToken>, IEnumerable<IFormulaToken>> expectedTokens, Func<FormulaTree, UnaryFormulaTree> formulaCreator)
        {
            var x = _random.NextBool();
            TestTokenizer(
                expectedTokens: expectedTokens.Invoke(Enumerable.Repeat(FormulaTokenFactory.CreateTruthValueToken(x), 1)), 
                formula: formulaCreator.Invoke(FormulaTreeFactory.CreateTruthValueNode(x)));

            // test default value
            TestTokenizer(
                expectedTokens: expectedTokens.Invoke(Enumerable.Empty<IFormulaToken>()), 
                formula: formulaCreator.Invoke(null));
        }

        // ReSharper disable once InconsistentNaming
        private void TestTokenizerNL(Func<IEnumerable<IFormulaToken>, IEnumerable<IFormulaToken>> expectedTokens, Func<FormulaTree, UnaryFormulaTree> formulaCreator)
        {
            TestTokenizerL(expectedTokens, formulaCreator);
            TestTokenizerN(expectedTokens, formulaCreator);
        }

        private void TestTokenizerN(Func<IEnumerable<IFormulaToken>, IEnumerable<IFormulaToken>, IEnumerable<IFormulaToken>> expectedTokens, Func<FormulaTree, FormulaTree, BinaryFormulaTree> formulaCreator)
        {
            var x = _random.Next();
            var y = _random.Next();
            TestTokenizer(
                expectedTokens: expectedTokens.Invoke(FormulaTreeFactory.CreateNumberNode(x).Tokenize(), FormulaTreeFactory.CreateNumberNode(y).Tokenize()),
                formula: formulaCreator.Invoke(FormulaTreeFactory.CreateNumberNode(x), FormulaTreeFactory.CreateNumberNode(y)));
            TestTokenizer(
                expectedTokens: expectedTokens.Invoke(Enumerable.Empty<IFormulaToken>(), Enumerable.Empty<IFormulaToken>()),
                formula: formulaCreator.Invoke(null, null));
        }

        private void TestTokenizerL(Func<IEnumerable<IFormulaToken>, IEnumerable<IFormulaToken>, IEnumerable<IFormulaToken>> expectedTokens, Func<FormulaTree, FormulaTree, BinaryFormulaTree> formulaCreator)
        {
            var x = _random.Next();
            var y = _random.Next();
            TestTokenizer(
                expectedTokens: expectedTokens.Invoke(FormulaTreeFactory.CreateNumberNode(x).Tokenize(), FormulaTreeFactory.CreateNumberNode(y).Tokenize()),
                formula: formulaCreator.Invoke(FormulaTreeFactory.CreateNumberNode(x), FormulaTreeFactory.CreateNumberNode(y)));
            TestTokenizer(
                expectedTokens: expectedTokens.Invoke(Enumerable.Empty<IFormulaToken>(), Enumerable.Empty<IFormulaToken>()),
                formula: formulaCreator.Invoke(null, null));
        }

        // ReSharper disable once InconsistentNaming
        private void TestTokenizerNL(Func<IEnumerable<IFormulaToken>, IEnumerable<IFormulaToken>, IEnumerable<IFormulaToken>> expectedTokens, Func<FormulaTree, FormulaTree, BinaryFormulaTree> formulaCreator)
        {
            TestTokenizerN(expectedTokens, formulaCreator);
            TestTokenizerL(expectedTokens, formulaCreator);
        }

        #endregion
    }
}
