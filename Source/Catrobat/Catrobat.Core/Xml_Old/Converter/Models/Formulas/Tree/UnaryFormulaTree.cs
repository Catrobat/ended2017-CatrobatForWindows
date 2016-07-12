using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.Tree
{
    abstract partial class UnaryFormulaTree
    {
        protected abstract XmlFormulaTree ToXml(XmlFormulaTree child);

        protected internal override XmlFormulaTree ToXmlObject2()
        {
            return ToXml(Child == null ? null : Child.ToXmlObject2());
        }
    }
}
