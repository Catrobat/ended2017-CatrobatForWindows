using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.Converter;
using Catrobat.IDE.Core.Xml.XmlObjects;

namespace Catrobat.IDE.Core.XmlModelConvertion
{
    public abstract class XmlModelConverter
        <TXmlType, TModelType> : IXmlModelConverter
        where TXmlType : XmlObjectNode
        where TModelType : Model
    {
        protected readonly IXmlModelConversionService Converter;

        protected XmlModelConverter()
        {
            Converter = ServiceLocator.XmlModelConversionService;
        }

        public abstract TModelType Convert(TXmlType o, XmlModelConvertContext c);

        public abstract TXmlType Convert(TModelType m, XmlModelConvertBackContext c);

        public Model Convert(XmlObjectNode o, XmlModelConvertContext c)
        {
            return Convert((TXmlType)o, c);
        }

        public XmlObjectNode Convert(Model m, XmlModelConvertBackContext c)
        {
            return Convert((TModelType)m, c);
        }
    }
}
