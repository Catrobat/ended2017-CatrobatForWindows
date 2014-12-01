using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects;

namespace Catrobat.IDE.Core.XmlModelConvertion
{
    public abstract class XmlModelConverter
        <TXmlType, TModelType> : IXmlModelConverter
        where TXmlType : XmlObject
        where TModelType  : ModelBase
    {
        public abstract TModelType Convert(TXmlType o, XmlModelConvertContext c);

        public abstract TXmlType Convert(TModelType m, XmlModelConvertBackContext c);


        public ModelBase Convert(XmlObject o, XmlModelConvertContext c)
        {
            return Convert((TXmlType)o, c);
        }

        //public ModelBase Convert(XmlObject o, XmlModelConvertContext c, bool pointerOnly)
        //{
        //    return Convert((TXmlType)o, c, pointerOnly);
        //}

        //public virtual TModelType Convert(TXmlType o, XmlModelConvertContext c, bool pointerOnly)
        //{
        //    return null;
        //}

        public XmlObject Convert(ModelBase m, XmlModelConvertBackContext c)
        {
            return Convert((TModelType)m, c);
        }

        //public virtual XmlObject Convert(ModelBase m, XmlModelConvertBackContext c, bool pointerOnly)
        //{
        //    return Convert((TModelType)m, c, pointerOnly);
        //}

        //public virtual TXmlType Convert(TModelType m, XmlModelConvertBackContext c, bool pointerOnly)
        //{
        //    return null;
        //}
    }
}
