using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class IfLogicElseBrickConverter : BrickConverterBase<XmlIfLogicElseBrick, ElseBrick>
    {
        public override ElseBrick Convert1(XmlIfLogicElseBrick o, XmlModelConvertContext c)
        {
            var ifLogicBeginBrickConverter = new IfLogicBrickConverter();
            var ifLogicEndBrickConverter = new IfLogicEndBrickConverter();

            var result = new ElseBrick();
            c.Bricks[o] = result;
            result.Begin = o.IfLogicBeginBrick == null ? null : (IfBrick)ifLogicBeginBrickConverter.Convert(o.IfLogicBeginBrick, c);
            result.End = o.IfLogicEndBrick == null ? null : (EndIfBrick)ifLogicEndBrickConverter.Convert(o.IfLogicEndBrick, c);
            return result;
        }

        public override XmlIfLogicElseBrick Convert1(ElseBrick m, XmlModelConvertBackContext c)
        {
            var ifLogicBeginBrickConverter = new IfLogicBrickConverter();
            var ifLogicEndBrickConverter = new IfLogicEndBrickConverter();

            var result = new XmlIfLogicElseBrick();
            c.Bricks[m] = result;
            result.IfLogicBeginBrick = m.Begin == null ? null : (XmlIfLogicBeginBrick)ifLogicBeginBrickConverter.Convert(m.Begin, c);
            result.IfLogicEndBrick = m.End == null ? null : (XmlIfLogicEndBrick)ifLogicEndBrickConverter.Convert(m.End, c);
            return result;
        }
    }
}
