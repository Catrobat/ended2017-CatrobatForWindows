using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Variables;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.Xml.XmlObjects.Variables;
using Catrobat.IDE.Core.XmlModelConvertion.Converters.Formulas;
using Catrobat.IDE.Core.XmlModelConvertion;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class SetVariableBrickConverter : BrickConverterBase<XmlSetVariableBrick, SetVariableBrick>
    {
        public SetVariableBrickConverter() { }

        public override SetVariableBrick Convert1(XmlSetVariableBrick o, XmlModelConvertContext c)
        {
            var formulaConverter = new FormulaConverter();
            Variable variable = null;
            //TODO: part of dirty hack:
            if (o.UserVariable != null)
                foreach(var entry in c.variables)
                {
                    if (entry.Key.Name == o.UserVariable.Name)
                    {
                        variable = entry.Value;
                        break;
                    }
                }
            //maybe its necessary to override .Equals() for the class as it got more properties now

            //old undirty version //if (o.UserVariable != null) c.Variables.TryGetValue(o.UserVariable, out variable);
            return new SetVariableBrick
            {
                Variable = variable,
                Value = o.VariableFormula == null ? null : formulaConverter.Convert(o.VariableFormula, c)
            };
        }

        public override XmlSetVariableBrick Convert1(SetVariableBrick m, XmlModelConvertBackContext c)
        {
            var formulaConverter = new FormulaConverter();

            XmlUserVariable variable = null;
            if (m.Variable != null) c.Variables.TryGetValue(m.Variable, out variable);
            return new XmlSetVariableBrick
            {
                UserVariable = variable,
                VariableFormula = m.Value == null ? new XmlFormula() : formulaConverter.Convert(m.Value, c)
            };
        }
    }
}
