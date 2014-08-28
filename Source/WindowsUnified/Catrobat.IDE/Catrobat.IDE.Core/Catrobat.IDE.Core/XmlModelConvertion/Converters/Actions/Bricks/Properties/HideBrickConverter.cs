using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class HideBrickConverter : BrickConverterBase<XmlHideBrick, HideBrick>
    {
        public HideBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override HideBrick Convert1(XmlHideBrick o, XmlModelConvertContext c)
        {
            return new HideBrick();
        }

        public override XmlHideBrick Convert1(HideBrick m, XmlModelConvertBackContext c)
        {
            return new XmlHideBrick();
        }
    }
}
