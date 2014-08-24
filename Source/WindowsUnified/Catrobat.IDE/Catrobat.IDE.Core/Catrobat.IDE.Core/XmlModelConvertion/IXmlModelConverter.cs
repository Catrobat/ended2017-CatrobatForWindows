using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Xml.XmlObjects;

namespace Catrobat.IDE.Core.XmlModelConvertion
{
    public interface IXmlModelConverter
    {
        Model Convert(XmlObjectNode o);

        XmlObjectNode Convert(Model m);
    }
}