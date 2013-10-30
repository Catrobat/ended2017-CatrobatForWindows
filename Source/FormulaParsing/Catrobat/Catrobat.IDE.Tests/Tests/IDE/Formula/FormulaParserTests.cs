using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.FormulaEditor.Editor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Catrobat.IDE.Tests.Tests.IDE.Formula
{
    [TestClass]
    public class FormulaParserTests
    {
        private readonly FormulaParser _parser = new FormulaParser();

        [TestMethod]
        public void FormulaParserTests_NullOrWhitespace()
        {
            foreach (var input in new[] { null, string.Empty, " ", "  " })
            {
                IEnumerable<string> parsingErrors;
                IFormulaTree formula;
                Assert.IsTrue(_parser.Parse(input, out formula, out parsingErrors));
                Assert.IsFalse(parsingErrors.Any());
                Assert.AreEqual(FormulaTreeFactory.CreateFormulaTree(), formula);
            }
        }

        #region numbers

        [TestMethod]
        public void FormulaParserTests_Pi()
        {
            foreach (var input in new[] { "pi", "PI", "Pi", " pi " })
            {
                IEnumerable<string> parsingErrors;
                IFormulaTree formula;
                Assert.IsTrue(_parser.Parse(input, out formula, out parsingErrors));
                Assert.IsFalse(parsingErrors.Any());
                Assert.AreEqual(FormulaTreeFactory.CreatePiNode(), formula);
            }
        }

        #endregion

        #region arithmetic

        [TestMethod]
        public void FormulaParserTests_Plus()
        {
            Assert.Inconclusive();
        }

        #endregion

    }
}
