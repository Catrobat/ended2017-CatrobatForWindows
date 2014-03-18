using Catrobat.IDE.Core.CatrobatObjects.Formulas;

namespace Catrobat.IDE.Core.FormulaEditor
{
    public interface IXmlFormulaConvertible
    {
        XmlFormulaTree ToXmlFormula();
    }
}
