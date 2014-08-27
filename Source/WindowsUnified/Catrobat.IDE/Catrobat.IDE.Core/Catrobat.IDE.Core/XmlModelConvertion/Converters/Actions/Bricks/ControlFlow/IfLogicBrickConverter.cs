using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class IfLogicBrickConverter : BrickConverterBase<XmlIfLogicBeginBrick, IfBrick>
    {
        public IfLogicBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override IfBrick Convert1(XmlIfLogicBeginBrick o, XmlModelConvertContext c)
        {
            var result = new IfBrick
            {
                Condition = o.IfCondition == null ? null : (FormulaTree)Converter.Convert(o.IfCondition)
            };
            c.Bricks[o] = result;
            result.Else = o.IfLogicElseBrick == null ? null : (ElseBrick)Converter.Convert(o.IfLogicElseBrick);
            result.End = o.IfLogicEndBrick == null ? null : (EndIfBrick)Converter.Convert(o.IfLogicEndBrick);
            return result;
        }

        public override XmlIfLogicBeginBrick Convert1(IfBrick m, XmlModelConvertBackContext c)
        {
            var result = new XmlIfLogicBeginBrick
            {
                IfCondition = m.Condition == null ? new XmlFormula() : (XmlFormula)Converter.Convert(m.Condition)
            };
            c.Bricks[m] = result;
            result.IfLogicElseBrick = m.Else == null ? null : (XmlIfLogicElseBrick)Converter.Convert(m.Else);
            result.IfLogicEndBrick = m.End == null ? null : (XmlIfLogicEndBrick)Converter.Convert(m.End);
            return result;
        }
    }
}
