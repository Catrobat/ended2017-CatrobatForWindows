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
            var converter = new RepeatBrickConverter();

            var result = new EndRepeatBrick();
            c.Bricks[o] = result;
            result.Begin = (RepeatBrick) (o.LoopBeginBrick == null ? null : 
                converter.Convert(o.LoopBeginBrick, c));
            return result;
        }

        public override XmlRepeatLoopEndBrick Convert1(EndRepeatBrick m, XmlModelConvertBackContext c)
        {
            var converter = new RepeatBrickConverter();

            var result = new XmlRepeatLoopEndBrick();
            c.Bricks[m] = result;
            result.LoopBeginBrick = m.Begin == null ? null : converter.Convert(m.Begin, c);
            return result;
        }
    }
}
