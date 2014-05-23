using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaTree
{
    abstract partial class FormulaNodeProperty
    {
    }

    #region Implementations

    partial class FormulaNodeBrightness
    {
        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateBrightnessNode();
        }
    }

    partial class FormulaNodeLayer
    {
        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateLayerNode();
        }
    }

    partial class FormulaNodeTransparency
    {
        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateTransparencyNode();
        }
    }

    partial class FormulaNodePositionX
    {
        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreatePositionXNode();
        }
    }

    partial class FormulaNodePositionY
    {
        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreatePositionYNode();
        }
    }

    partial class FormulaNodeRotation
    {
        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateRotationNode();
        }
    }

    partial class FormulaNodeSize
    {
        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateSizeNode();
        }
    }

    #endregion
}
