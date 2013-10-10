using Catrobat.Core.CatrobatObjects.Formulas;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Misc
{
    public static class FormulaComparer
    {
        public static void CompareFormulas(FormulaTree expectedFromula, FormulaTree actualFormula)
        {
            if (expectedFromula == null && actualFormula == null)
                return;

            Assert.IsNotNull(expectedFromula);
            Assert.IsNotNull(actualFormula);

            if (expectedFromula.VariableType != null || actualFormula.VariableType != null)
            Assert.AreEqual(expectedFromula.VariableType.ToLowerInvariant(),
                actualFormula.VariableType.ToLowerInvariant());

            if (expectedFromula.VariableValue != null || actualFormula.VariableValue != null)
            Assert.AreEqual(expectedFromula.VariableValue.ToLowerInvariant(),
                actualFormula.VariableValue.ToLowerInvariant());

            CompareFormulas(expectedFromula.LeftChild, actualFormula.LeftChild);
            CompareFormulas(expectedFromula.RightChild, actualFormula.RightChild);
        }
    }
}
