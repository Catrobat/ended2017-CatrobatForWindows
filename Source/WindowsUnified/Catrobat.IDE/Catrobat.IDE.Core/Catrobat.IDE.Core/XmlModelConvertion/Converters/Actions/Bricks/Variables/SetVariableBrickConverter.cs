using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Variables;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.Xml.XmlObjects.Variables;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class SetVariableBrickConverter : BrickConverterBase<XmlSetVariableBrick, SetVariableBrick>
    {
        public SetVariableBrickConverter(IXmlModelConversionService converter)  
            : base(converter) { }

        public override SetVariableBrick Convert1(XmlSetVariableBrick o, XmlModelConvertContext c)
        {
            Variable variable = null;
            if (o.UserVariable != null) c.Variables.TryGetValue(o.UserVariable, out variable);
            return new SetVariableBrick
            {
                Variable = variable,
                Value = o.VariableFormula == null ? null : (FormulaTree)Converter.Convert(o.VariableFormula)
            };
        }

        public override XmlSetVariableBrick Convert1(SetVariableBrick m, XmlModelConvertBackContext c)
        {
            XmlUserVariable variable = null;
            if (m.Variable != null) c.Variables.TryGetValue(m.Variable, out variable);
            return new XmlSetVariableBrick
            {
                UserVariable = variable,
                VariableFormula = m.Value == null ? new XmlFormula() : (XmlFormula)Converter.Convert(m.Value)
            };
        }
    }
}
