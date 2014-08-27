using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class ShowBrickConverter : BrickConverterBase<XmlShowBrick, ShowBrick>
    {
        public ShowBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override ShowBrick Convert1(XmlShowBrick o, XmlModelConvertContext c)
        {
            return new ShowBrick();
        }

        public override XmlShowBrick Convert1(ShowBrick m, XmlModelConvertBackContext c)
        {
            return new XmlShowBrick();
        }
    }
}
