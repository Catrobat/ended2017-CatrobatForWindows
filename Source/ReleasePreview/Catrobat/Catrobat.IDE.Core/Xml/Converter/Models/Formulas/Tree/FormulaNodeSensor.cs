using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.Tree
{
    abstract partial class FormulaNodeSensor
    {
    }

    #region Implementations

    partial class FormulaNodeAccelerationX
    {
        protected internal override XmlFormulaTree ToXmlObject2()
        {
            return XmlFormulaTreeFactory.CreateAccelerationXNode();
        }
    }

    partial class FormulaNodeAccelerationY
    {
        protected internal override XmlFormulaTree ToXmlObject2()
        {
            return XmlFormulaTreeFactory.CreateAccelerationYNode();
        }
    }

    partial class FormulaNodeAccelerationZ
    {
        protected internal override XmlFormulaTree ToXmlObject2()
        {
            return XmlFormulaTreeFactory.CreateAccelerationZNode();
        }
    }
    
    partial class FormulaNodeCompass
    {
        protected internal override XmlFormulaTree ToXmlObject2()
        {
            return XmlFormulaTreeFactory.CreateCompassNode();
        }
    }

    partial class FormulaNodeInclinationX
    {
        protected internal override XmlFormulaTree ToXmlObject2()
        {
            return XmlFormulaTreeFactory.CreateInclinationXNode();
        }
    }

    partial class FormulaNodeInclinationY
    {
        protected internal override XmlFormulaTree ToXmlObject2()
        {
            return XmlFormulaTreeFactory.CreateInclinationYNode();
        }
    }

    partial class FormulaNodeLoudness
    {
        protected internal override XmlFormulaTree ToXmlObject2()
        {
            return XmlFormulaTreeFactory.CreateLoudnessNode();
        }
    }

    #endregion
}
