using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.XmlModelConvertion.Converters.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class SetRotationBrickConverter : BrickConverterBase<XmlPointInDirectionBrick, SetRotationBrick>
    {
        public override SetRotationBrick Convert1(XmlPointInDirectionBrick o, XmlModelConvertContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new SetRotationBrick
            {
                Value = o.Degrees == null ? null : formulaConverter.Convert(o.Degrees, c)
            };
        }

        public override XmlPointInDirectionBrick Convert1(SetRotationBrick m, XmlModelConvertBackContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new XmlPointInDirectionBrick
            {
                Degrees = m.Value == null ? new XmlFormula() : formulaConverter.Convert(m.Value, c)
            };
        }
    }
}
