using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace Catrobat.IDE.Tests.Misc
{
    public static class FormulaComparer
    {
        public static void CompareFormulas(XmlFormulaTree expectedFormula, XmlFormulaTree actualFormula)
        {
            if (expectedFormula == null && actualFormula == null) return;
            Assert.IsNotNull(expectedFormula);
            Assert.IsNotNull(actualFormula);

            CompareStrings(expectedFormula.VariableType, actualFormula.VariableType);
            CompareStrings(expectedFormula.VariableValue, actualFormula.VariableValue);
            CompareFormulas(expectedFormula.LeftChild, actualFormula.LeftChild);
            CompareFormulas(expectedFormula.RightChild, actualFormula.RightChild);
        }

        private static void CompareStrings(string expected, string actual)
        {
            Assert.AreEqual(expected, actual, true, CultureInfo.InvariantCulture);
        }

        public static void CompareFormulasGenerously(XmlFormulaTree expectedFormula, XmlFormulaTree actualFormula)
        {
            if (expectedFormula == null && actualFormula == null) return;
            Assert.IsNotNull(expectedFormula);
            Assert.IsNotNull(actualFormula);

            CompareStringsGenerously(expectedFormula.VariableType, actualFormula.VariableType);
            CompareStringsGenerously(expectedFormula.VariableValue, actualFormula.VariableValue);
            CompareFormulasGenerously(expectedFormula.LeftChild, actualFormula.LeftChild);
            CompareFormulasGenerously(expectedFormula.RightChild, actualFormula.RightChild);
        }

        private static void CompareStringsGenerously(string expected, string actual)
        {
            // special case null or empty
            if (string.IsNullOrEmpty(expected))
            {
                Assert.IsTrue(string.IsNullOrEmpty(actual));
                return;
            }

            // special case number
            double expectedNumber;
            if (double.TryParse(expected, NumberStyles.Number, CultureInfo.InvariantCulture, out expectedNumber))
            {
                double actualNumber;
                Assert.IsTrue(double.TryParse(actual, NumberStyles.Number, CultureInfo.InvariantCulture, out actualNumber));
                Assert.AreEqual(expectedNumber, actualNumber);
                return;
            }

            CompareStrings(expected, actual);
        }

    }
}
