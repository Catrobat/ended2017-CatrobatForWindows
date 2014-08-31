using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.XmlModelConvertion.Converters.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class SetPositionYBrickConverter : BrickConverterBase<XmlSetYBrick, SetPositionYBrick>
    {
        public override SetPositionYBrick Convert1(XmlSetYBrick o, XmlModelConvertContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new SetPositionYBrick
            {
                Value = o.YPosition == null ? null : formulaConverter.Convert(o.YPosition, c)
            };
        }

        public override XmlSetYBrick Convert1(SetPositionYBrick m, XmlModelConvertBackContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new XmlSetYBrick
            {
                YPosition = m.Value == null ? new XmlFormula() : formulaConverter.Convert(m.Value, c)
            };
        }
    }
}
