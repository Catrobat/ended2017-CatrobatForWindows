using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class SetTransparencyBrickConverter : BrickConverterBase<XmlSetGhostEffectBrick, SetTransparencyBrick>
    {
        public SetTransparencyBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override SetTransparencyBrick Convert1(XmlSetGhostEffectBrick o, XmlModelConvertContext c)
        {
            return new SetTransparencyBrick
            {
                Percentage = o.Transparency == null ? null : (FormulaTree)Converter.Convert(o.Transparency)
            };
        }

        public override XmlSetGhostEffectBrick Convert1(SetTransparencyBrick m, XmlModelConvertBackContext c)
        {
            return new XmlSetGhostEffectBrick
            {
                Transparency = m.Percentage == null ? new XmlFormula() : (XmlFormula)Converter.Convert(m.Percentage)
            };
        }
    }
}
