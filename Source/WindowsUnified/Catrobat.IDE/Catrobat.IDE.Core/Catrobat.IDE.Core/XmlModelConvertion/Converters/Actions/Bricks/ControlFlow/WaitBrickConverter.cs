using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.XmlModelConvertion.Converters.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class WaitBrickConverter : BrickConverterBase<XmlWaitBrick, DelayBrick>
    {
        public override DelayBrick Convert1(XmlWaitBrick o, XmlModelConvertContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new DelayBrick
            {
                Duration = o.TimeToWaitInSeconds == null ? null : formulaConverter.Convert(o.TimeToWaitInSeconds, c)
            };
        }

        public override XmlWaitBrick Convert1(DelayBrick m, XmlModelConvertBackContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new XmlWaitBrick
            {
                TimeToWaitInSeconds = m.Duration == null ? new XmlFormula() : formulaConverter.Convert(m.Duration, c)
            };
        }
    }
}
