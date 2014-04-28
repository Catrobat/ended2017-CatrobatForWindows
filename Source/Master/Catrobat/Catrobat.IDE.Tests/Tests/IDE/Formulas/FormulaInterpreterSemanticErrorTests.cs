using System.Globalization;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.Formulas;
using Catrobat.IDE.Core.Models.Formulas.FormulaToken;
using Catrobat.IDE.Core.Models.Formulas.FormulaTree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Tests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Catrobat.IDE.Tests.Tests.IDE.Formulas
{
    /// <summary>Tests <see cref="FormulaInterpreter.Interpret" /> with semantically invalid tokens. </summary>
    [TestClass]
    public class FormulaInterpreterSemanticErrorTests
    {
        private readonly Random _random = new Random();
        private readonly UserVariable[] _localVariables = {new UserVariable {Name = "LocalVariable"}};
        private readonly UserVariable[] _globalVariables = {new UserVariable {Name = "GlobalVariable"}};
        private readonly FormulaTokenizer _tokenizer;

        public FormulaInterpreterSemanticErrorTests()
        {
            _tokenizer = new FormulaTokenizer(_localVariables, _globalVariables);
        }

        [TestInitialize]
        public void TestClassInitialize()
        {
            ServiceLocator.Register<CultureServiceTest>(TypeCreationMode.Lazy);
            ServiceLocator.CultureService.SetCulture(CultureInfo.InvariantCulture);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestSemanticError()
        {
            TestParsingError("Child must be number", 0, 4, "sin(True)");

            Assert.Inconclusive();
        }

        #region Helpers

        private void TestParsingError(string expectedMessage, int expectedIndex, int expectedLength, string input, bool randomDigits = true)
        {
            // tokenize input
            ParsingError parsingError;
            var tokens = _tokenizer.Tokenize(input, out parsingError);
            Assert.IsNull(parsingError);

            // randomize digits
            if (tokens != null) tokens = tokens.Select(token => randomDigits && token is FormulaNodeNumber
                        ? FormulaTokenFactory.CreateDigitToken(_random.Next(0, 10))
                        : token);

            // interpret tokens
            ParsingError actual;
            Assert.IsNull(FormulaInterpreter.Interpret(tokens == null ? null : tokens.ToList(), out actual));

            // validate parsing errors
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedMessage, actual.Message);
            Assert.AreEqual(expectedIndex, actual.Index);
            Assert.AreEqual(expectedLength, actual.Length);
        }

        #endregion
    }
}
