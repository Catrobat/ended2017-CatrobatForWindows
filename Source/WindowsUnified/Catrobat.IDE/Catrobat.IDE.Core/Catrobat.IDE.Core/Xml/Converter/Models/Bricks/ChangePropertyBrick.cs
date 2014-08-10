using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProgramConverter.ConvertBackContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Bricks
{
    partial class ChangePropertyBrick
    {
    }

    #region Implementations

    partial class ChangePositionXBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlChangeXByBrick
            {
                XMovement = RelativeValue == null ? new XmlFormula() : RelativeValue.ToXmlObject()
            };
        }
    }

    partial class ChangePositionYBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlChangeYByBrick
            {
                YMovement = RelativeValue == null ? new XmlFormula() : RelativeValue.ToXmlObject()
            };
        }
    }

    partial class MoveBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlMoveNStepsBrick
            {
                Steps = Steps == null ? new XmlFormula() : Steps.ToXmlObject()
            };
        }
    }

    partial class ChangeSizeBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlChangeSizeByNBrick
            {
                Size = RelativePercentage == null ? new XmlFormula() : RelativePercentage.ToXmlObject()
            };
        }
    }

    partial class ChangeRotationBrick
    {
    }

    partial class TurnLeftBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlTurnLeftBrick
            {
                Degrees = RelativeValue == null ? new XmlFormula() : RelativeValue.ToXmlObject()
            };
        }
    }

    partial class TurnRightBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlTurnRightBrick
            {
                Degrees = RelativeValue == null ? new XmlFormula() : RelativeValue.ToXmlObject()
            };
        }
    }

    partial class ChangeBrightnessBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlChangeBrightnessBrick
            {
                ChangeBrightness = RelativePercentage == null ? new XmlFormula() : RelativePercentage.ToXmlObject()
            };
        }
    }

    partial class ChangeTransparencyBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlChangeGhostEffectBrick
            {
                ChangeGhostEffect = RelativePercentage == null ? new XmlFormula() : RelativePercentage.ToXmlObject()
            };
        }
    }

    partial class DecreaseZOrderBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlGoNStepsBackBrick
            {
                Steps = RelativeValue == null ? new XmlFormula() : RelativeValue.ToXmlObject()
            };
        }
    }

    #endregion
}
