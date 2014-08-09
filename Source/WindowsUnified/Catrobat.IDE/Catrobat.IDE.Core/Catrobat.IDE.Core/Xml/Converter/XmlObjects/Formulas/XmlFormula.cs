using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Xml.Converter;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProgramConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Formulas
{
    partial class XmlFormula : IModelConvertible<FormulaTree, Context>
    {
        FormulaTree IModelConvertible<FormulaTree, Context>.ToModel(Context context)
        {
            return context.FormulaConverter.Convert(this);
        }
    }
}
