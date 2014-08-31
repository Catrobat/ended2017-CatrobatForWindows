using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.XmlModelConvertion.Converters.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class TurnLeftBrickConverter : BrickConverterBase<XmlTurnLeftBrick, TurnLeftBrick>
    {
        public TurnLeftBrickConverter() { }

        public override TurnLeftBrick Convert1(XmlTurnLeftBrick o, XmlModelConvertContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new TurnLeftBrick
            {
                RelativeValue = o.Degrees == null ? null : formulaConverter.Convert(o.Degrees, c)
            };
        }

        public override XmlTurnLeftBrick Convert1(TurnLeftBrick m, XmlModelConvertBackContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new XmlTurnLeftBrick
            {
                Degrees = m.RelativeValue == null ? new XmlFormula() : formulaConverter.Convert(m.RelativeValue, c)
            };
        }
    }
}
