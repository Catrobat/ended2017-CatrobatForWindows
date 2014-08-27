using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class IfLogicEndBrickConverter : BrickConverterBase<XmlIfLogicEndBrick, EndIfBrick>
    {
        public IfLogicEndBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override EndIfBrick Convert1(XmlIfLogicEndBrick o, XmlModelConvertContext c)
        {
            var result = new EndIfBrick();
            c.Bricks[o] = result;
            result.Begin = o.IfLogicBeginBrick == null ? null : (IfBrick)Converter.Convert(o.IfLogicBeginBrick);
            result.Else = o.IfLogicElseBrick == null ? null : (ElseBrick)Converter.Convert(o.IfLogicElseBrick);
            return result;
        }

        public override XmlIfLogicEndBrick Convert1(EndIfBrick m, XmlModelConvertBackContext c)
        {
            var result = new XmlIfLogicEndBrick();
            c.Bricks[m] = result;
            result.IfLogicBeginBrick = m.Begin == null ? null : (XmlIfLogicBeginBrick)Converter.Convert(m.Begin);
            result.IfLogicElseBrick = m.Else == null ? null : (XmlIfLogicElseBrick)Converter.Convert(m.Else);
            return result;
        }
    }
}
