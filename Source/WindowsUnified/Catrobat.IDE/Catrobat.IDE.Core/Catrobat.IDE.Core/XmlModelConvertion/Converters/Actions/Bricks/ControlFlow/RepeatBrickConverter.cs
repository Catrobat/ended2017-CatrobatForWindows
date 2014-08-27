using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class RepeatBrickConverter : BrickConverterBase<XmlRepeatBrick, RepeatBrick>
    {
        public RepeatBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override RepeatBrick Convert1(XmlRepeatBrick o, XmlModelConvertContext c)
        {
            var result = new RepeatBrick
            {
                Count = o.TimesToRepeat == null ? null : (FormulaTree)Converter.Convert(o.TimesToRepeat)
            };
            c.Bricks[o] = result;
            result.End = o.LoopEndBrick == null ? null : (EndRepeatBrick) Converter.Convert(o.LoopEndBrick);
            return result;
        }

        public override XmlRepeatBrick Convert1(RepeatBrick m, XmlModelConvertBackContext c)
        {
            var result = new XmlRepeatBrick
            {
                TimesToRepeat = m.Count == null ? new XmlFormula() : (XmlFormula)Converter.Convert(m.Count)
            };
            c.Bricks[m] = result;
            result.LoopEndBrick = m.End == null ? null : (XmlRepeatLoopEndBrick)Converter.Convert(m.End);
            return result;
        }
    }
}
