using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Nxt;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class ChangeNxtMotorAngleBrickConverter : BrickConverterBase<XmlNxtMotorTurnAngleBrick, ChangeNxtMotorAngleBrick>
    {
        public ChangeNxtMotorAngleBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override ChangeNxtMotorAngleBrick Convert1(XmlNxtMotorTurnAngleBrick o, XmlModelConvertContext c)
        {
            return new ChangeNxtMotorAngleBrick
            {
                Motor = o.Motor,
                RelativeValue = o.Degrees == null ? null : (FormulaTree)Converter.Convert(o.Degrees)
            };
        }

        public override XmlNxtMotorTurnAngleBrick Convert1(ChangeNxtMotorAngleBrick m, XmlModelConvertBackContext c)
        {
            return new XmlNxtMotorTurnAngleBrick
            {
                Motor = m.Motor,
                Degrees = m.RelativeValue == null ? new XmlFormula() : (XmlFormula)Converter.Convert(m.RelativeValue)
            };
        }
    }
}
