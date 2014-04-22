using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;
using Catrobat.IDE.Core.FormulaEditor;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree
{
    abstract partial class UnaryFormulaTree
    {
        #region Implements IFormulaInterpreter

        protected bool IsNumberN1N()
        {
            // TODO: meaningful (translated?) error message
            if (!Child.IsNumber()) throw new SemanticsErrorException(this, "Child must be number");
            return true;
        }

        protected bool IsNumberL1L()
        {
            // TODO: meaningful (translated?) error message
            if (Child.IsNumber()) throw new SemanticsErrorException(this, "Child must be logic value");
            return true;
        }

        protected bool IsNumberT1T()
        {
            return Child.IsNumber();
        }

        #endregion

        #region Implements IXmlFormulaConvertible

        protected abstract XmlFormulaTree ToXmlFormula(XmlFormulaTree child);

        public override XmlFormulaTree ToXmlFormula()
        {
            return ToXmlFormula(Child == null ? null : Child.ToXmlFormula());
        }

        #endregion
    }
}
