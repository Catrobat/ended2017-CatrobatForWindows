using System;
using System.Globalization;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Misc
{
    public static class XmlFormulaTreeComparer
    {
        public static bool TestEquals(XmlFormulaTree expectedFormula, XmlFormulaTree actualFormula, bool detectNumbers = true)
        {
            if (expectedFormula == null && actualFormula == null) return true;
            Assert.IsNotNull(expectedFormula);
            Assert.IsNotNull(actualFormula);

            return CompareStrings(expectedFormula.VariableType, actualFormula.VariableType, false, detectNumbers) && 
                CompareStrings(expectedFormula.VariableValue, actualFormula.VariableValue, false, detectNumbers) &&
                TestEquals(expectedFormula.LeftChild, actualFormula.LeftChild, detectNumbers) &&
                TestEquals(expectedFormula.RightChild, actualFormula.RightChild, detectNumbers);
        }

        private static bool CompareStrings(string expected, string actual, bool ignoreCase, bool detectNumbers)
        {
            // handle numbers
            double expectedValue;
            if (detectNumbers && !string.IsNullOrEmpty(expected) && double.TryParse(expected, NumberStyles.Number, CultureInfo.InvariantCulture, out expectedValue))
            {
                double actualNumber;
                return double.TryParse(actual, NumberStyles.Number, CultureInfo.InvariantCulture, out actualNumber) && 
                    Equals(expectedValue, actualNumber);
            }

            return string.Equals(expected, actual, ignoreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture);
        }
    }
}
