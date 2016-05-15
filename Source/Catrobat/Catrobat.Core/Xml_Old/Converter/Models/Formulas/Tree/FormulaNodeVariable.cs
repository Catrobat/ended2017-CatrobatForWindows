using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.Xml.XmlObjects.Variables;
using Catrobat.IDE.Core.XmlModelConvertion.Converters;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.Tree
{
    partial class FormulaNodeVariable
    {
    }

    #region Implementations

    partial class FormulaNodeLocalVariable
    {
        protected internal override XmlFormulaTree ToXmlObject2()
        {
            var xmlVariableConverter = new VariableConverter<LocalVariable>();
            var xmlVariable = xmlVariableConverter.Convert(Variable, null);
            return XmlFormulaTreeFactory.CreateLocalVariableNode(xmlVariable);
        }
    }

    partial class FormulaNodeGlobalVariable
    {
        protected internal override XmlFormulaTree ToXmlObject2()
        {
            var xmlVariableConverter = new VariableConverter<GlobalVariable>();
            var xmlVariable = xmlVariableConverter.Convert(Variable, null);
            return XmlFormulaTreeFactory.CreateGlobalVariableNode(xmlVariable);
        }
    }

    #endregion
}
