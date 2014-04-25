using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Formulas;
using Catrobat.IDE.Core.Models.Formulas.FormulaToken;
using Catrobat.IDE.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Catrobat.IDE.Tests.Tests.IDE.Formula
{
    /// <summary>Tests <see cref="FormulaInterpreter.Interpret" /> with invalid tokens. </summary>
    [TestClass]
    public class FormulaInterpreterParsingErrorTests
    {
        private readonly Random _random = new Random();

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestNullOrEmpty()
        {
            ParsingError parsingError;
            Assert.IsNull(FormulaInterpreter.Interpret(null, out parsingError));
            Assert.IsNotNull(parsingError);
            Assert.IsNull(FormulaInterpreter.Interpret(new IFormulaToken[] { }, out parsingError));
            Assert.IsNotNull(parsingError);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestWrongParameter()
        {
            ParsingError parsingError;
            Assert.IsNull(FormulaInterpreter.Interpret(
                tokens: new IFormulaToken[]
                {
                    FormulaTokenFactory.CreateSinToken(), 
                    FormulaTokenFactory.CreateParenthesisToken(true), 
                    FormulaTokenFactory.CreateDigitToken(0), 
                    FormulaTokenFactory.CreateParameterSeparatorToken(), 
                    FormulaTokenFactory.CreateDigitToken(0), 
                    FormulaTokenFactory.CreateParenthesisToken(false)
                },
                parsingError: out parsingError));
            Assert.IsNotNull(parsingError);
            Assert.Inconclusive();
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestSemanticError()
        {
            ParsingError parsingError;
            Assert.IsNull(FormulaInterpreter.Interpret(
                tokens: new IFormulaToken[]
                {
                    FormulaTokenFactory.CreateSinToken(), 
                    FormulaTokenFactory.CreateParenthesisToken(true), 
                    FormulaTokenFactory.CreateTrueToken(), 
                    FormulaTokenFactory.CreateParenthesisToken(false)
                },
                parsingError: out parsingError));
            Assert.IsNotNull(parsingError);
            Assert.Inconclusive();
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void MonkeyTest()
        {
            const int iterations = 100000;
            const int minLength = 1;
            const int maxLength = 10;

            for (var iteration = 1; iteration <= iterations; iteration++)
            {
                var length = _random.Next(minLength, maxLength);
                var tokens = Enumerable.Range(1, length).Select(i => _random.NextFormulaToken()).ToList();
                ParsingError parsingError;
                var formula = FormulaInterpreter.Interpret(tokens, out parsingError);
                if (formula != null)
                {
                    Assert.IsTrue(formula.AsEnumerable().All(node => node != null));
                }
            }
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void MonkeyTest2()
        {
            const int iterations = 100000;
            const int minLength = 1;
            const int maxLength = 10;

            for (var iteration = 1; iteration <= iterations; iteration++)
            {
                var length = _random.Next(minLength, maxLength);
                var tokens = Enumerable.Range(1, length).Select(i => _random.NextFormulaToken()).ToList();
            }
        }

        #region Helpers

        #endregion
    }
}
