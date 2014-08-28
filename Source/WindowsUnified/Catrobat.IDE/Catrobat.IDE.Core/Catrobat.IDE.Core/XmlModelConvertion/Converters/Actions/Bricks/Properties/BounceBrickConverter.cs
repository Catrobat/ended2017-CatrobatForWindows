using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class BounceBrickConverter : BrickConverterBase<XmlIfOnEdgeBounceBrick, BounceBrick>
    {
        public BounceBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override BounceBrick Convert1(XmlIfOnEdgeBounceBrick o, XmlModelConvertContext c)
        {
            return new BounceBrick();
        }

        public override XmlIfOnEdgeBounceBrick Convert1(BounceBrick m, XmlModelConvertBackContext c)
        {
            return new XmlIfOnEdgeBounceBrick();
        }
    }
}
