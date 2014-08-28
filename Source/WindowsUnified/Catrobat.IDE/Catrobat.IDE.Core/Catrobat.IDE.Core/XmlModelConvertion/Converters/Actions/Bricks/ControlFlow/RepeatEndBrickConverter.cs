using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class RepeatEndBrickConverter : BrickConverterBase<XmlRepeatLoopEndBrick, EndRepeatBrick>
    {
        public RepeatEndBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override EndRepeatBrick Convert1(XmlRepeatLoopEndBrick o, XmlModelConvertContext c)
        {
            var result = new EndRepeatBrick();
            c.Bricks[o] = result;
            result.Begin = o.LoopBeginBrick == null ? null : (RepeatBrick)Converter.Convert(o.LoopBeginBrick);
            return result;
        }

        public override XmlRepeatLoopEndBrick Convert1(EndRepeatBrick m, XmlModelConvertBackContext c)
        {
            var result = new XmlRepeatLoopEndBrick();
            c.Bricks[m] = result;
            result.LoopBeginBrick = m.Begin == null ? null : (XmlRepeatBrick)Converter.Convert(m.Begin);
            return result;
        }
    }
}
