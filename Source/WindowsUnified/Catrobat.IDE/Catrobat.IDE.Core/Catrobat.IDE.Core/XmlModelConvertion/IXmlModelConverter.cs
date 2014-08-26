using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Xml.Converter;
using Catrobat.IDE.Core.Xml.XmlObjects;

namespace Catrobat.IDE.Core.XmlModelConvertion
{
    public interface IXmlModelConverter
    {
        ModelBase Convert(XmlObject o, XmlModelConvertContext c);

        XmlObject Convert(ModelBase m, XmlModelConvertBackContext c);
    }
}