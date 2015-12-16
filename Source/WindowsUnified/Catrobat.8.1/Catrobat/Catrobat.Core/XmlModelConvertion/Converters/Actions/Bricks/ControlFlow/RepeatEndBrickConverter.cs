using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class RepeatEndBrickConverter : BrickConverterBase<XmlRepeatLoopEndBrick, EndRepeatBrick>
    {

        public override EndRepeatBrick Convert1(XmlRepeatLoopEndBrick o, XmlModelConvertContext c)
        {
            var repeatBrickConverter = new RepeatBrickConverter();

            var result = new EndRepeatBrick();
            c.Bricks[o] = result;
            result.Begin = (RepeatBrick) (o.LoopBeginBrick == null ? null : repeatBrickConverter.Convert(o.LoopBeginBrick, c));
            return result;
        }

        public override XmlRepeatLoopEndBrick Convert1(EndRepeatBrick m, XmlModelConvertBackContext c)
        {
            var repeatBrickConverter = new RepeatBrickConverter();

            var result = new XmlRepeatLoopEndBrick();
            c.Bricks[m] = result;
            result.LoopBeginBrick = m.Begin == null ? null : repeatBrickConverter.Convert(m.Begin, c);
            return result;
        }
    }
}
