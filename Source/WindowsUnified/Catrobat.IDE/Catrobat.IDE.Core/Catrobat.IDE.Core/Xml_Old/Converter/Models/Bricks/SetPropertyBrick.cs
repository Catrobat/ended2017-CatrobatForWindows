using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProgramConverter.ConvertBackContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Bricks
{
    partial class SetPropertyBrick
    {
    }

    #region Implementations

    partial class SetPositionXBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlSetXBrick
            {
                XPosition = Value == null ? new XmlFormula() : Value.ToXmlObject()
            };
        }
    }

    partial class SetPositionYBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlSetYBrick
            {
                YPosition = Value == null ? new XmlFormula() : Value.ToXmlObject()
            };
        }
    }

    partial class SetPositionBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlPlaceAtBrick
            {
                XPosition = ValueX == null ? new XmlFormula() : ValueX.ToXmlObject(), 
                YPosition = ValueY == null ? new XmlFormula() : ValueY.ToXmlObject()
            };
        }
    }

    partial class BounceBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlIfOnEdgeBounceBrick();
        }
    }

    partial class SetSizeBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlSetSizeToBrick
            {
                 Size = Percentage == null ? new XmlFormula() : Percentage.ToXmlObject()
            };
        }
    }

    partial class SetRotationBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlPointInDirectionBrick
            {
                Degrees = Value == null ? new XmlFormula() : Value.ToXmlObject()
            };
        }
    }

    partial class LookAtBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlPointToBrick
            {
                PointedSprite = Target == null ? null : Target.ToXmlObject(context, pointerOnly: true)
            };
        }
    }

    partial class SetBrightnessBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlSetBrightnessBrick
            {
                Brightness = Percentage == null ? new XmlFormula() : Percentage.ToXmlObject()
            };
        }
    }

    partial class SetTransparencyBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlSetGhostEffectBrick
            {
                Transparency = Percentage == null ? new XmlFormula() : Percentage.ToXmlObject()
            };
        }
    }

    partial class ResetGraphicPropertiesBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlClearGraphicEffectBrick();
        }
    }

    partial class BringToFrontBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlComeToFrontBrick();
        }
    }

    #endregion
}
