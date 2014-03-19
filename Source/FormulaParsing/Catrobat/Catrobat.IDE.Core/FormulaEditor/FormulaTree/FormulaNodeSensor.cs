using System;
using System.Text;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree
{
    abstract partial class FormulaNodeSensor
    {
        #region Implements IFormulaEvaluation

        public override double EvaluateNumber()
        {
            // TODO: evaluate object variables
            throw new NotImplementedException();
        }

        #endregion

        #region Implements IFormulaSerialization

        protected override void SerializeToken(StringBuilder sb)
        {
            // not used
            throw new NotImplementedException();
        }

        #endregion

        #region Implements IFormulaInterpreter

        public override bool IsNumber()
        {
            return IsNumberN();
        }

        #endregion
    }

    #region Implementations

    partial class FormulaNodeAccelerationX
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateAccelerationXToken();
        }

        internal override void Serialize(StringBuilder sb)
        {
            // TODO: translate
            sb.Append("AccelerationX");
        }

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateAccelerationXNode();
        }
    }

    partial class FormulaNodeAccelerationY
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateAccelerationYToken();
        }

        internal override void Serialize(StringBuilder sb)
        {
            // TODO: translate
            sb.Append("AccelerationY");
        }

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateAccelerationYNode();
        }
    }

    partial class FormulaNodeAccelerationZ
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateAccelerationZToken();
        }

        internal override void Serialize(StringBuilder sb)
        {
            // TODO: translate
            sb.Append("AccelerationZ");
        }

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateAccelerationZNode();
        }
    }
    
    partial class FormulaNodeCompass
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateCompassToken();
        }

        internal override void Serialize(StringBuilder sb)
        {
            // TODO: translate
            sb.Append("Compass");
        }

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateCompassNode();
        }
    }

    partial class FormulaNodeInclinationX
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateInclinationXToken();
        }

        internal override void Serialize(StringBuilder sb)
        {
            // TODO: translate
            sb.Append("InclinationX");
        }

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateInclinationXNode();
        }
    }

    partial class FormulaNodeInclinationY
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateInclinationYToken();
        }

        internal override void Serialize(StringBuilder sb)
        {
            // TODO: translate
            sb.Append("InclinationY");
        }

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateInclinationYNode();
        }
    }

    #endregion
}
