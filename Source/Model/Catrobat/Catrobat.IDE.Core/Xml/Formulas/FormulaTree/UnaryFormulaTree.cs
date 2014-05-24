using Catrobat.IDE.Core.Xml.Formulas;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaTree
{
    abstract partial class UnaryFormulaTree
    {
        #region Implements IXmlConvertible

        protected abstract XmlFormulaTree ToXml(XmlFormulaTree child);

        public override XmlFormulaTree ToXml()
        {
            return ToXml(Child == null ? null : Child.ToXml());
        }

        #endregion
    }
}
