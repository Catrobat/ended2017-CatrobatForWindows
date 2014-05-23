using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertBackContext;

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
                XMovement = RelativeValue == null ? null : RelativeValue.ToXmlObject()
            };
        }
    }

    partial class ChangePositionYBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlChangeYByBrick
            {
                YMovement = RelativeValue == null ? null : RelativeValue.ToXmlObject()
            };
        }
    }

    partial class MoveBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlMoveNStepsBrick
            {
                Steps = Steps == null ? null : Steps.ToXmlObject()
            };
        }
    }

    partial class ChangeSizeBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlChangeSizeByNBrick
            {
                Size = RelativePercentage == null ? null : RelativePercentage.ToXmlObject()
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
                Degrees = RelativeValue == null ? null : RelativeValue.ToXmlObject()
            };
        }
    }

    partial class TurnRightBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlTurnRightBrick
            {
                Degrees = RelativeValue == null ? null : RelativeValue.ToXmlObject()
            };
        }
    }

    partial class ChangeBrightnessBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlChangeBrightnessBrick
            {
                ChangeBrightness = RelativePercentage == null ? null : RelativePercentage.ToXmlObject()
            };
        }
    }

    partial class ChangeTransparencyBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlChangeGhostEffectBrick
            {
                ChangeGhostEffect = RelativePercentage == null ? null : RelativePercentage.ToXmlObject()
            };
        }
    }

    #endregion
}
