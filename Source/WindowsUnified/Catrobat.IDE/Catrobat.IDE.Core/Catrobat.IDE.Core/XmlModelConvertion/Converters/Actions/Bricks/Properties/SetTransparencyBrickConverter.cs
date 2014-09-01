using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.XmlModelConvertion.Converters.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class SetTransparencyBrickConverter : BrickConverterBase<XmlSetGhostEffectBrick, SetTransparencyBrick>
    {
        public SetTransparencyBrickConverter() { }

        public override SetTransparencyBrick Convert1(XmlSetGhostEffectBrick o, XmlModelConvertContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new SetTransparencyBrick
            {
                Percentage = o.Transparency == null ? null : formulaConverter.Convert(o.Transparency, c)
            };
        }

        public override XmlSetGhostEffectBrick Convert1(SetTransparencyBrick m, XmlModelConvertBackContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new XmlSetGhostEffectBrick
            {
                Transparency = m.Percentage == null ? new XmlFormula() : formulaConverter.Convert(m.Percentage, c)
            };
        }
    }
}
