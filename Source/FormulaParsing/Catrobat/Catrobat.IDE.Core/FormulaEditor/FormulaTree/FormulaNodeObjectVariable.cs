using System;
using System.Text;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree
{
    abstract partial class FormulaNodeObjectVariable
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

    partial class FormulaNodeBrightness
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateBrightnessToken();
        }

        internal override void Serialize(StringBuilder sb)
        {
            // TODO: translate
            sb.Append("Brightness");
        }

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateBrightnessNode();
        }
    }

    partial class FormulaNodeDirection
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateDirectionToken();
        }

        internal override void Serialize(StringBuilder sb)
        {
            // TODO: translate
            sb.Append("Direction");
        }

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateDirectionNode();
        }
    }

    partial class FormulaNodeGhostEffect
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateGhostEffectToken();
        }

        internal override void Serialize(StringBuilder sb)
        {
            // TODO: translate
            sb.Append("GhostEffect");
        }

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateGhostEffectNode();
        }
    }

    partial class FormulaNodeLayer
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateLayerToken();
        }

        internal override void Serialize(StringBuilder sb)
        {
            // TODO: translate
            sb.Append("Layer");
        }

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateLayerNode();
        }
    }

    partial class FormulaNodeOpacity
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateOpacityToken();
        }

        internal override void Serialize(StringBuilder sb)
        {
            // TODO: translate
            sb.Append("Opacity");
        }

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateOpacityNode();
        }
    }

    partial class FormulaNodePositionX
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreatePositionXToken();
        }

        internal override void Serialize(StringBuilder sb)
        {
            // TODO: translate
            sb.Append("PositionX");
        }

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreatePositionXNode();
        }
    }

    partial class FormulaNodePositionY
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreatePositionYToken();
        }

        internal override void Serialize(StringBuilder sb)
        {
            // TODO: translate
            sb.Append("PositionY");
        }

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreatePositionYNode();
        }
    }

    partial class FormulaNodeRotation
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateRotationToken();
        }

        internal override void Serialize(StringBuilder sb)
        {
            // TODO: translate
            sb.Append("Rotation");
        }

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateRotationNode();
        }
    }

    partial class FormulaNodeSize
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateSizeToken();
        }

        internal override void Serialize(StringBuilder sb)
        {
            // TODO: translate
            sb.Append("Size");
        }

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateSizeNode();
        }
    }

    #endregion
}
