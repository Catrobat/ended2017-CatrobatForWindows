using System;
using System.Collections.Generic;
using System.Linq;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.FormulaEditor.Editor;
using Catrobat.IDE.Tests.Misc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Tests.IDE.Formula
{
    [TestClass]
    public class FormulaParserTests
    {
        private readonly FormulaParser _parser = new FormulaParser();

        [TestMethod]
        public void FormulaParserTests_NullOrWhitespace()
        {
            foreach (var input in new [] {null, string.Empty, " ", "  "})
            {
                IEnumerable<string> parsingErrors;
                XmlFormulaTree formula;
                Assert.IsTrue(_parser.Parse(input, out formula, out parsingErrors));
                Assert.IsFalse(parsingErrors.Any());
                // TODO: Assert.AreEqual(formula)
                FormulaComparer.CompareFormulas(XmlFormulaTreeFactory.CreateNumber(0), formula);
            }
        }

        [TestMethod]
        public void FormulaParserTests_Plus()
        {
            Assert.Inconclusive();
        }

    }
}
