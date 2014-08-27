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
    public abstract class FormulaConverterBase<TXmlScript, TScript> : 
        XmlModelConverter<TXmlScript, TScript>
        where TXmlScript : XmlFormula
        where TScript : FormulaTree
    {
        protected FormulaConverterBase(IXmlModelConversionService converter)
            : base(converter) { }

        public override TScript Convert(TXmlScript o, XmlModelConvertContext c)
        {
            //return c.FormulaConverter.Convert(o);
            return null;
        }

        public override TXmlScript Convert(TScript m, XmlModelConvertBackContext c)
        {
            var result = Convert1(m, c);
            return result;
        }

        public abstract TXmlScript Convert1(TScript m, XmlModelConvertBackContext c);
    }
}
