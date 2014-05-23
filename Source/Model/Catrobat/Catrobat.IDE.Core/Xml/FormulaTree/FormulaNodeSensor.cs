using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaTree
{
    abstract partial class FormulaNodeSensor
    {
    }

    #region Implementations

    partial class FormulaNodeAccelerationX
    {
        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateAccelerationXNode();
        }
    }

    partial class FormulaNodeAccelerationY
    {
        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateAccelerationYNode();
        }
    }

    partial class FormulaNodeAccelerationZ
    {
        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateAccelerationZNode();
        }
    }
    
    partial class FormulaNodeCompass
    {
        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateCompassNode();
        }
    }

    partial class FormulaNodeInclinationX
    {
        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateInclinationXNode();
        }
    }

    partial class FormulaNodeInclinationY
    {
        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateInclinationYNode();
        }
    }

    partial class FormulaNodeLoudness
    {
        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateLoudnessNode();
        }
    }

    #endregion
}
