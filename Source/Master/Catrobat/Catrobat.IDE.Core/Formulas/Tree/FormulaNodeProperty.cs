using Catrobat.IDE.Core.Models.Formulas.Tokens;
using Catrobat.IDE.Core.Resources.Localization;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.Tree
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
    }

    #endregion
}
