using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.XmlModelConvertion.Converters.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class GlideToBrickConverter : BrickConverterBase<XmlGlideToBrick, AnimatePositionBrick>
    {
        public GlideToBrickConverter() { }

        public override AnimatePositionBrick Convert1(XmlGlideToBrick o, XmlModelConvertContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new AnimatePositionBrick
            {
                Duration = o.DurationInSeconds == null ? null : formulaConverter.Convert(o.DurationInSeconds, c),
                ToX = o.XDestination == null ? null : formulaConverter.Convert(o.XDestination, c),
                ToY = o.YDestination == null ? null : formulaConverter.Convert(o.YDestination, c)
            };
        }

        public override XmlGlideToBrick Convert1(AnimatePositionBrick m, XmlModelConvertBackContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new XmlGlideToBrick
            {
                DurationInSeconds = m.Duration == null ? new XmlFormula() : formulaConverter.Convert(m.Duration, c),
                XDestination = m.ToX == null ? new XmlFormula() : formulaConverter.Convert(m.ToX, c),
                YDestination = m.ToY == null ? new XmlFormula() : formulaConverter.Convert(m.ToY, c)
            };
        }
    }
}
