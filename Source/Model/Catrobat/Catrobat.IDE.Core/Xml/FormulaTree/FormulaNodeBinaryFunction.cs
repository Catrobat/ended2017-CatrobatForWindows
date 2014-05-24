using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaTree
{
    abstract partial class FormulaNodeBinaryFunction
    {
    }

    #region Implementations

    partial class FormulaNodeMin
    {
        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree firstChild, XmlFormulaTree secondChild)
        {
            return XmlFormulaTreeFactory.CreateMinNode(firstChild, secondChild);
        }
    }

    partial class FormulaNodeMax
    {
        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree firstChild, XmlFormulaTree secondChild)
        {
            return XmlFormulaTreeFactory.CreateMaxNode(firstChild, secondChild);
        }
    }

    partial class FormulaNodeRandom
    {
        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree firstChild, XmlFormulaTree secondChild)
        {
            return XmlFormulaTreeFactory.CreateRandomNode(firstChild, secondChild);
        }
    }

    #endregion
}
