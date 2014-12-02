using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.XmlModelConvertion.Converters.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class ChangeTransparencyBrickConverter : BrickConverterBase<XmlChangeGhostEffectBrick, ChangeTransparencyBrick>
    {
        public override ChangeTransparencyBrick Convert1(XmlChangeGhostEffectBrick o, XmlModelConvertContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new ChangeTransparencyBrick
            {
                RelativePercentage = o.ChangeGhostEffect == null ? null : formulaConverter.Convert(o.ChangeGhostEffect, c)
            };
        }

        public override XmlChangeGhostEffectBrick Convert1(ChangeTransparencyBrick m, XmlModelConvertBackContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new XmlChangeGhostEffectBrick
            {
                ChangeGhostEffect = m.RelativePercentage == null ?
                new XmlFormula() : formulaConverter.Convert(m.RelativePercentage, c)
            };
        }
    }
}
