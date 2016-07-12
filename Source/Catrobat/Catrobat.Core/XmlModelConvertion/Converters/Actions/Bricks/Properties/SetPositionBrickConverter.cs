using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.XmlModelConvertion.Converters.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class SetPositionBrickConverter : BrickConverterBase<XmlPlaceAtBrick, SetPositionBrick>
    {
        public SetPositionBrickConverter() { }

        public override SetPositionBrick Convert1(XmlPlaceAtBrick o, XmlModelConvertContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new SetPositionBrick
            {
                ValueX = o.XPosition == null ? null : formulaConverter.Convert(o.XPosition, c),
                ValueY = o.YPosition == null ? null : formulaConverter.Convert(o.YPosition, c)
            };
        }

        public override XmlPlaceAtBrick Convert1(SetPositionBrick m, XmlModelConvertBackContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new XmlPlaceAtBrick
            {
                XPosition = m.ValueX == null ? new XmlFormula() : formulaConverter.Convert(m.ValueX, c),
                YPosition = m.ValueY == null ? new XmlFormula() : formulaConverter.Convert(m.ValueY, c)
            };
        }
    }
}
