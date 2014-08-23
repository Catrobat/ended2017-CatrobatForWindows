using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.Tree
{
    abstract partial class FormulaNodeProperty
    {
    }

    #region Implementations

    partial class FormulaNodeBrightness
    {
        protected internal override XmlFormulaTree ToXmlObject2()
        {
            return XmlFormulaTreeFactory.CreateBrightnessNode();
        }
    }

    partial class FormulaNodeLayer
    {
        protected internal override XmlFormulaTree ToXmlObject2()
        {
            return XmlFormulaTreeFactory.CreateLayerNode();
        }
    }

    partial class FormulaNodeTransparency
    {
        protected internal override XmlFormulaTree ToXmlObject2()
        {
            return XmlFormulaTreeFactory.CreateTransparencyNode();
        }
    }

    partial class FormulaNodePositionX
    {
        protected internal override XmlFormulaTree ToXmlObject2()
        {
            return XmlFormulaTreeFactory.CreatePositionXNode();
        }
    }

    partial class FormulaNodePositionY
    {
        protected internal override XmlFormulaTree ToXmlObject2()
        {
            return XmlFormulaTreeFactory.CreatePositionYNode();
        }
    }

    partial class FormulaNodeRotation
    {
        protected internal override XmlFormulaTree ToXmlObject2()
        {
            return XmlFormulaTreeFactory.CreateRotationNode();
        }
    }

    partial class FormulaNodeSize
    {
        protected internal override XmlFormulaTree ToXmlObject2()
        {
            return XmlFormulaTreeFactory.CreateSizeNode();
        }
    }

    #endregion
}
