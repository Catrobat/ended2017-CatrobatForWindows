using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.XmlModelConvertion.Converters.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class DecreaseZOrderBrickConverter : BrickConverterBase<XmlGoNStepsBackBrick, DecreaseZOrderBrick>
    {
        public DecreaseZOrderBrickConverter() { }

        public override DecreaseZOrderBrick Convert1(XmlGoNStepsBackBrick o, XmlModelConvertContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new DecreaseZOrderBrick
            {
                RelativeValue = o.StepsXML == null ? null : formulaConverter.Convert(o.StepsXML, c)
            };
        }

        public override XmlGoNStepsBackBrick Convert1(DecreaseZOrderBrick m, XmlModelConvertBackContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new XmlGoNStepsBackBrick
            {
                StepsXML = m.RelativeValue == null ? new XmlFormula() : formulaConverter.Convert(m.RelativeValue, c)
            };
        }
    }
}
