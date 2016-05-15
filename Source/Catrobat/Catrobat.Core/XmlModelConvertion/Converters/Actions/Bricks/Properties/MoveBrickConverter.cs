using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.XmlModelConvertion.Converters.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class MoveBrickConverter : BrickConverterBase<XmlMoveNStepsBrick, MoveBrick>
    {
        public override MoveBrick Convert1(XmlMoveNStepsBrick o, XmlModelConvertContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new MoveBrick
            {
                Steps = o.Steps == null ? null : formulaConverter.Convert(o.Steps, c)
            };
        }

        public override XmlMoveNStepsBrick Convert1(MoveBrick m, XmlModelConvertBackContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new XmlMoveNStepsBrick
            {
                Steps = m.Steps == null ? new XmlFormula() : formulaConverter.Convert(m.Steps, c)
            };
        }
    }
}
