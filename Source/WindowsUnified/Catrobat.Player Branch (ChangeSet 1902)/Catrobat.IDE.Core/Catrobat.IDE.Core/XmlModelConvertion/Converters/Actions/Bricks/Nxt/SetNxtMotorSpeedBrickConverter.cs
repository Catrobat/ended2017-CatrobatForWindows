using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Nxt;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.XmlModelConvertion.Converters.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class SetNxtMotorSpeedBrickConverter : BrickConverterBase<XmlNxtMotorActionBrick, SetNxtMotorSpeedBrick>
    {
        public SetNxtMotorSpeedBrickConverter() { }

        public override SetNxtMotorSpeedBrick Convert1(XmlNxtMotorActionBrick o, XmlModelConvertContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new SetNxtMotorSpeedBrick
            {
                Motor = o.Motor,
                Percentage = o.Speed == null ? null : formulaConverter.Convert(o.Speed, c)
            };
        }

        public override XmlNxtMotorActionBrick Convert1(SetNxtMotorSpeedBrick m, XmlModelConvertBackContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new XmlNxtMotorActionBrick
            {
                Motor = m.Motor,
                Speed = m.Percentage == null ? new XmlFormula() : formulaConverter.Convert(m.Percentage, c)
            };
        }
    }
}
