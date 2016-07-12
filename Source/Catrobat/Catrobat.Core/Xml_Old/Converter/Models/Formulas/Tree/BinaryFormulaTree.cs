using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.Tree
{
    abstract partial class BinaryFormulaTree
    {
        protected abstract XmlFormulaTree ToXml(XmlFormulaTree firstChild, XmlFormulaTree secondChild);

        protected internal override XmlFormulaTree ToXmlObject2()
        {
            return ToXml(
                firstChild: FirstChild == null ? null : FirstChild.ToXmlObject2(),
                secondChild: SecondChild == null ? null : SecondChild.ToXmlObject2());
        }
    }
}
