using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Variables
{
    partial class XmlSetVariableBrick
    {
        protected internal override Brick ToModel2(Context context)
        {
            Variable variable = null;
            if (UserVariable != null) context.Variables.TryGetValue(UserVariable, out variable);
            return new SetVariableBrick
            {
                Variable = variable,
                Value = VariableFormula == null ? null : VariableFormula.ToModel(context)
            };
        }
    }
}