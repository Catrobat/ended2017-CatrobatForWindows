using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Nxt;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class StopNxtMotorSpeedBrickConverter : BrickConverterBase<XmlNxtMotorStopBrick, StopNxtMotorBrick>
    {
        public StopNxtMotorSpeedBrickConverter() { }

        public override StopNxtMotorBrick Convert1(XmlNxtMotorStopBrick o, XmlModelConvertContext c)
        {
            return new StopNxtMotorBrick
            {
                Motor = o.Motor
            };
        }

        public override XmlNxtMotorStopBrick Convert1(StopNxtMotorBrick m, XmlModelConvertBackContext c)
        {
            return new XmlNxtMotorStopBrick
            {
                Motor = m.Motor
            };
        }
    }
}
