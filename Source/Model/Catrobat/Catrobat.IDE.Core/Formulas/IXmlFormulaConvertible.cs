using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;

namespace Catrobat.IDE.Core.Formulas
{
    public interface IXmlFormulaConvertible
    {
        XmlFormulaTree ToXmlFormula();
    }
}
