using System;
using System.Collections.Generic;
using System.Linq;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.XmlModelConvertion;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Formulas
{
    public class FormulaConverter : XmlModelConverter<XmlFormula, FormulaTree>
    {
        public FormulaConverter() { }

        public override FormulaTree Convert(XmlFormula o, XmlModelConvertContext c)
        {
            return c.FormulaConverter.Convert(o);
        }

        public override XmlFormula Convert(FormulaTree m, XmlModelConvertBackContext c)
        {
            return c.FormulaConverter.ConvertBack(m);
        }
    }
}
