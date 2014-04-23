using Catrobat.IDE.Core.FormulaEditor;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree
{
    partial interface IFormulaTree : IFormulaTokenizer, IFormulaSerialization, IFormulaInterpreter, IFormulaEvaluation, IXmlFormulaConvertible
    {
    }
}
