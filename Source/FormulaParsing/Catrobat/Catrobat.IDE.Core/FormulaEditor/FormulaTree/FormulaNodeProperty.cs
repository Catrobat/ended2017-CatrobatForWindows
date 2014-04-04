using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;
using System;
using System.Text;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree
{
    abstract partial class FormulaNodeProperty
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

    partial class FormulaNodeBrightness
    {
        public override double EvaluateNumber()
        {
            // see /catroid/src/org/catrobat/catroid/formulaeditor/FormulaElement.java:interpretObjectSensor()
            return 100;
        }

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

    partial class FormulaNodeLayer
    {
        public override double EvaluateNumber()
        {
            // see /catroid/src/org/catrobat/catroid/formulaeditor/FormulaElement.java:interpretObjectSensor()
            return -1;
        }

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

    partial class FormulaNodeTransparency
    {
        public override double EvaluateNumber()
        {
            // see /catroid/src/org/catrobat/catroid/formulaeditor/FormulaElement.java:interpretObjectSensor()
            return 0;
        }

        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateTransparencyToken();
        }

        internal override void Serialize(StringBuilder sb)
        {
            // TODO: translate
            sb.Append("Transparency");
        }

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateTransparencyNode();
        }
    }

    partial class FormulaNodePositionX
    {
        public override double EvaluateNumber()
        {
            // see /catroid/src/org/catrobat/catroid/formulaeditor/FormulaElement.java:interpretObjectSensor()
            return 0;
        }

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
        public override double EvaluateNumber()
        {
            // see /catroid/src/org/catrobat/catroid/formulaeditor/FormulaElement.java:interpretObjectSensor()
            return 0;
        }

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
        public override double EvaluateNumber()
        {
            // see /catroid/src/org/catrobat/catroid/formulaeditor/FormulaElement.java:interpretObjectSensor()
            return 90;
        }

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
        public override double EvaluateNumber()
        {
            // see /catroid/src/org/catrobat/catroid/formulaeditor/FormulaElement.java:interpretObjectSensor()
            return 100;
        }

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
