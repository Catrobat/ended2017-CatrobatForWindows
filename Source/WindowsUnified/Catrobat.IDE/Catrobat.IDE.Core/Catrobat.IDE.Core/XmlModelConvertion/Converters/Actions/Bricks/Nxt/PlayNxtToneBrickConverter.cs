using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Nxt;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.XmlModelConvertion.Converters.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class PlayNxtToneBrickConverter : BrickConverterBase<XmlNxtPlayToneBrick, PlayNxtToneBrick>
    {
        public override PlayNxtToneBrick Convert1(XmlNxtPlayToneBrick o, XmlModelConvertContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new PlayNxtToneBrick
            {
                Frequency = o.Frequency == null ? null : formulaConverter.Convert(o.Frequency, c),
                Duration = o.DurationInSeconds == null ? null : formulaConverter.Convert(o.DurationInSeconds, c)
            };
        }

        public override XmlNxtPlayToneBrick Convert1(PlayNxtToneBrick m, XmlModelConvertBackContext c)
        {
            var formulaConverter = new FormulaConverter();

            return new XmlNxtPlayToneBrick
            {
                Frequency = m.Frequency == null ? new XmlFormula() : formulaConverter.Convert(m.Frequency, c),
                DurationInSeconds = m.Duration == null ? new XmlFormula() : formulaConverter.Convert(m.Duration, c)
            };
        }
    }
}
