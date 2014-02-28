using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Misc
{
    public static class FormulaComparer
    {
        public static void CompareFormulas(FormulaTree expectedFormula, FormulaTree actualFormula)
        {
            if (expectedFormula == null && actualFormula == null)
                return;

            Assert.IsNotNull(expectedFormula);
            Assert.IsNotNull(actualFormula);

            if (expectedFormula.VariableType != null || actualFormula.VariableType != null)
                Assert.AreEqual(
                    expectedFormula.VariableType.ToLowerInvariant(),
                    actualFormula.VariableType.ToLowerInvariant());

            if (expectedFormula.VariableValue != null || actualFormula.VariableValue != null)
                Assert.AreEqual(
                    expectedFormula.VariableValue.ToLowerInvariant(),
                    actualFormula.VariableValue.ToLowerInvariant());

            CompareFormulas(expectedFormula.LeftChild, actualFormula.LeftChild);
            CompareFormulas(expectedFormula.RightChild, actualFormula.RightChild);
        }
    }
}
