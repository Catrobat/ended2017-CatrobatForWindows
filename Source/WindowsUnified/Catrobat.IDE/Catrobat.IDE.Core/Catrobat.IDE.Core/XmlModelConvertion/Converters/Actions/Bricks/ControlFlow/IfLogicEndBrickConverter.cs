using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class IfLogicEndBrickConverter : BrickConverterBase<XmlIfLogicEndBrick, EndIfBrick>
    {
        public override EndIfBrick Convert1(XmlIfLogicEndBrick o, XmlModelConvertContext c)
        {
            var ifConverter = new IfLogicBrickConverter();
            var elseConverter = new IfLogicElseBrickConverter();

            var result = new EndIfBrick();
            c.Bricks[o] = result;
            result.Begin = o.IfLogicBeginBrick == null ? null : ifConverter.Convert(o.IfLogicBeginBrick, c);
            result.Else = o.IfLogicElseBrick == null ? null : elseConverter.Convert(o.IfLogicElseBrick, c);
            return result;
        }

        public override XmlIfLogicEndBrick Convert1(EndIfBrick m, XmlModelConvertBackContext c)
        {
            var ifConverter = new IfLogicBrickConverter();
            var elseConverter = new IfLogicElseBrickConverter();

            var result = new XmlIfLogicEndBrick();
            c.Bricks[m] = result;
            result.IfLogicBeginBrick = m.Begin == null ? null : ifConverter.Convert(m.Begin, c);
            result.IfLogicElseBrick = m.Else == null ? null : elseConverter.Convert(m.Else, c);
            return result;
        }
    }
}
