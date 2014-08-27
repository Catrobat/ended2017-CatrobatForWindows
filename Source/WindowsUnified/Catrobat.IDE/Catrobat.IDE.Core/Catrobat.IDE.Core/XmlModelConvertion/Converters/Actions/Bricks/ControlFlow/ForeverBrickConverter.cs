using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class ForeverBrickConverter : BrickConverterBase<XmlForeverBrick, ForeverBrick>
    {
        public ForeverBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override ForeverBrick Convert1(XmlForeverBrick o, XmlModelConvertContext c)
        {
            var result = new ForeverBrick();
            c.Bricks[o] = result;
            result.End = o.LoopEndBrick == null ? null : (EndForeverBrick)Converter.Convert(o.LoopEndBrick);
            return result;
        }

        public override XmlForeverBrick Convert1(ForeverBrick m, XmlModelConvertBackContext c)
        {
            var result = new XmlForeverBrick();
            c.Bricks[m] = result;
            result.LoopEndBrick = m.End == null ? null : (XmlForeverLoopEndBrick)Converter.Convert(m.End);
            return result;
        }
    }
}
