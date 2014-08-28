using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class ResetGraphicPropertiesBrickConverter : BrickConverterBase<XmlClearGraphicEffectBrick, ResetGraphicPropertiesBrick>
    {
        public ResetGraphicPropertiesBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override ResetGraphicPropertiesBrick Convert1(XmlClearGraphicEffectBrick o, XmlModelConvertContext c)
        {
            return new ResetGraphicPropertiesBrick();
        }

        public override XmlClearGraphicEffectBrick Convert1(ResetGraphicPropertiesBrick m, XmlModelConvertBackContext c)
        {
            return new XmlClearGraphicEffectBrick();
        }
    }
}
