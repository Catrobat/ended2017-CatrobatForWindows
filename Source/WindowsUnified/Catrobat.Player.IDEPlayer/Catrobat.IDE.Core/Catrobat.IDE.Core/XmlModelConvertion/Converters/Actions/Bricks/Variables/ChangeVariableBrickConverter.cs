using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Variables;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.Xml.XmlObjects.Variables;
using Catrobat.IDE.Core.XmlModelConvertion.Converters.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class ChangeVariableBrickConverter : BrickConverterBase<XmlChangeVariableBrick, ChangeVariableBrick>
    {
        public ChangeVariableBrickConverter() { }

        public override ChangeVariableBrick Convert1(XmlChangeVariableBrick o, XmlModelConvertContext c)
        {
            var formulaConverter = new FormulaConverter();

            Variable variable = null;
            if (o.UserVariable != null) c.Variables.TryGetValue(o.UserVariable, out variable);
            return new ChangeVariableBrick
            {
                Variable = variable,
                RelativeValue = o.VariableFormula == null ? null : formulaConverter.Convert(o.VariableFormula, c)
            };
        }

        public override XmlChangeVariableBrick Convert1(ChangeVariableBrick m, XmlModelConvertBackContext c)
        {
            var formulaConverter = new FormulaConverter();

            XmlUserVariable variable = null;
            if (m.Variable != null) c.Variables.TryGetValue(m.Variable, out variable);
            return new XmlChangeVariableBrick
            {
                UserVariable = variable,
                VariableFormula = m.RelativeValue == null ? new XmlFormula() :
                  formulaConverter.Convert(m.RelativeValue, c)
            };
        }
    }
}
