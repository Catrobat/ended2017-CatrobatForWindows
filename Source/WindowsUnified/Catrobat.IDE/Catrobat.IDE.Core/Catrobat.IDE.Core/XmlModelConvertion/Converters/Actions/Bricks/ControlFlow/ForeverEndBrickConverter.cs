using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class ForeverEndBrickConverter : BrickConverterBase<XmlForeverLoopEndBrick, EndForeverBrick>
    {
        public ForeverEndBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override EndForeverBrick Convert1(XmlForeverLoopEndBrick o, XmlModelConvertContext c)
        {
            var result = new EndForeverBrick();
            c.Bricks[o] = result;
            result.Begin = o.LoopBeginBrick == null ? null : (ForeverBrick)Converter.Convert(o.LoopBeginBrick);
            return result;
        }

        public override XmlForeverLoopEndBrick Convert1(EndForeverBrick m, XmlModelConvertBackContext c)
        {
            var result = new XmlForeverLoopEndBrick();
            c.Bricks[m] = result;
            result.LoopBeginBrick = m.Begin == null ? null : (XmlForeverBrick)Converter.Convert(m.Begin);
            return result;
        }
    }
}
