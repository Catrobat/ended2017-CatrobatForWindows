using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace Catrobat.IDE.Tests.Misc
{
    public static class XmlFormulaTreeComparer
    {
        public static void CompareFormulas(XmlFormulaTree expectedFormula, XmlFormulaTree actualFormula, bool detectNumbers = true)
        {
            if (expectedFormula == null && actualFormula == null) return;
            Assert.IsNotNull(expectedFormula);
            Assert.IsNotNull(actualFormula);

            CompareStrings(expectedFormula.VariableType, actualFormula.VariableType, false, detectNumbers);
            CompareStrings(expectedFormula.VariableValue, actualFormula.VariableValue, false, detectNumbers);
            CompareFormulas(expectedFormula.LeftChild, actualFormula.LeftChild, detectNumbers);
            CompareFormulas(expectedFormula.RightChild, actualFormula.RightChild, detectNumbers);
        }

        private static void CompareStrings(string expected, string actual, bool ignoreCase, bool detectNumbers)
        {
            // handle numbers
            double expectedValue;
            if (detectNumbers && !string.IsNullOrEmpty(expected) && double.TryParse(expected, NumberStyles.Number, CultureInfo.InvariantCulture, out expectedValue))
            {
                double actualNumber;
                Assert.IsTrue(double.TryParse(actual, NumberStyles.Number, CultureInfo.InvariantCulture, out actualNumber));
                Assert.AreEqual(expectedValue, actualNumber);
                return;
            }

            Assert.AreEqual(expected, actual, ignoreCase, CultureInfo.InvariantCulture);
        }
    }
}
