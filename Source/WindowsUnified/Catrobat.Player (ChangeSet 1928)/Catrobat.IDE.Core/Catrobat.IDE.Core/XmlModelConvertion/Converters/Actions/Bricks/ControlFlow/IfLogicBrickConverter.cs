using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.XmlModelConvertion.Converters.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class IfLogicBrickConverter : BrickConverterBase<XmlIfLogicBeginBrick, IfBrick>
    {
        public IfLogicBrickConverter() { }

        public override IfBrick Convert1(XmlIfLogicBeginBrick o, XmlModelConvertContext c)
        {
            var formulaConverter = new FormulaConverter();
            var ifElseBrickConverter = new IfLogicElseBrickConverter();
            var ifLogicEndBrickConverter = new IfLogicEndBrickConverter();

            var result = new IfBrick
            {
                Condition = o.IfCondition == null ? null : formulaConverter.Convert(o.IfCondition, c)
            };
            c.Bricks[o] = result;
            result.Else = o.IfLogicElseBrick == null ? null : (ElseBrick)ifElseBrickConverter.Convert(o.IfLogicElseBrick, c);
            result.End = o.IfLogicEndBrick == null ? null : (EndIfBrick)ifLogicEndBrickConverter.Convert(o.IfLogicEndBrick, c);
            return result;
        }

        public override XmlIfLogicBeginBrick Convert1(IfBrick m, XmlModelConvertBackContext c)
        {
            var formulaConverter = new FormulaConverter();
            var ifElseBrickConverter = new IfLogicElseBrickConverter();
            var ifLogicEndBrickConverter = new IfLogicEndBrickConverter();

            var result = new XmlIfLogicBeginBrick
            {
                IfCondition = m.Condition == null ? new XmlFormula() : formulaConverter.Convert(m.Condition, c)
            };
            c.Bricks[m] = result;
            result.IfLogicElseBrick = m.Else == null ? null : (XmlIfLogicElseBrick)ifElseBrickConverter.Convert(m.Else, c);
            result.IfLogicEndBrick = m.End == null ? null : (XmlIfLogicEndBrick)ifLogicEndBrickConverter.Convert(m.End, c);
            return result;
        }
    }
}
