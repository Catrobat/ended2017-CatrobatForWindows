using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Formulas;
using Catrobat.IDE.Core.Models.Formulas.FormulaToken;
using Catrobat.IDE.Core.Models.Formulas.FormulaTree;
using Catrobat.IDE.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Catrobat.IDE.Tests.Tests.IDE.Formula
{
    [TestClass]
    public class FormulaTokenizerTests
    {
        private readonly Random _random = new Random();

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestNull()
        {
            Assert.IsNull(FormulaTokenizer.Tokenize(null));
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestConstants()
        {
            foreach (var value in new[] {0, _random.Next(), -_random.Next(), _random.NextDouble()})
            {
                var tokens = FormulaTokenizer.Tokenize(FormulaTreeFactory.CreateNumberNode(value));
                var valueString = tokens.Select(token =>
                {
                    var digitToken = token as FormulaNodeNumber;
                    if (digitToken != null)
                    {
                        var digitString = digitToken.Value.ToString(CultureInfo.InvariantCulture);
                        Assert.AreEqual(1, digitString.Length);
                        return digitString;
                    }

                    var minusToken = token as FormulaNodeSubtract;
                    if (minusToken != null)
                    {
                        return CultureInfo.InvariantCulture.NumberFormat.NegativeSign;
                    }

                    var decimalToken = token as FormulaTokenDecimalSeparator;
                    if (decimalToken != null)
                    {
                        return CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator;
                    }

                    Assert.Fail();
                    return null;
                }).Aggregate(string.Empty, (acc, x) => acc + x);
                double actualValue;
                Assert.IsTrue(double.TryParse(valueString, NumberStyles.Number, CultureInfo.InvariantCulture, out actualValue));
                Assert.AreEqual(value, actualValue);
            }

            TestTokenizer(new [] { FormulaTokenFactory.CreatePiToken() }, FormulaTreeFactory.CreatePiNode);

            TestTokenizer(new[] { FormulaTokenFactory.CreateTrueToken() }, FormulaTreeFactory.CreateTrueNode);
            TestTokenizer(new[] { FormulaTokenFactory.CreateFalseToken() }, FormulaTreeFactory.CreateFalseNode);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestOperators()
        {
            TestTokenizerN(CreateExpectedTokens(FormulaTokenFactory.CreatePlusToken()), FormulaTreeFactory.CreateAddNode);
            TestTokenizerN(CreateExpectedTokens(FormulaTokenFactory.CreateMinusToken()), FormulaTreeFactory.CreateSubtractNode);
            TestTokenizerN(x => Enumerable.Repeat(FormulaTokenFactory.CreateMinusToken(), 1).Concat(x), FormulaTreeFactory.CreateNegativeSignNode);
            TestTokenizerN(CreateExpectedTokens(FormulaTokenFactory.CreateMultiplyToken()), FormulaTreeFactory.CreateMultiplyNode);
            TestTokenizerN(CreateExpectedTokens(FormulaTokenFactory.CreateDivideToken()), FormulaTreeFactory.CreateDivideNode);
            TestTokenizerN(CreateExpectedTokens(FormulaTokenFactory.CreateCaretToken()), FormulaTreeFactory.CreatePowerNode);

            TestTokenizer(CreateExpectedTokens(FormulaTokenFactory.CreateEqualsToken()), FormulaTreeFactory.CreateEqualsNode);
            TestTokenizer(CreateExpectedTokens(FormulaTokenFactory.CreateNotEqualsToken()), FormulaTreeFactory.CreateNotEqualsNode);

            TestTokenizer(CreateExpectedTokens(FormulaTokenFactory.CreateLessToken()), FormulaTreeFactory.CreateLessNode);
            TestTokenizer(CreateExpectedTokens(FormulaTokenFactory.CreateLessEqualToken()), FormulaTreeFactory.CreateLessEqualNode);
            TestTokenizer(CreateExpectedTokens(FormulaTokenFactory.CreateGreaterToken()), FormulaTreeFactory.CreateGreaterNode);
            TestTokenizer(CreateExpectedTokens(FormulaTokenFactory.CreateGreaterEqualToken()), FormulaTreeFactory.CreateGreaterEqualNode);

            TestTokenizer(CreateExpectedTokens(FormulaTokenFactory.CreateAndToken()), FormulaTreeFactory.CreateAndNode);
            TestTokenizer(CreateExpectedTokens(FormulaTokenFactory.CreateOrToken()), FormulaTreeFactory.CreateOrNode);

            TestTokenizer(CreateExpectedTokens(FormulaTokenFactory.CreateNotToken()), FormulaTreeFactory.CreateNotNode);

            TestTokenizerN(CreateExpectedTokens(FormulaTokenFactory.CreateModToken()), FormulaTreeFactory.CreateModuloNode);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestFunctions()
        {
            TestTokenizerN(CreateExpectedTokens(FormulaTokenFactory.CreateExpToken()), FormulaTreeFactory.CreateExpNode);
            TestTokenizerN(CreateExpectedTokens(FormulaTokenFactory.CreateLogToken()), FormulaTreeFactory.CreateLogNode);
            TestTokenizerN(CreateExpectedTokens(FormulaTokenFactory.CreateLnToken()), FormulaTreeFactory.CreateLnNode);

            TestTokenizerN(CreateExpectedTokens(FormulaTokenFactory.CreateMinToken()), FormulaTreeFactory.CreateMinNode);
            TestTokenizerN(CreateExpectedTokens(FormulaTokenFactory.CreateMaxToken()), FormulaTreeFactory.CreateMaxNode);

            TestTokenizerN(CreateExpectedTokens(FormulaTokenFactory.CreateSinToken()), FormulaTreeFactory.CreateSinNode);
            TestTokenizerN(CreateExpectedTokens(FormulaTokenFactory.CreateCosToken()), FormulaTreeFactory.CreateCosNode);
            TestTokenizerN(CreateExpectedTokens(FormulaTokenFactory.CreateTanToken()), FormulaTreeFactory.CreateTanNode);
            TestTokenizerN(CreateExpectedTokens(FormulaTokenFactory.CreateArcsinToken()), FormulaTreeFactory.CreateArcsinNode);
            TestTokenizerN(CreateExpectedTokens(FormulaTokenFactory.CreateArccosToken()), FormulaTreeFactory.CreateArccosNode);
            TestTokenizerN(CreateExpectedTokens(FormulaTokenFactory.CreateArctanToken()), FormulaTreeFactory.CreateArctanNode);

            TestTokenizerN(CreateExpectedTokens(FormulaTokenFactory.CreateSqrtToken()), FormulaTreeFactory.CreateSqrtNode);

            TestTokenizerN(CreateExpectedTokens(FormulaTokenFactory.CreateAbsToken()), FormulaTreeFactory.CreateAbsNode);
            TestTokenizerN(CreateExpectedTokens(FormulaTokenFactory.CreateRoundToken()), FormulaTreeFactory.CreateRoundNode);
            TestTokenizerN(CreateExpectedTokens(FormulaTokenFactory.CreateRandomToken()), FormulaTreeFactory.CreateRandomNode);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
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

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
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

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestVariables()
        {
            TestTokenizer(FormulaTokenFactory.CreateLocalVariableToken, FormulaTreeFactory.CreateLocalVariableNode);
            TestTokenizer(FormulaTokenFactory.CreateGlobalVariableToken, FormulaTreeFactory.CreateGlobalVariableNode);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestParentheses()
        {
            TestTokenizer(
                expectedTokens: x => Enumerable.Repeat(FormulaTokenFactory.CreateParenthesisToken(true), 1)
                    .Concat(x)
                    .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateParenthesisToken(false), 1)), 
                formulaCreator: FormulaTreeFactory.CreateParenthesesNode);
        }

        #region Helpers

        private void TestTokenizer(IEnumerable<IFormulaToken> expectedTokens, IFormulaTree formula)
        {
            EnumerableAssert.AreEqual(expectedTokens, FormulaTokenizer.Tokenize(formula));
        }

        private void TestTokenizer(IEnumerable<IFormulaToken> expectedTokens, Func<IFormulaTree> formulaCreator)
        {
            TestTokenizer(expectedTokens, formulaCreator.Invoke());
        }

        private void TestTokenizer(Func<IFormulaToken> expectedToken, Func<ConstantFormulaTree> formulaCreator)
        {
            TestTokenizer(Enumerable.Repeat(expectedToken.Invoke(), 1), formulaCreator);
        }

        private void TestTokenizerN(Func<IEnumerable<IFormulaToken>, IEnumerable<IFormulaToken>> expectedTokens, Func<IFormulaTree, UnaryFormulaTree> formulaCreator)
        {
            var x = _random.Next();
            TestTokenizer(expectedTokens.Invoke(FormulaTreeFactory.CreateNumberNode(x).Tokenize()), formulaCreator.Invoke(FormulaTreeFactory.CreateNumberNode(x)));
            TestTokenizer(expectedTokens.Invoke(Enumerable.Empty<IFormulaToken>()), formulaCreator.Invoke(null));
        }

        private void TestTokenizerL(Func<IEnumerable<IFormulaToken>, IEnumerable<IFormulaToken>> expectedTokens, Func<IFormulaTree, UnaryFormulaTree> formulaCreator)
        {
            var x = _random.NextBool();
            TestTokenizer(expectedTokens.Invoke(Enumerable.Repeat(FormulaTokenFactory.CreateTruthValueToken(x), 1)), formulaCreator.Invoke(FormulaTreeFactory.CreateTruthValueNode(x)));
            TestTokenizer(expectedTokens.Invoke(Enumerable.Empty<IFormulaToken>()), formulaCreator.Invoke(null));
        }

        private void TestTokenizer(Func<IEnumerable<IFormulaToken>, IEnumerable<IFormulaToken>> expectedTokens, Func<IFormulaTree, UnaryFormulaTree> formulaCreator)
        {
            TestTokenizerL(expectedTokens, formulaCreator);
            TestTokenizerN(expectedTokens, formulaCreator);
        }

        private void TestTokenizerN(Func<IEnumerable<IFormulaToken>, IEnumerable<IFormulaToken>, IEnumerable<IFormulaToken>> expectedTokens, Func<IFormulaTree, IFormulaTree, BinaryFormulaTree> formulaCreator)
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

        private void TestTokenizerL(Func<IEnumerable<IFormulaToken>, IEnumerable<IFormulaToken>, IEnumerable<IFormulaToken>> expectedTokens, Func<IFormulaTree, IFormulaTree, BinaryFormulaTree> formulaCreator)
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

        private void TestTokenizer(Func<IEnumerable<IFormulaToken>, IEnumerable<IFormulaToken>, IEnumerable<IFormulaToken>> expectedTokens, Func<IFormulaTree, IFormulaTree, BinaryFormulaTree> formulaCreator)
        {
            TestTokenizerN(expectedTokens, formulaCreator);
            TestTokenizerL(expectedTokens, formulaCreator);
        }

        private void TestTokenizer(Func<UserVariable, IFormulaToken> expectedToken, Func<UserVariable, ConstantFormulaTree> formulaCreator)
        {
            var x = new UserVariable
            {
                Name = "TestVariable"
            };
            var y = new UserVariable();
            TestTokenizer(Enumerable.Repeat(expectedToken.Invoke(x), 1), formulaCreator.Invoke(x));
            TestTokenizer(Enumerable.Repeat(expectedToken.Invoke(y), 1), formulaCreator.Invoke(y));
            TestTokenizer(Enumerable.Repeat(expectedToken.Invoke(null), 1), formulaCreator.Invoke(null));
        }

        private Func<IEnumerable<IFormulaToken>, IEnumerable<IFormulaToken>> CreateExpectedTokens(FormulaNodeUnaryFunction expectedFunctionToken)
        {
            return x => Enumerable.Repeat<IFormulaToken>(expectedFunctionToken, 1)
                .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateParenthesisToken(true), 1))
                .Concat(x)
                .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateParenthesisToken(false), 1));
        }

        private Func<IEnumerable<IFormulaToken>, IEnumerable<IFormulaToken>, IEnumerable<IFormulaToken>> CreateExpectedTokens(FormulaNodeBinaryFunction expectedFunctionToken)
        {
            return (x, y) => Enumerable.Repeat<IFormulaToken>(expectedFunctionToken, 1)
                .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateParenthesisToken(true), 1))
                .Concat(x)
                .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateParameterSeparatorToken(), 1))
                .Concat(y)
                .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateParenthesisToken(false), 1));
        }

        private Func<IEnumerable<IFormulaToken>, IEnumerable<IFormulaToken>> CreateExpectedTokens(FormulaNodePrefixOperator expectedPrefixToken)
        {
            return x => Enumerable.Repeat(expectedPrefixToken, 1)
                .Concat(x);
        }

        private Func<IEnumerable<IFormulaToken>, IEnumerable<IFormulaToken>, IEnumerable<IFormulaToken>> CreateExpectedTokens(FormulaNodeInfixOperator expectedInfixToken)
        {
            return (x, y) => x
                .Concat(Enumerable.Repeat(expectedInfixToken, 1))
                .Concat(y);
        }

        #endregion
    }
}
