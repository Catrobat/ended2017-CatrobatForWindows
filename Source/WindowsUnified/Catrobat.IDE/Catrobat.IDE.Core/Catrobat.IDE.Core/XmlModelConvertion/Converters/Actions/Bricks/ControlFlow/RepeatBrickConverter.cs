using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.XmlModelConvertion.Converters.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class RepeatBrickConverter : BrickConverterBase<XmlRepeatBrick, RepeatBrick>
    {
        public override RepeatBrick Convert1(XmlRepeatBrick o, XmlModelConvertContext c)
        {
            var formulaConverter = new FormulaConverter();
            var repeatEndBrickConverter = new RepeatEndBrickConverter();

            var result = new RepeatBrick
            {
                Count = o.TimesToRepeat == null ? null : formulaConverter.Convert(o.TimesToRepeat, c)
            };
            c.Bricks[o] = result;
            result.End = o.LoopEndBrick == null ? null : (EndRepeatBrick)repeatEndBrickConverter.Convert(o.LoopEndBrick, c);
            return result;
        }

        public override XmlRepeatBrick Convert1(RepeatBrick m, XmlModelConvertBackContext c)
        {
            var formulaConverter = new FormulaConverter();
            var loopEndBrickConverter = new RepeatEndBrickConverter();

            var result = new XmlRepeatBrick
            {
                TimesToRepeat = m.Count == null ? new XmlFormula() : formulaConverter.Convert(m.Count, c)
            };
            c.Bricks[m] = result;
            result.LoopEndBrick = m.End == null ? null : (XmlRepeatLoopEndBrick)loopEndBrickConverter.Convert(m.End, c);
            return result;
        }
    }
}
