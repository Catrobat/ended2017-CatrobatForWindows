using System;
using System.Text;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;
using Catrobat.IDE.Core.Models.Formulas.FormulaToken;
using Catrobat.IDE.Core.Services;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaTree
{
    abstract partial class FormulaNodeSensor
    {
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
            return true;
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

        public override double EvaluateNumber()
        {
            return ServiceLocator.SensorService.GetAccelerationX();
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

        public override double EvaluateNumber()
        {
            return ServiceLocator.SensorService.GetAccelerationY();
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

        public override double EvaluateNumber()
        {
            return ServiceLocator.SensorService.GetAccelerationZ();
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

        public override double EvaluateNumber()
        {
            return ServiceLocator.SensorService.GetCompass();
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

        public override double EvaluateNumber()
        {
            return ServiceLocator.SensorService.GetInclinationX();
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

        public override double EvaluateNumber()
        {
            return ServiceLocator.SensorService.GetInclinationY();
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

    partial class FormulaNodeLoudness
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateLoudnessToken();
        }

        public override double EvaluateNumber()
        {
            return ServiceLocator.SensorService.GetLoudness();
        }

        internal override void Serialize(StringBuilder sb)
        {
            // TODO: translate
            sb.Append("Loudness");
        }

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateLoudnessNode();
        }
    }

    #endregion
}
