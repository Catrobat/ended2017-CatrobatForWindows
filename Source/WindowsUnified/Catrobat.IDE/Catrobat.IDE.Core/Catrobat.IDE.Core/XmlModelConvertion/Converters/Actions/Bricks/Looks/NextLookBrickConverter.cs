using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Looks;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class NextLookBrickConverter : BrickConverterBase<XmlNextLookBrick, NextLookBrick>
    {
        public NextLookBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override NextLookBrick Convert1(XmlNextLookBrick o, XmlModelConvertContext c)
        {
            return new NextLookBrick();
        }

        public override XmlNextLookBrick Convert1(NextLookBrick m, XmlModelConvertBackContext c)
        {
            return new XmlNextLookBrick();
        }
    }
}
