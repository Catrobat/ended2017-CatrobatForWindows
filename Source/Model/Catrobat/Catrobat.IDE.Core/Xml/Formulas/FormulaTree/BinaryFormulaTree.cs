using Catrobat.IDE.Core.Xml.Formulas;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaTree
{
    abstract partial class BinaryFormulaTree
    {
        #region Implements IXmlConvertible

        protected abstract XmlFormulaTree ToXml(XmlFormulaTree firstChild, XmlFormulaTree secondChild);

        public override XmlFormulaTree ToXml()
        {
            return ToXml(
                firstChild: FirstChild == null ? null : FirstChild.ToXml(),
                secondChild: SecondChild == null ? null : SecondChild.ToXml());
        }

        #endregion
    }
}
