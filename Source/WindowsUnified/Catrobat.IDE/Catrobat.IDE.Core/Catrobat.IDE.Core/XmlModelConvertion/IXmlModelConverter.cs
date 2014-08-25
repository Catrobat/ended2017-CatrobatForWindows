using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Xml.Converter;
using Catrobat.IDE.Core.Xml.XmlObjects;

namespace Catrobat.IDE.Core.XmlModelConvertion
{
    public interface IXmlModelConverter
    {
        Model Convert(XmlObjectNode o, XmlModelConvertContext c);

        XmlObjectNode Convert(Model m, XmlModelConvertBackContext c);
    }
}