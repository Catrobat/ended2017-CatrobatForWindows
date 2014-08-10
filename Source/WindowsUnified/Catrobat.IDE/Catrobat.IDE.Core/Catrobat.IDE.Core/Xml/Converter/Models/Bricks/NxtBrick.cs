using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Nxt;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProgramConverter.ConvertBackContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Bricks
{
    partial class NxtBrick
    {
    }

    partial class NxtMotorBrick
    {
    }

    #region Implementations


    partial class PlayNxtToneBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlNxtPlayToneBrick
            {
                Frequency = Frequency == null ? new XmlFormula() : Frequency.ToXmlObject(), 
                DurationInSeconds = Duration == null ? new XmlFormula() : Duration.ToXmlObject()
            };
        }
    }

    partial class SetNxtMotorSpeedBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlNxtMotorActionBrick
            {
                Motor = Motor, 
                Speed = Percentage == null ? new XmlFormula() : Percentage.ToXmlObject()
            };
        }
    }

    partial class ChangeNxtMotorAngleBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlNxtMotorTurnAngleBrick
            {
                Motor = Motor,
                Degrees = RelativeValue == null ? new XmlFormula() : RelativeValue.ToXmlObject()
            };
        }
    }

    partial class StopNxtMotorBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlNxtMotorStopBrick
            {
                Motor = Motor
            };
        }
    }

    #endregion
}
