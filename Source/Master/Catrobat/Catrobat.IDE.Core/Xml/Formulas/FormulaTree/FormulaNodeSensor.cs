using Catrobat.IDE.Core.Xml.Formulas;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaTree
{
    abstract partial class FormulaNodeSensor
    {
    }

    #region Implementations

    partial class FormulaNodeAccelerationX
    {
        public override XmlFormulaTree ToXml()
        {
            return XmlFormulaTreeFactory.CreateAccelerationXNode();
        }
    }

    partial class FormulaNodeAccelerationY
    {
        public override XmlFormulaTree ToXml()
        {
            return XmlFormulaTreeFactory.CreateAccelerationYNode();
        }
    }

    partial class FormulaNodeAccelerationZ
    {
        public override XmlFormulaTree ToXml()
        {
            return XmlFormulaTreeFactory.CreateAccelerationZNode();
        }
    }
    
    partial class FormulaNodeCompass
    {
        public override XmlFormulaTree ToXml()
        {
            return XmlFormulaTreeFactory.CreateCompassNode();
        }
    }

    partial class FormulaNodeInclinationX
    {
        public override XmlFormulaTree ToXml()
        {
            return XmlFormulaTreeFactory.CreateInclinationXNode();
        }
    }

    partial class FormulaNodeInclinationY
    {
        public override XmlFormulaTree ToXml()
        {
            return XmlFormulaTreeFactory.CreateInclinationYNode();
        }
    }

    partial class FormulaNodeLoudness
    {
        public override XmlFormulaTree ToXml()
        {
            return XmlFormulaTreeFactory.CreateLoudnessNode();
        }
    }

    #endregion
}
