using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class BringToFrontBrickConverter : BrickConverterBase<XmlComeToFrontBrick, BringToFrontBrick>
    {
        public BringToFrontBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override BringToFrontBrick Convert1(XmlComeToFrontBrick o, XmlModelConvertContext c)
        {
            return new BringToFrontBrick();
        }

        public override XmlComeToFrontBrick Convert1(BringToFrontBrick m, XmlModelConvertBackContext c)
        {
            return new XmlComeToFrontBrick();
        }
    }
}
