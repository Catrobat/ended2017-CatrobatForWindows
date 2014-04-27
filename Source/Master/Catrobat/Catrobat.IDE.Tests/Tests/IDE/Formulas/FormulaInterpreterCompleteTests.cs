using System.Globalization;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.Formulas;
using Catrobat.IDE.Core.Models.Formulas.FormulaToken;
using Catrobat.IDE.Core.Models.Formulas.FormulaTree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Tests.Extensions;
using Catrobat.IDE.Tests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catrobat.IDE.Tests.Tests.IDE.Formulas
{
    /// <summary>Tests <see cref="FormulaInterpreter.Complete" />. </summary>
    [TestClass]
    public class FormulaInterpreterCompleteTests
    {
        const int Iterations = 1000;

        private readonly Random _random = new Random();
        private readonly FormulaTokenizer _tokenizer = new FormulaTokenizer(Enumerable.Empty<UserVariable>(), Enumerable.Empty<UserVariable>());

        [TestInitialize]
        public void TestClassInitialize()
        {
            ServiceLocator.Register<CultureServiceTest>(TypeCreationMode.Lazy);
            ServiceLocator.CultureService.SetCulture(CultureInfo.InvariantCulture);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestNullOrEmpty()
        {
            TestComplete((IFormulaToken[]) null);
            TestComplete(new IFormulaToken[] {});
        }

        #region Constants

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestConstants()
        {
            for (var digit = 0; digit <= 9; digit++)
            {
                TestComplete(new IFormulaToken[] { FormulaTokenFactory.CreateDigitToken(digit) });
                TestComplete(new IFormulaToken[] { FormulaTokenFactory.CreateMinusToken(), FormulaTokenFactory.CreateDigitToken(digit) });
            }
            TestComplete(new IFormulaToken[] { FormulaTokenFactory.CreateDigitToken(4), FormulaTokenFactory.CreateDigitToken(2) });
            TestComplete(new IFormulaToken[] { FormulaTokenFactory.CreateDigitToken(0), FormulaTokenFactory.CreateDecimalSeparatorToken(), FormulaTokenFactory.CreateDigitToken(4), FormulaTokenFactory.CreateDigitToken(2) });
            TestComplete(FormulaTokenFactory.CreatePiToken);
            TestComplete(FormulaTokenFactory.CreateTrueToken);
            TestComplete(FormulaTokenFactory.CreateFalseToken);
        }

        #endregion

        #region Operators

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestOperators()
        {
            TestOperator(Range.FromLength(0, 2), "not true", 0);
            TestOperator(Range.FromLength(0, 3), "rotation <= compass", 1);
            TestOperator(Range.FromLength(0, 3), "inclinationy * positiony", 1);
            TestOperator(Range.FromLength(0, 8), "sin(.)*(+)", 4);
            TestOperator(Range.FromLength(1, 6), ".1.2*.2.", 4);
            TestOperator(Range.FromLength(2, 3), "1-2*3", 3, false);
            TestOperator(Range.FromLength(0, 5), "1-2*3", 1, false);
            TestOperator(Range.FromLength(1, 4), "-1--2", 2, false);
            TestOperator(Range.FromLength(0, 5), "-1=-2", 2, false);
            TestOperator(Range.FromLength(2, 4), "true not true*not true not", 3, false);
            TestOperator(Range.FromLength(1, 5), "true not true=not true not", 3, false);
            TestOperator(Range.FromLength(2, 3), "1+2+3+4", 3, false);
        }

        private void TestOperator(Range expected, string input, int index, bool wrapTokens = true)
        {
            // tokenize input
            ParsingError parsingError;
            var tokens = _tokenizer.Tokenize(input, out parsingError);
            Assert.IsNull(parsingError);

            // randomize digits
            tokens = tokens.Select(token => token is FormulaNodeNumber
                ? FormulaTokenFactory.CreateDigitToken(_random.Next(0, 10))
                : token);

            // wrap random tokens
            var tokensBefore = wrapTokens ? CreateNonOperatorTokens(0, 10) : new List<IFormulaToken>();
            var tokensAfterwards = wrapTokens ? CreateNonOperatorTokens(0, 10) : new List<IFormulaToken>();
            tokens = tokensBefore.Concat(tokens).Concat(tokensAfterwards);
            expected.Start += tokensBefore.Count;

            TestComplete(expected, tokens.ToList(), index + tokensBefore.Count);
        }

        #endregion

        #region Functions

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestEmptyFunction()
        {
            TestFunction(functionToken =>
            {
                var tokensBefore = CreateTokens(0, 10);
                var tokensAfterwards = CreateNonArgumentTokens(0, 10);
                var tokens = tokensBefore
                    .Concat(Enumerable.Repeat(functionToken, 1))
                    .Concat(tokensAfterwards)
                    .ToList();

                // complete function without argument
                TestComplete(Range.FromLength(tokensBefore.Count, 1), tokens, tokensBefore.Count);
            });
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestFunctionWithUnfinishedArgument()
        {
            TestFunction(functionToken =>
            {
                var tokensBefore = CreateTokens(0, 10);
                var tokensAfterwards = CreateLeftUnmatchingTokens(0, 10);
                var tokens = tokensBefore
                    .Concat(Enumerable.Repeat(functionToken, 1))
                    .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateParenthesisToken(true), 1))
                    .Concat(tokensAfterwards)
                    .ToList();

                TestComplete(Range.FromLength(tokensBefore.Count, tokensAfterwards.Count + 2), tokens, tokensBefore.Count);
            });
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestFunctionWithArgument()
        {
            TestFunction(functionToken =>
            {
                var tokensBefore = CreateTokens(0, 10);
                var tokensBetween = CreateUnmatchingTokens(0, 10);
                var tokensAfterwards = CreateTokens(0, 10);
                var tokens = tokensBefore
                    .Concat(Enumerable.Repeat(functionToken, 1))
                    .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateParenthesisToken(true), 1))
                    .Concat(tokensBetween)
                    .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateParenthesisToken(false), 1))
                    .Concat(tokensAfterwards)
                    .ToList();

                TestComplete(Range.FromLength(tokensBefore.Count, tokensBetween.Count + 3), tokens, tokensBefore.Count);
            });
        }

        private void TestFunction(Action<IFormulaFunction> action)
        {
            var functionTokens = typeof(FormulaTokenFactory).GetMethods()
                .Where(method => method.IsStatic && method.GetParameters().Length == 0 && method.ReturnType.IsImplementationOf<IFormulaFunction>())
                .Select(method => method.AsFunction<IFormulaFunction>().Invoke());

            foreach (var functionToken in functionTokens)
            {
                for (var iteration = 1; iteration <= Iterations; iteration++)
                {
                    action.Invoke(functionToken);
                }
            }
        }

        #endregion

        #region Sensors

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestSensors()
        {
            TestComplete<FormulaNodeSensor>();
        }

        #endregion

        #region Properties

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestProperty()
        {
            TestComplete<FormulaNodeProperty>();
        }

        #endregion

        #region Variables

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestVariables()
        {
            TestVariable(FormulaTokenFactory.CreateLocalVariableToken);
            TestVariable(FormulaTokenFactory.CreateGlobalVariableToken);
        }

        private void TestVariable(Func<UserVariable, ConstantFormulaTree> tokenCreator)
        {
            TestComplete(tokenCreator.Invoke(new UserVariable { Name = "TestVariable" }));
            TestComplete(tokenCreator.Invoke(new UserVariable()));
            TestComplete(tokenCreator.Invoke(null));
        }

        #endregion

        #region Brackets

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestClosedBracket()
        {
            for (var iteration = 1; iteration <= Iterations; iteration++)
            {
                var tokensBefore = CreateTokens(0, 10);
                var tokensBetween = CreateUnmatchingTokens(0, 10);
                var tokensAfterwards = CreateTokens(0, 10);
                var tokens = tokensBefore
                    .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateParenthesisToken(true), 1))
                    .Concat(tokensBetween)
                    .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateParenthesisToken(false), 1))
                    .Concat(tokensAfterwards)
                    .ToList();

                // complete opening parenthesis
                TestComplete(Range.FromLength(tokensBefore.Count, tokensBetween.Count + 2), tokens, tokensBefore.Count);

                // complete closing parenthesis
                TestComplete(Range.FromLength(tokensBefore.Count, tokensBetween.Count + 2), tokens, tokensBefore.Count + 1 + tokensBetween.Count);
            }
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestLeftOpenBracket()
        {
            for (var iteration = 1; iteration <= Iterations; iteration++)
            {
                var tokensBefore = CreateRightUnmatchingTokens(0, 10);
                var tokensAfterwards = CreateTokens(0, 10);
                var tokens = tokensBefore
                    .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateParenthesisToken(false), 1))
                    .Concat(tokensAfterwards)
                    .ToList();

                // complete closing parenthesis
                TestComplete(Range.FromLength(0, tokensBefore.Count + 1), tokens, tokensBefore.Count);
            }
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestRightOpenBracket()
        {
            for (var iteration = 1; iteration <= Iterations; iteration++)
            {
                var tokensBefore = CreateTokens(0, 10);
                var tokensAfterwards = CreateLeftUnmatchingTokens(0, 10);
                var tokens = tokensBefore
                    .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateParenthesisToken(true), 1))
                    .Concat(tokensAfterwards)
                    .ToList();

                // complete opening parenthesis
                TestComplete(Range.FromLength(tokensBefore.Count, tokensAfterwards.Count + 1), tokens, tokensBefore.Count);
            }
        }

        #endregion

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void MonkeyTest()
        {
            const int minLength = 1;
            const int maxLength = 15;

            for (var iteration = 1; iteration <= Iterations; iteration++)
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

        private void TestComplete(ConstantFormulaTree token)
        {
            for (var iteration = 1; iteration <= Iterations; iteration++)
            {
                var tokensBefore = CreateTokens(0, 10);
                var tokensAfterwards = CreateTokens(0, 10);
                TestComplete(
                    expected: Range.FromLength(tokensBefore.Count, 1), 
                    tokens: tokensBefore
                        .Concat(Enumerable.Repeat(token, 1))
                        .Concat(tokensAfterwards)
                        .ToList(), 
                    index: tokensBefore.Count);
            }
        }

        private void TestComplete(Func<ConstantFormulaTree> tokenCreator)
        {
            TestComplete(tokenCreator.Invoke());
        }

        private void TestComplete<TConstant>() where TConstant : ConstantFormulaTree
        {
            var tokens = typeof(FormulaTokenFactory).GetMethods()
                .Where(method => method.IsStatic && method.GetParameters().Length == 0 && method.ReturnType.IsSubclassOf<TConstant>())
                .Select(method => method.AsFunction<TConstant>().Invoke());

            foreach (var token in tokens)
            {
                TestComplete(token);
            }
        }

        private IList<IFormulaToken> CreateTokens(int minCount, int maxCount)
        {
            return Enumerable.Range(1, _random.Next(minCount, maxCount))
                .Select(i => _random.NextFormulaToken())
                .ToList();
        }

        private IList<IFormulaToken> CreateUnmatchingTokens(int minCount, int maxCount)
        {
            while (true)
            {
                var tokens = CreateTokens(minCount, maxCount);

                // ensure parentheses don't match with parentheses outside
                var balance = 0;
                foreach (var parenthesis in tokens.OfType<FormulaTokenParenthesis>())
                {
                    balance += parenthesis.IsOpening ? 1 : -1;
                    if (balance < 0) break;
                }
                if (balance != 0) continue;

                return tokens;
            }
        }

        private IList<IFormulaToken> CreateLeftUnmatchingTokens(int minCount, int maxCount)
        {
            while (true)
            {
                var tokens = CreateTokens(minCount, maxCount);

                // ensure parentheses don't match with opening parenthesis outside
                var balance = 0;
                foreach (var parenthesis in tokens.OfType<FormulaTokenParenthesis>())
                {
                    balance += parenthesis.IsOpening ? 1 : -1;
                    if (balance < 0) break;
                }
                if (balance < 0) continue;

                return tokens;
            }
        }

        private IList<IFormulaToken> CreateRightUnmatchingTokens(int minCount, int maxCount)
        {
            while (true)
            {
                var tokens = CreateTokens(minCount, maxCount);

                // ensure parentheses don't match with opening parenthesis outside
                var balance = 0;
                foreach (var parenthesis in tokens.OfType<FormulaTokenParenthesis>().Reverse())
                {
                    balance += parenthesis.IsOpening ? 1 : -1;
                    if (balance > 0) break;
                }
                if (balance > 0) continue;

                return tokens;
            }
        }

        private IEnumerable<IFormulaToken> CreateNonArgumentTokens(int minCount, int maxCount)
        {
            while (true)
            {
                var tokens = CreateTokens(minCount, maxCount);

                // ensure tokens don't start with opening parenthesis
                var parenthesisToken = tokens.FirstOrDefault() as FormulaTokenParenthesis;
                if (parenthesisToken != null && parenthesisToken.IsOpening) continue;

                return tokens;
            }
        }

        private IList<IFormulaToken> CreateNonOperatorTokens(int minCount, int maxCount)
        {
            while (true)
            {
                var tokens = CreateTokens(minCount, maxCount);

                // ensure tokens don't start/end with opening operators
                var infixOperatorToken = tokens.FirstOrDefault() as FormulaNodeInfixOperator;
                if (infixOperatorToken != null) continue;
                var operatorToken = tokens.LastOrDefault() as IFormulaOperator;
                if (operatorToken != null) continue;

                return tokens;
            }
        }

        #endregion
    }
}
