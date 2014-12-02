using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.XmlModelConvertion.Converters.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class ChangePositionXBrickConverter : BrickConverterBase<XmlChangeXByBrick, ChangePositionXBrick>
    {
        public override ChangePositionXBrick Convert1(XmlChangeXByBrick o, XmlModelConvertContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new ChangePositionXBrick
            {
                RelativeValue = o.XMovement == null ? null : formulaConverter.Convert(o.XMovement, c)
            };
        }

        public override XmlChangeXByBrick Convert1(ChangePositionXBrick m, XmlModelConvertBackContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new XmlChangeXByBrick
            {
                XMovement = m.RelativeValue == null ? new XmlFormula() : formulaConverter.Convert(m.RelativeValue, c)
            };
        }
    }
}
