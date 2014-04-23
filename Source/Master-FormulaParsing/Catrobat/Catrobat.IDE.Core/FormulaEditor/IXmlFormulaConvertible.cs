using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;

namespace Catrobat.IDE.Core.FormulaEditor
{
    public interface IXmlFormulaConvertible
    {
        XmlFormulaTree ToXmlFormula();
    }
}
