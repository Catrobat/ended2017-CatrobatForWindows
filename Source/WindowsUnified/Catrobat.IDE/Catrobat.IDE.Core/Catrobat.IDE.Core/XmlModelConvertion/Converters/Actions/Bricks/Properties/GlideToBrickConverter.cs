using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class GlideToBrickConverter : BrickConverterBase<XmlGlideToBrick, AnimatePositionBrick>
    {
        public GlideToBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override AnimatePositionBrick Convert1(XmlGlideToBrick o, XmlModelConvertContext c)
        {
            return new AnimatePositionBrick
            {
                Duration = o.DurationInSeconds == null ? null : (FormulaTree)Converter.Convert(o. DurationInSeconds),
                ToX = o.XDestination == null ? null : (FormulaTree)Converter.Convert(o.XDestination),
                ToY = o.YDestination == null ? null : (FormulaTree)Converter.Convert(o.YDestination)
            };
        }

        public override XmlGlideToBrick Convert1(AnimatePositionBrick m, XmlModelConvertBackContext c)
        {
            return new XmlGlideToBrick
            {
                DurationInSeconds = m.Duration == null ? new XmlFormula() : (XmlFormula)Converter.Convert(m.Duration),
                XDestination = m.ToX == null ? new XmlFormula() : (XmlFormula)Converter.Convert(m.ToX),
                YDestination = m.ToY == null ? new XmlFormula() : (XmlFormula)Converter.Convert(m.ToY)
            };
        }
    }
}
