using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class ChangeTransparencyBrickConverter : BrickConverterBase<XmlChangeGhostEffectBrick, ChangeTransparencyBrick>
    {
        public ChangeTransparencyBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override ChangeTransparencyBrick Convert1(XmlChangeGhostEffectBrick o, XmlModelConvertContext c)
        {
            return new ChangeTransparencyBrick
            {
                RelativePercentage = o.ChangeGhostEffect == null ? null : (FormulaTree)Converter.Convert(o.ChangeGhostEffect)
            };
        }

        public override XmlChangeGhostEffectBrick Convert1(ChangeTransparencyBrick m, XmlModelConvertBackContext c)
        {
            return new XmlChangeGhostEffectBrick
            {
                ChangeGhostEffect = m.RelativePercentage == null ? new XmlFormula() : (XmlFormula)Converter.Convert(m.RelativePercentage)
            };
        }
    }
}
