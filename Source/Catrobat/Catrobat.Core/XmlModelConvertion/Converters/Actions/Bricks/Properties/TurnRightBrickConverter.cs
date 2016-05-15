using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.XmlModelConvertion.Converters.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class TurnRightBrickConverter : BrickConverterBase<XmlTurnRightBrick, TurnRightBrick>
    {
        public TurnRightBrickConverter() { }

        public override TurnRightBrick Convert1(XmlTurnRightBrick o, XmlModelConvertContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new TurnRightBrick
            {
                RelativeValue = o.Degrees == null ? null : formulaConverter.Convert(o.Degrees, c)
            };
        }

        public override XmlTurnRightBrick Convert1(TurnRightBrick m, XmlModelConvertBackContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new XmlTurnRightBrick
            {
                Degrees = m.RelativeValue == null ? new XmlFormula() : formulaConverter.Convert(m.RelativeValue, c)
            };
        }
    }
}
