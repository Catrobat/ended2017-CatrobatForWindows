using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Nxt;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.XmlModelConvertion.Converters.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class ChangeNxtMotorAngleBrickConverter : BrickConverterBase<XmlNxtMotorTurnAngleBrick, ChangeNxtMotorAngleBrick>
    {
        public ChangeNxtMotorAngleBrickConverter() { }

        public override ChangeNxtMotorAngleBrick Convert1(XmlNxtMotorTurnAngleBrick o, XmlModelConvertContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new ChangeNxtMotorAngleBrick
            {
                Motor = o.Motor,
                RelativeValue = o.Degrees == null ? null : formulaConverter.Convert(o.Degrees, c)
            };
        }

        public override XmlNxtMotorTurnAngleBrick Convert1(ChangeNxtMotorAngleBrick m, XmlModelConvertBackContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new XmlNxtMotorTurnAngleBrick
            {
                Motor = m.Motor,
                Degrees = m.RelativeValue == null ? new XmlFormula() : formulaConverter.Convert(m.RelativeValue, c)
            };
        }
    }
}
