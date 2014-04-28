using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;
using Catrobat.IDE.Core.Models.Formulas.FormulaToken;
using Catrobat.IDE.Core.Resources.Localization;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaTree
{
    abstract partial class FormulaNodeProperty
    {
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

        public override string Serialize()
        {
            return AppResources.Formula_Property_Brightness;
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

        public override string Serialize()
        {
            return AppResources.Formula_Property_Layer;
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

        public override string Serialize()
        {
            return AppResources.Formula_Property_Transparency;
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

        public override string Serialize()
        {
            return AppResources.Formula_Property_PositionX;
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

        public override string Serialize()
        {
            return AppResources.Formula_Property_PositionY;
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

        public override string Serialize()
        {
            return AppResources.Formula_Property_Rotation;
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

        public override string Serialize()
        {
            return AppResources.Formula_Property_Size;
        }

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateSizeNode();
        }
    }

    #endregion
}
