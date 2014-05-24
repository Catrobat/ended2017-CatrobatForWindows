using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaTree
{
    abstract partial class BinaryFormulaTree
    {
        #region Implements IXmlFormulaConvertible

        protected abstract XmlFormulaTree ToXmlFormula(XmlFormulaTree firstChild, XmlFormulaTree secondChild);

        public override XmlFormulaTree ToXmlFormula()
        {
            return ToXmlFormula(
                firstChild: FirstChild == null ? null : FirstChild.ToXmlFormula(),
                secondChild: SecondChild == null ? null : SecondChild.ToXmlFormula());
        }

        #endregion
    }
}
