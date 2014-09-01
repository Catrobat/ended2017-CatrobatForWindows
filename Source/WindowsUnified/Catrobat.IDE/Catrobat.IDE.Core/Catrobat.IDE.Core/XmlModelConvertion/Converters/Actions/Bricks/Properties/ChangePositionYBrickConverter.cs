using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.XmlModelConvertion.Converters.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class ChangePositionYBrickConverter : BrickConverterBase<XmlChangeYByBrick, ChangePositionYBrick>
    {
        public ChangePositionYBrickConverter() { }

        public override ChangePositionYBrick Convert1(XmlChangeYByBrick o, XmlModelConvertContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new ChangePositionYBrick
            {
                RelativeValue = o.YMovement == null ? null : formulaConverter.Convert(o.YMovement, c)
            };
        }

        public override XmlChangeYByBrick Convert1(ChangePositionYBrick m, XmlModelConvertBackContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new XmlChangeYByBrick
            {
                YMovement = m.RelativeValue == null ? new XmlFormula() : formulaConverter.Convert(m.RelativeValue, c)
            };
        }
    }
}
