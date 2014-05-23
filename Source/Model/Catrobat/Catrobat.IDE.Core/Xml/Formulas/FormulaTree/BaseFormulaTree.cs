using Catrobat.IDE.Core.Xml.Formulas;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaTree
{
    abstract partial class BaseFormulaTree
    {
        #region Implements IXmlConvertible

        public abstract XmlFormulaTree ToXml();

        #endregion
    }
}
