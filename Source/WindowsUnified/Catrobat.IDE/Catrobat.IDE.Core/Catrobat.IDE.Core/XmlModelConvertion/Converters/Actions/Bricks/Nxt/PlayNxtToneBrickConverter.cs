using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Nxt;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class PlayNxtToneBrickConverter : BrickConverterBase<XmlNxtPlayToneBrick, PlayNxtToneBrick>
    {
        public PlayNxtToneBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override PlayNxtToneBrick Convert1(XmlNxtPlayToneBrick o, XmlModelConvertContext c)
        {
            return new PlayNxtToneBrick
            {
                Frequency = o.Frequency == null ? null : (FormulaTree)Converter.Convert(o.Frequency),
                Duration = o.DurationInSeconds == null ? null : (FormulaTree)Converter.Convert(o.DurationInSeconds)
            };
        }

        public override XmlNxtPlayToneBrick Convert1(PlayNxtToneBrick m, XmlModelConvertBackContext c)
        {
            return new XmlNxtPlayToneBrick
            {
                Frequency = m.Frequency == null ? new XmlFormula() : (XmlFormula)Converter.Convert(m.Frequency),
                DurationInSeconds = m.Duration == null ? new XmlFormula() : (XmlFormula)Converter.Convert(m.Duration)
            };
        }
    }
}
