using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class ForeverBrickConverter : BrickConverterBase<XmlForeverBrick, ForeverBrick>
    {
        public override ForeverBrick Convert1(XmlForeverBrick o, XmlModelConvertContext c)
        {
            var loopEndBrickConverter = new RepeatBrickConverter();

            var result = new ForeverBrick();
            c.Bricks[o] = result;
            result.End = (EndForeverBrick) (o.LoopEndBrick == null ? null : 
                loopEndBrickConverter.Convert(o.LoopEndBrick, c));
            return result;
        }

        public override XmlForeverBrick Convert1(ForeverBrick m, XmlModelConvertBackContext c)
        {
            var converter = new ForeverEndBrickConverter();

            var result = new XmlForeverBrick();
            c.Bricks[m] = result;
            result.LoopEndBrick = m.End == null ? null : converter.Convert(m.End, c);
            return result;
        }
    }
}
