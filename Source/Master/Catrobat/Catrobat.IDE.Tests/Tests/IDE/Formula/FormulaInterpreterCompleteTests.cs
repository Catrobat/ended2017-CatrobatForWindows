using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.Formulas;
using Catrobat.IDE.Core.Models.Formulas.FormulaToken;
using Catrobat.IDE.Core.Models.Formulas.FormulaTree;
using Catrobat.IDE.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catrobat.IDE.Tests.Tests.IDE.Formula
{
    /// <summary>Tests <see cref="FormulaInterpreter.Complete" />. </summary>
    [TestClass]
    public class FormulaInterpreterCompleteTests
    {
        private readonly Random _random = new Random();

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestNullOrEmpty()
        {
            TestComplete(null);
            TestComplete(new IFormulaToken[] {});
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestConstants()
        {
            for (var digit = 0; digit <= 9; digit++)
            {
                TestComplete(new IFormulaToken[] { FormulaTokenFactory.CreateDigitToken(digit) });
                TestComplete(new IFormulaToken[] { FormulaTokenFactory.CreateMinusToken(), FormulaTokenFactory.CreateDigitToken(digit) });
            }
            TestComplete(new IFormulaToken[] { FormulaTokenFactory.CreateDigitToken(4), FormulaTokenFactory.CreateDigitToken(2) });
            TestComplete(new IFormulaToken[] { FormulaTokenFactory.CreateDigitToken(0), FormulaTokenFactory.CreateDecimalSeparatorToken(), FormulaTokenFactory.CreateDigitToken(4), FormulaTokenFactory.CreateDigitToken(2) });
            TestConstant(FormulaTokenFactory.CreatePiToken);
            TestConstant(FormulaTokenFactory.CreateTrueToken);
            TestConstant(FormulaTokenFactory.CreateFalseToken);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestOperators()
        {
            Assert.Inconclusive();
            //TestDoubleInfixOperator(FormulaTreeFactory.CreateAddNode, FormulaTokenFactory.CreatePlusToken);
            //TestDoubleInfixOperator(FormulaTreeFactory.CreateSubtractNode, FormulaTokenFactory.CreateMinusToken);
            //TestInterpret(
            //        expected: FormulaTreeFactory.CreateNegativeSignNode(FormulaTreeFactory.CreatePiNode()),
            //        tokens: new IFormulaToken[] { FormulaTokenFactory.CreateMinusToken(), FormulaTokenFactory.CreatePiToken() });
            //TestDoubleInfixOperator(FormulaTreeFactory.CreateMultiplyNode, FormulaTokenFactory.CreateMultiplyToken);
            //TestDoubleInfixOperator(FormulaTreeFactory.CreateDivideNode, FormulaTokenFactory.CreateDivideToken);
            //TestDoubleInfixOperator(FormulaTreeFactory.CreatePowerNode, FormulaTokenFactory.CreateCaretToken);
            //TestBoolInfixOperator(FormulaTreeFactory.CreateEqualsNode, FormulaTokenFactory.CreateEqualsToken);
            //TestDoubleInfixOperator(FormulaTreeFactory.CreateEqualsNode, FormulaTokenFactory.CreateEqualsToken);
            //TestBoolInfixOperator(FormulaTreeFactory.CreateNotEqualsNode, FormulaTokenFactory.CreateNotEqualsToken);
            //TestDoubleInfixOperator(FormulaTreeFactory.CreateNotEqualsNode, FormulaTokenFactory.CreateNotEqualsToken);
            //TestDoubleInfixOperator(FormulaTreeFactory.CreateLessNode, FormulaTokenFactory.CreateLessToken);
            //TestDoubleInfixOperator(FormulaTreeFactory.CreateLessEqualNode, FormulaTokenFactory.CreateLessEqualToken);
            //TestDoubleInfixOperator(FormulaTreeFactory.CreateGreaterNode, FormulaTokenFactory.CreateGreaterToken);
            //TestDoubleInfixOperator(FormulaTreeFactory.CreateGreaterEqualNode, FormulaTokenFactory.CreateGreaterEqualToken);
            //TestDoubleInfixOperator(FormulaTreeFactory.CreateAndNode, FormulaTokenFactory.CreateAndToken);
            //TestBoolInfixOperator(FormulaTreeFactory.CreateAndNode, FormulaTokenFactory.CreateAndToken);
            //TestDoubleInfixOperator(FormulaTreeFactory.CreateOrNode, FormulaTokenFactory.CreateOrToken);
            //TestBoolInfixOperator(FormulaTreeFactory.CreateOrNode, FormulaTokenFactory.CreateOrToken);
            //TestDoublePrefixOperator(FormulaTreeFactory.CreateNotNode, FormulaTokenFactory.CreateNotToken);
            //TestBoolPrefixOperator(FormulaTreeFactory.CreateNotNode, FormulaTokenFactory.CreateNotToken);
            //TestDoubleInfixOperator(FormulaTreeFactory.CreateModuloNode, FormulaTokenFactory.CreateModToken);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestFunctions()
        {
            Assert.Inconclusive();
            //TestDoubleUnaryFunction(FormulaTreeFactory.CreateExpNode, FormulaTokenFactory.CreateExpToken);
            //TestDoubleUnaryFunction(FormulaTreeFactory.CreateLogNode, FormulaTokenFactory.CreateLogToken);
            //TestDoubleUnaryFunction(FormulaTreeFactory.CreateLnNode, FormulaTokenFactory.CreateLnToken);
            //TestDoubleBinaryFunction(FormulaTreeFactory.CreateMinNode, FormulaTokenFactory.CreateMinToken);
            //TestDoubleBinaryFunction(FormulaTreeFactory.CreateMaxNode, FormulaTokenFactory.CreateMaxToken);
            //TestDoubleUnaryFunction(FormulaTreeFactory.CreateSinNode, FormulaTokenFactory.CreateSinToken);
            //TestDoubleUnaryFunction(FormulaTreeFactory.CreateCosNode, FormulaTokenFactory.CreateCosToken);
            //TestDoubleUnaryFunction(FormulaTreeFactory.CreateTanNode, FormulaTokenFactory.CreateTanToken);
            //TestDoubleUnaryFunction(FormulaTreeFactory.CreateArcsinNode, FormulaTokenFactory.CreateArcsinToken);
            //TestDoubleUnaryFunction(FormulaTreeFactory.CreateArccosNode, FormulaTokenFactory.CreateArccosToken);
            //TestDoubleUnaryFunction(FormulaTreeFactory.CreateArctanNode, FormulaTokenFactory.CreateArctanToken);
            //TestDoubleUnaryFunction(FormulaTreeFactory.CreateSqrtNode, FormulaTokenFactory.CreateSqrtToken);
            //TestDoubleUnaryFunction(FormulaTreeFactory.CreateAbsNode, FormulaTokenFactory.CreateAbsToken);
            //TestDoubleUnaryFunction(FormulaTreeFactory.CreateRoundNode, FormulaTokenFactory.CreateRoundToken);
            //TestDoubleBinaryFunction(FormulaTreeFactory.CreateRandomNode, FormulaTokenFactory.CreateRandomToken);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestSensors()
        {
            TestConstant(FormulaTokenFactory.CreateAccelerationXToken);
            TestConstant(FormulaTokenFactory.CreateAccelerationYToken);
            TestConstant(FormulaTokenFactory.CreateAccelerationZToken);
            TestConstant(FormulaTokenFactory.CreateCompassToken);
            TestConstant(FormulaTokenFactory.CreateInclinationXToken);
            TestConstant(FormulaTokenFactory.CreateInclinationYToken);
            TestConstant(FormulaTokenFactory.CreateLoudnessToken);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestProperties()
        {
            TestConstant(FormulaTokenFactory.CreateBrightnessToken);
            TestConstant(FormulaTokenFactory.CreateLayerToken);
            TestConstant(FormulaTokenFactory.CreatePositionXToken);
            TestConstant(FormulaTokenFactory.CreatePositionYToken);
            TestConstant(FormulaTokenFactory.CreateRotationToken);
            TestConstant(FormulaTokenFactory.CreateSizeToken);
            TestConstant(FormulaTokenFactory.CreateTransparencyToken);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestVariables()
        {
            TestVariable(FormulaTokenFactory.CreateLocalVariableToken);
            TestVariable(FormulaTokenFactory.CreateGlobalVariableToken);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestBrackets()
        {
            Assert.Inconclusive();
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void Test()
        {
            Assert.Inconclusive("Write some tests. ");
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void MonkeyTest()
        {
            const int iterations = 100000;
            const int minLength = 1;
            const int maxLength = 15;

            for (var iteration = 1; iteration <= iterations; iteration++)
            {
                var length = _random.Next(minLength, maxLength);
                if (length == 0) continue;
                var tokens = Enumerable.Range(1, length).Select(i => _random.NextFormulaToken()).ToList();
                var index = _random.Next(0, length);
                var range = FormulaInterpreter.Complete(tokens, index);
                Assert.IsNotNull(range);
                Assert.IsTrue(range.Length > 0);
                Assert.IsTrue(0 <= range.Start && range.End <= length);
            }
        }

        #region Helpers

        private void TestComplete(Range expected, IList<IFormulaToken> tokens, int index = 0)
        {
            var actual = FormulaInterpreter.Complete(tokens, index);
            Assert.AreEqual(expected, actual);
        }

        private void TestComplete(IList<IFormulaToken> tokens, int index = 0)
        {
            TestComplete(Range.FromLength(0, tokens == null ? 0 : tokens.Count), tokens, index);
        }

        private void TestConstant(Func<ConstantFormulaTree> tokenCreator)
        {
            TestComplete(Range.FromLength(0, 1), new IFormulaToken[] { tokenCreator.Invoke() });
            foreach (var dummyToken in new Func<IFormulaToken>[]
            {
                FormulaTokenFactory.CreateParameterSeparatorToken, 
                FormulaTokenFactory.CreatePlusToken, 
                FormulaTokenFactory.CreateMinusToken
            }.Select(dummyTokenCreator => dummyTokenCreator.Invoke()))
            {
                TestComplete(Range.FromLength(1, 1), new[] { dummyToken, tokenCreator.Invoke(), dummyToken }, 1);
            }
            TestComplete(Range.FromLength(1, 1), new IFormulaToken[]
            {
                FormulaTokenFactory.CreateParenthesisToken(true),
                tokenCreator.Invoke(), 
                FormulaTokenFactory.CreateParenthesisToken(false)
            }, 1);
        }

        private void TestVariable(Func<UserVariable, ConstantFormulaTree> tokenCreator)
        {
            var x = new UserVariable
            {
                Name = "TestVariable"
            };
            var y = new UserVariable();
            TestConstant(() => tokenCreator.Invoke(x));
            TestConstant(() => tokenCreator.Invoke(y));
            TestConstant(() => tokenCreator.Invoke(null));
        }

        #endregion
    }
}
