using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Catrobat.Data.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Tests.Misc
{
    /// <summary>
    /// Compares two instances of <see cref="XmlFormula"/> /> by using <see cref="XmlFormulaTreeComparer.TestEquals"/>. 
    /// </summary>
    internal class XmlFormulaEqualityComparer : EqualityComparer<XmlFormula>
    {
        #region Inherits EqualityComparer

        public override bool Equals(XmlFormula x, XmlFormula y)
        {
            return x == null 
                ? y == null 
                : y != null && XmlFormulaTreeComparer.TestEquals(x.FormulaTree, y.FormulaTree);
        }

        public override int GetHashCode(XmlFormula obj)
        {
            return RuntimeHelpers.GetHashCode(obj);
        }

        #endregion
    }
}
