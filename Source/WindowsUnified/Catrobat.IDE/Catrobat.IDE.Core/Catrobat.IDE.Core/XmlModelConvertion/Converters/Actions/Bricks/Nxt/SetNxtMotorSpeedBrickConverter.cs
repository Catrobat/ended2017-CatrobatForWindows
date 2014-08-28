using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Nxt;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class SetNxtMotorSpeedBrickConverter : BrickConverterBase<XmlNxtMotorActionBrick, SetNxtMotorSpeedBrick>
    {
        public SetNxtMotorSpeedBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override SetNxtMotorSpeedBrick Convert1(XmlNxtMotorActionBrick o, XmlModelConvertContext c)
        {
            return new SetNxtMotorSpeedBrick
            {
                Motor = o.Motor,
                Percentage = o.Speed == null ? null : (FormulaTree)Converter.Convert(o.Speed)
            };
        }

        public override XmlNxtMotorActionBrick Convert1(SetNxtMotorSpeedBrick m, XmlModelConvertBackContext c)
        {
            return new XmlNxtMotorActionBrick
            {
                Motor = m.Motor,
                Speed = m.Percentage == null ? new XmlFormula() : (XmlFormula)Converter.Convert(m.Percentage)
            };
        }
    }
}
