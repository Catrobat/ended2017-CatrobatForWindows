using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Variables;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.Xml.XmlObjects.Variables;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertBackContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Bricks
{
    partial class VariableBrick
    {
    }

    #region Implementations

    partial class SetVariableBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            XmlUserVariable variable = null;
            if (Variable != null) context.Variables.TryGetValue(Variable, out variable);
            return new XmlSetVariableBrick
            {
                UserVariable = variable, 
                VariableFormula = Value == null ? new XmlFormula() : Value.ToXmlObject(), 
            };
        }
    }

    partial class ChangeVariableBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            XmlUserVariable variable = null;
            if (Variable != null) context.Variables.TryGetValue(Variable, out variable);
            return new XmlChangeVariableBrick
            {
                UserVariable = variable, 
                VariableFormula = RelativeValue == null ? new XmlFormula() : RelativeValue.ToXmlObject(),
            };
        }
    }

    #endregion
}
