using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.XmlModelConvertion
{
    public class XmlFormulaConverterContextPair
    {
        public XmlModelConvertBackContext ConvertContext;

        public XmlModelConvertBackContext ConvertBackContext;

        public XmlFormulaConverterContextPair()
        {
            throw new NotImplementedException();

            //ConvertContext = new XmlModelConvertBackContext();

            //ConvertBackContext = new XmlModelConvertBackContext();
        }
    }
}
