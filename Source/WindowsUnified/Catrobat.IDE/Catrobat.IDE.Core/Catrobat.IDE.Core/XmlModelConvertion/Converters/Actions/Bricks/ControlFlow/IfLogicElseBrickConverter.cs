using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class IfLogicElseBrickConverter : BrickConverterBase<XmlIfLogicElseBrick, ElseBrick>
    {
        public IfLogicElseBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override ElseBrick Convert1(XmlIfLogicElseBrick o, XmlModelConvertContext c)
        {
            var result = new ElseBrick();
            c.Bricks[o] = result;
            result.Begin = o.IfLogicBeginBrick == null ? null : (IfBrick) Converter.Convert(o.IfLogicBeginBrick);
            result.End = o.IfLogicEndBrick == null ? null : (EndIfBrick)Converter.Convert(o.IfLogicEndBrick);
            return result;
        }

        public override XmlIfLogicElseBrick Convert1(ElseBrick m, XmlModelConvertBackContext c)
        {
            var result = new XmlIfLogicElseBrick();
            c.Bricks[m] = result;
            result.IfLogicBeginBrick = m.Begin == null ? null : (XmlIfLogicBeginBrick)Converter.Convert(m.Begin);
            result.IfLogicEndBrick = m.End == null ? null : (XmlIfLogicEndBrick)Converter.Convert(m.End);
            return result;
        }
    }
}
