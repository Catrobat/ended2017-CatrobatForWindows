using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.XmlModelConvertion.Converters.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class SetPositionXBrickConverter : BrickConverterBase<XmlSetXBrick, SetPositionXBrick>
    {
        public override SetPositionXBrick Convert1(XmlSetXBrick o, XmlModelConvertContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new SetPositionXBrick
            {
                Value = o.XPosition == null ? null : formulaConverter.Convert(o.XPosition, c)
            };
        }

        public override XmlSetXBrick Convert1(SetPositionXBrick m, XmlModelConvertBackContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new XmlSetXBrick
            {
                XPosition = m.Value == null ? new XmlFormula() : formulaConverter.Convert(m.Value, c)
            };
        }
    }
}
