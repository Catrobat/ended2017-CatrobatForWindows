using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Xml.Converter;
using Catrobat.IDE.Core.Xml.XmlObjects;

namespace Catrobat.IDE.Core.XmlModelConvertion
{
    public abstract class XmlModelConverter<TXmlType, TModelType> : IXmlModelConverter
        where TXmlType : XmlObject 
        where TModelType : Model

    {
        public abstract TModelType Convert(TXmlType o);

        public abstract TXmlType Convert(TModelType m);

        public Model Convert(XmlObject o)
        {
            return Convert((TXmlType)o);
        }

        public XmlObject Convert(Model m)
        {
            return Convert((TModelType)m);
        }
    }
}
