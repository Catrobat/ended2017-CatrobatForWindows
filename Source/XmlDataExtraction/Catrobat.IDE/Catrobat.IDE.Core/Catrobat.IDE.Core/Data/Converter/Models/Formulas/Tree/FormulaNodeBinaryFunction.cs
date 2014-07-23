using Catrobat.Data.Xml.XmlObjects.Formulas;
// ReSharper disable once CheckNamespace


namespace Catrobat.IDE.Core.Models.Formulas.Tree
{
    abstract partial class FormulaNodeBinaryFunction
    {
    }

    #region Implementations

    partial class FormulaNodeMin
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree firstChild, XmlFormulaTree secondChild)
        {
            return XmlFormulaTreeFactory.CreateMinNode(firstChild, secondChild);
        }
    }

    partial class FormulaNodeMax
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree firstChild, XmlFormulaTree secondChild)
        {
            return XmlFormulaTreeFactory.CreateMaxNode(firstChild, secondChild);
        }
    }

    partial class FormulaNodeRandom
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree firstChild, XmlFormulaTree secondChild)
        {
            return XmlFormulaTreeFactory.CreateRandomNode(firstChild, secondChild);
        }
    }

    #endregion
}
